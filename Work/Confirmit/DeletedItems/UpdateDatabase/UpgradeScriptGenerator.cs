using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.Specialized;

using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

namespace UpdateDatabase
{
    class UpgradeScriptGenerator
    {
        private readonly Database m_DatabaseEngine;

        private Microsoft.SqlServer.Management.Smo.Server m_SQLServer;
        private Microsoft.SqlServer.Management.Smo.Database m_ProductionDatabase;
        private Microsoft.SqlServer.Management.Smo.Database m_SourceDatabase;

        public UpgradeScriptGenerator(
            string productionDatabaseName)
        {
            m_DatabaseEngine = new Database(productionDatabaseName);
        }

        public static string TrimSymbols(string str)
        {
            //    // Delete tabs and transfer strings 
            //    // Delete spaces from begin and end of string
            //    // Delete '[' and ']' symbols
            //    string newStr = str.Replace("\r\n", " ").Replace("\t", " ").Replace("[", "").Replace("]", "").ToLower();

            //    // Delete comments
            //    while (newStr.IndexOf("/*") != -1)
            //    {
            //        int first = newStr.IndexOf("/*");
            //        int second = newStr.IndexOf("*/", first + 2);
            //        newStr = newStr.Substring(0, first) + newStr.Substring(second + 2);
            //    }

            //    // Replace a few spaces to one space
            //    while (newStr.IndexOf("  ") != -1)
            //    {
            //        newStr = newStr.Replace("  ", " ");
            //    }

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
                if (productionTable == null && !string.IsNullOrEmpty(m_DatabaseEngine.GetOriginalProcedureNameByTemplateProcedureName(productionObject.Name)))
                    continue;

                bool isNeedDrop = true;

                if (sourceCollection.ContainsKey(productionObjectName))
                {
                    MySmoObjectBase sourceObject = sourceCollection[productionObjectName];

                    //
                    // Compare these objects (own compare for every object)
                    //
                    if (sourceObject.IsEqual(productionObject))
                        continue;

                    //
                    // if our object is column - add it into alter collection
                    // otherwise - into create and drop collection
                    //
                    if (alterItems != null)
                    {
                        var newMySmoObject = new MySmoObjectBase(sourceObject.CreateSplitItem(productionTable, 0, null))
                        {
                            ProductionSmoObject = productionObject.SourceSmoObject
                        };
                        alterItems.Add(newMySmoObject);
                        isNeedDrop = false;
                    }
                    else
                    {
                        if (productionTable != null)
                            createItems.Add(new MySmoObjectBase(sourceObject.CreateSplitItem(productionTable, 0, null)));
                        else
                            createItems.Add(new MySmoObjectBase(sourceObject.CreateSplitItem(m_ProductionDatabase, 0, null)));
                    }

                    #region Cycle for all split objects. We will create new split versions of object, if it have split versions
                    if ((productionTable != null && m_DatabaseEngine.IsSplitTable(productionTable.Name)) ||
                         m_DatabaseEngine.IsSplitProcedure(sourceObject.Name))
                    {
                        foreach (int surveyId in m_DatabaseEngine.SurveyIds)
                        {
                            MySmoObjectBase splitObject;
                            var splitTable = new Table();
                            if (productionTable != null)
                            {
                                //
                                // Get split table 
                                //
                                string splitTableName = m_DatabaseEngine.GetTemplateTableNameByOriginalTableName(
                                    productionTable.Name,
                                    surveyId);
                                splitTable = m_ProductionDatabase.Tables[splitTableName];

                                //
                                // Create split object on this table
                                //
                                splitObject = sourceObject.CreateSplitItem(
                                    splitTable,
                                    surveyId,
                                    m_DatabaseEngine);
                            }
                            else
                            {
                                //
                                // Create split stored procedure
                                //
                                splitObject = sourceObject.CreateSplitItem(m_ProductionDatabase, surveyId, m_DatabaseEngine);
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
                                splitObject.ProductionSmoObject = templateCollection[splitObject.Name].SourceSmoObject;

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
                    //
                    // Add split objects to drop collection, if this table (stored procedure) 
                    // have split versions
                    //
                    if ((productionTable != null && m_DatabaseEngine.IsSplitTable(productionTable.Name)) ||
                        m_DatabaseEngine.IsSplitProcedure(productionObject.Name))
                    {
                        foreach (int surveyId in m_DatabaseEngine.SurveyIds)
                        {
                            if (productionTable != null)
                            {
                                //
                                // Get object collection from each split table
                                //
                                string splitTableName = m_DatabaseEngine.GetTemplateTableNameByOriginalTableName(
                                        productionTable.Name,
                                        surveyId);
                                var myTable = new MyTable(m_ProductionDatabase.Tables[splitTableName]);
                                Dictionary<string, MySmoObjectBase> templateCollection = myTable.GetMyObjects(productionObject);
                                string splitObjectName = productionObject.Name;
                                if (productionObject.SourceSmoObject.GetType() != typeof(Column))
                                {
                                    splitObjectName = m_DatabaseEngine.GetTemplateNameByOriginalName(productionObject.Name, surveyId);
                                }

                                //
                                // Find our object from this collection and add it into drop collection
                                //
                                MySmoObjectBase splitObject = templateCollection[splitObjectName];
                                dropItems.Add(new MySmoObjectBase(splitObject));
                            }
                            else
                            {
                                StoredProcedure splitProcedure = m_ProductionDatabase.StoredProcedures[m_DatabaseEngine.GetTemplateNameByOriginalName(productionObject.Name, surveyId)];
                                //
                                // If splitProcedure is new procedure, added just now - 
                                // IsSystemObject parameter threw exception
                                //
                                try
                                {
                                    dropItems.Add(new MySmoObjectBase(
                                        splitProcedure,
                                        splitProcedure.Name,
                                        m_ProductionDatabase.Name));
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
                            createItems.Add(newObject);
                    }
                    else
                        createItems.Add(new MySmoObjectBase(sourceObject.CreateSplitItem(m_ProductionDatabase, 0, null)));

                    //
                    // Add split objects, if this table (stored procedure) have split version
                    //
                    if ((productionTable != null && m_DatabaseEngine.IsSplitTable(productionTable.Name)) ||
                         m_DatabaseEngine.IsSplitProcedure(sourceObject.Name))
                    {
                        foreach (int surveyId in m_DatabaseEngine.SurveyIds)
                        {
                            MySmoObjectBase splitObject;
                            if (productionTable != null)
                            {
                                string splitTableName = m_DatabaseEngine.GetTemplateTableNameByOriginalTableName(
                                            productionTable.Name,
                                            surveyId);
                                //
                                // Create split object for split table
                                //
                                splitObject = sourceObject.CreateSplitItem(
                                    m_ProductionDatabase.Tables[splitTableName],
                                    surveyId,
                                    m_DatabaseEngine);
                            }
                            else
                            {
                                //
                                // Create split stored procedure
                                //
                                splitObject = sourceObject.CreateSplitItem(m_ProductionDatabase, surveyId, m_DatabaseEngine);
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
        /// <param name="dropItems"></param>
        /// <param name="createItems"></param>
        /// <param name="alterColumns"></param>
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
            foreach (Table sourceTable in m_SourceDatabase.Tables)
            {
                string sourceTableName = sourceTable.Name;
                if (sourceTable.IsSystemObject == false && !m_ProductionDatabase.Tables.Contains(sourceTableName))
                {
                    Trace.TraceInformation(string.Format("Database={0} Table={1} Collecting new tables...\r\n", m_SourceDatabase.Name, sourceTable.Name));
                    var productionTable = new Table(
                        m_ProductionDatabase,
                        sourceTable.Name,
                        sourceTable.Schema);

                    m_ProductionDatabase.Tables.Add(productionTable);
                    createTableItems.Add(new MySmoObjectBase(
                        productionTable,
                        productionTable.Name,
                        productionTable.Parent.Name));

                    if (m_DatabaseEngine.IsSplitTable(sourceTableName))
                    {
                        throw new Exception(
                                    string.Format(
                                        "New split table {0} added to the upgrade database or dropped in the production database. Upgrade utility does not support adding new template tables",
                                        sourceTableName));
                    }
                }
            }
            #endregion

            foreach (Table productionTable in m_ProductionDatabase.Tables)
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
                    if (!string.IsNullOrEmpty(m_DatabaseEngine.GetOriginalTableNameByTemplateTableName(productionTable.Name)))
                    {
                        continue;
                    }

                    if (m_SourceDatabase.Tables.Contains(productionTable.Name))
                    {
                        var myProductionTable = new MyTable(productionTable);
                        var mySourceTable = new MyTable(m_SourceDatabase.Tables[productionTable.Name]);

                        #region Collect Dinamical Items
                        Trace.TraceInformation(string.Format("Database={0} Table={1} Collecting items for triggers...\r\n", m_ProductionDatabase.Name, productionTable.Name));
                        DifferentSplitItems(
                            createTriggerItems,
                            dropTriggerItems,
                            null,
                            myProductionTable.MyTriggers,
                            mySourceTable.MyTriggers,
                            productionTable);

                        Trace.TraceInformation(string.Format("Database={0} Table={1} Collecting items for indexes...\r\n", m_ProductionDatabase.Name, productionTable.Name));
                        DifferentSplitItems(
                            createIndexItems,
                            dropIndexItems,
                            null,
                            myProductionTable.MyIndexes,
                            mySourceTable.MyIndexes,
                            productionTable);

                        Trace.TraceInformation(string.Format("Database={0} Table={1} Collecting items for foreign keys...\r\n", m_ProductionDatabase.Name, productionTable.Name));
                        DifferentSplitItems(
                            createForeignKeyItems,
                            dropForeignKeyItems,
                            null,
                            myProductionTable.MyForeignKeys,
                            mySourceTable.MyForeignKeys,
                            productionTable);

                        Trace.TraceInformation(string.Format("Database={0} Table={1} Collecting items for columns...\r\n", m_ProductionDatabase.Name, productionTable.Name));
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
                    }//if (index.IndexedColumns.Contains(column.Name))
                }//foreach (Index index in columnParent.Indexes)
            }//foreach (MySmoObjectBase alterColumn in alterColumns)
            #endregion

            #region Find and recreate all FK's, whitch point to deleted PK's or UK's
            foreach (MySmoObjectBase dropIndexItem in dropIndexItems)
            {
                var index = (Index)dropIndexItem.SourceSmoObject;
                if (index.IndexKeyType == IndexKeyType.DriPrimaryKey ||
                    (index.IndexKeyType == IndexKeyType.DriUniqueKey))
                {
                    foreach (Table table in m_ProductionDatabase.Tables)
                    {
                        foreach (ForeignKey fk in table.ForeignKeys)
                        {
                            if (fk.ReferencedTable == ((Table)(index.Parent)).Name &&
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
                        }//foreach (ForeignKey fk in table.ForeignKeys)
                    }//foreach (Table table in m_ProductionDatabase.Tables)
                }//if (index.IndexKeyType == IndexKeyType.DriPrimaryKey || (index.IndexKeyType == IndexKeyType.DriUniqueKey))
            }//foreach (MySmoObjectBase dropIndexItem in dropIndexItems)
            #endregion

            #region Add table items to lists
            foreach (MySmoObjectBase dropForeignKeyItem in dropForeignKeyItems)
                dropItems.Add(dropForeignKeyItem);
            foreach (MySmoObjectBase dropIndexItem in dropIndexItems)
                dropItems.Add(dropIndexItem);
            foreach (MySmoObjectBase dropTriggerItem in dropTriggerItems)
                dropItems.Add(dropTriggerItem);
            foreach (MySmoObjectBase dropColumnItem in dropColumnItems)
                dropItems.Add(dropColumnItem);
            foreach (MySmoObjectBase dropTableItem in dropTableItems)
                dropItems.Add(dropTableItem);

            foreach (MySmoObjectBase createTableItem in createTableItems)
                createItems.Add(createTableItem);
            foreach (MySmoObjectBase createColumnItem in createColumnItems)
                createItems.Add(createColumnItem);
            foreach (MySmoObjectBase createTriggerItem in createTriggerItems)
                createItems.Add(createTriggerItem);
            foreach (MySmoObjectBase createIndexItem in createIndexItems)
                createItems.Add(createIndexItem);
            foreach (MySmoObjectBase createForeignKeyItem in createForeignKeyItems)
                createItems.Add(createForeignKeyItem);
            #endregion
        }


        /// <summary>
        /// Collect list for drop/create/alter of stores
        /// </summary>
        /// <param name="dropItems"></param>
        /// <param name="createItems"></param>
        private void CollectStoreProcedureItems(
           ICollection<MySmoObjectBase> dropItems,
           ICollection<MySmoObjectBase> createItems)
        {
            var productionProcedures = m_DatabaseEngine.GetStoredProceduresInfo(m_ProductionDatabase, m_SQLServer);
            var sourceProcedures = m_DatabaseEngine.GetStoredProceduresInfo(m_SourceDatabase, m_SQLServer);

            Trace.TraceInformation(string.Format("Database={0} Collecting items for stored procedures...\r\n", m_ProductionDatabase.Name));

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
            Trace.TraceInformation(string.Format("Database={0} Drop old clr stored procedures...\r\n", m_ProductionDatabase.Name));

            foreach (StoredProcedure storedProcedure in m_ProductionDatabase.StoredProcedures)
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
            Trace.TraceInformation(string.Format("Database={0} Create new clr stored procedures...\r\n", m_ProductionDatabase.Name));
            foreach (StoredProcedure storedProcedure in m_SourceDatabase.StoredProcedures)
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
                    createItems.Add(myStoredProcedure.CreateSplitItem(m_ProductionDatabase, 0, null));
                }
            }
        }


        private void CollectScriptItems(
            ICollection<IDroppable> items2Drop, 
            ICollection<ICreatable> items2Create, 
            ICollection<IAlterable> items2Alter)
        {
            var dropItems = new List<MySmoObjectBase>();
            var createItems = new List<MySmoObjectBase>();
            var alterColumns = new List<MySmoObjectBase>();

            WriteTime("Start collect CLR assemblies");

            #region Generate DROP and CREATE script for CLR assemblies

            Trace.TraceInformation(string.Format("Database={0} Collecting items for CLR assemblies...\r\n", m_ProductionDatabase.Name));            

            createItems = m_DatabaseEngine.GetCLRAssembliesInfo(m_SourceDatabase);
            List<MySmoObjectBase> dropAssemblyItems = m_DatabaseEngine.GetCLRAssembliesInfo(m_ProductionDatabase);

            #endregion

            WriteTime("Start collect tables items");

            #region Generate DROP and CREATE script for table items

            //
            // Generate DROP script for table related stuff
            //            
            Trace.TraceInformation(string.Format("Database={0} Collecting items for tables...\r\n", m_ProductionDatabase.Name));

            CollectTableItems(dropItems, createItems, alterColumns);

            #endregion

            WriteTime("Start collect UDFs");

            #region Generate DROP and CREATE script for user defined function

            Trace.TraceInformation(string.Format("Database={0} Collecting items for user defined function...\r\n", m_ProductionDatabase.Name));

            DifferentItems(
                m_DatabaseEngine.GetUserDefinedFunctionsInfo(m_ProductionDatabase),
                m_DatabaseEngine.GetUserDefinedFunctionsInfo(m_SourceDatabase),
                dropItems,
                createItems);

            #endregion

            WriteTime("Start collect views");

            #region Generate DROP and CREATE script for views

            Trace.TraceInformation(string.Format("Database={0} Collecting items for views...\r\n", m_ProductionDatabase.Name));

            DifferentItems(
                m_DatabaseEngine.GetViewsInfo(m_ProductionDatabase),
                m_DatabaseEngine.GetViewsInfo(m_SourceDatabase),
                dropItems,
                createItems);

            #endregion

            WriteTime("Start collect stored procedures");

            #region Generate DROP and CREATE script for stored procedures

            CollectStoreProcedureItems(dropItems, createItems);

            // Add dropped assemblies to drop list
            foreach (MySmoObjectBase droppedAssembly in dropAssemblyItems)
            {
                dropItems.Add(droppedAssembly);
            }

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
                    var newAssembly = new SqlAssembly(m_ProductionDatabase, assembly.Name)
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
                var item = (IAlterable)mySmoObject.ProductionSmoObject;

                var productionColumn = (Column)mySmoObject.ProductionSmoObject;
                var sourceColumn = (Column)mySmoObject.SourceSmoObject;

                if (productionColumn.Nullable != sourceColumn.Nullable)
                    ((Column)item).Nullable = sourceColumn.Nullable;

                if (productionColumn.Identity != sourceColumn.Identity)
                    ((Column)item).Identity = sourceColumn.Identity;

                if (productionColumn.IdentitySeed != sourceColumn.IdentitySeed)
                    ((Column)item).IdentitySeed = sourceColumn.IdentitySeed;

                if (productionColumn.IdentityIncrement != sourceColumn.IdentityIncrement)
                    ((Column)item).IdentityIncrement = sourceColumn.IdentityIncrement;

                if (productionColumn.Collation != sourceColumn.Collation)
                    ((Column)item).Collation = sourceColumn.Collation;

                if (productionColumn.DefaultConstraint != null)
                {
                    if (sourceColumn.DefaultConstraint == null)
                    {
                        items2Drop.Add(((Column)(mySmoObject.ProductionSmoObject)).DefaultConstraint);
                    }
                    else
                    {
                        if (productionColumn.DefaultConstraint.Text != sourceColumn.DefaultConstraint.Text)
                        {
                            items2Drop.Add(((Column)(mySmoObject.ProductionSmoObject)).DefaultConstraint);
                            items2Create.Add(((Column)(mySmoObject.SourceSmoObject)).DefaultConstraint);
                        }
                    }
                }
                else
                {
                    if (sourceColumn.DefaultConstraint != null)
                    {
                        items2Create.Add(((Column)(mySmoObject.SourceSmoObject)).DefaultConstraint);
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
            const string path = @"C:\Time.txt";

            using (var sw = new StreamWriter(path, true))
            {
                sw.WriteLine(DateTime.Now.ToLongTimeString() + "\t" + text);
            }
        }


        /// <summary>
        /// Compare components from two databases and create script for create new oject,
        /// drop old object and alter changed indexes and columns
        /// </summary>
        /// <param name="productionDatabase"></param>
        /// <param name="sourceDatabase"></param>
        /// <param name="dropScriptText"></param>
        /// <param name="createScriptText"></param>
        /// <param name="alterScriptText"></param>
        public void GenerateUpgradeScript(
            string productionDatabase,
            string sourceDatabase,
            out string dropScriptText,
            out string createScriptText,
            out string alterScriptText)
        {
            DateTime startTime = DateTime.Now;
            WriteTime("Start work for " + productionDatabase + " database.");

            string sqlServerName = string.IsNullOrEmpty(Configuration.Default.SqlServerName) == false 
                                       ? Configuration.Default.SqlServerName 
                                       : Environment.MachineName;

            m_SQLServer = new Server(
                new ServerConnection(
                    sqlServerName,
                    Configuration.Default.SqlUserName,
                    Configuration.Default.SqlPassword));

            m_SQLServer.SetDefaultInitFields(true);

            m_ProductionDatabase = m_SQLServer.Databases[productionDatabase];

            m_SourceDatabase = m_SQLServer.Databases[sourceDatabase];

            /*******************************************************************************/

            var items2Drop = new List<IDroppable>();
            var items2Create = new List<ICreatable>();
            var items2Alter = new List<IAlterable>();

            WriteTime("Start collect script items");
            //
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

            m_SQLServer.ConnectionContext.SqlExecutionModes = SqlExecutionModes.CaptureSql;
            m_SQLServer.ConnectionContext.CapturedSql.Clear();
            foreach (IDroppable droppableItem in items2Drop)
            {
                droppableItem.Drop();
            }
            generatedDropScript.Add(m_SQLServer.ConnectionContext.CapturedSql.Text);


            WriteTime("Create alter scripts...");

            m_SQLServer.ConnectionContext.SqlExecutionModes = SqlExecutionModes.CaptureSql;
            m_SQLServer.ConnectionContext.CapturedSql.Clear();
            foreach (IAlterable alterableItem in items2Alter)
            {
                alterableItem.Alter();
            }
            generatedAlterScript.Add(m_SQLServer.ConnectionContext.CapturedSql.Text);


            WriteTime("Create create scripts...");

            m_SQLServer.ConnectionContext.SqlExecutionModes = SqlExecutionModes.CaptureSql;
            m_SQLServer.ConnectionContext.CapturedSql.Clear();
            foreach (ICreatable creatableItem in items2Create)
            {
                creatableItem.Create();
            }
            var preSql = new StringCollection
            {
                string.Format("ALTER DATABASE  {0} set TRUSTWORTHY ON\r\n", productionDatabase)
            };

            generatedCreateScript.Add(preSql);

            generatedCreateScript.Add(m_SQLServer.ConnectionContext.CapturedSql.Text);
            m_SQLServer.ConnectionContext.CapturedSql.Clear();


            //
            // Generate BvSpDataSync_CreateSurveySBObjects call for all
            // surveys, except default instance.
            //
            var postSql = new StringCollection();

            if (!string.IsNullOrEmpty(m_DatabaseEngine.GetInstanceName()))
            {
                foreach (int surveyId in m_DatabaseEngine.SurveyIds)
                {
                    postSql.Add(
                        string.Format(
                            "EXEC BvSpDataSync_CreateSurveySBObjects {0}, '{1}', null",
                            surveyId,
                            m_DatabaseEngine.GetSurveyNameBySurveySid(surveyId)));
                }
            }

            generatedCreateScript.Add(postSql);

            /*******************************************************************************/

            StringBuilder dropScript = PostProcessScriptText(
                productionDatabase,
                generatedDropScript);

            dropScriptText = dropScript.ToString();

            StringBuilder createScript = PostProcessScriptText(
                productionDatabase,
                generatedCreateScript);

            createScriptText = createScript.ToString();

            StringBuilder alterScript = PostProcessScriptText(
                productionDatabase,
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
