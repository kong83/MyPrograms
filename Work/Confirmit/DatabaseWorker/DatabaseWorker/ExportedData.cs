using System.Globalization;

namespace DatabaseWorker
{
    public class ExportedData
    {
        public string Name { get; private set; }
        public string TimeInSec { get; private set; }
        public string TimeInMin { get; private set; }
        public string Percent { get; private set; }

        public ExportedData(string name, int duration, int allDuration)
        {
            Name = name;
            TimeInSec = (duration / 1000).ToString(CultureInfo.CurrentCulture);
            if (duration > 60000)
            {
                TimeInMin = (duration / 60000.0).ToString("F", CultureInfo.CurrentCulture);
            }

            if (duration < allDuration)
            {
                Percent = ((duration * 100.0) / allDuration).ToString("F", CultureInfo.CurrentCulture) + "%";
            }
        }
    }
}
