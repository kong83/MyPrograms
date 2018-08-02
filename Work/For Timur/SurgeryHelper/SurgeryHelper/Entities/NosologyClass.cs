namespace SurgeryHelper.Entities
{
    /// <summary>
    /// Класс с информацией о нозологии
    /// </summary>
    public class NosologyClass : MedicalClass
    {
        public NosologyClass()
        {
        }

        public NosologyClass(NosologyClass orderlyInfo)
        {
            Id = orderlyInfo.Id;
            LastNameWithInitials = orderlyInfo.LastNameWithInitials;
        }

        public static int Compare(NosologyClass nosologyInfo1, NosologyClass nosologyInfo2)
        {
            return string.Compare(nosologyInfo1.LastNameWithInitials, nosologyInfo2.LastNameWithInitials);
        }
    }
}