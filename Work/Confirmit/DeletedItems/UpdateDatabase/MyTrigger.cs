using Microsoft.SqlServer.Management.Smo;

namespace UpdateDatabase
{
    class MyTrigger : MySmoObjectBase
    {
        public MyTrigger(SmoObjectBase smoObject, string name, string parentName, string textBody) :
            base(smoObject, name, parentName, textBody)
        {
        }

        public override bool IsEqual(MySmoObjectBase newValue)
        {
            if (!base.IsEqual(newValue))
            {
                return false;
            }

            var productionTrigger = (Trigger)SourceSmoObject;
            var sourceTrigger = (Trigger)newValue.SourceSmoObject;

            return productionTrigger.Delete == sourceTrigger.Delete &&
                productionTrigger.DeleteOrder == sourceTrigger.DeleteOrder &&
                productionTrigger.Insert == sourceTrigger.Insert &&
                productionTrigger.InsertOrder == sourceTrigger.InsertOrder &&
                productionTrigger.Update == sourceTrigger.Update &&
                productionTrigger.UpdateOrder == sourceTrigger.UpdateOrder &&
                productionTrigger.QuotedIdentifierStatus == sourceTrigger.QuotedIdentifierStatus;
        }


        /// <summary>
        /// Create trigger for split table
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
            Trigger newTrigger;
            var modelTrigger = (Trigger)SourceSmoObject;

            if (surveyId == 0)
            {
                newTrigger = new Trigger(
                    productionTable,
                    modelTrigger.Name);
            }
            else
            {
                newTrigger = new Trigger(
                    productionTable,
                    databaseEngine.GetTemplateNameByOriginalName(
                        modelTrigger.Name,
                        surveyId));
            }

            newTrigger.TextMode = false;

            newTrigger.Delete = modelTrigger.Delete;
            newTrigger.DeleteOrder = modelTrigger.DeleteOrder;

            newTrigger.Insert = modelTrigger.Insert;
            newTrigger.InsertOrder = modelTrigger.InsertOrder;

            newTrigger.Update = modelTrigger.Update;
            newTrigger.UpdateOrder = modelTrigger.UpdateOrder;

            newTrigger.QuotedIdentifierStatus = modelTrigger.QuotedIdentifierStatus;

            if (surveyId == 0)
            {
                newTrigger.TextBody = modelTrigger.TextBody;
            }
            else
            {
                newTrigger.TextBody = databaseEngine.GetTemplateTSQL(
                    modelTrigger.TextBody,
                    surveyId);
            }

            return new MySmoObjectBase(
                newTrigger,
                newTrigger.Name,
                productionTable.Name,
                newTrigger.TextBody,
                newTrigger.TextHeader);
        }
    }
}
