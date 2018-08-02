using System;

namespace UpdateDatabase
{
    public class UpdateDatabaseException : Exception
    {
        public UpdateDatabaseException(string message) :
            base(message)
        {
        }
    }
}