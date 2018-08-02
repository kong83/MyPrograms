using Microsoft.SqlServer.Management.Smo;

namespace UpdateDatabase
{
    public class MyForeignKey : MySmoObjectBase
    {
        public MyForeignKey(SmoObjectBase smoObject, string name, string parentName) :
            base(smoObject, name, parentName)
        {
        }

        public override bool IsEqual(MySmoObjectBase newValue)
        {
            if (!base.IsEqual(newValue))
            {
                return false;
            }

            var productionForeignKey = (ForeignKey)SourceSmoObject;
            var sourceForeignKey = (ForeignKey)newValue.SourceSmoObject;

            if (productionForeignKey.Columns.Count != sourceForeignKey.Columns.Count)
            {
                return false;
            }

            for (int num = 0; num < productionForeignKey.Columns.Count; num++)
            {
                ForeignKeyColumn productionForeignKeyColumn = productionForeignKey.Columns[num];
                ForeignKeyColumn sourceForeignKeyColumn = sourceForeignKey.Columns[num];

                if (productionForeignKeyColumn.Name != sourceForeignKeyColumn.Name ||
                   productionForeignKeyColumn.ReferencedColumn != sourceForeignKeyColumn.ReferencedColumn)
                {
                    return false;
                }
            }

            return productionForeignKey.IsEnabled == sourceForeignKey.IsEnabled &&
                productionForeignKey.IsChecked == sourceForeignKey.IsChecked &&
                productionForeignKey.DeleteAction == sourceForeignKey.DeleteAction &&
                productionForeignKey.UpdateAction == sourceForeignKey.UpdateAction &&
                productionForeignKey.ReferencedTable == sourceForeignKey.ReferencedTable &&
                productionForeignKey.ReferencedTableSchema == sourceForeignKey.ReferencedTableSchema;
        }

        /// <summary>
        /// Create FK for split table
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
            var newForeignKey = new ForeignKey();
            var modelForeignKey = (ForeignKey)SourceSmoObject;

            newForeignKey.Parent = productionTable;

            if (surveyId == 0)
            {
                newForeignKey.Name = modelForeignKey.Name;
            }
            else
            {
                newForeignKey.Name = Database.GetTemplateNameByOriginalName(
                    modelForeignKey.Name,
                    surveyId);
            }

            newForeignKey.IsEnabled = modelForeignKey.IsEnabled;
            newForeignKey.IsChecked = modelForeignKey.IsChecked;

            newForeignKey.DeleteAction = modelForeignKey.DeleteAction;
            newForeignKey.UpdateAction = modelForeignKey.UpdateAction;

            newForeignKey.ReferencedTable = modelForeignKey.ReferencedTable;
            newForeignKey.ReferencedTableSchema = modelForeignKey.ReferencedTableSchema;

            foreach (ForeignKeyColumn column in modelForeignKey.Columns)
            {
                var newColumn = new ForeignKeyColumn(
                    newForeignKey,
                    column.Name,
                    column.ReferencedColumn);

                newForeignKey.Columns.Add(newColumn);
            }

            return new MySmoObjectBase(
                newForeignKey,
                newForeignKey.Name,
                productionTable.Name);
        }
    }
}
