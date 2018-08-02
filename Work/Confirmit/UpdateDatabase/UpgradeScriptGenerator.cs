using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Text;

using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace UpdateDatabase
{
    public class UpgradeScriptGenerator
    {
        private Database mDatabaseEngine;

        private readonly Server mSqlServer;
        private Microsoft.SqlServer.Management.Smo.Database mProductionDatabase;
        private readonly Microsoft.SqlServer.Management.Smo.Database mSourceDatabase;

        public UpgradeScriptGenerator()
        {
            string sqlServerName =
                string.IsNullOrEmpty(Configuration.Default.SqlServerName) == false ?
                Configuration.Default.SqlServerName :
                Environment.MachineName;

            mSqlServer = new Server(
                new ServerConnection(
                    sqlServerName,
                    Configuration.Default.SqlUserName,
                    Configuration.Default.SqlPassword));

            mSqlServer.SetDefaultInitFields(true);

            mSourceDatabase = mSqlServer.Databases[Configuration.Default.SourceDatabaseName];
        }

        public static string TrimSymbols(string str)
        {
            return str.Trim(new[] { ' ', '\r', '\n' });
        }


        /// <summary>
        /// Get update information from two lists of smo ojects with current types:
        /// Views, CLRAssemblies and UserDefinedFunction (and other databases components)
        /// </summary>
        /// <param name="productionItems">Objects from production database</param>
        /// <param name="sourceItems">Objects from source database</param>
        /// <param name="dropItems">Objects for drop from production database</param>
        /// <param name="createItems">Objects for create in production database</param>
        private static void DifferentItems(
            Dictionary<string, MySmoObjectBase> productionItems,
            Dictionary<string, MySmoObjectBase> sourceItems,
            ICollection<MySmoObjectBase> dropItems,
            ICollection<MySmoObjectBase> createItems)
        {
            //
            // Get dropped items from production database
            //
            foreach (string productionItemKey in productionItems.Keys)
            {
                MySmoObjectBase productionItem = productionItems[productionItemKey];

                bool isContains = false;

                if (sourceItems.ContainsKey(productionItemKey))
                {
                    MySmoObjectBase sourceItem = sourceItems[productionItemKey];
                    isContains = true;
                    if (!productionItem.IsEqual(sourceItem))
                    {
                        createItems.Add(new MySmoObjectBase(sourceItem));
                        isContains = false;
                    }
                }

                if (!isContains)
                {
                    dropItems.Add(new MySmoObjectBase(productionItem));
                }
            }

            //
            // Get created items from source database
            //
            foreach (string sourceItemKey in sourceItems.Keys)
            {
                MySmoObjectBase sourceItem = sourceItems[sourceItemKey];

                if (!productionItems.ContainsKey(sourceItemKey))
                {
                    createItems.Add(new MySmoObjectBase(sourceItem));
                }
            }
        }


        /// <summary>
        /// Collect items from table: drop tables, create/drop triggers, create/drop checks,
        /// create/drop foreignKeys, create/drop/alter indexes and create/drop/alter columns
        /// </summary>
        /// <param name="createItems">List of items for creating</param>
        /// <param name="dropItems">List of items for deleting</param>
        /// <param name="alterItems">List of items for altering (not null only for column)</param>
        /// <param name="productionCollection">Collection of objects from production database</param>
        /// <param name="sourceCollection">Collection of objects from sourse database</param>
        /// <param name="productionTable">Production database (null for stored procedure)</param>
        private void DifferentSplitItems(
            ICollection<MySmoObjectBase> createItems,
            ICollection<MySmoObjectBase> dropItems,
            ICollection<MySmoObjectBase> alterItems,
            Dictionary<string, MySmoObjectBase> productionCollection,
            Dictionary<string, MySmoObjectBase> sourceCollection,
            Table productionTable)
        {
            #region Find object from production collection in source collection
            foreach (string productionObjectName in productionCollection.Keys)
            {
                MySmoObjectBase productionObject = productionCollection[productionObjectName];
                
                //
                // if productionObject is split stored procedure - move to next object
                //               
                if (productionTable == null &&
                    !string.IsNullOrEmpty(
                        mDatabaseEngine.GetOriginalProcedureNameByTemplateProcedureName(productionObject.Name)))
                {
                    continue;
                }

                bool isNeedDrop = true;

                if (sourceCollection.ContainsKey(productionObjectName))
                {
                    MySmoObjectBase sourceObject = sourceCollection[productionObjectName];

                    //
                    // Compare these objects (own compare for every object)
                    //
                    if (sourceObject.IsEqual(productionObject))
                    {
                        continue;
                    }

                    //
                    // if our object is column - add it into alter collection
                    // otherwise - into create and drop collection
                    //
                    if (alterItems != null)
                    {
                        var newMySmoObject = new MySmoObjectBase(sourceObject.CreateSplitItem(productionTable, 0, null))
                        {
                            MProductionSmoObject = productionObject.SourceSmoObject
                        };
                        alterItems.Add(newMySmoObject);
                        isNeedDrop = false;
                    }
                    else
                    {
                        createItems.Add(
                            productionTable != null
                                ? new MySmoObjectBase(sourceObject.CreateSplitItem(productionTable, 0, null))
                                : new MySmoObjectBase(sourceObject.CreateSplitItem(this.mProductionDatabase, 0, null)));
                    }

                    #region Cycle for all split objects. We will create new split versions of object, if it have split versions
                    if ((productionTable != null && Database.IsSplitTable(productionTable.Name)) ||
                         Database.IsSplitProcedure(sourceObject.Name))
                    {
                        foreach (int surveyId in mDatabaseEngine.SurveyIds)
                        {
                            MySmoObjectBase splitObject;
                            var splitTable = new Table();
                            if (productionTable != null)
                            {
                                //
                                // Get split table 
                                //
                                string splitTableName = Database.GetTemplateTableNameByOriginalTableName(
                                    productionTable.Name,
                                    surveyId);
                                splitTable = mProductionDatabase.Tables[splitTableName];

                                //
                                // Create split object on this table
                                //
                                splitObject = sourceObject.CreateSplitItem(
                                    splitTable,
                                    surveyId,
                                    mDatabaseEngine);
                            }
                            else
                            {
                                //
                                // Create split stored procedure
                                //
                                splitObject = sourceObject.CreateSplitItem(mProductionDatabase, surveyId, mDatabaseEngine);
                            }

                            //
                            // if our object is column - add it into alter collection
                            // otherwise - into create collection
                            //
                            if (alterItems != null)
                            {
                                //
                                // Find old version of this object in production database
                                //            
                                var myTable = new MyTable(splitTable);
                                Dictionary<string, MySmoObjectBase> templateCollection = myTable.GetMyObjects(splitObject);
                                splitObject.MProductionSmoObject = templateCollection[splitObject.Name].SourceSmoObject;

                                alterItems.Add(new MySmoObjectBase(splitObject));
                            }
                            else
                            {
                                createItems.Add(new MySmoObjectBase(splitObject));
                            }
                        }
                    }
                    #endregion
                }

                #region Add object and its split versions to drop collection
                if (isNeedDrop)
                {
                    dropItems.Add(new MySmoObjectBase(productionObject));
                    
                    // Add split objects to drop collection, if this table (stored procedure) 
                    // have split versions                    
                    if ((productionTable != null && Database.IsSplitTable(productionTable.Name)) ||
                        Database.IsSplitProcedure(productionObject.Name))
                    {
                        foreach (int surveyId in mDatabaseEngine.SurveyIds)
                        {
                            if (productionTable != null)
                            {
                                //
                                // Get object collection from each split table
                                //
                                string splitTableName = Database.GetTemplateTableNameByOriginalTableName(
                                        productionTable.Name,
                                        surveyId);
                                var myTable = new MyTable(mProductionDatabase.Tables[splitTableName]);
                                Dictionary<string, MySmoObjectBase> templateCollection = myTable.GetMyObjects(productionObject);
                                string splitObjectName = productionObject.Name;
                                if (productionObject.SourceSmoObject.GetType() != typeof(Column))
                                {
                                    splitObjectName = Database.GetTemplateNameByOriginalName(productionObject.Name, surveyId);
                                }

                                //
                                // Find our object from this collection and add it into drop collection
                                //
                                MySmoObjectBase splitObject = templateCollection[splitObjectName];
                                dropItems.Add(new MySmoObjectBase(splitObject));
                            }
                            else
                            {
                                StoredProcedure splitProcedure = mProductionDatabase.StoredProcedures[Database.GetTemplateNameByOriginalName(productionObject.Name, surveyId)];

                                // If splitProcedure is (added just now) new procedure  - 
                                // getting IsSystemObject parameter will throw exception
                                try
                                {
                                    if (!splitProcedure.IsSystemObject)
                                    {
                                        dropItems.Add(new MySmoObjectBase(
                                                          splitProcedure,
                                                          splitProcedure.Name,
                                                          mProductionDatabase.Name));
                                    }
                                }
                                catch (Microsoft.SqlServer.Management.Smo.PropertyNotSetException)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            #endregion

            #region Add new objects from source database
            foreach (string sourceObjectName in sourceCollection.Keys)
            {
                MySmoObjectBase sourceObject = sourceCollection[sourceObjectName];

                //
                // If source database contains new object - add it into create collection
                //
                if (!productionCollection.ContainsKey(sourceObjectName))
                {
                    if (productionTable != null)
                    {
                        var newObject = new MySmoObjectBase(sourceObject.CreateSplitItem(productionTable, 0, null));

                        // If column added to new table, we wount create new column separate, 
                        // it will create with table creating
                        if (newObject.SourceSmoObject != null)
                        {
                            createItems.Add(newObject);
                        }
                    }
                    else
                    {
                        createItems.Add(new MySmoObjectBase(sourceObject.CreateSplitItem(mProductionDatabase, 0, null)));
                    }

                    //
                    // Add split objects, if this table (stored procedure) have split version
                    //
                    if ((productionTable != null && Database.IsSplitTable(productionTable.Name)) ||
                         Database.IsSplitProcedure(sourceObject.Name))
                    {
                        foreach (int surveyId in mDatabaseEngine.SurveyIds)
                        {
                            MySmoObjectBase splitObject;
                            if (productionTable != null)
                            {
                                string splitTableName = Database.GetTemplateTableNameByOriginalTableName(
                                            productionTable.Name,
                                            surveyId);
                                
                                // Create split object for split table
                                splitObject = sourceObject.CreateSplitItem(
                                    mProductionDatabase.Tables[splitTableName],
                                    surveyId,
                                    mDatabaseEngine);
                            }
                            else
                            {
                                // Create split stored procedure
                                splitObject = sourceObject.CreateSplitItem(mProductionDatabase, surveyId, mDatabaseEngine);
                            }

                            createItems.Add(new MySmoObjectBase(splitObject));
                        }
                    }
                }
            }
            #endregion
        }


        /// <summary>
        /// Collect lists for drop/create/alter of tables, indexes, columns, triggers, foreignKeys and checks
        /// </summary>
        /// <param name="dropItems">Drop itmes list</param>
        /// <param name="createItems">Create itmes list</param>
        /// <param name="alterColumns">Alter itmes list</param>
        private void CollectTableItems(
            ICollection<MySmoObjectBase> dropItems,
            ICollection<MySmoObjectBase> createItems,
            ICollection<MySmoObjectBase> alterColumns)
        {
            //
            // We could not add table/column items to the items2GenerateScript right here.
            // E.g. because to drop column we first need drop all associated indexes, so
            // we use additional collections and will add table/column items after cycle.
            //
            #region Definition the collection of items
            var dropIndexItems = new List<MySmoObjectBase>();
            var dropTableItems = new List<MySmoObjectBase>();
            var dropColumnItems = new List<MySmoObjectBase>();
            var dropForeignKeyItems = new List<MySmoObjectBase>();
            var dropTriggerItems = new List<MySmoObjectBase>();

            var createIndexItems = new List<MySmoObjectBase>();
            var createTableItems = new List<MySmoObjectBase>();
            var createColumnItems = new List<MySmoObjectBase>();
            var createForeignKeyItems = new List<MySmoObjectBase>();
            var createTriggerItems = new List<MySmoObjectBase>();
            #endregion

            #region Add new tables to create objects list
            foreach (Table sourceTable in mSourceDatabase.Tables)
            {
                string sourceTableName = sourceTable.Name;
                if (sourceTable.IsSystemObject == false && !mProductionDatabase.Tables.Contains(sourceTableName))
                {
                    Trace.TraceInformation(string.Format("Database={0} Table={1} Collecting new tables...\r\n", mSourceDatabase.Name, sourceTable.Name));
                    var productionTable = new Table(
                        mProductionDatabase,
                        sourceTable.Name,
                        sourceTable.Schema);

                    mProductionDatabase.Tables.Add(productionTable);
                    createTableItems.Add(new MySmoObjectBase(
                        productionTable,
                        productionTable.Name,
                        productionTable.Parent.Name));

                    if (Database.IsSplitTable(sourceTableName))
                    {
                        throw new Exception(
                                    string.Format(
                                        "New split table {0} added to the upgrade database or dropped in the production database. Upgrade utility does not support adding new template tables",
                                        sourceTableName));
                    }
                }
            }
            #endregion

            foreach (Table productionTable in mProductionDatabase.Tables)
            {
                // For new table IsSystemObject property is not set
                bool isSystemObject = false;
                try
                {
                    isSystemObject = productionTable.IsSystemObject;
                }
                catch (Microsoft.SqlServer.Management.Smo.PropertyNotSetException)
                {
                }

                if (isSystemObject == false)
                {
                    //
                    // If its splittable then use original name to check if table exist or
                    // to iterate through columns. 
                    //
                    if (!string.IsNullOrEmpty(Database.GetOriginalTableNameByTemplateTableName(productionTable.Name)))
                    {
                        continue;
                    }

                    if (mSourceDatabase.Tables.Contains(productionTable.Name))
                    {
                        var myProductionTable = new MyTable(productionTable);
                        var mySourceTable = new MyTable(mSourceDatabase.Tables[productionTable.Name]);

                        #region Collect Dinamical Items
                        Trace.TraceInformation(string.Format("Database={0} Table={1} Collecting items for triggers...\r\n", mProductionDatabase.Name, productionTable.Name));
                        DifferentSplitItems(
                            createTriggerItems,
                            dropTriggerItems,
                            null,
                            myProductionTable.MyTriggers,
                            mySourceTable.MyTriggers,
                            productionTable);

                        Trace.TraceInformation(string.Format("Database={0} Table={1} Collecting items for indexes...\r\n", mProductionDatabase.Name, productionTable.Name));
                        DifferentSplitItems(
                            createIndexItems,
                            dropIndexItems,
                            null,
                            myProductionTable.MyIndexes,
                            mySourceTable.MyIndexes,
                            productionTable);

                        Trace.TraceInformation(string.Format("Database={0} Table={1} Collecting items for foreign keys...\r\n", mProductionDatabase.Name, productionTable.Name));
                        DifferentSplitItems(
                            createForeignKeyItems,
                            dropForeignKeyItems,
                            null,
                            myProductionTable.MyForeignKeys,
                            mySourceTable.MyForeignKeys,
                            productionTable);

                        Trace.TraceInformation(string.Format("Database={0} Table={1} Collecting items for columns...\r\n", mProductionDatabase.Name, productionTable.Name));
                        DifferentSplitItems(
                            createColumnItems,
                            dropColumnItems,
                            alterColumns,
                            myProductionTable.MyColumns,
                            mySourceTable.MyColumns,
                            productionTable);
                        #endregion
                    }
                    else
                    {
                        //
                        // Table does not exists, we've to drop it.
                        //
                        dropTableItems.Add(new MySmoObjectBase(
                            productionTable,
                            productionTable.Name,
                            productionTable.Parent.Name));
                    }
                }
            }

            #region Find and recreate all indexes, whitch have changed columns
            foreach (MySmoObjectBase alterColumn in alterColumns)
            {
                var column = (Column)alterColumn.SourceSmoObject;

                var columnParent = (Table)column.Parent;
                foreach (Index index in columnParent.Indexes)
                {
                    if (index.IndexedColumns.Contains(column.Name))
                    {
                        MySmoObjectBase foundIndex = new MyIndex(index, index.Name, columnParent.Name);
                        bool isContains = false;
                        foreach (MySmoObjectBase smoObject in dropIndexItems)
                        {
                            if (foundIndex.IsEqual(smoObject))
                            {
                                isContains = true;
                                break;
                            }
                        }

                        if (!isContains)
                        {
                            dropForeignKeyItems.Add(foundIndex);
                            createForeignKeyItems.Add(foundIndex);
                        }
                    } // if (index.IndexedColumns.Contains(column.Name))
                } // foreach (Index index in columnParent.Indexes)
            } // foreach (MySmoObjectBase alterColumn in alterColumns)
            #endregion

            #region Find and recreate all FK's, whitch point to deleted PK's or UK's
            foreach (MySmoObjectBase dropIndexItem in dropIndexItems)
            {
                var index = (Index)dropIndexItem.SourceSmoObject;
                if (index.IndexKeyType == IndexKeyType.DriPrimaryKey ||
                    (index.IndexKeyType == IndexKeyType.DriUniqueKey))
                {
                    foreach (Table table in mProductionDatabase.Tables)
                    {
                        foreach (ForeignKey fk in table.ForeignKeys)
                        {
                            if (fk.ReferencedTable == ((Table)index.Parent).Name &&
                                fk.ReferencedKey == index.Name)
                            {
                                MySmoObjectBase foundFK = new MyIndex(fk, fk.Name, table.Name);
                                bool isContains = false;
                                foreach (MySmoObjectBase smoObject in dropForeignKeyItems)
                                {
                                    if (foundFK.IsEqual(smoObject))
                                    {
                                        isContains = true;
                                        break;
                                    }
                                }

                                if (!isContains)
                                {
                                    dropForeignKeyItems.Add(foundFK);
                                    createForeignKeyItems.Add(foundFK);
                                }
                            }
                        } // foreach (ForeignKey fk in table.ForeignKeys)
                    } // foreach (Table table in _productionDatabase.Tables)
                } // if (index.IndexKeyType == IndexKeyType.DriPrimaryKey || (index.IndexKeyType == IndexKeyType.DriUniqueKey))
            } // foreach (MySmoObjectBase dropIndexItem in dropIndexItems)
            #endregion

            #region Add table items to lists

            foreach (MySmoObjectBase dropForeignKeyItem in dropForeignKeyItems)
            {
                dropItems.Add(dropForeignKeyItem);
            }

            foreach (MySmoObjectBase dropIndexItem in dropIndexItems)
            {
                dropItems.Add(dropIndexItem);
            }

            foreach (MySmoObjectBase dropTriggerItem in dropTriggerItems)
            {
                dropItems.Add(dropTriggerItem);
            }

            foreach (MySmoObjectBase dropColumnItem in dropColumnItems)
            {
                dropItems.Add(dropColumnItem);
            }

            foreach (MySmoObjectBase dropTableItem in dropTableItems)
            {
                dropItems.Add(dropTableItem);
            }

            foreach (MySmoObjectBase createTableItem in createTableItems)
            {
                createItems.Add(createTableItem);
            }

            foreach (MySmoObjectBase createColumnItem in createColumnItems)
            {
                createItems.Add(createColumnItem);
            }

            foreach (MySmoObjectBase createTriggerItem in createTriggerItems)
            {
                createItems.Add(createTriggerItem);
            }

            foreach (MySmoObjectBase createIndexItem in createIndexItems)
            {
                createItems.Add(createIndexItem);
            }

            foreach (MySmoObjectBase createForeignKeyItem in createForeignKeyItems)
            {
                createItems.Add(createForeignKeyItem);
            }

            #endregion
        }


        /// <summary>
        /// Collect list for drop/create/alter of stores
        /// </summary>
        /// <param name="dropItems">Drop items list</param>
        /// <param name="createItems">Create items list</param>        
        private void CollectStoreProcedureItems(
           ICollection<MySmoObjectBase> dropItems,
           ICollection<MySmoObjectBase> createItems)
        {
            Trace.TraceInformation(string.Format("Database={0} Collecting items for stored procedures...\r\n", mProductionDatabase.Name));

            var productionProcedures = Database.GetStoredProceduresInfo(mProductionDatabase, mSqlServer);
            var sourceProcedures = Database.GetStoredProceduresInfo(mSourceDatabase, mSqlServer);

            DifferentSplitItems(
                            createItems,
                            dropItems,
                            null,
                            productionProcedures,
                            sourceProcedures,
                            null);

            //
            // Add NET stores from production database to drop list
            //
            Trace.TraceInformation(string.Format("Database={0} Drop old clr stored procedures...\r\n", mProductionDatabase.Name));

            foreach (StoredProcedure storedProcedure in mProductionDatabase.StoredProcedures)
            {
                // New stored procedure has no IsSystemObject parameter
                try
                {
                    if (storedProcedure.IsSystemObject)
                    {
                        continue;
                    }
                }
                catch (Microsoft.SqlServer.Management.Smo.PropertyNotSetException)
                {
                    continue;
                }

                if (!productionProcedures.ContainsKey(storedProcedure.Name))
                {
                    dropItems.Add(new MySmoObjectBase(
                        storedProcedure,
                        storedProcedure.Name,
                        storedProcedure.Parent.Name));
                }
            }

            //
            // Add NET stores from source database to create list
            //
            Trace.TraceInformation(string.Format("Database={0} Create new clr stored procedures...\r\n", mProductionDatabase.Name));
            foreach (StoredProcedure storedProcedure in mSourceDatabase.StoredProcedures)
            {
                if (storedProcedure.IsSystemObject)
                {
                    continue;
                }

                if (!sourceProcedures.ContainsKey(storedProcedure.Name))
                {
                    MySmoObjectBase myStoredProcedure = new MyStoredProcedure(
                        storedProcedure,
                        storedProcedure.Name,
                        storedProcedure.Parent.Name,
                        storedProcedure.TextBody);
                    createItems.Add(myStoredProcedure.CreateSplitItem(mProductionDatabase, 0, null));
                }
            }
        }


        /// <summary>
        /// Collect Script Items
        /// </summary>
        /// <param name="items2Drop">Drop items list</param>
        /// <param name="items2Create">Create items list</param>
        /// <param name="items2Alter">Akter items list</param>
        private void CollectScriptItems(
            ICollection<IDroppable> items2Drop,
            ICollection<ICreatable> items2Create,
            ICollection<IAlterable> items2Alter)
        {
            var dropItems = new List<MySmoObjectBase>();
            var alterColumns = new List<MySmoObjectBase>();

            WriteTime("Start collect CLR assemblies");

            #region Generate DROP and CREATE script for CLR assemblies

            Trace.TraceInformation(string.Format("Database={0} Collecting items for CLR assemblies...\r\n", mProductionDatabase.Name));

            var createItems = Database.GetCLRAssembliesInfo(mSourceDatabase);
            var dropAssemblyItems = Database.GetCLRAssembliesInfo(mProductionDatabase);
            #endregion

            WriteTime("Start collect tables items");

            #region Generate DROP and CREATE script for table items

            //
            // Generate DROP script for table related stuff
            //            
            Trace.TraceInformation(string.Format("Database={0} Collecting items for tables...\r\n", mProductionDatabase.Name));

            CollectTableItems(dropItems, createItems, alterColumns);

            #endregion

            WriteTime("Start collect UDFs");

            #region Generate DROP and CREATE script for user defined function

            Trace.TraceInformation(string.Format("Database={0} Collecting items for user defined function...\r\n", mProductionDatabase.Name));

            DifferentItems(
                Database.GetUserDefinedFunctionsInfo(mProductionDatabase),
                Database.GetUserDefinedFunctionsInfo(mSourceDatabase),
                dropItems,
                createItems);

            #endregion

            WriteTime("Start collect views");

            #region Generate DROP and CREATE script for views

            Trace.TraceInformation(string.Format("Database={0} Collecting items for views...\r\n", mProductionDatabase.Name));

            DifferentItems(
                Database.GetViewsInfo(mProductionDatabase),
                Database.GetViewsInfo(mSourceDatabase),
                dropItems,
                createItems);

            #endregion

            WriteTime("Start collect stored procedures");

            #region Generate DROP and CREATE script for stored procedures

            CollectStoreProcedureItems(dropItems, createItems);

            // Add dropped assemblies to drop list
            dropItems.AddRange(dropAssemblyItems);

            #endregion

            WriteTime("Start create object collection");

            #region Create object collections

            foreach (MySmoObjectBase mySmoObject in dropItems)
            {
                items2Drop.Add((IDroppable)mySmoObject.SourceSmoObject);
            }

            foreach (MySmoObjectBase mySmoObject in createItems)
            {
                if (mySmoObject.SourceSmoObject.GetType() != typeof(SqlAssembly))
                {
                    items2Create.Add((ICreatable)mySmoObject.SourceSmoObject);
                }
                else
                {
                    #region Create ICreatable intefrace for SqlAssembly
                    // SqlAssembly not implement ICreatable interfase
                    var assembly = (SqlAssembly)mySmoObject.SourceSmoObject;
                    var newAssembly = new SqlAssembly(mProductionDatabase, assembly.Name)
                    {
                        PublicKey = assembly.PublicKey,
                        AssemblySecurityLevel = assembly.AssemblySecurityLevel
                    };

                    //
                    // Save assembly to the file, assume single file assembly.
                    //
                    string assemblyFileName =
                        Path.Combine(
                            Configuration.Default.OutputFolder,
                            Path.ChangeExtension(assembly.Name, "dll"));

                    byte[] assemblyBody = assembly.SqlAssemblyFiles[0].GetFileBytes();

                    using (var file = new FileStream(assemblyFileName, FileMode.CreateNew))
                    {
                        file.Write(assemblyBody, 0, assemblyBody.Length);
                    }

                    using (var file = new StreamWriter(assemblyFileName, true))
                    {
                        file.Write("\r\nwith permission_set = unsafe");
                    }

                    var tempAssembly = new SqlAssemblyEx(newAssembly, assemblyFileName);
                    items2Create.Add(tempAssembly);
                    #endregion
                }
            }

            // Collect alter items for indexes and columns
            foreach (MySmoObjectBase mySmoObject in alterColumns)
            {
                var item = (IAlterable)mySmoObject.MProductionSmoObject;

                var productionColumn = (Column)mySmoObject.MProductionSmoObject;
                var sourceColumn = (Column)mySmoObject.SourceSmoObject;

                if (productionColumn.Nullable != sourceColumn.Nullable)
                {
                    ((Column)item).Nullable = sourceColumn.Nullable;
                }

                if (productionColumn.Identity != sourceColumn.Identity)
                {
                    ((Column)item).Identity = sourceColumn.Identity;
                }

                if (productionColumn.IdentitySeed != sourceColumn.IdentitySeed)
                {
                    ((Column)item).IdentitySeed = sourceColumn.IdentitySeed;
                }

                if (productionColumn.IdentityIncrement != sourceColumn.IdentityIncrement)
                {
                    ((Column)item).IdentityIncrement = sourceColumn.IdentityIncrement;
                }

                if (productionColumn.Collation != sourceColumn.Collation)
                {
                    ((Column)item).Collation = sourceColumn.Collation;
                }

                if (productionColumn.DefaultConstraint != null)
                {
                    if (sourceColumn.DefaultConstraint == null)
                    {
                        items2Drop.Add(((Column)mySmoObject.MProductionSmoObject).DefaultConstraint);
                    }
                    else
                    {
                        if (productionColumn.DefaultConstraint.Text != sourceColumn.DefaultConstraint.Text)
                        {
                            items2Drop.Add(((Column)mySmoObject.MProductionSmoObject).DefaultConstraint);
                            items2Create.Add(((Column)mySmoObject.SourceSmoObject).DefaultConstraint);
                        }
                    }
                }
                else
                {
                    if (sourceColumn.DefaultConstraint != null)
                    {
                        items2Create.Add(((Column)mySmoObject.SourceSmoObject).DefaultConstraint);
                    }
                }

                items2Alter.Add(item);
            }
            #endregion
        }


        private static StringBuilder PostProcessScriptText(
            string productionDatabase,
            IEnumerable<StringCollection> generatedScript)
        {
            Trace.TraceInformation("Assembly script text..\r\n");

            var script = new StringBuilder();

            script.AppendFormat("USE {0}\r\nGO\r\n", productionDatabase);

            foreach (StringCollection scriptPart in generatedScript)
            {
                foreach (string scriptLine in scriptPart)
                {
                    //
                    // Skip USE databasename generated from captured sql.
                    //
                    if (!scriptLine.StartsWith("USE "))
                    {
                        if (scriptLine.EndsWith("\r\n"))
                        {
                            script.Append(scriptLine);
                        }
                        else
                        {
                            script.AppendLine(scriptLine);
                        }

                        script.Append("GO\r\n");
                    }
                }
            }

            return script;
        }

        private static void WriteTime(string text)
        {
            string path = Path.Combine(
                Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]),
                @"TimeInfo.txt");

            using (var sw = new StreamWriter(path, true))
            {
                sw.WriteLine(DateTime.Now.ToLongTimeString() + "\t" + text);
            }
        }


        /// <summary>
        /// Compare components from two databases and create script for create new oject,
        /// drop old object and alter changed indexes and columns
        /// </summary>
        /// <param name="productionDatabaseName">Production Database Name</param>        
        /// <param name="dropScriptText">Drop Script Text</param>
        /// <param name="createScriptText">Create Script Text</param>
        /// <param name="alterScriptText">Alter Script Text</param>
        public void GenerateUpgradeScript(
            string productionDatabaseName,
            out string dropScriptText,
            out string createScriptText,
            out string alterScriptText)
        {
            DateTime startTime = DateTime.Now;
            WriteTime("Start work for " + mProductionDatabase + " database.");

            mDatabaseEngine = new Database(productionDatabaseName);
            mProductionDatabase = mSqlServer.Databases[productionDatabaseName];


            /*******************************************************************************/

            var items2Drop = new List<IDroppable>();
            var items2Create = new List<ICreatable>();
            var items2Alter = new List<IAlterable>();

            WriteTime("Start collect script items");

            // Get droppable and creatable items info
            //            
            CollectScriptItems(items2Drop, items2Create, items2Alter);

            WriteTime("Stop collect script items");

            //
            // Create "drop script" and "create script"
            //
            var generatedDropScript = new List<StringCollection>();
            var generatedCreateScript = new List<StringCollection>();
            var generatedAlterScript = new List<StringCollection>();

            WriteTime("Create drop scripts...");

            Trace.TraceInformation("Create drop/alter/create scripts...\r\n");

            mSqlServer.ConnectionContext.SqlExecutionModes = SqlExecutionModes.CaptureSql;
            mSqlServer.ConnectionContext.CapturedSql.Clear();
            foreach (IDroppable droppableItem in items2Drop)
            {
                droppableItem.Drop();
            }

            generatedDropScript.Add(mSqlServer.ConnectionContext.CapturedSql.Text);


            WriteTime("Create alter scripts...");

            mSqlServer.ConnectionContext.SqlExecutionModes = SqlExecutionModes.CaptureSql;
            mSqlServer.ConnectionContext.CapturedSql.Clear();
            foreach (IAlterable alterableItem in items2Alter)
            {
                alterableItem.Alter();
            }

            generatedAlterScript.Add(mSqlServer.ConnectionContext.CapturedSql.Text);


            WriteTime("Create create scripts...");

            mSqlServer.ConnectionContext.SqlExecutionModes = SqlExecutionModes.CaptureSql;
            mSqlServer.ConnectionContext.CapturedSql.Clear();
            foreach (ICreatable creatableItem in items2Create)
            {
                creatableItem.Create();
            }

            var preSql = new StringCollection
            {
                string.Format(
                    "ALTER DATABASE  {0} set TRUSTWORTHY ON\r\n",
                    mProductionDatabase)
            };

            generatedCreateScript.Add(preSql);

            generatedCreateScript.Add(mSqlServer.ConnectionContext.CapturedSql.Text);
            mSqlServer.ConnectionContext.CapturedSql.Clear();


            //
            // Generate BvSpDataSync_CreateSurveySBObjects call for all
            // surveys, except default instance.
            //
            var postSql = new StringCollection();

            if (!string.IsNullOrEmpty(mDatabaseEngine.GetInstanceName()))
            {
                foreach (int surveyId in mDatabaseEngine.SurveyIds)
                {
                    postSql.Add(
                        string.Format(
                            "EXEC BvSpDataSync_CreateSurveySBObjects {0}, '{1}', null",
                            surveyId,
                            mDatabaseEngine.GetSurveyNameBySurveySid(surveyId)));
                }
            }

            generatedCreateScript.Add(postSql);

            /*******************************************************************************/

            StringBuilder dropScript = PostProcessScriptText(
                productionDatabaseName,
                generatedDropScript);

            dropScriptText = dropScript.ToString();

            StringBuilder createScript = PostProcessScriptText(
                productionDatabaseName,
                generatedCreateScript);

            createScriptText = createScript.ToString();

            StringBuilder alterScript = PostProcessScriptText(
                productionDatabaseName,
                generatedAlterScript);

            alterScriptText = alterScript.ToString();

            DateTime stopTime = DateTime.Now;
            stopTime = stopTime.AddHours(-startTime.Hour);
            stopTime = stopTime.AddMinutes(-startTime.Minute);
            stopTime = stopTime.AddSeconds(-startTime.Second);
            WriteTime(string.Format("Stop work: {0}\r\n\r\n", stopTime.ToLongTimeString()));
        }
    }
}