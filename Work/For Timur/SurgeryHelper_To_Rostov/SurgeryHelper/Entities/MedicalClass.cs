namespace SurgeryHelper.Entities
{
    /// <summary>
    /// Абстрактный класс для хранения информации по медицинским работникам
    /// </summary>
    public abstract class MedicalClass
    {
        /// <summary>
        /// Уникальный id
        /// </summary>
        public int Id;

        /// <summary>
        /// Фамилия с инициалами
        /// </summary>
        public string LastNameWithInitials;

        protected MedicalClass()
        {
            Id = 0;
        }
    }
}
