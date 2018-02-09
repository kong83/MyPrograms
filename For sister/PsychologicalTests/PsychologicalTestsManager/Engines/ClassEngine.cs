using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PsychologicalTestsManager.Infos;
using System.Windows.Forms;

namespace PsychologicalTestsManager.Engines
{
    public class ClassEngine
    {
        private readonly string _classDbPath;
        public List<ClassInfo> ClassInfos { get; private set; }

        public ClassEngine(string dbFolderPath)
        {
            _classDbPath = Path.Combine(dbFolderPath, "Class.db");
        }

        public void AddClassInfoSorted(ClassInfo classInfo)
        {
            if (ClassInfos.Any(x => x.Id == classInfo.Id))
            {
                return;
            }

            var sortedClassInfo = new List<ClassInfo>();

            int index = ClassInfos.Count;
            for (int i = 0; i < ClassInfos.Count; i++)
            {
                if (string.Compare(classInfo.Name, ClassInfos[i].Name, StringComparison.CurrentCulture) < 0)
                {
                    index = i;
                    break;
                }
            }

            for (int i = 0; i < index; i++)
            {
                sortedClassInfo.Add(ClassInfos[i]);
            }

            sortedClassInfo.Add(classInfo);

            for (int i = index; i < ClassInfos.Count; i++)
            {
                sortedClassInfo.Add(ClassInfos[i]);
            }

            ClassInfos = sortedClassInfo;
        }

        public int GetClassId(string className)
        {
            int index = -1;
            foreach (ClassInfo classInfo in ClassInfos)
            {
                if (classInfo.Id > index)
                {
                    index = classInfo.Id;
                }

                if (className == classInfo.Name)
                {
                    return classInfo.Id;
                }
            }

            return index + 1;
        }

        public void SaveClass()
        {
            var content = new StringBuilder();
            foreach (ClassInfo classIfno in ClassInfos)
            {
                content.AppendFormat("{2}{0}{3}{0}{4}{1}", DatabaseEngine.ValueSeparator, DatabaseEngine.RecordSeparator, classIfno.Id, classIfno.Name, classIfno.Note);
            }

            File.WriteAllText(_classDbPath, content.ToString());
        }

        public void LoadClass()
        {
            ClassInfos = new List<ClassInfo>();
            if (!File.Exists(_classDbPath))
            {
                return;
            }

            string content = File.ReadAllText(_classDbPath);
            string[] parameters = content.Split(new[] { DatabaseEngine.RecordSeparator }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string parameter in parameters)
            {
                string[] values = parameter.Split(new[] { DatabaseEngine.ValueSeparator }, StringSplitOptions.None);
                var classInfo = new ClassInfo(values);
                ClassInfos.Add(classInfo);
            }
        }

        public void UpdateNote(int id, string newNote)
        {
            ClassInfo classInfo = ClassInfos.FirstOrDefault(x => x.Id == id);

            if (classInfo == null)
            {
                MessageBox.Show(string.Format("Внутренняя ошибка программы: класс с id={0} не найден. Изменение поля невозможно.", id), "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            classInfo.Note = newNote;

            SaveClass();
        }

        internal ClassInfo GetClassInfoById(int id)
        {
            return ClassInfos.First(x => x.Id == id);
        }
    }
}
