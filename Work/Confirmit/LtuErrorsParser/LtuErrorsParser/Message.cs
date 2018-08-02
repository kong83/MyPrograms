namespace LtuErrorsParser
{
    class Message
    {
        public string ServerName { get; private set; }
        public string EventTime { get; private set; }
        public string FullError { get; private set; }
        public int ErrorCount { get; private set; }

        public Message(string serverName, string eventTime, string fullError)
        {
            ServerName = serverName;
            EventTime = eventTime;
            FullError = fullError;
            ErrorCount = 1;
        }

        public void AddCount()
        {
            ErrorCount++;
        }
    }
}
