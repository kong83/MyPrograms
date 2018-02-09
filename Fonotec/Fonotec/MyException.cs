using System;

namespace Fonotec
{
    class MyException : Exception
    {
        public MyException(string message) :
            base(message)
        { 
        
        }
    }
}
