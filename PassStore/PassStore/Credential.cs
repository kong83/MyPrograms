using System;

namespace PassStore
{
    public class Credential
    {
        public string Name { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public Credential(string[] oneRow)
        {
            Name = oneRow[0];
            Login = oneRow[1];
            Password = oneRow[2];
        }

        public Credential(string registryInfo)
        {
            string[] cells = registryInfo.Split(new[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);

            Name = cells[0];
            Login = cells[1];
            Password = cells.Length > 2 ? cells[2] : string.Empty;
        }
    }
}
