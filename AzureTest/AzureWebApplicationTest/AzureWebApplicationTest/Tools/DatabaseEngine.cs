using System.Collections.Generic;
using System.Data.SqlClient;

namespace AzureWebApplicationTest.Tools
{
    public class DatabaseEngine
    {
        public string GetConnectionString()
        {
            return "Server=tcp:azurewebapptestdb.database.windows.net,1433;Initial Catalog=AzureWebAppTestDB;Persist Security Info=False;User ID=grigoryk;Password=CdjzBuhf1Kjrjvjnbd;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        }

        public void AddValue(string value)
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                var actualCallStates = new List<int>();
                connection.Open();

                var checkCallStateCommand = new SqlCommand($"INSERT INTO [TestTable] VALUES ('{value}') ", connection);

                checkCallStateCommand.ExecuteNonQuery();
            }
        }

        public string GetValue(int id)
        {
            using (var connection = new SqlConnection(GetConnectionString()))
            {
                var actualCallStates = new List<int>();
                connection.Open();

                var checkCallStateCommand = new SqlCommand($"SELECT Value FROM [TestTable] WHERE Id={id}", connection);

                var result = checkCallStateCommand.ExecuteScalar();

                if (result == null)
                {
                    return "Unknown id";
                }

                return result.ToString();
            }
        }
    }
}

