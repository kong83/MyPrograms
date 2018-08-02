using SurgeryHelper.Essences;

namespace SurgeryHelper.Workers
{
    public abstract class CBaseWorker
    {
        protected const string ObjSplitStr = "^!&!^";
        protected const string DataSplitStr = "^%#%^";
        public const string ListSplitStr = ";^;";


        /// <summary>
        /// Сгенерировать новый Id
        /// </summary>
        /// <param name="list">Последовательность сущностей, для которых будет генериться новый Id</param>
        /// <returns></returns>
        protected int GetNewID(CIdEssence[] list)
        {
            int max = 0;
            foreach (CIdEssence essence in list)
            {
                if (essence.Id > max)
                {
                    max = essence.Id;
                }
            }

            return max + 1;
        }
    }
}
