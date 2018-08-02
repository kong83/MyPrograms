namespace SurgeryHelper.Entities
{
    /// <summary>
    /// Класс с информацией по хирургам
    /// </summary>
    public class SurgeonClass : MedicalClass
    {
        public SurgeonClass()
        {
        }

        public SurgeonClass(SurgeonClass surgeonInfo)
        {
            Id = surgeonInfo.Id;
            LastNameWithInitials = surgeonInfo.LastNameWithInitials;
        }

        public static int Compare(SurgeonClass surgeonInfo1, SurgeonClass surgeonInfo2)
        {
            return string.Compare(surgeonInfo1.LastNameWithInitials, surgeonInfo2.LastNameWithInitials);
        }
    }
}
