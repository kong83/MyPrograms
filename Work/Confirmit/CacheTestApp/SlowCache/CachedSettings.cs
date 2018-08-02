namespace SlowCache
{
    public class CachedSettings
    {
        public string Name { get; private set; }
        public string Value { get; private set; }

        public CachedSettings(string name, string value)
        {
            Name = name;
            Value = value;
        }
    }
}
