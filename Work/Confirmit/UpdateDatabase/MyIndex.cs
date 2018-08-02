using System.Collections.Generic;
using Microsoft.SqlServer.Management.Smo;

namespace UpdateDatabase
{
    public class MyIndex : MySmoObjectBase
    {
        public static string ChangeName(string name)
        { // PK__TempTableOneColu__7C8480AE
            if (name.StartsWith("PK__"))
            {
                return name.Substring(0, name.LastIndexOf("__"));
            }

            return name;
        }
       
        public MyIndex(SmoObjectBase smoObject, string name, string parentName) :
            base(smoObject, ChangeName(name), parentName)
        {            
        }

        public override bool IsEqual(MySmoObjectBase newValue)
        {
            if (!base.IsEqual(newValue))
            {
                return false;
            }

            var productionIndex = (Index)SourceSmoObject;
            var sourceIndex = (Index)newValue.SourceSmoObject;            
            
            //
            // Check include column and non include column separate
            //            
            var includedColumnsProduction = new List<IndexedColumn>();
            var includedColumnsSource = new List<IndexedColumn>();
            var indexedColumnsProduction = new List<IndexedColumn>();
            var indexedColumnsSource = new List<IndexedColumn>();

            // Get production DB's columns
            for (int i = 0; i < productionIndex.IndexedColumns.Count; i++)
            {
                if (productionIndex.IndexedColumns[i].IsIncluded)
                {
                    includedColumnsProduction.Add(productionIndex.IndexedColumns[i]);
                }
                else
                {
                    indexedColumnsProduction.Add(productionIndex.IndexedColumns[i]);
                }
            }

            // Get source DB's columns
            for (int i = 0; i < sourceIndex.IndexedColumns.Count; i++)
            {
                if (sourceIndex.IndexedColumns[i].IsIncluded)
                {
                    includedColumnsSource.Add(sourceIndex.IndexedColumns[i]);
                }
                else
                {
                    indexedColumnsSource.Add(sourceIndex.IndexedColumns[i]);
                }
            }

            if (includedColumnsProduction.Count != includedColumnsSource.Count ||
                indexedColumnsProduction.Count != indexedColumnsSource.Count)
            {
                return false;
            }

            // Check include column 
            foreach (IndexedColumn includedColumnSource in includedColumnsSource)
            {
                bool isContains = false;
                foreach (IndexedColumn includedColumnProduction in includedColumnsProduction)
                {
                    if (includedColumnSource.Name == includedColumnProduction.Name &&
                        includedColumnSource.Descending == includedColumnProduction.Descending)
                    {
                        isContains = true;
                        break;
                    }
                }

                if (!isContains)
                {
                    return false;
                }
            }

            // Check non include column
            for (int i = 0; i < indexedColumnsSource.Count; i++)
            {
                if (indexedColumnsSource[i].Name != indexedColumnsProduction[i].Name ||
                    indexedColumnsSource[i].Descending != indexedColumnsProduction[i].Descending)
                {
                    return false;
                }
            }

            return productionIndex.PadIndex == sourceIndex.PadIndex &&
                productionIndex.FillFactor == sourceIndex.FillFactor &&
                productionIndex.MaximumDegreeOfParallelism == sourceIndex.MaximumDegreeOfParallelism &&
                productionIndex.SortInTempdb == sourceIndex.SortInTempdb &&
                productionIndex.IsFullTextKey == sourceIndex.IsFullTextKey &&
                productionIndex.DisallowPageLocks == sourceIndex.DisallowPageLocks &&
                productionIndex.DisallowRowLocks == sourceIndex.DisallowRowLocks &&
                productionIndex.IgnoreDuplicateKeys == sourceIndex.IgnoreDuplicateKeys &&
                productionIndex.IndexKeyType == sourceIndex.IndexKeyType &&
                productionIndex.IsClustered == sourceIndex.IsClustered &&
                productionIndex.IsUnique == sourceIndex.IsUnique &&
                productionIndex.NoAutomaticRecomputation == sourceIndex.NoAutomaticRecomputation &&
                productionIndex.OnlineIndexOperation == sourceIndex.OnlineIndexOperation;
        }

        /// <summary>
        /// Create index for split table
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
            var newIndex = new Index();
            var modelIndex = (Index)SourceSmoObject;

            newIndex.Parent = productionTable;

            if (surveyId == 0)
            {
                newIndex.Name = modelIndex.Name;
            }
            else
            {
                newIndex.Name = Database.GetTemplateNameByOriginalName(
                    modelIndex.Name,
                    surveyId);
            }

            newIndex.IgnoreDuplicateKeys = modelIndex.IgnoreDuplicateKeys;
            newIndex.IndexKeyType = modelIndex.IndexKeyType;
            newIndex.IsClustered = modelIndex.IsClustered;
            newIndex.IsUnique = modelIndex.IsUnique;
            newIndex.NoAutomaticRecomputation = modelIndex.NoAutomaticRecomputation;
            newIndex.OnlineIndexOperation = modelIndex.OnlineIndexOperation;
            newIndex.DisallowPageLocks = modelIndex.DisallowPageLocks;
            newIndex.DisallowRowLocks = modelIndex.DisallowRowLocks;
            newIndex.SortInTempdb = modelIndex.SortInTempdb;
            newIndex.IsFullTextKey = modelIndex.IsFullTextKey;
            newIndex.PadIndex = modelIndex.PadIndex;
            newIndex.FillFactor = modelIndex.FillFactor;
            newIndex.MaximumDegreeOfParallelism = modelIndex.MaximumDegreeOfParallelism;
            
            /*
            newIndex.CompactLargeObjects = modelIndex.CompactLargeObjects;            
            newIndex.FileGroup = modelIndex.FileGroup;
            newIndex.ParentXmlIndex = modelIndex.ParentXmlIndex;
            newIndex.PartitionScheme = modelIndex.PartitionScheme;
            newIndex.SecondaryXmlIndexType = modelIndex.SecondaryXmlIndexType;
            newIndex.UserData = modelIndex.UserData;*/
            
            foreach (IndexedColumn indexedColumn in modelIndex.IndexedColumns)
            {
                var newIndexedColumn = new IndexedColumn(
                    newIndex,
                    indexedColumn.Name,
                    indexedColumn.Descending)
                {
                    IsIncluded = indexedColumn.IsIncluded
                };

                newIndex.IndexedColumns.Add(newIndexedColumn);
            }

            return new MySmoObjectBase(
                newIndex,
                newIndex.Name,
                productionTable.Name);
        }
    }
}
