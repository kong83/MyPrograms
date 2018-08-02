namespace LtuErrorsParser
{
    public class EnumClass
    {
        public string Test1 ;

        public EnumClass(string test1)
        {
            Test1 = test1;
        }
    }

    public class TestClass
    {
        EnumClass _enumClass;

        public TestClass(EnumClass enumClass)
        {
            _enumClass = enumClass;
        }

        public string GetTest1()
        {
            return _enumClass.Test1;
        }

        public void ChangeTest1(string newvalue)
        {
            _enumClass.Test1 = newvalue;
        }
    }

    public class TestClass1
    {
        EnumClass _enumClass;

        public TestClass1(EnumClass enumClass)
        {
            _enumClass = enumClass;
        }

        public string GetTest1()
        {
            return _enumClass.Test1;
        }
    }
}