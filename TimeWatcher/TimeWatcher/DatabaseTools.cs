using System;
using System.Data;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using zlib;

namespace TimeWatcher
{
    public class DatabaseTools
    {
        /// <summary>
        /// Tables name is database
        /// </summary>
        enum TableNames
        {
            Project,
            Times
        }

        /// <summary>
        /// Load DB indicate flag
        /// </summary>
        private string m_ResLoad = "";

        private DataSet m_DataSet;
        private string mDatabasePath;

        private static DataColumn CreateIdColumn()
        {
            var idColumn = new DataColumn("id", Type.GetType("System.Int32"))
            {
                AutoIncrement = true,
                AllowDBNull = false,
                Unique = true,
                ReadOnly = true
            };
            return idColumn;
        }
              

        /// <summary>
        /// Конструктор
        /// </summary>
        public DatabaseTools()
        {
            m_DataSet = new DataSet();

            mDatabasePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Database\\Database.db");

            //
            //  if database file found - all ok
            //
            var newThread = new Thread(LoadDB);
            Thread.Sleep(100);
            newThread.Start();
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(1000);
                Application.DoEvents();
                if (m_ResLoad != "")
                {
                    break;
                }
            }
            Thread.Sleep(100);
            newThread.Abort();
            Thread.Sleep(100);
            newThread.Join();

            if (m_ResLoad != "OK")
            {
                var fi = new FileInfo(mDatabasePath);
                if (fi.Exists)
                {
                    var fb = new FileInfo(mDatabasePath + "_backup");
                    int n = 0;
                    while (fb.Exists)
                    {
                        n++;
                        fb = new FileInfo(mDatabasePath + "_backup" + n);                        
                    }                    
                    fi.CopyTo(mDatabasePath + "_backup" + n);
                    fi.Delete();
                }

                //
                // if database file not found - create database
                //                

                //
                // create table Project
                //
                m_DataSet = new DataSet();
                var table = new DataTable(TableNames.Project.ToString());
                var pk = new DataColumn[1];
                table.Columns.Add(CreateIdColumn());

                var nameColumn = new DataColumn("name", Type.GetType("System.String"))
                {
                    Unique = true,
                    AllowDBNull = false
                };
                table.Columns.Add(nameColumn);

                var payColumn = new DataColumn("pay", Type.GetType("System.Int32")) { AllowDBNull = false };
                table.Columns.Add(payColumn);

                var infoColumn = new DataColumn("info", Type.GetType("System.String")) { AllowDBNull = false };
                table.Columns.Add(infoColumn);

                pk[0] = table.Columns["id"];
                table.PrimaryKey = pk;

                m_DataSet.Tables.Add(table);

                //
                // create table Times
                //
                table = new DataTable(TableNames.Times.ToString());                
                table.Columns.Add(CreateIdColumn());

                var pidColumn = new DataColumn("pid", Type.GetType("System.Int32")) { AllowDBNull = false };
                table.Columns.Add(pidColumn);

                var dateStatrColumn = new DataColumn("dateStart", Type.GetType("System.DateTime")) { AllowDBNull = false };
                table.Columns.Add(dateStatrColumn);

                var dateStopColumn = new DataColumn("dateStop", Type.GetType("System.DateTime")) { AllowDBNull = true };
                table.Columns.Add(dateStopColumn);

                pk[0] = table.Columns["id"];
                table.PrimaryKey = pk;
                m_DataSet.Tables.Add(table);

                //
                // create relations between Product table and Certificate table
                //
                var relation = new DataRelation("" + TableNames.Project + TableNames.Times,
                  m_DataSet.Tables[TableNames.Project.ToString()].Columns["id"],
                  m_DataSet.Tables[TableNames.Times.ToString()].Columns["pid"]);
                m_DataSet.Relations.Add(relation);
                
                SaveDB();
            }
        }


        #region Внутренние функция для сохранения базы
        /// <summary>
        ///  Save DB to file
        /// </summary>
        private void SaveDB()
        {
            m_DataSet.AcceptChanges();
            var ms = new MemoryStream();
            m_DataSet.WriteXml(ms, XmlWriteMode.WriteSchema);

            var bytesBuffer = new byte[ms.Length];
            ms.Position = 0;
            ms.Read(bytesBuffer, 0, bytesBuffer.Length);
            ms.Close();
            ms.Dispose();

            byte[] rez = PackXML(bytesBuffer);

            var fi = new FileInfo(mDatabasePath);
            if (!fi.Exists)
            {
                if (fi.DirectoryName != null)
                {
                    var di = new DirectoryInfo(fi.DirectoryName);
                    if (!di.Exists)
                    {
                        di.Create();
                    }
                }
                else
                {
                    throw new Exception(string.Format("Странная внутренняя ошибка: нет директории {0} для {1}", fi.DirectoryName, fi.FullName));
                }
                StreamWriter sw = fi.CreateText();
                sw.Close();
            }
            else
            {
                fi.Delete();
            }

            FileStream fs = File.OpenWrite(mDatabasePath);
            fs.Write(rez, 0, rez.Length);
            fs.Close();
            fs.Dispose();
        }

        
        /// <summary>
        /// Load DB from file
        /// </summary>
        /// <returns>Success - true, oherwise - false</returns>
        private void LoadDB()
        {
            var fi = new FileInfo(mDatabasePath);
            if (!fi.Exists)
            {
                m_ResLoad = "notOK";
                return;
            }

            FileStream fs = File.OpenRead(mDatabasePath);
            var bytesBuffer = new byte[fs.Length];
            fs.Read(bytesBuffer, 0, (int)fs.Length);
            fs.Close();
            fs.Dispose();

            byte[] rez = UnpackXML(bytesBuffer);

            if (rez.Length > 0)
            {
                var ms = new MemoryStream(rez);
                m_DataSet = new DataSet();
                m_DataSet.ReadXml(ms);
                m_DataSet.AcceptChanges();
                ms.Close();
                ms.Dispose();
                m_ResLoad = "OK";
            }
            else
            {
                m_ResLoad = "notOK";
            }
        }

        
        /// <summary>
        /// Pack byte array
        /// </summary>
        /// <param name="xmlInfo">Array for packing</param>
        /// <returns></returns>
        private static byte[] PackXML(byte[] xmlInfo)
        {
           var newByffer = new byte[xmlInfo.Length * 100];
            var stream = new MemoryStream(newByffer);
            var zStream = new ZOutputStream(stream, zlibConst.Z_DEFAULT_COMPRESSION);
            zStream.Write(xmlInfo, 0, xmlInfo.Length);
            zStream.Close();

            int i = xmlInfo.Length * 100 - 1;
            while (i >= 0 && newByffer[i] == 0)
            {
                i--;
            }
            var rez = new byte[i + 1];
            for (int j = 0; j <= i; j++)
            {
                rez[j] = newByffer[j];
            }

            return rez;
        }

        
        /// <summary>
        /// Unpack byte array
        /// </summary>
        /// <param name="xmlInfo">Array for unpacking</param>
        /// <returns></returns>
        private static byte[] UnpackXML(byte[] xmlInfo)
        {
            var newByffer = new byte[xmlInfo.Length * 100];
            var stream = new MemoryStream(newByffer);
            var zStream = new ZOutputStream(stream);
            
            zStream.Write(xmlInfo, 0, xmlInfo.Length);
            zStream.Close();

            int i = xmlInfo.Length * 100 - 1;
            while (i >= 0 && newByffer[i] == 0)
            {
                i--;
            }
            var rez = new byte[i + 1];
            for (int j = 0; j <= i; j++)
            {
                rez[j] = newByffer[j];
            }

            return rez;
        }
        #endregion


        #region Работа с Project таблицей
        /// <summary>
        /// Add project into table Project
        /// </summary>
        /// <param name="projectName">Product name</param>
        /// <param name="pay">Стоимость часа работы</param>
        /// <param name="info">Information</param>
        /// <returns></returns>
        public void AddProject(string projectName, int pay, string info)
        {
            DataTable table = m_DataSet.Tables[TableNames.Project.ToString()];
            DataRow[] rows = table.Select("name='" + projectName + "'");
            if (rows.Length != 0)
            {
                throw new Exception("Проект с названием " + projectName + " уже существует");
            }

            var param = new object[4];
            param[1] = projectName;
            param[2] = pay;
            param[3] = info;
            table.Rows.Add(param);
            SaveDB();                        
        }

        
        /// <summary>
        /// Edit project in the table Project
        /// </summary>
        /// <param name="id">id from table</param>
        /// <param name="projectNewName">Новое название проекта</param>
        /// <param name="newPay">Новая стоимость часа работы</param>
        /// <param name="newInfo">Новая информация о проекте</param>
        /// <returns></returns>
        public void EditProject(int id, string projectNewName, int newPay, string newInfo)
        {
            DataTable table = m_DataSet.Tables[TableNames.Project.ToString()];
            DataRow[] rows = table.Select("name='" + projectNewName + "'");
            if (rows.Length != 0 && (int)rows[0]["id"] != id)
            {
                throw new Exception("Проект с названием " + projectNewName + " уже существует");
            }

            rows = table.Select("id='" + id + "'");
            if (rows.Length != 1)
            {
                throw new Exception("Найдено неверное количество записей с id " + id + ": должно быть 1, найдено " + rows.Length);
            }

            rows[0]["name"] = projectNewName;
            rows[0]["pay"] = newPay;
            rows[0]["info"] = newInfo;
            SaveDB();
        }
    

        /// <summary>
        /// Delete project from table Project
        /// </summary>
        /// <param name="id">Product's id</param>
        /// <returns></returns>
        public void DeleteProject(int id)
        {
            DataTable table = m_DataSet.Tables[TableNames.Project.ToString()];
            DataRow[] rows = table.Select("id='" + id + "'");
            if (rows.Length != 1)
            {
                throw new Exception("Найдено неверное количество записей с id " + id + ": должно быть 1, найдено " + rows.Length);
            }

            DataTable timesTable = m_DataSet.Tables[TableNames.Times.ToString()];
            DataRow[] timeRows = timesTable.Select("pid='" + id + "'");
            foreach (DataRow row in timeRows)
            {
                DeleteTimes(Convert.ToInt32(row[0].ToString()));
            }

            table.Rows.Remove(rows[0]);
            SaveDB();            
        }


        /// <summary>
        /// Get all rows from Project table
        /// </summary>
        /// <returns></returns>
        public Exchange.ProjectInfo[] GetProjectRows()
        {
            DataTable productTable = m_DataSet.Tables[TableNames.Project.ToString()];
            var resRows = new Exchange.ProjectInfo[productTable.Rows.Count];
            
            DataRow[] allRows = productTable.Select("", "name");
           
            for (int i = 0; i < allRows.Length; i++)
            {
                resRows[i].ID = (int)allRows[i]["id"];
                resRows[i].Name = (string)allRows[i]["name"];
                resRows[i].Pay = (int)allRows[i]["pay"];
                resRows[i].Info = (string)allRows[i]["info"];
            }

            return resRows;
        }
        #endregion


        #region Работа с Times таблицей
        /// <summary>
        /// Add times into table Times
        /// </summary>                
        /// <param name="pid">pid from table</param>
        /// <param name="startTime">Start time</param>
        /// <returns></returns>
        public void AddStartTimes(int pid, DateTime startTime)
        {
            DataTable table = m_DataSet.Tables[TableNames.Times.ToString()];
            
            var param = new object[3];            
            param[1] = pid;
            param[2] = startTime;
            table.Rows.Add(param);
            SaveDB();            
        }


        /// <summary>
        /// Add times into table Times
        /// </summary>        
        /// <param name="id">id from table</param>        
        /// <param name="stopTime">Start time</param>
        /// <returns></returns>
        public void AddStopTimes(int id, DateTime stopTime)
        {
            DataTable table = m_DataSet.Tables[TableNames.Times.ToString()];
            DataRow[] rows = table.Select("id='" + id + "'");
            if (rows.Length != 1)
            {
                throw new Exception("Найдено неверное количество записей с id " + id + ": должно быть 1, найдено " + rows.Length);
            }
            rows[0]["dateStop"] = stopTime;
            SaveDB();
            return;
            
        }


        /// <summary>
        /// Edit times in the table Times
        /// </summary>
        /// <param name="id">id from table</param>        
        /// <param name="startTime"></param>
        /// <param name="stopTime"></param>
        /// <returns></returns>
        public void EditTimes(int id, DateTime startTime, DateTime stopTime)
        {
            DataTable table = m_DataSet.Tables[TableNames.Times.ToString()];
            DataRow[] rows = table.Select("id='" + id + "'");            
            if (rows.Length != 1)
            {
                throw new Exception("Найдено неверное количество записей с id " + id + ": должно быть 1, найдено " + rows.Length);                
            }
            rows[0]["dateStart"] = startTime;
            rows[0]["dateStop"] = stopTime;
            SaveDB();
        }


        /// <summary>
        /// Delete times from table Times
        /// </summary>
        /// <param name="id">Product's id</param>
        /// <returns></returns>
        public void DeleteTimes(int id)
        {
            DataTable table = m_DataSet.Tables[TableNames.Times.ToString()];
            DataRow[] rows = table.Select("id='" + id + "'");
            if (rows.Length != 1)
            {
                throw new Exception("Найдено неверное количество записей с id " + id + ": должно быть 1, найдено " + rows.Length);
            }

            table.Rows.Remove(rows[0]);
            SaveDB();
        }


        /// <summary>
        /// Get all rows from Times table
        /// </summary>
        /// <returns></returns>
        public Exchange.TimesInfo[] GetTimesRows(int pid)
        {
            DataTable productTable = m_DataSet.Tables[TableNames.Times.ToString()];            

            DataRow[] allRows = productTable.Select("pid='" + pid + "'", "id");
            var resRows = new Exchange.TimesInfo[allRows.Length];
            for (int i = 0; i < allRows.Length; i++)
            {
                resRows[i].ID = (int)allRows[i]["id"];
                resRows[i].PID = (int)allRows[i]["pid"];
                resRows[i].DateStart = (DateTime)allRows[i]["dateStart"];
                if (allRows[i]["dateStop"].ToString() != "")
                {
                    resRows[i].DateStop = (DateTime)allRows[i]["dateStop"];
                }
                else
                {
                    resRows[i].DateStop = null;
                }
            }

            return resRows;
        }
        #endregion
    }
}
