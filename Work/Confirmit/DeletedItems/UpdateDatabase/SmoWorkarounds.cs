using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;

namespace UpdateDatabase
{
    /// <summary>
    /// Unfortunately, SqlAssembly does not implements ICreatable.
    /// So, to save common workflow I'v implement it.
    /// </summary>
    class SqlAssemblyEx : ICreatable
    {
        readonly SqlAssembly m_Assembly;
        readonly string m_AssemblyFileName;

        public SqlAssemblyEx(
            SqlAssembly assembly,
            string assemblyFileName)
        {
            m_Assembly = assembly;
            m_AssemblyFileName = assemblyFileName;
        }

        public void Create()
        {
            m_Assembly.Create(new[] { m_AssemblyFileName });
        }
    }

}
