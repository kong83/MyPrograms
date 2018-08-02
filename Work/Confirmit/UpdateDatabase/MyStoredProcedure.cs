using System.Diagnostics;

using Microsoft.SqlServer.Management.Smo;

namespace UpdateDatabase
{
    public class MyStoredProcedure : MySmoObjectBase
    {
        public MyStoredProcedure(SmoObjectBase smoObject, string name, string parentName, string textBody) :
            base(smoObject, name, parentName, textBody)
        {
        }

        public override bool IsEqual(MySmoObjectBase newValue)
        {
            Trace.TraceInformation("Compare {0} procedures from {1} and {2} databases...\r\n", Name, ParentName, newValue.ParentName);

            if (!base.IsEqual(newValue))
            {
                return false;
            }

            var productionStoredProcedure = (StoredProcedure)SourceSmoObject;
            var sourceStoredProcedure = (StoredProcedure)newValue.SourceSmoObject;

            if (productionStoredProcedure.Parameters.Count != sourceStoredProcedure.Parameters.Count)
            {
                return false;
            }
            
            foreach (StoredProcedureParameter productionStoredProcedureParameter in productionStoredProcedure.Parameters)
            {
                if (!sourceStoredProcedure.Parameters.Contains(productionStoredProcedureParameter.Name))
                {
                    return false;
                }

                StoredProcedureParameter sourceStoredProcedureParameter = sourceStoredProcedure.Parameters[productionStoredProcedureParameter.Name];

                if (sourceStoredProcedureParameter.DataType.SqlDataType != productionStoredProcedureParameter.DataType.SqlDataType ||
                    sourceStoredProcedureParameter.DataType.MaximumLength != productionStoredProcedureParameter.DataType.MaximumLength ||
                    sourceStoredProcedureParameter.DataType.NumericPrecision != productionStoredProcedureParameter.DataType.NumericPrecision ||
                    sourceStoredProcedureParameter.DataType.NumericScale != productionStoredProcedureParameter.DataType.NumericScale ||
                    sourceStoredProcedureParameter.DefaultValue != productionStoredProcedureParameter.DefaultValue ||
                    sourceStoredProcedureParameter.IsOutputParameter != productionStoredProcedureParameter.IsOutputParameter)
                {
                    return false;
                }
            }

            return productionStoredProcedure.QuotedIdentifierStatus == sourceStoredProcedure.QuotedIdentifierStatus &&
                productionStoredProcedure.Startup == sourceStoredProcedure.Startup;
        }


        /// <summary>
        /// Create stored procedure for split table
        /// </summary>
        /// <param name="productionDatabase">Split production database</param>
        /// <param name="surveyId">Survey ID</param>
        /// <param name="databaseEngine">Pointer of database class</param>
        /// <returns></returns>
        public override MySmoObjectBase CreateSplitItem(
            Microsoft.SqlServer.Management.Smo.Database productionDatabase,
            int surveyId,
            Database databaseEngine)
        {
            StoredProcedure newStoredProcedure;
            var modelProcedure = (StoredProcedure)SourceSmoObject;

            string originalProcedureName = Name;

            if (surveyId == 0)
            {
                newStoredProcedure = new StoredProcedure(
                    productionDatabase,
                    originalProcedureName);
            }
            else
            {
                newStoredProcedure = new StoredProcedure(
                    productionDatabase,
                    Database.GetTemplateNameByOriginalName(originalProcedureName, surveyId));
            }

            newStoredProcedure.TextMode = false;

            newStoredProcedure.ImplementationType = modelProcedure.ImplementationType;            

            newStoredProcedure.Startup = modelProcedure.Startup;

            foreach (StoredProcedureParameter storedProcedureParameter in modelProcedure.Parameters)
            {
                var newStoredProcedureParameter = new StoredProcedureParameter(
                    newStoredProcedure,
                    storedProcedureParameter.Name,
                    storedProcedureParameter.DataType)
                {
                    DefaultValue = storedProcedureParameter.DefaultValue,
                    IsOutputParameter = storedProcedureParameter.IsOutputParameter
                };

                newStoredProcedure.Parameters.Add(newStoredProcedureParameter);
            }

            if (newStoredProcedure.ImplementationType == ImplementationType.TransactSql)
            {
                newStoredProcedure.QuotedIdentifierStatus = modelProcedure.QuotedIdentifierStatus;
                newStoredProcedure.AssemblyName = string.Empty;
                newStoredProcedure.ClassName = string.Empty;
                newStoredProcedure.MethodName = string.Empty;

                newStoredProcedure.TextMode = true;
                if (surveyId == 0)
                {
                    newStoredProcedure.TextBody = modelProcedure.TextBody;
                    newStoredProcedure.TextHeader = modelProcedure.TextHeader;
                }
                else
                {
                    newStoredProcedure.TextBody = Database.GetTemplateTSQL(
                        modelProcedure.TextBody,
                        surveyId);
                    newStoredProcedure.TextHeader = Database.GetTemplateTSQL(
                        modelProcedure.TextHeader,
                        surveyId);
                }               
            }
            else
            {
                newStoredProcedure.AssemblyName = modelProcedure.AssemblyName;
                newStoredProcedure.ClassName = modelProcedure.ClassName;
                newStoredProcedure.MethodName = modelProcedure.MethodName;
            }

            if (!productionDatabase.StoredProcedures.Contains(newStoredProcedure.Name))
            {
                productionDatabase.StoredProcedures.Add(newStoredProcedure);
            }

            return new MySmoObjectBase(
                newStoredProcedure,
                newStoredProcedure.Name,
                productionDatabase.Name,
                newStoredProcedure.TextBody);
        }        
    }
}
