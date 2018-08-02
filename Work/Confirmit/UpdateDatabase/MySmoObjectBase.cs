using System;
using System.Diagnostics;
using System.Linq;

using Microsoft.SqlServer.Management.Smo;

namespace UpdateDatabase
{
    [DebuggerDisplay("Name={ParentName}_{Name} Type={SourceSmoObject.GetType().ToString().Remove(0, 35)}")]
    public class MySmoObjectBase
    {
        /// <summary>
        /// Object from source database
        /// </summary>
        public readonly SmoObjectBase SourceSmoObject;

        /// <summary>
        /// Name of source database object
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Name of source database object's parent (table or database)
        /// </summary>
        public readonly string ParentName;

        /// <summary>
        /// Additional parameters of source database object for equals
        /// </summary>
        public readonly string[] ExtraParams;

        private SmoObjectBase mProductionSmoObject;

        /// <summary>
        /// Object from production database (only for columns)
        /// </summary>
        public SmoObjectBase MProductionSmoObject
        {
            get
            {
                if (this.mProductionSmoObject == null)
                {
                    throw new Exception("Inaccessible usage of productionSmoObject parameter. It can to use only for altering column objects.");
                }

                return this.mProductionSmoObject;
            }

            set
            {
                this.mProductionSmoObject = value;
            }
        }


        public MySmoObjectBase(
            SmoObjectBase smoObject,
            string name,
            string parentName,
            params string[] extraParams)
        {
            SourceSmoObject = smoObject;
            Name = name;
            ParentName = parentName;
            ExtraParams = new string[extraParams.Length];
            for (int i = 0; i < extraParams.Length; i++)
            {
                ExtraParams[i] = UpgradeScriptGenerator.TrimSymbols(extraParams[i]);
            }
        }

        public MySmoObjectBase(MySmoObjectBase mySmoObject)
        {
            SourceSmoObject = mySmoObject.SourceSmoObject;
            Name = mySmoObject.Name;
            ParentName = mySmoObject.ParentName;
            this.mProductionSmoObject = mySmoObject.mProductionSmoObject;
            ExtraParams = new string[mySmoObject.ExtraParams.Length];
            mySmoObject.ExtraParams.CopyTo(ExtraParams, 0);
        }

        public virtual bool IsEqual(MySmoObjectBase newValue)
        {
            if (ExtraParams.Length != newValue.ExtraParams.Length)
            {
                return false;
            }

            if (this.ExtraParams.Where((t, i) => t != newValue.ExtraParams[i]).Any())
            {
                return false;
            }

            return SourceSmoObject.GetType() == newValue.SourceSmoObject.GetType() &&
                Name == newValue.Name;
        }


        /// <summary>
        /// Create new split item
        /// </summary>
        /// <param name="productionTable">Split production table</param>
        /// <param name="surveyId">Survey ID</param>
        /// <param name="databaseEngine">Pointer of database class</param>
        /// <returns></returns>
        public virtual MySmoObjectBase CreateSplitItem(
            Table productionTable,
            int surveyId,
            Database databaseEngine)
        {
            throw new Exception("CreateSplitItem method must be override.");
        }


        /// <summary>
        /// Create new split item
        /// </summary>
        /// <param name="productionDatabase">Split production database</param>
        /// <param name="surveyId">Survey ID</param>
        /// <param name="databaseEngine">Pointer of database class</param>
        /// <returns></returns>
        public virtual MySmoObjectBase CreateSplitItem(
            Microsoft.SqlServer.Management.Smo.Database productionDatabase,
            int surveyId,
            Database databaseEngine)
        {
            throw new Exception("CreateSplitItem method must be override.");
        }
    }
}
