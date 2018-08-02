namespace SurgeryHelper.Essences
{
    /// <summary>
    /// Класс с информацией по хирургам
    /// </summary>
    public class CSurgeon : CBaseMedical
    {
        public string Header;

        public CSurgeon()
        { 
        }

        public CSurgeon(CSurgeon surgeonInfo)
        {
            Id = surgeonInfo.Id;
            Name = surgeonInfo.Name;
            Header = surgeonInfo.Header;
        }

        public static int Compare(CSurgeon surgeonInfo1, CSurgeon surgeonInfo2)
        {
            return string.Compare(surgeonInfo1.Name, surgeonInfo2.Name);
        }
    }
}
