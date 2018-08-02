namespace SurgeryHelper.Essences
{
    /// <summary>
    /// Класс с информацией о санитарах
    /// </summary>
    public class COrderly : CBaseMedical
    {
        public COrderly()
        { 
        }

        public COrderly(COrderly orderlyInfo)
        {
            Id = orderlyInfo.Id;
            Name = orderlyInfo.Name;
        }

        public static int Compare(COrderly orderlyInfo1, COrderly orderlyInfo2)
        {
            return string.Compare(orderlyInfo1.Name, orderlyInfo2.Name);
        }
    }
}
