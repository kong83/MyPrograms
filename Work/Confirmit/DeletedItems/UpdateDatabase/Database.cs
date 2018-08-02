using System;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

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
    class Database
    {
        private readonly DatabaseEngine m_DatabaseEngine;

        public readonly List<int> SurveyIds;

        /// <summary>
        /// Case insensitive replace implementation.
        ///
        /// NOTE:
        ///   .NET Framework does not support case insensitive replace.
        /// </summary>
        private static string ReplaceEx(string expr, string find, string repl)
        {
            // Get input string length
            int exprLen = expr.Length;
            int findLen = find.Length;

            // Check inputs
            if (0 == exprLen || 0 == findLen || findLen > exprLen)
                return expr;

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
                        (0 == string.Compare(expr, pos, find, 0, findLen, true)))
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
            m_DatabaseEngine = new DatabaseEngine(databaseName);

            SurveyIds = GetSurveyIds();
        }

        private List<int> GetSurveyIds()
        {
            var surveySids = m_DatabaseEngine.ExecuteDataTable<DataTable>(
                "SELECT SID FROM BvSurvey WHERE [Name] <> @TemplateSurveyName",
                CommandType.Text,
                new SqlParameter(
                    "@TemplateSurveyName",
                    Configuration.Default.TemplateSurveyName));

            var surveySidsList = new List<int>();
            foreach (DataRow row in surveySids.Rows)
                surveySidsList.Add((int)row["SID"]);

            return surveySidsList;
        }

        public string GetSurveyNameBySurveySid(
            int surveySid)
        {
            return m_DatabaseEngine.ExecuteScalar<string>(
                "SELECT [Name] FROM BvSurvey WHERE SID=@SID",
                CommandType.Text,
                new SqlParameter(
                    "@SID",
                    surveySid));
        }

        public string GetInstanceName()
        {
            return m_DatabaseEngine.ExecuteScalar<string>(
                "SELECT instanceName FROM BvSite",
                CommandType.Text);
        }

        public bool IsSplitTable(
            string originalTableName)
        {
            foreach (SplitTable splitTable in Configuration.Default.SplitTables)
                if (string.Compare(originalTableName, splitTable.OriginalName, true) == 0)
                    return true;

            return false;
        }

        public bool IsSplitProcedure(
            string originalProcedureName)
        {
            foreach (string currentOriginalProcedureName in Configuration.Default.SplitProceduresList)
                if (string.Compare(originalProcedureName, currentOriginalProcedureName, true) == 0)
                    return true;

            return false;
        }

        public string GetTemplateTableNameByOriginalTableName(
            string originalName,
            int surveyId)
        {
            foreach (SplitTable splitTable in Configuration.Default.SplitTables)
                if (string.Compare(splitTable.OriginalName, originalName, true) == 0)
                    return string.Format("t{0}_{1}", surveyId, splitTable.TemplateName);

            throw new Exception(
                string.Format("Cannot find template table name for the original table {0}, survey SID {1}",
                originalName,
                surveyId));
        }

        public string GetOriginalTableNameByTemplateTableName(
            string templateName)
        {
            foreach (SplitTable splitTable in Configuration.Default.SplitTables)
                if (templateName.EndsWith("_" + splitTable.TemplateName, StringComparison.OrdinalIgnoreCase))
                    return splitTable.OriginalName;

            return null;
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
            foreach (int surveyId in SurveyIds)
            {
                if (sidAsDigit == surveyId)
                {
                    return templateName.Remove(4, sid.Length);
                }
            }
            throw new Exception(string.Format("Stored procedure {0} was found, but survey id {1} was not found", templateName, sid));
        }


        /// <summary>
        /// Used for procedures/triggers/indexes
        /// </summary>
        public string GetTemplateNameByOriginalName(
            string originalName,
            int surveyId)
        {
            return
                originalName.Substring(0, 4) +
                surveyId +
                originalName.Substring(4);
        }

        public string GetTemplateTSQL(
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
                    GetTemplateTableNameByOriginalTableName(
                        splitTable.OriginalName,
                        surveyId));
            }

            //
            // Replace all original stored procedure names with corresponding template procedure names.
            //
            foreach (string originalProcedure in Configuration.Default.SplitProceduresList)
            {
                templateTSQL = ReplaceEx(
                    templateTSQL,
                    originalProcedure,
                    GetTemplateNameByOriginalName(
                        originalProcedure,
                        surveyId));
            }

            return templateTSQL;
        }

        /// <summary>
        /// Get views from indicated database
        /// </summary>
        /// <param name="database">Database</param>
        /// <returns></returns>
        public Dictionary<string, MySmoObjectBase> GetViewsInfo(Microsoft.SqlServer.Management.Smo.Database database)
        {
            var views = new Dictionary<string, MySmoObjectBase>();

            foreach (View view in database.Views)
            {
                if (view.IsSystemObject == false)
                {
                    views.Add(view.Name, new MySmoObjectBase(
                         view,
                         view.Name,
                         database.Name,
                         view.TextBody,
                         view.TextHeader));
                }
            }
            return views;
        }


        /// <summary>
        /// Get UserDefinedFunctions from indicated database
        /// </summary>
        /// <param name="database">Database</param>
        /// <returns></returns>
        public Dictionary<string, MySmoObjectBase> GetUserDefinedFunctionsInfo(Microsoft.SqlServer.Management.Smo.Database database)
        {
            var userDefinedFunctions = new Dictionary<string, MySmoObjectBase>();

            foreach (UserDefinedFunction userDefinedFunction in database.UserDefinedFunctions)
            {
                if (userDefinedFunction.IsSystemObject == false)
                {                    
                    userDefinedFunctions.Add(userDefinedFunction.Name, new MySmoObjectBase(
                         userDefinedFunction,
                         userDefinedFunction.Name,
                         database.Name,
                         userDefinedFunction.TextBody,
                         userDefinedFunction.TextHeader));
                }
            }
            return userDefinedFunctions;
        }


        /// <summary>
        /// Get CLRAssemblies from indicated database
        /// </summary>
        /// <param name="database">Database</param>
        /// <returns></returns>
        public List<MySmoObjectBase> GetCLRAssembliesInfo(Microsoft.SqlServer.Management.Smo.Database database)
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
        /// <param name="sqlServer"></param>
        /// <returns></returns>
        public Dictionary<string, MySmoObjectBase> GetStoredProceduresInfo(
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
                if (sqlDR != null)
                {
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
            }
            return myStoredProcedures;
        }
    }
}
