using System;

namespace PsychologicalTestsManager.Infos
{
    public class PupilInfo
    {
        public int Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int ClassId { get; private set; }

        public PupilInfo(int id, string firstName, string lastName, int classId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            ClassId = classId;
        }

        public PupilInfo(string[] values)
        {
            if (values.Length > 0)
            {
                Id = Convert.ToInt32(values[0]);
            }

            if (values.Length > 1)
            {
                FirstName = values[1];
            }

            if (values.Length > 2)
            {
                LastName = values[2];
            }

            if (values.Length > 3)
            {
                ClassId = Convert.ToInt32(values[3]);
            }
        } 
    }
}