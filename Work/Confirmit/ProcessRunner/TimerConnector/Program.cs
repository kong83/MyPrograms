using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Text;
using System.Threading;

namespace TimerConnector
{
    class Program
    {
        static void Main()
        {
            new Program().StartProgram();
        }

        private const int ArrayCount = 5;
        private Random _random;
        private Timer[] _30SecTimers;
        private Timer[] _1MinTimers;
        private Timer[] _5MinTimers;

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
            _30SecTimers = new Timer[ArrayCount];
            int dueTime = 0;
            for (int i = 0; i < ArrayCount; i++)
            {
                _30SecTimers[i] = new Timer(Timer30SecTick);
                _30SecTimers[i].Change(dueTime, 30000);
                dueTime += 500;
            }

            _1MinTimers = new Timer[ArrayCount];
            for (int i = 0; i < ArrayCount; i++)
            {
                _1MinTimers[i] = new Timer(Timer1MinTick);
                _1MinTimers[i].Change(dueTime, 60000);
                dueTime += 500;
            }

            _5MinTimers = new Timer[ArrayCount];
            for (int i = 0; i < ArrayCount; i++)
            {
                _5MinTimers[i] = new Timer(Timer5MinTick);
                _5MinTimers[i].Change(dueTime, 300000);
                dueTime += 500;
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

        private void Timer30SecTick(object state)
        {
            try
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
            }
            catch (Exception ex)
            {
               Console.WriteLine("Connection is wrong. See details:\r\n" + ex.Message);
            }
        }

        private void Timer1MinTick(object state)
        {
            try
            {
                using (var cn = new SqlConnection(CreateConnectionString()))
                using (var cmd = new SqlCommand("select count(*) from TestTable", cn))
                {
                    cn.Open();

                    cmd.CommandType = CommandType.Text;
                    var count = (int)cmd.ExecuteScalar();

                    Console.WriteLine("TestTable contains " + count + " rows on this moment");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection is wrong. See details:\r\n" + ex.Message);
            }
        }

        private void Timer5MinTick(object state)
        {
            try
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
