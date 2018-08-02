namespace SurgeryHelper.Entities
{
    /// <summary>
    /// Класс с информацией о санитарах
    /// </summary>
    public class OrderlyClass : MedicalClass
    {
        public OrderlyClass()
        {
        }

        public OrderlyClass(OrderlyClass orderlyInfo)
        {
            Id = orderlyInfo.Id;
            LastNameWithInitials = orderlyInfo.LastNameWithInitials;
        }

        public static int Compare(OrderlyClass orderlyInfo1, OrderlyClass orderlyInfo2)
        {
            return string.Compare(orderlyInfo1.LastNameWithInitials, orderlyInfo2.LastNameWithInitials);
        }
    }
}
