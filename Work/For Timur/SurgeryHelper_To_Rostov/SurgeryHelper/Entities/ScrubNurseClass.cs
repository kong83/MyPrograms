namespace SurgeryHelper.Entities
{
    /// <summary>
    ///  Класс с информацией по операционным мед. сёстрам
    /// </summary>
    public class ScrubNurseClass : MedicalClass
    {
        public ScrubNurseClass()
        {
        }

        public ScrubNurseClass(ScrubNurseClass scrubNurseInfo)
        {
            Id = scrubNurseInfo.Id;
            LastNameWithInitials = scrubNurseInfo.LastNameWithInitials;
        }

        public static int Compare(ScrubNurseClass scrubNurseInfo1, ScrubNurseClass scrubNurseInfo2)
        {
            return string.Compare(scrubNurseInfo1.LastNameWithInitials, scrubNurseInfo2.LastNameWithInitials);
        }
    }
}
