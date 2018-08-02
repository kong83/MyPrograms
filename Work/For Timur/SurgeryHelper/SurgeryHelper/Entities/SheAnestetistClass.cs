namespace SurgeryHelper.Entities
{
    /// <summary>
    /// Класс с информацией об анестизистках
    /// </summary>
    public class SheAnestethistClass : MedicalClass
    {
        public SheAnestethistClass()
        {
        }

        public SheAnestethistClass(SheAnestethistClass sheAnestethistInfo)
        {
            Id = sheAnestethistInfo.Id;
            LastNameWithInitials = sheAnestethistInfo.LastNameWithInitials;
        }

        public static int Compare(SheAnestethistClass sheAnestethistInfo1, SheAnestethistClass sheAnestethistInfo2)
        {
            return string.Compare(sheAnestethistInfo1.LastNameWithInitials, sheAnestethistInfo2.LastNameWithInitials);
        }
    }
}
