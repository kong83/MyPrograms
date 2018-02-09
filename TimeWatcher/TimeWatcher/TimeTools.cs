using System;

namespace TimeWatcher
{
    class TimeTools
    {
        /// <summary>
        /// Вычислить разницу в секундах для двух дат
        /// </summary>
        /// <param name="startDate">Дата начала</param>
        /// <param name="stopDate">Дата окончания</param>
        /// <returns></returns>
        public long GetSeconds(DateTime startDate, DateTime stopDate)
        {
            return (stopDate.Ticks - startDate.Ticks)/10000000;            
        }


        /// <summary>
        /// Получить строку вида h:mm:ss для переданного количества секунд
        /// </summary>
        /// <param name="seconds">Количество секунд</param>
        /// <returns></returns>
        public string GetTimeFromSecond(long seconds)
        {
            long hours = seconds / 3600;
            seconds -= hours * 3600;
            long minutes = seconds / 60;
            seconds -= minutes * 60;
            return string.Format("{0}:{1}:{2}", 
                hours, 
                minutes < 10 ? "0" + minutes : minutes.ToString(),
                seconds < 10 ? "0" + seconds : seconds.ToString());
        }
    }
}
