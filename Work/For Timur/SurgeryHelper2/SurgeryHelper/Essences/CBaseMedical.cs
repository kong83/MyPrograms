namespace SurgeryHelper.Essences
{
    /// <summary>
    /// Абстрактный класс для хранения информации по медицинским работникам
    /// </summary>
    public abstract class CBaseMedical : CIdEssence
    {
        /// <summary>
        /// Фамилия с инициалами или название
        /// </summary>
        public string Name;
    }
}
