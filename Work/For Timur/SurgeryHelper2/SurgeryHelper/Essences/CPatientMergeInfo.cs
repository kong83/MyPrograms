using SurgeryHelper.Tools;
namespace SurgeryHelper.Essences
{
    /// <summary>
    /// Класс для хранения информации об одном пациенте, для отображения в форме с мержем
    /// </summary>
    public class CMergeInfo
    {
        public string FIO { get; set; }

        public string Nosology { get; set; }

        public string Difference { get; set; }

        public string Value { get; set; }

        public object Object { get; set; }

        public int IdOwnPatient { get; set; }

        public int IdForeignPatient { get; set; }

        public int IdOwnHospitalization { get; set; }

        public int IdForeignHospitalization { get; set; }

        public int IdOwnVisit { get; set; }

        public int IdForeignVisit { get; set; }

        public int IdOperation { get; set; }        

        public ObjectType TypeOfObject { get; set; }

        public CardType TypeOfCard { get; set; }

        public CardSide SideOfCard { get; set; }

        public CMergeInfo()
        {
            FIO = Nosology = Difference = Value = string.Empty;

            IdOwnPatient = IdForeignPatient = IdOwnHospitalization  =
            IdForeignHospitalization = IdOwnVisit = IdForeignVisit  =
            IdOperation = - 1;
        }
    }
}
