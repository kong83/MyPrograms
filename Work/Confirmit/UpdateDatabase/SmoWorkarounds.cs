using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace UpdateDatabase
{
    /// <summary>
    /// Unfortunately, SqlAssembly does not implements ICreatable.
    /// So, to save common workflow I'v implement it.
    /// </summary>
    public class SqlAssemblyEx : ICreatable
    {
        private readonly SqlAssembly mAssembly;
        private readonly string mAssemblyFileName;

        public SqlAssemblyEx(
            SqlAssembly assembly,
            string assemblyFileName)
        {
            mAssembly = assembly;
            mAssemblyFileName = assemblyFileName;
        }

        public void Create()
        {
            mAssembly.Create(new[] { mAssemblyFileName });
        }
    }
}