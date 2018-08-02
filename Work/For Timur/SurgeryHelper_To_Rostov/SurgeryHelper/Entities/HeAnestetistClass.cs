namespace SurgeryHelper.Entities
{
    /// <summary>
    /// Класс с информацией об анестизистах
    /// </summary>
    public class HeAnestethistClass : MedicalClass
    {
        public HeAnestethistClass()
        {
        }

        public HeAnestethistClass(HeAnestethistClass heAnestethistInfo)
        {
            Id = heAnestethistInfo.Id;
            LastNameWithInitials = heAnestethistInfo.LastNameWithInitials;
        }

        public static int Compare(HeAnestethistClass heAnestethistInfo1, HeAnestethistClass heAnestethistInfo2)
        {
            return string.Compare(heAnestethistInfo1.LastNameWithInitials, heAnestethistInfo2.LastNameWithInitials);
        }
    }
}
