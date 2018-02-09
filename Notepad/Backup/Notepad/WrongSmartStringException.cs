using System;
using System.Collections.Generic;
using System.Text;

namespace Notepad
{
    public class WrongSmartStringException : Exception
    {
        public WrongSmartStringException(string errorString) :
            base(errorString)
        {
        }
    }
}
