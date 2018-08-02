namespace SurgeryHelper.Entities
{
    public class GlobalSettingsClass
    {
        /// <summary>
        /// Название отделения (обычно номер)
        /// </summary>
        public string DepartmentName;

        /// <summary>
        /// Заведующая отделением
        /// </summary>
        public string BranchManager;

        /// <summary>
        /// Путь до файла с шапкой для выписного эпикриза
        /// </summary>
        public string DischargeEpicrisisHeaderFileName;

        /// <summary>
        /// Включено ли логирование
        /// </summary>
        public bool IsLoggingEnabled;

        public GlobalSettingsClass()
        {
            DepartmentName = "8";
            BranchManager = "В.А.Калантырская";
            DischargeEpicrisisHeaderFileName = "DischargeEpicrisisHeader.doc";
            IsLoggingEnabled = false;
        }


        /// <summary>
        /// Опередляет, начинается ли название отделения с указанного номера
        /// </summary>
        /// <param name="number">Номер отделения</param>
        /// <returns></returns>
        public bool IsDepartmentNameStartWithNumber(string number)
        {
            if (DepartmentName.Trim() == number)
            {
                return true;
            }

            if (DepartmentName.Length > number.Length + 1 &&
               DepartmentName.StartsWith(number + " "))
            {
                return true;
            }

            return false;
        }
    }
}
