using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PsychologicalTestsManager.Infos;

namespace PsychologicalTestsManager.Engines
{
    public class PupilEngine
    {
        private readonly string _pupilDbPath;
        public List<PupilInfo> PupilInfos { get; private set; }

        public PupilEngine(string dbFolderPath)
        {
            _pupilDbPath = Path.Combine(dbFolderPath, "Pupil.db");
        }

        public void AddPupilInfoSorted(PupilInfo pupilInfo)
        {
            if (PupilInfos.Any(x => x.Id == pupilInfo.Id))
            {
                return;
            }

            var sortedPupilInfo = new List<PupilInfo>();

            int index = PupilInfos.Count;
            for (int i = 0; i < PupilInfos.Count; i++)
            {
                if (string.Compare(pupilInfo.LastName + " " + pupilInfo.FirstName, PupilInfos[i].LastName + " " + PupilInfos[i].FirstName, StringComparison.CurrentCulture) < 0)
                {
                    index = i;
                    break;
                }
            }

            for (int i = 0; i < index; i++)
            {
                sortedPupilInfo.Add(PupilInfos[i]);
            }

            sortedPupilInfo.Add(pupilInfo);

            for (int i = index; i < PupilInfos.Count; i++)
            {
                sortedPupilInfo.Add(PupilInfos[i]);
            }

            PupilInfos = sortedPupilInfo;
        }

        public int GetPupilId(string firstName, string lastName)
        {
            int index = -1;
            foreach (PupilInfo pupilInfo in PupilInfos)
            {
                if (pupilInfo.Id > index)
                {
                    index = pupilInfo.Id;
                }

                if (firstName == pupilInfo.FirstName && lastName == pupilInfo.LastName)
                {
                    return pupilInfo.Id;
                }
            }

            return index + 1;
        }

        public void SavePupils()
        {
            var content = new StringBuilder();
            foreach (PupilInfo pupilInfo in PupilInfos)
            {
                content.AppendFormat("{2}{0}{3}{0}{4}{0}{5}{1}", DatabaseEngine.ValueSeparator, DatabaseEngine.RecordSeparator, pupilInfo.Id, pupilInfo.FirstName, pupilInfo.LastName, pupilInfo.ClassId);
            }

            File.WriteAllText(_pupilDbPath, content.ToString());
        }

        public void LoadPupils()
        {
            PupilInfos = new List<PupilInfo>();
            if (!File.Exists(_pupilDbPath))
            {
                return;
            }

            string content = File.ReadAllText(_pupilDbPath);
            string[] parameters = content.Split(new[] { DatabaseEngine.RecordSeparator }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string parameter in parameters)
            {
                string[] values = parameter.Split(new[] { DatabaseEngine.ValueSeparator }, StringSplitOptions.None);
                var pupilInfo = new PupilInfo(values);
                PupilInfos.Add(pupilInfo);
            }
        }

        public int GetPupilCountInClass(int classId)
        {
            return PupilInfos.Count(x => x.ClassId == classId);
        }

        public List<PupilInfo> GetPupilsForClass(int classId)
        {
            return PupilInfos.Where(x => x.ClassId == classId).ToList();
        }
    }
}
