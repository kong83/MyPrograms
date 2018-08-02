namespace SurgeryHelper.Essences
{
    /// <summary>
    ///  Класс с информацией по операционным мед. сёстрам
    /// </summary>
    public class CScrubNurse : CBaseMedical
    {
        public CScrubNurse()
        { 
        }

        public CScrubNurse(CScrubNurse scrubNurseInfo)
        {
            Id = scrubNurseInfo.Id;
            Name = scrubNurseInfo.Name;
        }

        public static int Compare(CScrubNurse scrubNurseInfo1, CScrubNurse scrubNurseInfo2)
        {
            return string.Compare(scrubNurseInfo1.Name, scrubNurseInfo2.Name);
        }
    }
}
