namespace SurgeryHelper.Entities
{
    public class CureClass
    {
        /// <summary>
        /// Название лекарства с дозой
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дефолтное количество приёмов в день
        /// </summary>
        public string DefaultPerDayCount { get; set; }

        /// <summary>
        /// Дефолтный способ введения
        /// </summary>
        public string DefaultReceivingMethod { get; set; }

        /// <summary>
        /// Дефолтная продолжительность приёма
        /// </summary>
        public string DefaultDuration { get; set; }

        public CureClass()
        {
            Name = DefaultPerDayCount = DefaultReceivingMethod = DefaultDuration = string.Empty;
        }

        public CureClass(CureClass cureClass)
        {
            Name = cureClass.Name;
            DefaultPerDayCount = cureClass.DefaultPerDayCount;
            DefaultReceivingMethod = cureClass.DefaultReceivingMethod;
            DefaultDuration = cureClass.DefaultDuration;
        }

        public static int Compare(CureClass cureInfo1, CureClass cureInfo2)
        {
            return string.Compare(cureInfo1.Name, cureInfo2.Name);
        }
    }
}
