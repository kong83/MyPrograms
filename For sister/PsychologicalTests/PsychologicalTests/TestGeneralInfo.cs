namespace PsychologicalTests
{
    public class TestGeneralInfo
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string ClassInfo { get; private set; }
        public string TestName { get; private set; }
        public string TestSavePath { get; private set; }

        public TestGeneralInfo(string name, string lastName, string classInfo, string testName, string testSavePath)
        {
            FirstName = name;
            LastName = lastName;
            ClassInfo = classInfo;
            TestName = testName;
            TestSavePath = testSavePath;
        }
    }
}