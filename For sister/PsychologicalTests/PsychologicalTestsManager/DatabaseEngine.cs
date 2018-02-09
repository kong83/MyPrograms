using System;
using System.IO;
using PsychologicalTestsManager.Engines;
using PsychologicalTestsManager.Infos;
using TestResultMaker;

namespace PsychologicalTestsManager
{
    public class DatabaseEngine
    {
        public const string RecordSeparator = "^&!&^";
        public const string ValueSeparator = "!^!";

        private readonly AnxietyTestResultMaker _anxietyTestResultMaker;

        private readonly ClassEngine _classEngine;
        private readonly PupilEngine _pupilEngine;
        private readonly TestResultEngine _testResultEngine;

        public DatabaseEngine(AnxietyTestResultMaker anxietyTestResultMaker, ClassEngine classEngine, PupilEngine pupilEngine, TestResultEngine testResultEngine)
        {
            _anxietyTestResultMaker = anxietyTestResultMaker;

            _classEngine = classEngine;
            _pupilEngine = pupilEngine;
            _testResultEngine = testResultEngine;
        }

        /// <summary>
        /// Загрузить новые результаты тестов в базу
        /// </summary>
        /// <param name="path">Путь до файлов с результатами. Что-то типа такого: d:\Public\Test_Phillipsa\</param>
        /// <param name="passingTime">Дата и время прохождения теста</param>
        public void AddNewResults(string path, DateTime passingTime)
        {
            AddNewInfos(path, passingTime);

            SaveDatabase();
        }

        public string GetLastPathPart(string path)
        {
            path = path.TrimEnd('\\');
            return path.Substring(path.LastIndexOf('\\') + 1);
        }

        private void AddNewInfos(string path, DateTime passingTime)
        {
            string testName = GetLastPathPart(path);

            foreach (string classFolderPath in Directory.GetDirectories(path))
            {
                string className = GetLastPathPart(classFolderPath);

                foreach (string pupilFilePath in Directory.GetFiles(classFolderPath, "*.txt"))
                {
                    string pupilFileName = GetLastPathPart(pupilFilePath);

                    int spaceIndex = pupilFileName.IndexOf(' ');
                    int pointIndex = pupilFileName.IndexOf('.');
                    string lastName = pupilFileName.Substring(0, spaceIndex);
                    string firstName = pupilFileName.Substring(spaceIndex + 1, pointIndex - spaceIndex - 1);
                    string result = File.ReadAllText(pupilFilePath);

                    int classId = _classEngine.GetClassId(className);
                    int pupilId = _pupilEngine.GetPupilId(firstName, lastName);
                    var classInfo = new ClassInfo(classId, className, string.Empty);
                    var pupilInfo = new PupilInfo(pupilId, firstName, lastName, classId);
                    AnxietyResults anxietyResults = _anxietyTestResultMaker.MakeResults(result);
                    var testResultInfo = new TestResultInfo(_testResultEngine.GetNextTestResultId(), testName, pupilId, result, passingTime, _anxietyTestResultMaker.DoesHighAnxietyExist(anxietyResults), classId, string.Empty);

                    _classEngine.AddClassInfoSorted(classInfo);
                    _pupilEngine.AddPupilInfoSorted(pupilInfo);
                    _testResultEngine.AddTestResultInfoSorted(testResultInfo);
                }
            }
        }

        private void SaveDatabase()
        {
            _classEngine.SaveClass();
            _pupilEngine.SavePupils();
            _testResultEngine.SaveTestResults();
        }

        /// <summary>
        /// Считать данных из файлов нашей БД 
        /// </summary>
        public void LoadDatabase()
        {
            _classEngine.LoadClass();
            _pupilEngine.LoadPupils();
            _testResultEngine.LoadTestResults();
        }
    }
}
