using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PsychologicalTestsManager.Infos;

namespace PsychologicalTestsManager.Engines
{
    public class TestResultEngine
    {
        private readonly string _testResultsDbPath;
        public List<TestResultInfo> TestResultInfos { get; private set; }

        public TestResultEngine(string dbFolderPath)
        {
            _testResultsDbPath = Path.Combine(dbFolderPath, "TestResults.db");
        }

        public void AddTestResultInfoSorted(TestResultInfo testResultInfo)
        {
            var sortedTestResultInfo = new List<TestResultInfo>();

            int index = TestResultInfos.Count;
            for (int i = 0; i < TestResultInfos.Count; i++)
            {
                if (DateTime.Compare(testResultInfo.PassingDate, TestResultInfos[i].PassingDate) < 0)
                {
                    index = i;
                    break;
                }
            }

            for (int i = 0; i < index; i++)
            {
                sortedTestResultInfo.Add(TestResultInfos[i]);
            }

            sortedTestResultInfo.Add(testResultInfo);

            for (int i = index; i < TestResultInfos.Count; i++)
            {
                sortedTestResultInfo.Add(TestResultInfos[i]);
            }

            TestResultInfos = sortedTestResultInfo;
        }

        public int GetNextTestResultId()
        {
            int index = -1;
            foreach (TestResultInfo testResultInfo in TestResultInfos)
            {
                if (testResultInfo.Id > index)
                {
                    index = testResultInfo.Id;
                }
            }

            return index + 1;
        }

        public void SaveTestResults()
        {
            var content = new StringBuilder();
            foreach (TestResultInfo testResultInfo in TestResultInfos)
            {
                content.AppendFormat("{2}{0}{3}{0}{4}{0}{5}{0}{6}{0}{7}{0}{8}{0}{9}{1}", DatabaseEngine.ValueSeparator, DatabaseEngine.RecordSeparator, testResultInfo.Id, testResultInfo.Name, testResultInfo.PupilId, testResultInfo.Result, testResultInfo.PassingDate, testResultInfo.HasHighAnxietys, testResultInfo.ClassId, testResultInfo.Note);
            }

            File.WriteAllText(_testResultsDbPath, content.ToString());
        }

        public void LoadTestResults()
        {
            TestResultInfos = new List<TestResultInfo>();
            if (!File.Exists(_testResultsDbPath))
            {
                return;
            }

            string content = File.ReadAllText(_testResultsDbPath);
            string[] parameters = content.Split(new[] { DatabaseEngine.RecordSeparator }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string parameter in parameters)
            {
                string[] values = parameter.Split(new[] { DatabaseEngine.ValueSeparator }, StringSplitOptions.None);
                var testResulInfo = new TestResultInfo(values);
                TestResultInfos.Add(testResulInfo);
            }
        }

        public int GetPupilsWithHighAnxietyForClass(int classId)
        {
            var testResultsWithHighAnxiety = TestResultInfos.Where(x => x.ClassId == classId && x.HasHighAnxietys);
            var uniquePupilIds = new List<int>();
            foreach (TestResultInfo testResult in testResultsWithHighAnxiety)
            {
                if (!uniquePupilIds.Contains(testResult.PupilId))
                {
                    uniquePupilIds.Add(testResult.PupilId);
                }
            }

            return uniquePupilIds.Count();
        }

        public int GetTestsCountForPupil(int pupilId)
        {
            return TestResultInfos.Count(x => x.PupilId == pupilId);
        }

        public bool DoesPupilHasHighAnxiety(int pupilId)
        {
            TestResultInfo testResultInfo = TestResultInfos.FirstOrDefault(x => x.PupilId == pupilId && x.Name == "Test_Phillipsa");
            if (testResultInfo == null)
            {
                return false;
            }

            return testResultInfo.HasHighAnxietys;
        }

        public List<TestResultInfo> GetTestResultsForPupils(int pupilId)
        {
            return TestResultInfos.Where(x => x.PupilId == pupilId).ToList();
        }

        public TestResultInfo GetTestResultById(int id)
        {
            return TestResultInfos.First(x => x.Id == id);
        }

        public List<TestResultInfo> GetTestResultsForClass(int classId, string testName)
        {
            return TestResultInfos.Where(x => x.ClassId == classId && x.Name == testName).ToList();
        }

        public void RemoveById(int id)
        {
            TestResultInfo testResultInfo = TestResultInfos.FirstOrDefault(x => x.Id == id);

            if (testResultInfo == null)
            {
                MessageBox.Show(string.Format("Внутренняя ошибка программы: результат теста с id={0} не найден. Удаление невозможно.", id), "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            TestResultInfos.Remove(testResultInfo);

            SaveTestResults();
        }

        public void UpdateNote(int id, string newNote)
        {
            TestResultInfo testResultInfo = TestResultInfos.FirstOrDefault(x => x.Id == id);

            if (testResultInfo == null)
            {
                MessageBox.Show(string.Format("Внутренняя ошибка программы: результат теста с id={0} не найден. Изменение поля невозможно.", id), "Критическая ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            testResultInfo.Note = newNote;

            SaveTestResults();
        }
    }
}
