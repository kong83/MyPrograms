using System;

namespace PsychologicalTestsManager.Infos
{
    public class ClassInfo
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Note { get; set; }

        public ClassInfo(int id, string name, string note)
        {
            Id = id;
            Name = name;
            Note = note;
        }

        public ClassInfo(string[] values)
        {
            if(values.Length > 0)
            {
               Id =  Convert.ToInt32(values[0]);
            }

            if(values.Length > 1)
            {
               Name = values[1];
            }

            Note = string.Empty;
            if (values.Length > 2)
            {
                Note = values[2];
            }
        }
    }
}