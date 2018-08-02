using Confirmit.CATI.Core.DAL.Generated.Cache;

namespace TypemockBug
{
    class Program
    {        
        static void Main()
        {
            BvSurveyCache.Instance.OnTableChanged();
        }
    }
}
