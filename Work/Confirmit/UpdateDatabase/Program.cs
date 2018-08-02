using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace UpdateDatabase
{
    public class Program
    {
        private static void PrintHelp()
        {
            const string help = "usage:  updatedatabase.exe <command name>\r\n" +
                                "Commands:\r\n" +
                                "  generatebackup\r\n" +
                                "  generateupdate <production database name> | *\r\n" +
                                "  addnonsplitobjects <production database name> | *\r\n" +
                                "\r\n";

            Console.WriteLine(help);
        }

        private static void GenerateBackup()
        {
            //
            // Prepare SQL Script 
            //
            var generatedSqlScript = new StringBuilder();

            foreach (string sqlScriptPath in Configuration.Default.BackupContent_SqlScriptFiles)
            {
                using (var sqlScriptFile = new StreamReader(Environment.ExpandEnvironmentVariables(sqlScriptPath)))
                {
                    string sqlScript = sqlScriptFile.ReadToEnd();

                    generatedSqlScript.Append(
                        sqlScript.Replace(
                            "#SURVEYSID#",
                            string.Empty));
                    generatedSqlScript.Append("\r\nGO\r\n");
                }
            }

            //
            // Script created, we're save it to the file.
            // Just to have a copy.
            //
            string outSqlScriptFilePath =
                    PathsEngine.ConvertRelativePath2RootedPathIfNeeded(
                        Path.ChangeExtension(
                            Environment.ExpandEnvironmentVariables(
                                Configuration.Default.SourceDatabaseBackupFilePath),
                            "sql"));

            using (var outSqlScriptFile = new StreamWriter(outSqlScriptFilePath))
            {
                outSqlScriptFile.Write(generatedSqlScript.ToString());
            }

            var databaseEngine = new DatabaseEngine();

            databaseEngine.CreateDatabaseFromScriptFile(
                Configuration.Default.SourceDatabaseName,
                outSqlScriptFilePath,
                Configuration.Default.BackupContent_ClrAssembyFiles);

            //
            // Now lets generate backup
            //
            string outSqlBackupFilePath =
                PathsEngine.ConvertRelativePath2RootedPathIfNeeded(
                    Environment.ExpandEnvironmentVariables(
                        Configuration.Default.SourceDatabaseBackupFilePath));

            databaseEngine.CreateBackup(
                Configuration.Default.SourceDatabaseName,
                outSqlBackupFilePath);
        }

        private static void SaveScriptText(
            string scriptText,
            string scriptName)
        {
            string generatedScriptPath = Path.Combine(Configuration.Default.OutputFolder, scriptName);

            using (var generatedScriptFile = new StreamWriter(generatedScriptPath))
            {
                generatedScriptFile.Write(scriptText);
            }
        }


        /// <summary>
        /// Run external pre and post script
        /// </summary>
        /// <param name="dbEngine">DB Engine object.</param>
        /// <param name="databaseName">The database Name.</param>
        /// <param name="externalUpdateScriptPath">The external Update Script Path.</param>
        private static void RunExternalScript(
            DatabaseEngine dbEngine,
            string databaseName,
            string externalUpdateScriptPath)
        {
            Trace.TraceInformation(
                "Execute external script {0} for the database {1}...\r\n",
                externalUpdateScriptPath,
                databaseName);

            using (var sqlScriptFile = new StreamReader(externalUpdateScriptPath))
            {
                string externalSqlScript = string.Format(
                    "USE {0}\r\nGO\r\n",
                    databaseName);

                externalSqlScript += sqlScriptFile.ReadToEnd();

                externalSqlScript = externalSqlScript.Replace(
                    "$__CURRENT_DATABASE_NAME__",
                    databaseName);

                externalSqlScript = externalSqlScript.Replace("$__IS_DEFAULT_DATABASE__", string.Compare(databaseName, Configuration.Default.DefaultDatabaseName, true) == 0 ? "1" : "0");

                dbEngine.ExecuteBatch(
                    externalSqlScript);
            }
        }


        private static void GenerateUpdate(
            string productionDatabaseName)
        {
            //
            // Restore so called source database.
            //
            var dbEngine = new DatabaseEngine();

            Trace.TraceInformation("Restore {0} database...\r\n", Configuration.Default.SourceDatabaseName);

            string sourceDatabaseBackupFilePath =
                PathsEngine.CorrectBackupPathAccordingToConfig(
                    PathsEngine.ConvertRelativePath2RootedPathIfNeeded(
                        Configuration.Default.SourceDatabaseBackupFilePath));

            dbEngine.CreateDatabaseFromBackupFile(
                Configuration.Default.SourceDatabaseName,
                sourceDatabaseBackupFilePath);

            if (Configuration.Default.IsTestModeEnabled && productionDatabaseName != "*")
            {
                //
                // Restore production database from the backup file.
                // Needed just to simplify testing.
                //
                Trace.TraceInformation("Restore {0} database...\r\n", productionDatabaseName);

                string productionDatabaseBackupFilePath =
                    PathsEngine.CorrectBackupPathAccordingToConfig(
                        PathsEngine.ConvertRelativePath2RootedPathIfNeeded(
                            Configuration.Default.ProductionDatabaseBackupFilePath));

                dbEngine.CreateDatabaseFromBackupFile(
                    productionDatabaseName,
                    productionDatabaseBackupFilePath);
            }

            var scriptGenerator = new UpgradeScriptGenerator();

            DateTime now = DateTime.Now;
            int numberDB = 1;

            foreach (string databaseName in dbEngine.GetDatabasesForUpgrade(productionDatabaseName))
            {
                if (numberDB % 5 == 0)
                {
                    scriptGenerator = new UpgradeScriptGenerator();
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    GC.Collect();
                }

                string outputFolder = string.Format(
                    "Output\\{0}\\{1}",
                    now.ToString("yyyy.MM.dd HH.mm.ss"),
                    databaseName);

                Configuration.Default.OutputFolder = Path.Combine(
                    Configuration.Default.Path,
                    outputFolder);

                //
                // Backup production database. For the case,
                // If update will do something wrong, then we can restore
                // original database from the backup.
                //
                Trace.TraceInformation("Backup production database {0}...\r\n", databaseName);

                dbEngine.CreateBackup(
                    databaseName,
                    PathsEngine.CorrectBackupPathAccordingToConfig(
                        Path.Combine(
                            Configuration.Default.OutputFolder,
                            databaseName + ".bak")));

                string dropScriptText;
                string createScriptText;
                string alterScriptText;

                scriptGenerator.GenerateUpgradeScript(
                    databaseName,
                    out dropScriptText,
                    out createScriptText,
                    out alterScriptText);

                SaveScriptText(
                    dropScriptText,
                    "DROP.sql");

                SaveScriptText(
                    createScriptText,
                    "CREATE.sql");

                SaveScriptText(
                   alterScriptText,
                   "UPDATE.sql");

                numberDB++;
            } // foreach (string databaseName in dbEngine.GetDatabasesForUpgrade(productionDatabaseName))

            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            if (Configuration.Default.ExecuteUpdateScript)
            {
                Console.WriteLine(
                    "All scripts for update production databases was generated.\r\n" +
                    "Press Y to continue or N to exit");

                ConsoleKeyInfo key = Console.ReadKey();
                Console.WriteLine();

                if (key.KeyChar != 'y' && key.KeyChar != 'Y')
                {
                    return;
                }

                foreach (string databaseName in dbEngine.GetDatabasesForUpgrade(productionDatabaseName))
                {
                    if (string.IsNullOrEmpty(Configuration.Default.ExternalPreUpdateScriptPath) == false)
                    {
                        Trace.TraceInformation("Execute PRE script for the database {0}...\r\n", databaseName);

                        string externalUpdateScriptPath = PathsEngine.ConvertRelativePath2RootedPathIfNeeded(
                            Configuration.Default.ExternalPreUpdateScriptPath);

                        RunExternalScript(dbEngine, databaseName, externalUpdateScriptPath);
                    }

                    string outputFolder = string.Format(
                        "Output\\{0}\\{1}",
                        now.ToString("yyyy.MM.dd HH.mm.ss"),
                        databaseName);

                    string dropScriptText;
                    using (var sr = new StreamReader(outputFolder + @"\DROP.sql"))
                    {
                        dropScriptText = sr.ReadToEnd();
                    }

                    Trace.TraceInformation("Execute DROP script for the database {0}...\r\n", databaseName);

                    dbEngine.ExecuteBatch(
                        dropScriptText);

                    string alterScriptText;
                    using (var sr = new StreamReader(outputFolder + @"\UPDATE.sql"))
                    {
                        alterScriptText = sr.ReadToEnd();
                    }

                    Trace.TraceInformation("Execute ALTER script for the database {0}...\r\n", databaseName);

                    dbEngine.ExecuteBatch(
                        alterScriptText);

                    string createScriptText;
                    using (var sr = new StreamReader(outputFolder + @"\CREATE.sql"))
                    {
                        createScriptText = sr.ReadToEnd();
                    }

                    Trace.TraceInformation("Execute CREATE script for the database {0}...\r\n", databaseName);

                    dbEngine.ExecuteBatch(
                        createScriptText);

                    if (string.IsNullOrEmpty(Configuration.Default.ExternalPostUpdateScriptPath) == false)
                    {
                        Trace.TraceInformation("Execute POST script for the database {0}...\r\n", databaseName);

                        string externalUpdateScriptPath = PathsEngine.ConvertRelativePath2RootedPathIfNeeded(
                            Configuration.Default.ExternalPostUpdateScriptPath);

                        RunExternalScript(dbEngine, databaseName, externalUpdateScriptPath);
                    }
                } // foreach (string databaseName in dbEngine.GetDatabasesForUpgrade(productionDatabaseName))
            } // if (Configuration.Default.ExecuteUpdateScript == true)


            //
            // All databases successfully upgraded, now we need:
            // 1. Create new backup file if needed
            // 2. Drop source "upgrade" database created by utility
            //
            if (Configuration.Default.UpdateDefaultDatabaseBackup)
            {
                Trace.TraceInformation(
                    "Backuping default database {0}, path {1}...\r\n",
                    Configuration.Default.DefaultDatabaseName,
                    Configuration.Default.DefaultDatabaseBackupFilePath);

                dbEngine.CreateBackup(
                    Configuration.Default.DefaultDatabaseName,
                    Configuration.Default.DefaultDatabaseBackupFilePath);
            }

            dbEngine.DropDatabase(
                Configuration.Default.SourceDatabaseName);
        }


        /// <summary>
        /// Add non split objects to databases
        /// </summary>
        /// <param name="productionDatabaseName">Production Database Name.</param>
        private static void AddNonSplitObjects(string productionDatabaseName)
        {
            //
            // Restore so called source database.
            //
            var dbEngine = new DatabaseEngine();

            DateTime now = DateTime.Now;

            foreach (string databaseName in dbEngine.GetDatabasesForUpgrade(productionDatabaseName))
            {
                var currentDBEngine = new DatabaseEngine(databaseName);

                string outputFolder = string.Format(
                   "Output\\{0}\\{1}",
                   now.ToString("yyyy.MM.dd HH.mm.ss"),
                   databaseName);

                Configuration.Default.OutputFolder = Path.Combine(
                    Configuration.Default.Path,
                    outputFolder);

                //
                // Backup production database. For the case,
                // If update will do something wrong, then we can restore
                // original database from the backup.
                //
                Trace.TraceInformation("Backup production database {0}...\r\n", databaseName);

                currentDBEngine.CreateBackup(
                    databaseName,
                    PathsEngine.CorrectBackupPathAccordingToConfig(
                        Path.Combine(
                            Configuration.Default.OutputFolder,
                            databaseName + ".bak")));

                string proceduresPath = Environment.ExpandEnvironmentVariables(@"%PROJPATH%\UNITS\bv7\SQL\PROCEDURES\");
                foreach (string currentOriginalProcedureName in Configuration.Default.SplitProceduresList)
                {
                    string procedurePath = proceduresPath + currentOriginalProcedureName + ".sql";

                    Trace.TraceInformation("Create procedure {0} for the database {1}...\r\n", currentOriginalProcedureName, databaseName);
                    using (var sr = new StreamReader(procedurePath))
                    {
                        try
                        {
                            currentDBEngine.ExecuteBatch(sr.ReadToEnd().Replace("#SURVEYSID#", string.Empty));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            Console.ReadKey();
                        }
                    }
                }

                string tablesPath = Environment.ExpandEnvironmentVariables(@"%PROJPATH%\UNITS\bv7\SQL\TABLES\");
                foreach (string currentTableNames in Configuration.Default.SplitTablesList)
                {
                    string currentOriginalTableName = currentTableNames.Split(new[] { ';' })[0];
                    string tablePath = tablesPath + currentOriginalTableName + ".sql";

                    Trace.TraceInformation("Create table {0} for the database {1}...\r\n", currentOriginalTableName, databaseName);
                    using (var sr = new StreamReader(tablePath))
                    {
                        try
                        {
                            currentDBEngine.ExecuteBatch(sr.ReadToEnd().Replace("#SURVEYSID#", string.Empty));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            Console.ReadKey();
                        }
                    }
                }

                // Add three triggers for BvInterview
                var triggersPath = new[]
                {
                    Environment.ExpandEnvironmentVariables(
                        @"%PROJPATH%\UNITS\bv7\SQL\PROCEDURES\BvTrInterview_InterviewsDelete.sql"),
                    Environment.ExpandEnvironmentVariables(
                        @"%PROJPATH%\UNITS\bv7\SQL\PROCEDURES\BvTrInterview_InterviewsInsert.sql"),
                    Environment.ExpandEnvironmentVariables(
                        @"%PROJPATH%\UNITS\bv7\SQL\PROCEDURES\BvTrInterview_InterviewsUpdate.sql")
                };
                foreach (string triggerPath in triggersPath)
                {
                    if (!File.Exists(triggerPath))
                    {
                        continue;
                    }

                    Trace.TraceInformation("Create trigger {0} for table BvInterview...\r\n", triggerPath);
                    using (var sr = new StreamReader(triggerPath))
                    {
                        try
                        {
                            currentDBEngine.ExecuteBatch(sr.ReadToEnd().Replace("#SURVEYSID#", string.Empty));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            Console.ReadKey();
                        }
                    }
                }
            }
        }


        private static int Main(string[] args)
        {
            try
            {
                //
                // Check input parameters.
                //
                if (args.Length == 0)
                {
                    PrintHelp();
                    return 1;
                }

                string command = args[0];

                if (string.Compare(command, "addnonsplitobjects", true) == 0)
                {
                    if (args.Length != 2)
                    {
                        PrintHelp();
                        return 1;
                    }

                    string productionDatabaseName = args[1];

                    AddNonSplitObjects(productionDatabaseName);
                }
                else
                {
                    if (string.Compare(command, "generatebackup", true) == 0)
                    {
                        GenerateBackup();
                    }
                    else if (string.Compare(command, "generateupdate", true) == 0)
                    {
                        if (args.Length != 2)
                        {
                            PrintHelp();
                            return 1;
                        }

                        string productionDatabaseName = args[1];

                        var dbEngine = new DatabaseEngine();

                        if (dbEngine.IsDatabaseExists(Configuration.Default.DefaultDatabaseName) == false)
                        {
                            Console.WriteLine(
                                "Default database {0} does not exists or access denied",
                                Configuration.Default.DefaultDatabaseName);

                            return 1;
                        }

                        ConsoleKeyInfo key;

                        if (productionDatabaseName == "*")
                        {
                            //
                            // Update all databases by name pattern mode.
                            //
                            if (Configuration.Default.IsTestModeEnabled)
                            {
                                Console.WriteLine(
                                    "Test mode cannot be used with '*' as database name.");

                                return 1;
                            }
                        }
                        else
                        {
                            //
                            // Update single database mode.
                            //
                            if (Configuration.Default.IsTestModeEnabled)
                            {
                                Console.WriteLine(
                                    "UpdateDatabase.exe started in the test mode.\r\n" +
                                    "Test mode drops and creates production database ({0}) from the backup file.\r\n" +
                                    "Are you sure you want use test mode and drop database {0}?\r\n" +
                                    "Press Y to continue or N to exit",
                                    productionDatabaseName);

                                key = Console.ReadKey();

                                if ((key.KeyChar != 'y') && (key.KeyChar != 'Y'))
                                {
                                    return 1;
                                }

                                Console.WriteLine();
                            }

                            if (dbEngine.IsDatabaseExists(productionDatabaseName) == false)
                            {
                                Console.WriteLine(
                                    "Database {0} does not exists or access denied",
                                    productionDatabaseName);

                                return 1;
                            }
                        } // if (productionDatabaseName == "*")


                        var confirmation = new StringBuilder();

                        confirmation.Append(
                            "Following databases will be updated, are you sure?\r\n" +
                            "Press Y to continue or N to exit\r\n");

                        foreach (string databaseFoUpdate in dbEngine.GetDatabasesForUpgrade(productionDatabaseName))
                        {
                            confirmation.AppendFormat("* {0}\r\n", databaseFoUpdate);
                        }

                        Console.Write(confirmation.ToString());

                        key = Console.ReadKey();

                        if ((key.KeyChar != 'y') && (key.KeyChar != 'Y'))
                        {
                            return 1;
                        }

                        Console.WriteLine();

                        if (Configuration.Default.UpdateDefaultDatabaseBackup)
                        {
                            Console.Write(
                                "\r\n" +
                                "Database utility will update default database backup file. " +
                                "So you don't need update it manually. " +
                                "All newly created instances will use new, updated database.\r\n" +
                                "Default database name: {0}\r\n" +
                                "Default database backup file path: {1}\r\n" +
                                "\r\n" +
                                "Please check settings are correct.\r\n" +
                                "If something is wrong press N, correct configuration file and restart utility.\r\n" +
                                "\r\n" +
                                "Would you like to continue?\r\n" +
                                "Press Y to continue or N to exit\r\n",
                                Configuration.Default.DefaultDatabaseName,
                                Configuration.Default.DefaultDatabaseBackupFilePath);

                            key = Console.ReadKey();

                            if ((key.KeyChar != 'y') && (key.KeyChar != 'Y'))
                            {
                                return 1;
                            }

                            Console.WriteLine();
                        }
                        else
                        {
                            Console.Write(
                                "\r\n" +
                                "Default database name: {0}\r\n" +
                                "\r\n" +
                                "Please check settings are correct.\r\n" +
                                "If something is wrong press N, correct configuration file and restart utility.\r\n" +
                                "\r\n" +
                                "Would you like to continue?\r\n" +
                                "Press Y to continue or N to exit\r\n",
                                Configuration.Default.DefaultDatabaseName);

                            key = Console.ReadKey();

                            if ((key.KeyChar != 'y') && (key.KeyChar != 'Y'))
                            {
                                return 1;
                            }

                            Console.WriteLine();
                        }

                        GenerateUpdate(productionDatabaseName);
                    }
                    else
                    {
                        Console.WriteLine("Unknown command {0}", command);
                        PrintHelp();
                        return 1;
                    }
                }

                return 0;
            }
            catch (UpdateDatabaseException ex)
            {
                Trace.TraceError(
                    "Program trew this exception itself:\n" +
                    ex);

                return 1;
            }
            catch (Exception ex)
            {
                Trace.TraceError(
                    "Program had unknown error:\n" +
                    ex);

                return 1;
            }
        }
    }
}