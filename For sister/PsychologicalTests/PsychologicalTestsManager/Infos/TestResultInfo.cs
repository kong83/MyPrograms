using System;

namespace PsychologicalTestsManager.Infos
{
    public class TestResultInfo
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int PupilId { get; private set; }
        public string Result { get; private set; }
        public DateTime PassingDate { get; private set; }
        public bool HasHighAnxietys { get; private set; }
        public int ClassId { get; private set; }
        public string Note { get; set; }

        public TestResultInfo(int id, string name, int pupilId, string result, DateTime passingDate, bool anxietys, int classId, string note)
        {
            Id = id;
            Name = name;
            PupilId = pupilId;
            Result = result;
            PassingDate = passingDate;
            HasHighAnxietys = anxietys;
            ClassId = classId;
            Note = note;
        }

        public TestResultInfo(string[] values)
        {
            if(values.Length > 0)
            {
               Id =  Convert.ToInt32(values[0]);
            }

            if(values.Length > 1)
            {
               Name =  values[1];
            }

            if(values.Length > 2)
            {
               PupilId =  Convert.ToInt32(values[2]);
            }

            if(values.Length > 3)
            {
               Result =  values[3];
            }

            if(values.Length > 4)
            {
               PassingDate =  GetDateTimeFromString(values[4]);
            }

            HasHighAnxietys = values.Length > 5 && Convert.ToBoolean(values[5]);

            ClassId = values.Length > 6 ? Convert.ToInt32(values[6]) : 0;

            Note = string.Empty;
            if (values.Length > 7)
            {
                Note = values[7];
            }
        }

        /// <summary>
        /// Возвращает объект DateTime из строки вида 11.12.1111 11:11 или 12/11/1111 11:11
        /// </summary>
        /// <param name="dateTimeStr">Строка с датой</param>
        /// <returns></returns>
        private DateTime GetDateTimeFromString(string dateTimeStr)
        {
            string[] dateAndTime = dateTimeStr.Split(' ');
            if (dateAndTime.Length == 0)
            {
                throw new Exception("Строка " + dateTimeStr + " не распознана как тип DateTime");
            }

            int day, month, year, hour = 0, minut = 0, second = 0;

            string[] dayMonthYear = dateAndTime[0].Split('.');
            if (dayMonthYear.Length == 3)
            {
                day = Convert.ToInt32(dayMonthYear[0]);
                month = Convert.ToInt32(dayMonthYear[1]);
                year = Convert.ToInt32(dayMonthYear[2]);
            }
            else
            {
                dayMonthYear = dateAndTime[0].Split('/');
                if (dayMonthYear.Length != 3)
                {
                    throw new Exception("Строка " + dateTimeStr + " не распознана как тип DateTime");
                }

                month = Convert.ToInt32(dayMonthYear[0]);
                day = Convert.ToInt32(dayMonthYear[1]);
                year = Convert.ToInt32(dayMonthYear[2]);
            }

            if (dateAndTime.Length == 2)
            {
                string[] hourMinuteSec = dateAndTime[1].Split(':');
                if (dayMonthYear.Length < 2)
                {
                    throw new Exception("Строка " + dateTimeStr + " не распознана как тип DateTime");
                }

                hour = Convert.ToInt32(hourMinuteSec[0]);
                minut = Convert.ToInt32(hourMinuteSec[1]);
                second = Convert.ToInt32(hourMinuteSec[2]);
            }

            return new DateTime(year, month, day, hour, minut, second);
        }
    }
}