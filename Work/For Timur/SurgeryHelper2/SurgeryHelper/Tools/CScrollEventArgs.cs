using System;

namespace SurgeryHelper.Tools
{
    public class CScrollEventArgs : EventArgs
    {
        public CScrollEventArgs(int position)
        {
            Position = position;
        }

        public int Position { get; set; }
    } 
}
