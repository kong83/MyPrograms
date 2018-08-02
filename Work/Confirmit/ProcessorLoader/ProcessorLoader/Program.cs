using System;
using System.Threading;
using System.Windows.Forms;

namespace ProcessorLoader
{
    public class Program
    {
        private readonly DatabaseEngine dbEngine = new DatabaseEngine(TestDatabaseName);

        private const string TestDatabaseName = "TestDatabase486453";

        private int threadCount;
        private int stopVariable;
        private int sleepVariable;

        private static void Main()
        {
            Console.WriteLine("This program will be to hang sql and processor");            
            try
            {
                new Program().StartProgram();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private static void ShowHelp()
        {
            Console.WriteLine("\r\nPrint \"stop\" to stop work (don't close window)");
            Console.WriteLine("Print \"sleep n\" to sleep all threads during n sec");
            Console.WriteLine("Print \"up\" to up prority");
            Console.WriteLine("Print \"down\" to down priority");
            Console.WriteLine("Print \"help\" to show this information\r\n");
        }

        private static void DecreasePriority(Thread thread)
        {
            switch (thread.Priority)
            {
                case ThreadPriority.BelowNormal:
                    thread.Priority = ThreadPriority.Lowest;
                    break;
                case ThreadPriority.Normal:
                    thread.Priority = ThreadPriority.BelowNormal;
                    break;
                case ThreadPriority.AboveNormal:
                    thread.Priority = ThreadPriority.Normal;
                    break;
                case ThreadPriority.Highest:
                    thread.Priority = ThreadPriority.AboveNormal;
                    break;
            }
        }

        private static void IncreasePriority(Thread thread)
        {
            switch (thread.Priority)
            {
                case ThreadPriority.Lowest:
                    thread.Priority = ThreadPriority.BelowNormal;
                    break;
                case ThreadPriority.BelowNormal:
                    thread.Priority = ThreadPriority.Normal;
                    break;
                case ThreadPriority.Normal:
                    thread.Priority = ThreadPriority.AboveNormal;
                    break;
                case ThreadPriority.AboveNormal:
                    thread.Priority = ThreadPriority.Highest;
                    break;
            }
        }

        private static ThreadPriority SelectPriority()
        {
            Console.WriteLine("Select thread priority:");
            Console.WriteLine("1 - Lowest");
            Console.WriteLine("2 - BelowNormal");
            Console.WriteLine("3 - Normal");
            Console.WriteLine("4 - AboveNormal");
            Console.WriteLine("5 - Highest (not recommend :) )");
            Console.WriteLine("stop - stop work");
            string threadPriorityString = Console.ReadLine();

            ThreadPriority threadPriority = ThreadPriority.Normal;
            switch (threadPriorityString)
            {
                case "1":
                    threadPriority = ThreadPriority.Lowest;
                    break;
                case "2":
                    threadPriority = ThreadPriority.BelowNormal;
                    break;
                case "4":
                    threadPriority = ThreadPriority.AboveNormal;
                    break;
                case "5":
                    threadPriority = ThreadPriority.Highest;
                    break;
                case "stop":
                    Environment.Exit(0);
                    break;
            }

            Console.WriteLine("Selected priority for threads: " + threadPriority);

            return threadPriority;
        }

        private static int SelectThreadsCount()
        {
            Console.WriteLine("Print count of threads (from 2 to 20):");
            Console.WriteLine("The first half of threads will be to hang a SQL, the second half will be to hang a processor.");
            string threadsCountString = Console.ReadLine();
            int threadsCount;
            if (!int.TryParse(threadsCountString, out threadsCount) ||
                threadsCount < 2 || threadsCount > 20)
            {
                threadsCount = 10;
                Console.WriteLine("Count of threads is " + threadsCount);
            }

            return threadsCount;
        }

        private void StartProgram()
        {
            stopVariable = 0;
            sleepVariable = 0;

            threadCount = SelectThreadsCount();
            ThreadPriority threadPriority = SelectPriority();

            var threads = new Thread[threadCount];

            for (int i = 0; i < threadCount / 2; i++)
            {
                threads[i] = new Thread(HangSQL)
                {
                    Priority = threadPriority
                };
                threads[i].Start();
            }

            for (int i = threadCount / 2; i < threadCount; i++)
            {
                threads[i] = new Thread(HangProcessor)
                {
                    Priority = threadPriority
                };
                threads[i].Start();
            }

            Thread.Sleep(300);
            ShowHelp();

            string command = string.Empty;

            while (command != "stop")
            {
                command = Console.ReadLine() ?? string.Empty;
                command = command.ToLower();

                if (command.StartsWith("sleep"))
                {
                    int timeoutValue;
                    string[] parseCommand = command.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parseCommand.Length == 1 || !int.TryParse(parseCommand[1], out timeoutValue))
                    {
                        timeoutValue = 1;
                    }

                    sleepVariable = 1;
                    Console.WriteLine(string.Format("Start sleeping {0} sec", timeoutValue));
                    Thread.Sleep(timeoutValue * 1000);
                    Console.WriteLine("Stop sleeping");
                    sleepVariable = 0;
                }
                else
                {
                    switch (command)
                    {
                        case "up":
                            for (int i = 0; i < threadCount; i++)
                            {
                                IncreasePriority(threads[i]);
                            }

                            Console.WriteLine("New priority for threads: " + threads[0].Priority);
                            break;
                        case "down":
                            for (int i = 0; i < threadCount; i++)
                            {
                                DecreasePriority(threads[i]);
                            }

                            Console.WriteLine("New priority for threads: " + threads[0].Priority);
                            break;
                        case "help":
                            ShowHelp();
                            break;
                        case "stop":
                            Console.WriteLine("Stopping threads...");
                            break;
                        default:
                            Console.WriteLine("Unknown command");
                            break;
                    }
                }
            }

            stopVariable = 1;
            Thread.Sleep(1000);
            for (int i = 0; i < threadCount; i++)
            {
                if (threads[i].IsAlive)
                {
                    threads[i].Abort();
                }
            }

            dbEngine.DropDatabase(TestDatabaseName);
        }


        private void HangProcessor()
        {
            Console.WriteLine("thread was started");

            double tempResult;
            var random = new Random();
            while (true)
            {
                double firstNumber = random.NextDouble();
                double secondNumber = random.NextDouble();
                tempResult = firstNumber * secondNumber;
                if (stopVariable > 0)
                {
                    break;
                }

                if (sleepVariable > 0)
                {
                    Thread.Sleep(100);
                }
            }

            if (tempResult >= 0)
            {
                Console.WriteLine("thread was stoped");
            }
        }

        private void HangSQL()
        {
            Console.WriteLine("thread was started");

            var randomValue = new Random();
            while (true)
            {
                this.dbEngine.ExecuteNonQuery(
                    string.Format(
                        "insert into [TestTable] " +
                        "([column2], [column3],[column4],[column5],[column6],[column7],[column8], " +
                        "[column9], [column10])" +
                        "values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}')",
                        randomValue.Next(1, 10000),
                        randomValue.Next(1, 10000),
                        randomValue.Next(1, 10000),
                        randomValue.Next(1, 10000),
                        randomValue.Next(1, 10000),
                        randomValue.Next(1, 10000),
                        randomValue.Next(1, 10000),
                        randomValue.Next(1, 10000),
                        randomValue.Next(1, 10000)));

                if (stopVariable > 0)
                {
                    break;
                }

                if (sleepVariable > 0)
                {
                    Thread.Sleep(100);
                }
            }

            Console.WriteLine("thread was stoped");
        }
    }
}
