using System;
using Microsoft.SqlServer.Management.Smo;

namespace UpdateDatabase
{
    public class MyColumn : MySmoObjectBase
    {
        private readonly Random mRandom = new Random();

        private static string ChangeName(string name)
        { // DF__AddColumnTa__SID__0425A276
            if (name.StartsWith("DF__"))
            {
                return name.Substring(0, name.LastIndexOf("__"));
            }

            return name;
        }

        private string CreateUniqueDefaultConstraintName(string name)
        { // DF__AddColumnTa__SID__0425A276
            if (name.StartsWith("DF__"))
            {
                string uniqueStr = string.Empty;                
                for (int i = 0; i < 8; i++)
                {
                    uniqueStr += mRandom.Next(0, 10);
                }

                return name.Substring(0, name.LastIndexOf("__") + 2) + uniqueStr;
            }

            return name;
        }
        
        public MyColumn(SmoObjectBase smoObject, string name, string parentName) :
            base(smoObject, name, parentName)
        {
        }

        public override bool IsEqual(MySmoObjectBase newValue)
        {
            if (!base.IsEqual(newValue))
            {
                return false;
            }

            var productionColumn = (Column)SourceSmoObject;
            var sourceColumn = (Column)newValue.SourceSmoObject;

            if (productionColumn.Name == sourceColumn.Name &&
                productionColumn.DataType.SqlDataType != sourceColumn.DataType.SqlDataType)
            {
                throw new Exception(string.Format(
                    "Column {0}.{1} has different types in source and production databases",
                    ((Table)productionColumn.Parent).Name,
                    productionColumn.Name));
            }

            if (productionColumn.DefaultConstraint != null)
            {
                if (sourceColumn.DefaultConstraint == null)
                {
                    return false;
                }

                string prodColName = ChangeName(productionColumn.DefaultConstraint.Name);
                string sourColName = ChangeName(sourceColumn.DefaultConstraint.Name);
                if (prodColName != sourColName ||
                    productionColumn.DefaultConstraint.Text != sourceColumn.DefaultConstraint.Text)
                {
                    return false;
                }
            }
            else
            {
                if (sourceColumn.DefaultConstraint != null)
                {
                    return false;
                }
            }

            return productionColumn.Nullable == sourceColumn.Nullable &&
                productionColumn.Identity == sourceColumn.Identity &&
                productionColumn.IdentitySeed == sourceColumn.IdentitySeed &&
                productionColumn.IdentityIncrement == sourceColumn.IdentityIncrement;
        }


        /// <summary>
        /// Create column for split table
        /// </summary>
        /// <param name="productionTable">Split production table</param>
        /// <param name="surveyId">Survey ID</param>
        /// <param name="databaseEngine">Pointer of database class</param>
        /// <returns></returns>
        public override MySmoObjectBase CreateSplitItem(
            Table productionTable,
            int surveyId,
            Database databaseEngine)
        {
            var modelColumn = (Column)SourceSmoObject;
            var newColumn = new Column(
                productionTable,
                modelColumn.Name,
                modelColumn.DataType)
            {
                Collation = modelColumn.Collation,
                Nullable = modelColumn.Nullable,
                Identity = modelColumn.Identity,
                IdentitySeed = modelColumn.IdentitySeed,
                IdentityIncrement = modelColumn.IdentityIncrement
            };

            if (!productionTable.Columns.Contains(modelColumn.Name) && modelColumn.DefaultConstraint == null)
            {
                try
                {
                    // For new table will generate PropertyNotSetException
                    if (!productionTable.IsSystemObject)
                    {
                        throw new Exception(string.Format(
                                                "Added column {0} for table {1} does not have default value",
                                                modelColumn.Name,
                                                productionTable.Name));
                    }
                }
                catch (PropertyNotSetException)
                { 
                }
            }

            if (modelColumn.DefaultConstraint != null)
            {
                newColumn.AddDefaultConstraint(CreateUniqueDefaultConstraintName(modelColumn.DefaultConstraint.Name)).Text = modelColumn.DefaultConstraint.Text;
            }

            if (!productionTable.Columns.Contains(modelColumn.Name))
            {
                productionTable.Columns.Add(newColumn);
            }

            // if productionTable is new table - return null MySmoObjectBase. 
            // Column will create with table creating
            try
            {
#pragma warning disable 168
                bool temp = productionTable.IsSystemObject;
#pragma warning restore 168

                return new MySmoObjectBase(newColumn, newColumn.Name, productionTable.Name);
            }
            catch (PropertyNotSetException)
            {
                return new MySmoObjectBase(null, string.Empty, string.Empty);
            }
        }
    }
}
