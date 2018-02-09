using System;

namespace TimeWatcher
{
    /// <summary>
    /// Class for exchange data between different form
    /// </summary>
    public class Exchange
    {
        /// <summary>
        /// For confirmation action to other form
        /// </summary>
        public bool OK;
       

        /// <summary>
        /// Struct for data transmitted between forms (about project)
        /// </summary>
        public struct ProjectInfo
        {
            /// <summary>
            /// id in database
            /// </summary>
            public int ID;
            /// <summary>
            /// Project name
            /// </summary>
            public string Name;
            /// <summary>
            /// Стоимость часа работы
            /// </summary>
            public int Pay;
            /// <summary>
            /// Project info
            /// </summary>
            public string Info;
        }

        /// <summary>
        /// Struct for data transmitted between forms (about times)
        /// </summary>
        public struct TimesInfo
        {
            /// <summary>
            /// id in database
            /// </summary>
            public int ID;
            /// <summary>
            /// Project id
            /// </summary>
            public int PID;
            /// <summary>
            /// Date and time of start work 
            /// </summary>
            public DateTime DateStart;
            /// <summary>
            /// Date and time of stop work 
            /// </summary>
            public DateTime? DateStop;
        }
    }
}


