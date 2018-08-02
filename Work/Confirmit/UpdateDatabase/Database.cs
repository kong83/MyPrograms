using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Microsoft.SqlServer.Management.Smo;

namespace UpdateDatabase
{
    /// <summary>
    /// Therminology
    ///     Split Table           - table that is created for the every survey.
    ///                             Split Table has 2 names, so colled original 
    ///                             name and template name.
    ///     Template Table Name   - name that is looks like "t46_interviews"
    ///     Original Table Name   - name that is looks like "BvInterview"
    /// </summary>
    public class Database
    {
        private readonly DatabaseEngine mDatabaseEngine;

        public readonly List<int> SurveyIds;

        /// <summary>
        /// Case insensitive replace implementation.
        /// NOTE:
        ///  .NET Framework does not support case insensitive replace.
        /// </summary>
        /// <param name="expr">Expression</param>
        /// <param name="find">String for finding</param>
        /// <param name="repl">String for replacing</param>
        /// <returns></returns>
        private static string ReplaceEx(string expr, string find, string repl)
        {
            // Get input string length
            var exprLen = expr.Length;
            var findLen = find.Length;

            // Check inputs
            if (0 == exprLen || 0 == findLen || findLen > exprLen)
            {
                return expr;
            }

            var sbRet = new StringBuilder(exprLen);
            int pos = 0;

            while (pos + findLen <= exprLen)
            {
                if (pos + findLen <= exprLen)
                {
                    //
                    // Do not replace in case of BvInterviewsNumbers
                    //
                    if (((pos + findLen < exprLen) && (char.IsLetter(expr, pos + findLen) == false)) &&
                        (0 == string.Compare(expr, pos, find, 0, findLen, StringComparison.OrdinalIgnoreCase)))
                    {
                        // Add the replaced string
                        sbRet.Append(repl);
                        pos += findLen;

                        continue;
                    }
                }

                // Advance one character
                sbRet.Append(expr, pos++, 1);
            }


            // Append remaining characters
            sbRet.Append(expr, pos, exprLen - pos);

            // Return string
            return sbRet.ToString();
        }

        public Database(string databaseName)
        {
            this.mDatabaseEngine = new DatabaseEngine(databaseName);

            SurveyIds = GetSurveyIds();
        }

        private List<int> GetSurveyIds()
        {
            var surveySids = this.mDatabaseEngine.ExecuteDataTable<DataTable>(
                "SELECT SID FROM BvSurvey WHERE [Name] <> @TemplateSurveyName",
                CommandType.Text,
                new SqlParameter("@TemplateSurveyName", Configuration.Default.TemplateSurveyName));

            return (from DataRow row in surveySids.Rows select (int)row["SID"]).ToList();
        }

        public string GetSurveyNameBySurveySid(
            int surveySid)
        {
            return this.mDatabaseEngine.ExecuteScalar<string>(
                "SELECT [Name] FROM BvSurvey WHERE SID=@SID",
                CommandType.Text,
                new SqlParameter("@SID", surveySid));
        }

        public string GetInstanceName()
        {
            return this.mDatabaseEngine.ExecuteScalar<string>(
                "SELECT instanceName FROM BvSite",
                CommandType.Text);
        }

        public static bool IsSplitTable(
            string originalTableName)
        {
            return Configuration.Default.SplitTables.Any(splitTable => string.Compare(originalTableName, splitTable.OriginalName, StringComparison.OrdinalIgnoreCase) == 0);
        }

        public static bool IsSplitProcedure(
            string originalProcedureName)
        {
            return Configuration.Default.SplitProceduresList.Cast<string>().Any(currentOriginalProcedureName => string.Compare(originalProcedureName, currentOriginalProcedureName, StringComparison.OrdinalIgnoreCase) == 0);
        }

        public static string GetTemplateTableNameByOriginalTableName(
            string originalName,
            int surveyId)
        {
            foreach (SplitTable splitTable in Configuration.Default.SplitTables)
            {
                if (string.Compare(splitTable.OriginalName, originalName, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    return string.Format(
                        "t{0}_{1}",
                        surveyId,
                        splitTable.TemplateName);
                }
            }

            throw new UpdateDatabaseException(
                string.Format(
                    "Cannot find template table name for the original table {0}, survey SID {1}",
                    originalName,
                    surveyId));
        }

        public static string GetOriginalTableNameByTemplateTableName(
            string templateName)
        {
            return (from splitTable in Configuration.Default.SplitTables
                    where templateName.EndsWith("_" + splitTable.TemplateName, StringComparison.OrdinalIgnoreCase)
                    select splitTable.OriginalName).FirstOrDefault();
        }

        public string GetOriginalProcedureNameByTemplateProcedureName(
            string templateName)
        {
            int n = 4;
            var sid = new StringBuilder();
            while (templateName[n] >= '0' && templateName[n] <= '9')
            {
                sid.Append(templateName[n++]);
            }

            if (string.IsNullOrEmpty(sid.ToString()))
            {
                return null;
            }

            int sidAsDigit = Convert.ToInt32(sid.ToString());
            if (this.SurveyIds.Any(surveyId => sidAsDigit == surveyId))
            {
                return templateName.Remove(4, sid.Length);
            }

            throw new UpdateDatabaseException(
                string.Format(
                    "Stored procedure {0} was found, but survey id {1} was not found",
                    templateName,
                    sid));
        }


        /// <summary>
        /// Used for procedures/triggers/indexes
        /// </summary>
        /// <param name="originalName">The original Name.</param>
        /// <param name="surveyId">The survey Id.</param>
        /// <returns>
        /// </returns>
        public static string GetTemplateNameByOriginalName(
            string originalName,
            int surveyId)
        {
            return
                originalName.Substring(0, 4) +
                surveyId +
                originalName.Substring(4);
        }

        public static string GetTemplateTSQL(
            string tsql,
            int surveyId)
        {
            string templateTSQL = tsql;

            //
            // Replace all original table names with corresponding template table names.
            //
            foreach (SplitTable splitTable in Configuration.Default.SplitTables)
            {
                templateTSQL = ReplaceEx(
                    templateTSQL,
                    splitTable.OriginalName,
                    GetTemplateTableNameByOriginalTableName(splitTable.OriginalName, surveyId));
            }

            //
            // Replace all original stored procedure names with corresponding template procedure names.
            //
            foreach (string originalProcedure in Configuration.Default.SplitProceduresList)
            {
                templateTSQL = ReplaceEx(
                    templateTSQL,
                    originalProcedure,
                    GetTemplateNameByOriginalName(originalProcedure, surveyId));
            }

            return templateTSQL;
        }

        /// <summary>
        /// Get views from indicated database
        /// </summary>
        /// <param name="database">Database</param>
        /// <returns></returns>
        public static Dictionary<string, MySmoObjectBase> GetViewsInfo(Microsoft.SqlServer.Management.Smo.Database database)
        {
            var views = new Dictionary<string, MySmoObjectBase>();

            foreach (View view in database.Views)
            {
                if (view.IsSystemObject == false)
                {
                    views.Add(
                        view.Name, 
                        new MySmoObjectBase(view, view.Name, database.Name, view.TextBody, view.TextHeader));
                }
            }

            return views;
        }


        /// <summary>
        /// Get UserDefinedFunctions from indicated database
        /// </summary>
        /// <param name="database">Database</param>
        /// <returns></returns>
        public static Dictionary<string, MySmoObjectBase> GetUserDefinedFunctionsInfo(Microsoft.SqlServer.Management.Smo.Database database)
        {
            var userDefinedFunctions = new Dictionary<string, MySmoObjectBase>();

            foreach (UserDefinedFunction userDefinedFunction in database.UserDefinedFunctions)
            {
                if (userDefinedFunction.IsSystemObject == false)
                {
                    var mySmoObjectBase = new MySmoObjectBase(
                        userDefinedFunction,
                        userDefinedFunction.Name,
                        database.Name,
                        userDefinedFunction.TextBody,
                        userDefinedFunction.TextHeader);

                    userDefinedFunctions.Add(userDefinedFunction.Name, mySmoObjectBase);
                }
            }

            return userDefinedFunctions;
        }


        /// <summary>
        /// Get CLRAssemblies from indicated database
        /// </summary>
        /// <param name="database">Database</param>
        /// <returns></returns>
        public static List<MySmoObjectBase> GetCLRAssembliesInfo(Microsoft.SqlServer.Management.Smo.Database database)
        {
            var sqlAssemblys = new List<MySmoObjectBase>();
            foreach (SqlAssembly sqlAssembly in database.Assemblies)
            {
                sqlAssemblys.Add(new MySmoObjectBase(
                     sqlAssembly,
                     sqlAssembly.Name,
                     database.Name));
            }

            return sqlAssemblys;
        }


        /// <summary>
        /// Get stored procedures from indicated database
        /// </summary>
        /// <param name="database">Database</param>
        /// <param name="sqlServer">Sql server</param>
        /// <returns></returns>
        public static Dictionary<string, MySmoObjectBase> GetStoredProceduresInfo(
            Microsoft.SqlServer.Management.Smo.Database database,
            Server sqlServer)
        {
            var myStoredProcedures = new Dictionary<string, MySmoObjectBase>();

            const string commandText = " select ROUTINE_NAME " +
                                       " from INFORMATION_SCHEMA.ROUTINES " +
                                       " where ROUTINE_DEFINITION <> 'NULL' and ROUTINE_TYPE = 'PROCEDURE'" +
                                       " order by ROUTINE_NAME";
            string connectionString = "server=" + sqlServer.Name +
                                     ";database=" + database.Name +
                                     ";uid=" + sqlServer.ConnectionContext.Login +
                                     ";password=" + sqlServer.ConnectionContext.Password;

            using (var cn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(commandText, cn))
            {
                cn.Open();

                cmd.CommandType = CommandType.Text;
                SqlDataReader sqlDR = cmd.ExecuteReader();
                
                while (sqlDR.Read())
                {
                    string name = sqlDR.GetString(0);
                    MySmoObjectBase myStoreProcedure = new MyStoredProcedure(
                        database.StoredProcedures[name],
                        name,
                        database.Name,
                        database.StoredProcedures[name].TextBody);
                    myStoredProcedures.Add(myStoreProcedure.Name, myStoreProcedure);
                }
            }

            return myStoredProcedures;
        }
    }
}