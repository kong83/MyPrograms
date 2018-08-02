using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadConnector
{
    class Program
    {
        static void Main()
        {
            new Program().StartProgram();
        }

        private const int ArrayCount = 5;
        private Random _random;

        private void StartProgram()
        {
            try
            {
                CheckSqlConnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return;
            }

            _random = new Random();
            for (int i = 0; i < ArrayCount; i++)
            {
                Task.Factory.StartNew(Thread30Sec);                
                Thread.Sleep(500);
            }

            for (int i = 0; i < ArrayCount; i++)
            {
                Task.Factory.StartNew(Thread1Min);
                Thread.Sleep(500);
            }

            for (int i = 0; i < ArrayCount; i++)
            {
                Task.Factory.StartNew(Thread5Min);
                Thread.Sleep(500);
            }

            while (true)
            {
                Thread.Sleep(1000);
            }
        }

        private void CheckSqlConnection()
        {
            using (var cn = new SqlConnection(CreateConnectionString()))
            {
                cn.Open();
            }
        }

        private void Thread30Sec()
        {
            try
            {
                while (true)
                {
                    int randValue = _random.Next(1000);

                    using (var cn = new SqlConnection(CreateConnectionString()))
                    using (var cmd = new SqlCommand("insert into TestTable VALUES (" + randValue + ")", cn))
                    {
                        cn.Open();

                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();

                        Console.WriteLine(randValue + " was inserted into TestTable");
                    }

                    Thread.Sleep(30000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection is wrong. See details:\r\n" + ex.Message);
            }
        }

        private void Thread1Min()
        {
            try
            {
                while (true)
                {
                    using (var cn = new SqlConnection(CreateConnectionString()))
                    using (var cmd = new SqlCommand("select count(*) from TestTable", cn))
                    {
                        cn.Open();

                        cmd.CommandType = CommandType.Text;
                        var count = (int)cmd.ExecuteScalar();

                        Console.WriteLine("TestTable contains " + count + " rows on this moment");
                    }

                    Thread.Sleep(60000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection is wrong. See details:\r\n" + ex.Message);
            }
        }

        private void Thread5Min()
        {
            try
            {
                while (true)
                {
                    var values = new StringBuilder();
                    using (var cn = new SqlConnection(CreateConnectionString()))
                    using (var cmd = new SqlCommand("select * from TestTable", cn))
                    {
                        cn.Open();

                        cmd.CommandType = CommandType.Text;
                        SqlDataReader dataReader = cmd.ExecuteReader();

                        while (dataReader.Read())
                        {
                            values.Append(((int)dataReader["id"]).ToString(CultureInfo.InvariantCulture) + ", ");
                        }

                        string resValue;
                        if (values.Length > 30)
                        {
                            resValue = "..." + values.ToString().Substring(values.Length - 30, 28);
                        }
                        else
                        {
                            resValue = values.ToString();
                        }

                        Console.WriteLine("TestTable contains the following values: " + resValue);
                    }

                    Thread.Sleep(300000);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection is wrong. See details:\r\n" + ex.Message);
            }
        }

        /// <summary>
        /// Create connection string for current server settings
        /// </summary>
        /// <returns></returns>
        private string CreateConnectionString()
        {
            var cnStringBuilder = new SqlConnectionStringBuilder
            {
                DataSource = ".",
                InitialCatalog = "TestDatabase_12321",
                IntegratedSecurity = true
            };

            return cnStringBuilder.ToString();
        }
    }
}
