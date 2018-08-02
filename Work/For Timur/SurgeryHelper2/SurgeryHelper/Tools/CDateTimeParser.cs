using System;

namespace SurgeryHelper.Tools
{
    public class CDateTimeParser
    {
        private int _day;
        private int _month;
        private int _year;
        private int _hour;
        private int _minut;

        private DateTime CreateDiateTimeObject()
        {
            return new DateTime(_year, _month, _day, _hour, _minut, 0);
        }

        public CDateTimeParser()
        {
            _day = DateTime.Now.Day;
            _month = DateTime.Now.Month;
            _year = DateTime.Now.Year;
            _hour = 0;
            _minut = 0;
        }

        /// <summary>
        /// Возвращает объект DateTime из строки вида 11.12.1111
        /// </summary>
        /// <param name="dateString">Строка с датой</param>
        /// <returns></returns>
        public DateTime ParseRusDate(string dateString)
        {
            string[] dayMonthYear = dateString.Split('.');
            if (dayMonthYear.Length != 3)
            {
                throw new Exception("Строка " + dateString + " не распознана как дата");                
            }

            _day = Convert.ToInt32(dayMonthYear[0]);
            _month = Convert.ToInt32(dayMonthYear[1]);
            _year = Convert.ToInt32(dayMonthYear[2]);

            return CreateDiateTimeObject();
        }

        /// <summary>
        /// Возвращает объект DateTime из строки вида 11.12.1111 11:11
        /// </summary>
        /// <param name="dateString">Строка с датой</param>
        /// <param name="timeString">Строка с временем</param>
        /// <returns></returns>
        public DateTime ParseRusDateTime(string dateString, string timeString)
        {
            ParseRusDate(dateString);
            ParseTime(timeString);
            return CreateDiateTimeObject();
        }

        /// <summary>
        /// Возвращает объект DateTime из строки вида 12/11/1111 11:11
        /// </summary>
        /// <param name="dateString">Строка с датой</param>
        /// <returns></returns>
        public DateTime ParseEngDate(string dateString)
        {
            string[] dayMonthYear = dateString.Split('/');
            if (dayMonthYear.Length != 3)
            {
                throw new Exception("Строка " + dateString + " не распознана как дата");
            }

            _month = Convert.ToInt32(dayMonthYear[0]);
            _day = Convert.ToInt32(dayMonthYear[1]);
            _year = Convert.ToInt32(dayMonthYear[2]);

            return CreateDiateTimeObject();
        }

        /// <summary>
        /// Возвращает объект DateTime из строки вида 12/11/1111
        /// </summary>
        /// <param name="dateString">Строка с датой</param>
        /// <param name="timeString">Строка с временем</param>
        /// <returns></returns>
        public DateTime ParseEngDateTime(string dateString, string timeString)
        {
            ParseEngDate(dateString);
            ParseTime(timeString);
            return CreateDiateTimeObject();
        }

        /// <summary>
        /// Возвращает объект DateTime из строки вида 11:11
        /// </summary>
        /// <param name="timeString">Строка с временем</param>
        /// <returns></returns>
        public DateTime ParseTime(string timeString)
        {           
            string[] hourMinut = timeString.Split(':');
            if (hourMinut.Length < 2)
            {
                throw new Exception("Строка " + timeString + " не распознана как время");
            }

            _hour = Convert.ToInt32(hourMinut[0]);
            _minut = Convert.ToInt32(hourMinut[1]);

            return CreateDiateTimeObject();
        }
    }
}
