using System;
using System.Collections.Generic;
using Microsoft.SqlServer.Management.Smo;

namespace UpdateDatabase
{
    public class MyTable : MySmoObjectBase
    {
        public MyTable(Table table) :
            base(table, table.Name, table.Parent.Name)
        {
        }


        /// <summary>
        /// Return all triggers from current table
        /// </summary>
        public Dictionary<string, MySmoObjectBase> MyTriggers
        {
            get
            {
                var table = (Table)SourceSmoObject;
                var myTriggers = new Dictionary<string, MySmoObjectBase>();

                foreach (Trigger trigger in table.Triggers)
                {
                    MySmoObjectBase myTrigger = new MyTrigger(
                        trigger,
                        trigger.Name,
                        table.Name,
                        trigger.TextBody);
                    myTriggers.Add(trigger.Name, myTrigger);
                }

                return myTriggers;
            }
        }


        /// <summary>
        /// Return all indexes from current table
        /// </summary>
        public Dictionary<string, MySmoObjectBase> MyIndexes
        {
            get
            {
                var table = (Table)SourceSmoObject;
                var myIndexes = new Dictionary<string, MySmoObjectBase>();

                foreach (Index index in table.Indexes)
                {
                    MySmoObjectBase myIndex = new MyIndex(
                        index,
                        index.Name,
                        table.Name);
                    myIndexes.Add(MyIndex.ChangeName(index.Name), myIndex);
                }

                return myIndexes;
            }
        }


        /// <summary>
        /// Return all foreign keys from current table
        /// </summary>
        public Dictionary<string, MySmoObjectBase> MyForeignKeys
        {
            get
            {
                var table = (Table)SourceSmoObject;
                var myForeignKeys = new Dictionary<string, MySmoObjectBase>();

                foreach (ForeignKey foreignKeys in table.ForeignKeys)
                {
                    MySmoObjectBase myForeignKey = new MyForeignKey(
                        foreignKeys,
                        foreignKeys.Name,
                        table.Name);
                    myForeignKeys.Add(foreignKeys.Name, myForeignKey);
                }

                return myForeignKeys;
            }
        }


        /// <summary>
        /// Return all columns from current table
        /// </summary>
        public Dictionary<string, MySmoObjectBase> MyColumns
        {
            get
            {
                var table = (Table)SourceSmoObject;
                var myColumns = new Dictionary<string, MySmoObjectBase>();

                foreach (Column column in table.Columns)
                {
                    MySmoObjectBase myColumn = new MyColumn(
                        column,
                        column.Name,
                        table.Name);
                    myColumns.Add(column.Name, myColumn);
                }

                return myColumns;
            }
        }

        /// <summary>
        /// Return all objects from current table
        /// </summary>
        /// <param name="myObjectType">Model object for detect its type</param>
        /// <returns></returns>
        public Dictionary<string, MySmoObjectBase> GetMyObjects(MySmoObjectBase myObjectType)
        {
            if (myObjectType.SourceSmoObject.GetType() == typeof(Trigger))
            {
                return MyTriggers;
            }

            if (myObjectType.SourceSmoObject.GetType() == typeof(Index))
            {
                return MyIndexes;
            }

            if (myObjectType.SourceSmoObject.GetType() == typeof(ForeignKey))
            {
                return MyForeignKeys;
            }

            if (myObjectType.SourceSmoObject.GetType() == typeof(Column))
            {
                return MyColumns;
            }

            throw new Exception(string.Format(
                "Unknown type {0} in function GetMyObjects", 
                myObjectType.SourceSmoObject.GetType()));
        }
    }
}
