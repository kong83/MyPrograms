using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SurgeryHelper.Entities;
using zlib;

namespace SurgeryHelper.Engines
{
    /// <summary>
    /// Class for worck with DB
    /// </summary>
    public class DbEngine
    {
        public string PassStr = string.Empty;

        private readonly List<PatientClass> _patientList;
        private List<SurgeonClass> _surgeonList;
        private List<ScrubNurseClass> _scrubNurseList;
        private List<OrderlyClass> _orderlyList;
        private List<HeAnestethistClass> _heAnestethistList;
        private List<SheAnestethistClass> _sheAnestethistList;
        private readonly List<NosologyClass> _nosologyList;
        private GlobalSettingsClass _globalSettings;        

        private readonly string _dataPath;
        private readonly string _patientPath;
        private readonly string _surgeonPath;
        private readonly string _scrubNursePath;
        private readonly string _orderlyPath;
        private readonly string _heAnestethistPath;
        private readonly string _sheAnestethistPath;
        private readonly string _nosologyPath;
        private readonly string _globalSettingsPath;

        private readonly string _nightKSGPath;
        private readonly string _dayKSGPath;

        private const string ObjSplitStr = "^!&!^";
        private const string DataSplitStr = "^%#%^";
        private const string OperationSplitStr = "^*$*^";
        public const string ListSplitStr = ";;;";

        public LoggerEngine Logger { get; private set; }

        private readonly ConfigurationEngine _configEngine;

        public ConfigurationEngine ConfigEngine
        {
            get
            {
                return _configEngine;
            }
        }

        public MKBEngine NightMKBEngine { private get; set; }
        public MKBEngine DayMKBEngine { private get; set; }

        public MKBEngine GetCorrectMKBEngine(string typeOfKSG)
        {
            if (typeOfKSG == "�")
            {
                return NightMKBEngine;
            }

            if (typeOfKSG == "�")
            {
                return DayMKBEngine;
            }

            return null;
        }

        public DbEngine()
        {
            _dataPath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) ?? string.Empty, "Data");
            if (!Directory.Exists(_dataPath))
            {
                Directory.CreateDirectory(_dataPath);
            }

            _patientPath = Path.Combine(_dataPath, "patients.save");
            _surgeonPath = Path.Combine(_dataPath, "surgeons.save");
            _scrubNursePath = Path.Combine(_dataPath, "scrub_nurses.save");
            _orderlyPath = Path.Combine(_dataPath, "orderlys.save");
            _heAnestethistPath = Path.Combine(_dataPath, "he_anestethist.save");
            _sheAnestethistPath = Path.Combine(_dataPath, "she_anestethist.save");
            _nosologyPath = Path.Combine(_dataPath, "nosologys.save");
            _globalSettingsPath = Path.Combine(_dataPath, "global_settings.save");
            _nightKSGPath = Path.Combine(_dataPath, "night_GSK.save");
            _dayKSGPath = Path.Combine(_dataPath, "day_GSK.save");

            _patientList = new List<PatientClass>();
            _nosologyList = new List<NosologyClass>();

            _configEngine = new ConfigurationEngine();
        }

        #region ������ � ����������� �����������
        /// <summary>
        /// ��� ���������� ���������
        /// </summary>
        public GlobalSettingsClass GlobalSettings
        {
            get
            {
                return _globalSettings;
            }

            set
            {
                _globalSettings.BranchManager = value.BranchManager;
                _globalSettings.DepartmentName = value.DepartmentName;
                _globalSettings.DischargeEpicrisisHeaderFileName = value.DischargeEpicrisisHeaderFileName;
                _globalSettings.IsLoggingEnabled = value.IsLoggingEnabled;
                SaveGlobalSettings();
            }
        }

        /// <summary>
        /// �������� ���������
        /// </summary>
        public string DepartmentName
        {
            get
            {
                return _globalSettings.DepartmentName;
            }

            set
            {
                _globalSettings.DepartmentName = value;
                SaveGlobalSettings();
            }
        }
        #endregion

        #region ������ � ����������
        public PatientClass[] PatientList
        {
            get;
            private set;
        }

        /// <summary>
        /// �������� ������ ���������, ��� ������� ���� ������, ����� �����, ����� ����� ��������
        /// </summary>
        public void GeneratePatientList()
        {
            switch (ConfigEngine.PatientFormFilterColumnName)
            {
                case "DeliveryDate":
                    _patientList.Sort(PatientClass.CompareByDeliveryDate);
                    break;
                case "ReleaseDate":
                    _patientList.Sort(PatientClass.CompareByReleaseDate);
                    break;
                case "OperationDate":
                    _patientList.Sort(PatientClass.CompareByOperationDate);
                    break;
                default:
                    _patientList.Sort(PatientClass.CompareByName);
                    break;
            }

            if (ConfigEngine.PatientFormFilterDirection == SortOrder.Descending)
            {
                _patientList.Reverse();
            }

            PatientList = new PatientClass[_patientList.Count];
            int cnt = 0;

            // ��������� � ������ ��������� ������ ���, � ������� ���� ������� - �������
            foreach (PatientClass patientInfo in _patientList)
            {
                if (patientInfo.ReleaseDate.HasValue &&
                        ConvertEngine.CompareDateTimes(patientInfo.ReleaseDate.Value, DateTime.Now, false) == 0)
                {
                    PatientList[cnt++] = patientInfo;
                }
            }

            // ��������� � ������ ��������� ������ ���, � ������� ���� ������� 
            // �� ������� ��� � �������
            foreach (PatientClass patientInfo in _patientList)
            {
                if (!patientInfo.ReleaseDate.HasValue ||
                    ConvertEngine.CompareDateTimes(patientInfo.ReleaseDate.Value, DateTime.Now, false) > 0)
                {
                    PatientList[cnt++] = patientInfo;
                }
            }

            // ��������� � ������ ��������� ������ ���, � ������� ���� ������� 
            // � �������
            foreach (PatientClass patientInfo in _patientList)
            {
                if (patientInfo.ReleaseDate.HasValue &&
                    ConvertEngine.CompareDateTimes(patientInfo.ReleaseDate.Value, DateTime.Now, false) < 0)
                {
                    PatientList[cnt++] = patientInfo;
                }
            }
        }

        /// <summary>
        /// ������������� ����� ID ��� ��������
        /// </summary>
        /// <returns></returns>
        private int GetNewPatientId()
        {
            int max = 0;
            foreach (PatientClass patientInfo in _patientList)
            {
                if (patientInfo.Id > max)
                {
                    max = patientInfo.Id;
                }
            }

            return max + 1;
        }

        /// <summary>
        /// �������� ������ ���������, � ������� ��������� ��������� ���������
        /// </summary>
        /// <param name="nosologyName">�������� ���������</param>
        /// <returns></returns>
        public List<PatientClass> GetPatientByNosology(string nosologyName)
        {
            var selectedPatients = new List<PatientClass>();

            foreach (PatientClass patientInfo in _patientList)
            {
                if (patientInfo.Nosology == nosologyName)
                {
                    selectedPatients.Add(patientInfo);
                }
            }

            return selectedPatients;
        }

        /// <summary>
        /// �������� ��������� � ���� ���������
        /// </summary>
        /// <param name="oldNosology">������ ���������</param>
        /// <param name="newNosology">����� ���������</param>
        public void ChangeNosologyForAllPatients(string oldNosology, string newNosology)
        {
            foreach (PatientClass patientInfo in GetPatientByNosology(oldNosology))
            {
                patientInfo.Nosology = newNosology;
            }

            SavePatients();
        }

        /// <summary>
        /// �������� ������ �������� � ������ ���������
        /// </summary>
        /// <param name="patientInfo">���������� � ��������</param>
        public void AddPatient(PatientClass patientInfo)
        {
            var newPationInfo = new PatientClass(patientInfo) { Id = GetNewPatientId() };
            _patientList.Add(newPationInfo);
            SavePatients();
        }


        /// <summary>
        /// �������� ���������� � ��������
        /// </summary>
        /// <param name="patientInfo">���������� � ��������</param>
        public void UpdatePatient(PatientClass patientInfo)
        {
            int n = 0;
            while (_patientList[n].Id != patientInfo.Id)
            {
                n++;
            }

            _patientList[n] = new PatientClass(patientInfo);
            SavePatients();
        }

        /// <summary>
        /// ������� ��������
        /// </summary>
        /// <param name="patientInfoId">ID ��������</param>
        public void RemovePatient(int patientInfoId)
        {
            int n = 0;
            while (_patientList[n].Id != patientInfoId)
            {
                n++;
            }

            _patientList.RemoveAt(n);
            SavePatients();
        }

        /// <summary>
        /// �������� �������� �� id
        /// </summary>
        /// <param name="patientInfoId">ID ��������</param>
        /// <returns></returns>
        public PatientClass GetPatientById(int patientInfoId)
        {
            int n = 0;
            while (_patientList[n].Id != patientInfoId)
            {
                n++;
            }

            return _patientList[n];
        }

        /// <summary>
        /// �������� ������ ��������� � ��������� ���, ����� ����������� � ���������
        /// </summary>
        /// <param name="fullName">��� ��������</param>
        /// <param name="deliveryDate">���� �����������</param>
        /// <param name="diagnose">������� ��������</param>
        /// <returns></returns>
        private List<PatientClass> GetPatientByGeneralData(string fullName, DateTime deliveryDate, string diagnose)
        {
            var selectedPatients = new List<PatientClass>();

            foreach (PatientClass patient in _patientList)
            {
                if (patient.GetFullName() == fullName &&
                    ConvertEngine.CompareDateTimes(patient.DeliveryDate, deliveryDate, false) == 0 &&
                    patient.Diagnose == diagnose)
                {
                    selectedPatients.Add(patient);
                }
            }

            return selectedPatients;
        }

        #endregion

        #region ������ � ���������
        /// <summary>
        /// �������� ������ ��������
        /// </summary>
        public SurgeonClass[] SurgeonList
        {
            get
            {
                return _surgeonList.ToArray();
            }
        }

        /// <summary>
        /// ������������� ����� ID ��� �������
        /// </summary>
        /// <returns></returns>
        private int GetNewSurgeonId()
        {
            int max = 0;
            foreach (SurgeonClass surgeonInfo in _surgeonList)
            {
                if (surgeonInfo.Id > max)
                {
                    max = surgeonInfo.Id;
                }
            }

            return max + 1;
        }

        /// <summary>
        /// �������� ������ ������� � ������ ��������
        /// </summary>
        /// <param name="surgeonInfo">���������� � �������</param>
        public void AddSurgeon(SurgeonClass surgeonInfo)
        {
            var newSurgeonInfo = new SurgeonClass(surgeonInfo) { Id = GetNewSurgeonId() };
            _surgeonList.Add(newSurgeonInfo);
            SaveSurgeons();
        }


        /// <summary>
        /// �������� ���������� � �������
        /// </summary>
        /// <param name="surgeonInfo">���������� � �������</param>
        public void UpdateSurgeon(SurgeonClass surgeonInfo)
        {
            int n = 0;
            while (_surgeonList[n].Id != surgeonInfo.Id)
            {
                n++;
            }

            _surgeonList[n] = new SurgeonClass(surgeonInfo);
            SaveSurgeons();
        }

        /// <summary>
        /// ������� �������
        /// </summary>
        /// <param name="surgeonInfoId">ID �������</param>
        public void RemoveSurgeon(int surgeonInfoId)
        {
            int n = 0;
            while (_surgeonList[n].Id != surgeonInfoId)
            {
                n++;
            }

            _surgeonList.RemoveAt(n);
            SaveSurgeons();
        }
        #endregion

        #region ������ � ������������� ���. �������
        /// <summary>
        /// �������� ������ ������������ ���. �����
        /// </summary>
        public ScrubNurseClass[] ScrubNurseList
        {
            get
            {
                return _scrubNurseList.ToArray();
            }
        }

        /// <summary>
        /// ������������� ����� ID ��� ������������ ���. ������
        /// </summary>
        /// <returns></returns>
        private int GetNewScrubNurseId()
        {
            int max = 0;
            foreach (ScrubNurseClass scrubNurseInfo in _scrubNurseList)
            {
                if (scrubNurseInfo.Id > max)
                {
                    max = scrubNurseInfo.Id;
                }
            }

            return max + 1;
        }

        /// <summary>
        /// �������� ����� ������������ ���. ������ � ������ ������������ ���. �����
        /// </summary>
        /// <param name="scrubNurseInfo">���������� �� ����. ���. ������</param>
        public void AddScrubNurse(ScrubNurseClass scrubNurseInfo)
        {
            var newScrubNurseInfo = new ScrubNurseClass(scrubNurseInfo) { Id = GetNewScrubNurseId() };
            _scrubNurseList.Add(newScrubNurseInfo);
            SaveScrubNurses();
        }


        /// <summary>
        /// �������� ���������� � ������������ ���. ������
        /// </summary>
        /// <param name="scrubNurseInfo">���������� �� ����. ���. ������</param>
        public void UpdateScrubNurse(ScrubNurseClass scrubNurseInfo)
        {
            int n = 0;
            while (_scrubNurseList[n].Id != scrubNurseInfo.Id)
            {
                n++;
            }

            _scrubNurseList[n] = new ScrubNurseClass(scrubNurseInfo);
            SaveScrubNurses();
        }

        /// <summary>
        /// ������� ������������ ���. ������
        /// </summary>
        /// <param name="scrubNurseInfoId">ID ����. ���. ������</param>
        public void RemoveScrubNurse(int scrubNurseInfoId)
        {
            int n = 0;
            while (_scrubNurseList[n].Id != scrubNurseInfoId)
            {
                n++;
            }

            _scrubNurseList.RemoveAt(n);
            SaveScrubNurses();
        }
        #endregion

        #region ������ � ����������
        /// <summary>
        /// �������� ������ ���������
        /// </summary>
        public OrderlyClass[] OrderlyList
        {
            get
            {
                return _orderlyList.ToArray();
            }
        }

        /// <summary>
        /// ������������� ����� ID ��� ��������
        /// </summary>
        /// <returns></returns>
        private int GetNewOrderlyId()
        {
            int max = 0;
            foreach (OrderlyClass orderlyInfo in _orderlyList)
            {
                if (orderlyInfo.Id > max)
                {
                    max = orderlyInfo.Id;
                }
            }

            return max + 1;
        }

        /// <summary>
        /// �������� ������ �������� � ������ ���������
        /// </summary>
        /// <param name="orderlyInfo">���������� � �������</param>
        public void AddOrderly(OrderlyClass orderlyInfo)
        {
            var newOrderlyInfo = new OrderlyClass(orderlyInfo) { Id = GetNewOrderlyId() };
            _orderlyList.Add(newOrderlyInfo);
            SaveOrderlys();
        }


        /// <summary>
        /// �������� ���������� � ��������
        /// </summary>
        /// <param name="orderlyInfo">���������� � ��������</param>
        public void UpdateOrderly(OrderlyClass orderlyInfo)
        {
            int n = 0;
            while (_orderlyList[n].Id != orderlyInfo.Id)
            {
                n++;
            }

            _orderlyList[n] = new OrderlyClass(orderlyInfo);
            SaveOrderlys();
        }

        /// <summary>
        /// ������� ��������
        /// </summary>
        /// <param name="orderlyInfoId">ID ��������</param>
        public void RemoveOrderly(int orderlyInfoId)
        {
            int n = 0;
            while (_orderlyList[n].Id != orderlyInfoId)
            {
                n++;
            }

            _orderlyList.RemoveAt(n);
            SaveOrderlys();
        }
        #endregion

        #region ������ � ���������������
        /// <summary>
        /// �������� ������ ��������������
        /// </summary>
        public HeAnestethistClass[] HeAnestethistList
        {
            get
            {
                return _heAnestethistList.ToArray();
            }
        }

        /// <summary>
        /// ������������� ����� ID ��� �������������
        /// </summary>
        /// <returns></returns>
        private int GetNewHeAnestethistId()
        {
            int max = 0;
            foreach (HeAnestethistClass heAnestethistInfo in _heAnestethistList)
            {
                if (heAnestethistInfo.Id > max)
                {
                    max = heAnestethistInfo.Id;
                }
            }

            return max + 1;
        }

        /// <summary>
        /// �������� ������ ������������� � ������ ��������������
        /// </summary>
        /// <param name="heAnestethistInfo">���������� �� �������������</param>
        public void AddHeAnestethist(HeAnestethistClass heAnestethistInfo)
        {
            var newHeAnestethistInfo = new HeAnestethistClass(heAnestethistInfo) { Id = GetNewHeAnestethistId() };
            _heAnestethistList.Add(newHeAnestethistInfo);
            SaveHeAnestethists();
        }


        /// <summary>
        /// �������� ���������� �� �������������
        /// </summary>
        /// <param name="heAnestethistInfo">���������� �� �������������</param>
        public void UpdateHeAnestethist(HeAnestethistClass heAnestethistInfo)
        {
            int n = 0;
            while (_heAnestethistList[n].Id != heAnestethistInfo.Id)
            {
                n++;
            }

            _heAnestethistList[n] = new HeAnestethistClass(heAnestethistInfo);
            SaveHeAnestethists();
        }

        /// <summary>
        /// ������� �������������
        /// </summary>
        /// <param name="heAnestethistInfoId">ID �������������</param>
        public void RemoveHeAnestethist(int heAnestethistInfoId)
        {
            int n = 0;
            while (_heAnestethistList[n].Id != heAnestethistInfoId)
            {
                n++;
            }

            _heAnestethistList.RemoveAt(n);
            SaveHeAnestethists();
        }
        #endregion

        #region ������ � ��������������
        /// <summary>
        /// �������� ������ ������������
        /// </summary>
        public SheAnestethistClass[] SheAnestethistList
        {
            get
            {
                return _sheAnestethistList.ToArray();
            }
        }

        /// <summary>
        /// ������������� ����� ID ��� ������������
        /// </summary>
        /// <returns></returns>
        private int GetNewSheAnestethistId()
        {
            int max = 0;
            foreach (SheAnestethistClass sheAnestethistInfo in _sheAnestethistList)
            {
                if (sheAnestethistInfo.Id > max)
                {
                    max = sheAnestethistInfo.Id;
                }
            }

            return max + 1;
        }

        /// <summary>
        /// �������� ������ ������������ � ������ ������������
        /// </summary>
        /// <param name="sheAnestethistInfo">���������� �� ������������</param>
        public void AddSheAnestethist(SheAnestethistClass sheAnestethistInfo)
        {
            var newSheAnestethistInfo = new SheAnestethistClass(sheAnestethistInfo) { Id = GetNewSheAnestethistId() };
            _sheAnestethistList.Add(newSheAnestethistInfo);
            SaveSheAnestethists();
        }


        /// <summary>
        /// �������� ���������� �� ������������
        /// </summary>
        /// <param name="sheAnestethistInfo">���������� �� ������������</param>
        public void UpdateSheAnestethist(SheAnestethistClass sheAnestethistInfo)
        {
            int n = 0;
            while (_sheAnestethistList[n].Id != sheAnestethistInfo.Id)
            {
                n++;
            }

            _sheAnestethistList[n] = new SheAnestethistClass(sheAnestethistInfo);
            SaveSheAnestethists();
        }

        /// <summary>
        /// ������� ������������
        /// </summary>
        /// <param name="sheAnestethistInfoId">ID ������������</param>
        public void RemoveSheAnestethist(int sheAnestethistInfoId)
        {
            int n = 0;
            while (_sheAnestethistList[n].Id != sheAnestethistInfoId)
            {
                n++;
            }

            _sheAnestethistList.RemoveAt(n);
            SaveSheAnestethists();
        }
        #endregion

        #region ������ � �����������
        /// <summary>
        /// �������� ������ ���������
        /// </summary>
        public NosologyClass[] NosologyList
        {
            get
            {
                return _nosologyList.ToArray();
            }
        }

        /// <summary>
        /// ������������� ����� ID ��� ���������
        /// </summary>
        /// <returns></returns>
        private int GetNewNosologyId()
        {
            int max = 0;
            foreach (NosologyClass nosologyInfo in _nosologyList)
            {
                if (nosologyInfo.Id > max)
                {
                    max = nosologyInfo.Id;
                }
            }

            return max + 1;
        }

        /// <summary>
        /// ������� ������ ��������� �� �����
        /// </summary>
        /// <param name="nosologyName">�������� ���������</param>
        /// <returns></returns>
        public List<NosologyClass> GetNosologyByName(string nosologyName)
        {
            var selectedNosologys = new List<NosologyClass>();
            foreach (NosologyClass nosology in _nosologyList)
            {
                if (nosology.LastNameWithInitials == nosologyName)
                {
                    selectedNosologys.Add(nosology);
                }
            }

            return selectedNosologys;
        }

        /// <summary>
        /// �������� ����� ��������� � ������ ���������
        /// </summary>
        /// <param name="nosologyName">���������� �� ���������</param>
        public void AddNosology(string nosologyName)
        {
            var newNosologyInfo = new NosologyClass
            {
                LastNameWithInitials = nosologyName,
                Id = GetNewNosologyId()
            };
            _nosologyList.Add(newNosologyInfo);
            SaveNosologys();
        }

        /// <summary>
        /// �������� ���������� � ���������
        /// </summary>
        /// <param name="oldNosologyInfo">���������� �� ���������</param>
        /// <param name="newNosologyName">����� �������� ���������</param>
        public void UpdateNosology(NosologyClass oldNosologyInfo, string newNosologyName)
        {
            int n = 0;
            while (_nosologyList[n].Id != oldNosologyInfo.Id)
            {
                n++;
            }

            ChangeNosologyForAllPatients(_nosologyList[n].LastNameWithInitials, newNosologyName);

            _nosologyList[n].LastNameWithInitials = newNosologyName;
            SaveNosologys();
        }

        /// <summary>
        /// ������� ���������
        /// </summary>
        /// <param name="nosologyInfoId">���������� �� ���������</param>
        public void RemoveNosology(int nosologyInfoId)
        {
            int n = 0;
            while (_nosologyList[n].Id != nosologyInfoId)
            {
                n++;
            }

            _nosologyList.RemoveAt(n);
            SaveNosologys();
        }
        #endregion

        #region ���������� ������
        /// <summary>
        /// ��������� ������ ���������
        /// </summary>
        private void SavePatients()
        {
            _patientList.Sort(PatientClass.CompareByName);

            var patientsStr = new StringBuilder();

            foreach (PatientClass patientInfo in _patientList)
            {
                var operationsStr = new StringBuilder();
                foreach (OperationClass operationInfo in patientInfo.Operations)
                {
                    operationsStr.Append(
                        "Assistents=" + ConvertEngine.ListToString(operationInfo.Assistents) + DataSplitStr +
                        "HeAnaesthetist=" + operationInfo.HeAnaesthetist + DataSplitStr +
                        "Id=" + operationInfo.Id + DataSplitStr +
                        "Name=" + operationInfo.Name + DataSplitStr +
                        "OperationCourse=" + operationInfo.OperationCourse + DataSplitStr +
                        "DataOfOperation=" + ConvertEngine.GetRightDateString(operationInfo.DataOfOperation, true) + DataSplitStr +
                        "StartTimeOfOperation=" + ConvertEngine.GetRightDateString(operationInfo.StartTimeOfOperation, true) + DataSplitStr +
                        "EndTimeOfOperation=" + ConvertEngine.GetRightDateString(operationInfo.EndTimeOfOperation, true) + DataSplitStr +
                        "Orderly=" + operationInfo.Orderly + DataSplitStr +
                        "ScrubNurse=" + operationInfo.ScrubNurse + DataSplitStr +
                        "SheAnaesthetist=" + operationInfo.SheAnaesthetist + DataSplitStr +
                        "BeforeOperationEpicrisisADFirst=" + operationInfo.BeforeOperationEpicrisisADFirst + DataSplitStr +
                        "BeforeOperationEpicrisisADSecond=" + operationInfo.BeforeOperationEpicrisisADSecond + DataSplitStr +
                        "BeforeOperationEpicrisisBreath=" + operationInfo.BeforeOperationEpicrisisBreath + DataSplitStr +
                        "BeforeOperationEpicrisisChDD=" + operationInfo.BeforeOperationEpicrisisChDD + DataSplitStr +
                        "BeforeOperationEpicrisisComplaints=" + operationInfo.BeforeOperationEpicrisisComplaints + DataSplitStr +
                        "BeforeOperationEpicrisisState=" + operationInfo.BeforeOperationEpicrisisState + DataSplitStr +
                        "BeforeOperationEpicrisisHeartRhythm=" + operationInfo.BeforeOperationEpicrisisHeartRhythm + DataSplitStr +
                        "BeforeOperationEpicrisisHeartSounds=" + operationInfo.BeforeOperationEpicrisisHeartSounds + DataSplitStr +
                        "BeforeOperationEpicrisisIsDairyEnabled=" + operationInfo.BeforeOperationEpicrisisIsDairyEnabled + DataSplitStr +
                        "BeforeOperationEpicrisisPulse=" + operationInfo.BeforeOperationEpicrisisPulse + DataSplitStr +
                        "BeforeOperationEpicrisisStLocalis=" + operationInfo.BeforeOperationEpicrisisStLocalis + DataSplitStr +
                        "BeforeOperationEpicrisisStomach=" + operationInfo.BeforeOperationEpicrisisStomach + DataSplitStr +
                        "BeforeOperationEpicrisisStool=" + operationInfo.BeforeOperationEpicrisisStool + DataSplitStr +
                        "BeforeOperationEpicrisisTemperature=" + operationInfo.BeforeOperationEpicrisisTemperature + DataSplitStr +
                        "BeforeOperationEpicrisisTimeWriting=" + ConvertEngine.GetRightDateString(operationInfo.BeforeOperationEpicrisisTimeWriting, true) + DataSplitStr +
                        "BeforeOperationEpicrisisUrination=" + operationInfo.BeforeOperationEpicrisisUrination + DataSplitStr +
                        "BeforeOperationEpicrisisWheeze=" + operationInfo.BeforeOperationEpicrisisWheeze + DataSplitStr +
                        "Surgeons=" + ConvertEngine.ListToString(operationInfo.Surgeons) + OperationSplitStr);
                }

                patientsStr.Append(
                    "Age=" + patientInfo.Age + DataSplitStr +
                    "BuildingNumber=" + patientInfo.BuildingNumber + DataSplitStr +
                    "Birthday=" + ConvertEngine.GetRightDateString(patientInfo.Birthday, true) + DataSplitStr +
                    "CityName=" + patientInfo.CityName + DataSplitStr +
                    "DeliveryDate=" + ConvertEngine.GetRightDateString(patientInfo.DeliveryDate, true) + DataSplitStr +
                    "Diagnose=" + patientInfo.Diagnose + DataSplitStr +
                    "DoctorInChargeOfTheCase=" + patientInfo.DoctorInChargeOfTheCase + DataSplitStr +
                    "FlatNumber=" + patientInfo.FlatNumber + DataSplitStr +
                    "WorkPlace=" + patientInfo.WorkPlace + DataSplitStr +
                    "HomeNumber=" + patientInfo.HomeNumber + DataSplitStr +
                    "Id=" + patientInfo.Id + DataSplitStr +
                    "LastName=" + patientInfo.LastName + DataSplitStr +
                    "Name=" + patientInfo.Name + DataSplitStr +
                    "Nosology=" + patientInfo.Nosology + DataSplitStr +
                    "NumberOfCaseHistory=" + patientInfo.NumberOfCaseHistory + DataSplitStr +
                    "Patronymic=" + patientInfo.Patronymic + DataSplitStr +
                    "Phone=" + patientInfo.Phone + DataSplitStr +
                    "TypeOfKSG=" + patientInfo.TypeOfKSG + DataSplitStr +
                    "MKB=" + patientInfo.MKB + DataSplitStr +
                    "KSG=" + patientInfo.KSG + DataSplitStr +
                    "ReleaseDate=" + ConvertEngine.GetRightDateString(patientInfo.ReleaseDate, true) + DataSplitStr +
                    "StreetName=" + patientInfo.StreetName + DataSplitStr +
                    "TransferEpicrisAfterOperationPeriod=" + patientInfo.TransferEpicrisAfterOperationPeriod + DataSplitStr +
                    "TransferEpicrisPlan=" + patientInfo.TransferEpicrisPlan + DataSplitStr +
                    "TransferEpicrisWritingDate=" + ConvertEngine.GetRightDateString(patientInfo.TransferEpicrisWritingDate, true) + DataSplitStr +
                    "TransferEpicrisAdditionalInfo=" + patientInfo.TransferEpicrisAdditionalInfo + DataSplitStr +
                    "TransferEpicrisDisabilityList=" + patientInfo.TransferEpicrisDisabilityList + DataSplitStr +
                    "TransferEpicrisIsIncludeDisabilityList=" + patientInfo.TransferEpicrisIsIncludeDisabilityList + DataSplitStr +
                    "LineOfCommEpicrisAdditionalInfo=" + patientInfo.LineOfCommEpicrisAdditionalInfo + DataSplitStr +
                    "LineOfCommEpicrisPlan=" + patientInfo.LineOfCommEpicrisPlan + DataSplitStr +
                    "LineOfCommEpicrisWritingDate=" + ConvertEngine.GetRightDateString(patientInfo.LineOfCommEpicrisWritingDate, true) + DataSplitStr +
                    "DischargeEpicrisAfterOperation=" + patientInfo.DischargeEpicrisAfterOperation + DataSplitStr +
                    "DischargeEpicrisConservativeTherapy=" + patientInfo.DischargeEpicrisConservativeTherapy + DataSplitStr +
                    "DischargeEpicrisEkg=" + patientInfo.DischargeEpicrisEkg + DataSplitStr +
                    "DischargeEpicrisOakEritrocits=" + patientInfo.DischargeEpicrisOakEritrocits + DataSplitStr +
                    "DischargeEpicrisOakHb=" + patientInfo.DischargeEpicrisOakHb + DataSplitStr +
                    "DischargeEpicrisOakLekocits=" + patientInfo.DischargeEpicrisOakLekocits + DataSplitStr +
                    "DischargeEpicrisOakSoe=" + patientInfo.DischargeEpicrisOakSoe + DataSplitStr +
                    "DischargeEpicrisOamColor=" + patientInfo.DischargeEpicrisOamColor + DataSplitStr +
                    "DischargeEpicrisOamDensity=" + patientInfo.DischargeEpicrisOamDensity + DataSplitStr +
                    "DischargeEpicrisOamEritrocits=" + patientInfo.DischargeEpicrisOamEritrocits + DataSplitStr +
                    "DischargeEpicrisOamLekocits=" + patientInfo.DischargeEpicrisOamLekocits + DataSplitStr +
                    "DischargeEpicrisBakBillirubin=" + patientInfo.DischargeEpicrisBakBillirubin + DataSplitStr +
                    "DischargeEpicrisBakGeneralProtein=" + patientInfo.DischargeEpicrisBakGeneralProtein + DataSplitStr +
                    "DischargeEpicrisBakPTI=" + patientInfo.DischargeEpicrisBakPTI + DataSplitStr +
                    "DischargeEpicrisBakSugar=" + patientInfo.DischargeEpicrisBakSugar + DataSplitStr +
                    "DischargeEpicrisAdditionalAnalises=" + patientInfo.DischargeEpicrisAdditionalAnalises + DataSplitStr +
                    "DischargeEpicrisRecomendations=" + ConvertEngine.ListToString(patientInfo.DischargeEpicrisRecomendations) + DataSplitStr +
                    "DischargeEpicrisAdditionalRecomendations=" + ConvertEngine.ListToString(patientInfo.DischargeEpicrisAdditionalRecomendations) + DataSplitStr +
                    "TreatmentPlanDate=" + ConvertEngine.GetRightDateString(patientInfo.TreatmentPlanDate, true) + DataSplitStr +
                    "TreatmentPlanInspection=" + patientInfo.TreatmentPlanInspection + DataSplitStr +
                    "IsTreatmentPlanActiveInOperationProtocol=" + patientInfo.IsTreatmentPlanActiveInOperationProtocol + DataSplitStr +
                    "MedicalInspectionAnamneseTraumaDate=" + ConvertEngine.GetRightDateString(patientInfo.MedicalInspectionAnamneseTraumaDate, false) + DataSplitStr +
                    "MedicalInspectionAnamneseAnMorbi=" + patientInfo.MedicalInspectionAnamneseAnMorbi + DataSplitStr +
                    "MedicalInspectionStLocalisDescription=" + patientInfo.MedicalInspectionStLocalisDescription + DataSplitStr +
                    "MedicalInspectionStLocalisRentgen=" + patientInfo.MedicalInspectionStLocalisRentgen + DataSplitStr +
                    "MedicalInspectionAnamneseAnVitae=" + ConvertEngine.ListBoolToString(patientInfo.MedicalInspectionAnamneseAnVitae) + DataSplitStr +
                    "MedicalInspectionAnamneseCheckboxes=" + ConvertEngine.ListBoolToString(patientInfo.MedicalInspectionAnamneseCheckboxes) + DataSplitStr +
                    "MedicalInspectionAnamneseTextBoxes=" + ConvertEngine.ListToString(patientInfo.MedicalInspectionAnamneseTextBoxes) + DataSplitStr +
                    "MedicalInspectionComplaints=" + patientInfo.MedicalInspectionComplaints + DataSplitStr +
                    "MedicalInspectionExpertAnamnese=" + patientInfo.MedicalInspectionExpertAnamnese + DataSplitStr +
                    "MedicalInspectionInspectionPlan=" + patientInfo.MedicalInspectionInspectionPlan + DataSplitStr +
                    "MedicalInspectionIsAnamneseActive=" + patientInfo.MedicalInspectionIsAnamneseActive + DataSplitStr +
                    "MedicalInspectionIsPlanEnabled=" + patientInfo.MedicalInspectionIsPlanEnabled + DataSplitStr +
                    "MedicalInspectionIsStLocalisPart1Enabled=" + patientInfo.MedicalInspectionIsStLocalisPart1Enabled + DataSplitStr +
                    "MedicalInspectionIsStLocalisPart2Enabled=" + patientInfo.MedicalInspectionIsStLocalisPart2Enabled + DataSplitStr +
                    "MedicalInspectionLnFirstDateStart=" + ConvertEngine.GetRightDateString(patientInfo.MedicalInspectionLnFirstDateStart) + DataSplitStr +
                    "MedicalInspectionLnWithNumberDateEnd=" + ConvertEngine.GetRightDateString(patientInfo.MedicalInspectionLnWithNumberDateEnd) + DataSplitStr +
                    "MedicalInspectionLnWithNumberDateStart=" + ConvertEngine.GetRightDateString(patientInfo.MedicalInspectionLnWithNumberDateStart) + DataSplitStr +
                    "MedicalInspectionStLocalisPart1Fields=" + ConvertEngine.ListToString(patientInfo.MedicalInspectionStLocalisPart1Fields) + DataSplitStr +
                    "MedicalInspectionStLocalisPart1OppositionFinger=" + patientInfo.MedicalInspectionStLocalisPart1OppositionFinger + DataSplitStr +
                    "MedicalInspectionStLocalisPart2ComboBoxes=" + ConvertEngine.ListToString(patientInfo.MedicalInspectionStLocalisPart2ComboBoxes) + DataSplitStr +
                    "MedicalInspectionStLocalisPart2LeftHand=" + ConvertEngine.ListToString(patientInfo.MedicalInspectionStLocalisPart2LeftHand) + DataSplitStr +
                    "MedicalInspectionStLocalisPart2NumericUpDown=" + patientInfo.MedicalInspectionStLocalisPart2NumericUpDown + DataSplitStr +
                    "MedicalInspectionStLocalisPart2RightHand=" + ConvertEngine.ListToString(patientInfo.MedicalInspectionStLocalisPart2RightHand) + DataSplitStr +
                    "MedicalInspectionStLocalisPart2TextBoxes=" + ConvertEngine.ListToString(patientInfo.MedicalInspectionStLocalisPart2TextBoxes) + DataSplitStr +
                    "MedicalInspectionStLocalisPart2WhichHand=" + patientInfo.MedicalInspectionStLocalisPart2WhichHand + DataSplitStr +
                    "MedicalInspectionStPraesensComboBoxes=" + ConvertEngine.ListToString(patientInfo.MedicalInspectionStPraesensComboBoxes) + DataSplitStr +
                    "MedicalInspectionStPraesensNumericUpDowns=" + ConvertEngine.ListIntToString(patientInfo.MedicalInspectionStPraesensNumericUpDowns) + DataSplitStr +
                    "MedicalInspectionStPraesensOthers=" + patientInfo.MedicalInspectionStPraesensOthers + DataSplitStr +
                    "MedicalInspectionStPraesensTextBoxes=" + ConvertEngine.ListToString(patientInfo.MedicalInspectionStPraesensTextBoxes) + DataSplitStr +
                    "MedicalInspectionTeoRisk=" + patientInfo.MedicalInspectionTeoRisk + DataSplitStr +
                    "PrivateFolder=" + patientInfo.PrivateFolder + OperationSplitStr +
                    operationsStr + ObjSplitStr);
            }

            PackedData(patientsStr.ToString(), _patientPath);
        }

        /// <summary>
        /// ��������� ������ ��������
        /// </summary>
        private void SaveSurgeons()
        {
            _surgeonList.Sort(SurgeonClass.Compare);

            var surgeonsStr = new StringBuilder();

            foreach (SurgeonClass surgeonInfo in _surgeonList)
            {
                surgeonsStr.Append(
                    "Id=" + surgeonInfo.Id + DataSplitStr +
                    "LastNameWithInitials=" + surgeonInfo.LastNameWithInitials + ObjSplitStr);
            }

            PackedData(surgeonsStr.ToString(), _surgeonPath);
        }

        /// <summary>
        /// ��������� ������ ������������ ���. �����
        /// </summary>
        private void SaveScrubNurses()
        {
            _scrubNurseList.Sort(ScrubNurseClass.Compare);

            var scrubNursesStr = new StringBuilder();

            foreach (ScrubNurseClass scrubNurseInfo in _scrubNurseList)
            {
                scrubNursesStr.Append(
                    "Id=" + scrubNurseInfo.Id + DataSplitStr +
                    "LastNameWithInitials=" + scrubNurseInfo.LastNameWithInitials + ObjSplitStr);
            }

            PackedData(scrubNursesStr.ToString(), _scrubNursePath);
        }

        /// <summary>
        /// ��������� ������ ���������
        /// </summary>
        private void SaveOrderlys()
        {
            _orderlyList.Sort(OrderlyClass.Compare);

            var orderlysStr = new StringBuilder();

            foreach (OrderlyClass orderlyInfo in _orderlyList)
            {
                orderlysStr.Append(
                    "Id=" + orderlyInfo.Id + DataSplitStr +
                    "LastNameWithInitials=" + orderlyInfo.LastNameWithInitials + ObjSplitStr);
            }

            PackedData(orderlysStr.ToString(), _orderlyPath);
        }

        /// <summary>
        /// ��������� ������ ��������������
        /// </summary>
        private void SaveHeAnestethists()
        {
            _heAnestethistList.Sort(HeAnestethistClass.Compare);

            var heAnestethistsStr = new StringBuilder();

            foreach (HeAnestethistClass heAnestethistInfo in _heAnestethistList)
            {
                heAnestethistsStr.Append(
                    "Id=" + heAnestethistInfo.Id + DataSplitStr +
                    "LastNameWithInitials=" + heAnestethistInfo.LastNameWithInitials + ObjSplitStr);
            }

            PackedData(heAnestethistsStr.ToString(), _heAnestethistPath);
        }

        /// <summary>
        /// ��������� ������ ������������
        /// </summary>
        private void SaveSheAnestethists()
        {
            _sheAnestethistList.Sort(SheAnestethistClass.Compare);

            var sheAnestethistsStr = new StringBuilder();

            foreach (SheAnestethistClass sheAnestethistInfo in _sheAnestethistList)
            {
                sheAnestethistsStr.Append(
                    "Id=" + sheAnestethistInfo.Id + DataSplitStr +
                    "LastNameWithInitials=" + sheAnestethistInfo.LastNameWithInitials + ObjSplitStr);
            }

            PackedData(sheAnestethistsStr.ToString(), _sheAnestethistPath);
        }

        /// <summary>
        /// ��������� ������ ���������
        /// </summary>
        private void SaveNosologys()
        {
            _nosologyList.Sort(NosologyClass.Compare);

            var nosologysStr = new StringBuilder();

            foreach (NosologyClass nosologyInfo in _nosologyList)
            {
                nosologysStr.Append(
                    "Id=" + nosologyInfo.Id + DataSplitStr +
                    "LastNameWithInitials=" + nosologyInfo.LastNameWithInitials + ObjSplitStr);
            }

            PackedData(nosologysStr.ToString(), _nosologyPath);
        }

        /// <summary>
        /// ���������� ���������� ��������
        /// </summary>
        private void SaveGlobalSettings()
        {
            string globalSettingsStr =
                "BranchManager=" + _globalSettings.BranchManager + DataSplitStr +
                "DischargeEpicrisisHeaderFileName=" + _globalSettings.DischargeEpicrisisHeaderFileName + DataSplitStr +
                "IsLoggingEnabled=" + _globalSettings.IsLoggingEnabled + DataSplitStr +
                "DepartmentName=" + _globalSettings.DepartmentName;

            PackedData(globalSettingsStr, _globalSettingsPath);
        }

        /// <summary>
        /// ��������� ������ ���������
        /// </summary>
        public void SaveKSG()
        {
            Logger.WriteLog("��������� ������� ������ � " + _dayKSGPath);
            PackedData(DayMKBEngine.PrepareDataToPack(), _dayKSGPath);
            Logger.WriteLog("��������� ������ ������ � " + _nightKSGPath);
            PackedData(NightMKBEngine.PrepareDataToPack(), _nightKSGPath);
            Logger.WriteLog("�� �����������");
        }
        #endregion

        #region ������ ������
        public void GetImportedData(
            string folderPath,
            out List<PatientClass> patients,
            out List<NosologyClass> nosologies)
        {
            string newPatientPath = Path.Combine(folderPath, "patients.save");
            string newNosologyPath = Path.Combine(folderPath, "nosologys.save");

            var foreignPatients = new List<PatientClass>();
            LoadPatients(foreignPatients, newPatientPath);

            var foreignNosologies = new List<NosologyClass>();
            LoadNosologys(foreignNosologies, newNosologyPath);

            patients = new List<PatientClass>();

            foreach (PatientClass foreignPatientInfo in foreignPatients)
            {
                if (GetPatientByGeneralData(foreignPatientInfo.GetFullName(), foreignPatientInfo.DeliveryDate, foreignPatientInfo.Diagnose).Count == 0)
                {
                    patients.Add(foreignPatientInfo);
                }
            }

            nosologies = new List<NosologyClass>();

            foreach (NosologyClass foreignNosologyInfo in foreignNosologies)
            {
                if (GetNosologyByName(foreignNosologyInfo.LastNameWithInitials).Count == 0)
                {
                    nosologies.Add(foreignNosologyInfo);
                }
            }
        }

        /// <summary>
        /// ������������� ���������� ��������� � ���������
        /// </summary>
        /// <param name="foreignPatients">������ ��������� �� ������� ����, ������� ���� �������� � ���� ����</param>
        /// <param name="foreignNosologies">������ ��������� �� ������� ����, ������� ���� �������� � ���� ����</param>
        public void ImportData(List<PatientClass> foreignPatients, List<NosologyClass> foreignNosologies)
        {
            foreach (PatientClass foreignPatientInfo in foreignPatients)
            {
                AddPatient(foreignPatientInfo);
            }

            foreach (NosologyClass foreignNosologyInfo in foreignNosologies)
            {
                AddNosology(foreignNosologyInfo.LastNameWithInitials);
            }
        }
        #endregion

        #region �������� ������
        /// <summary>
        /// ����������� �� ����� ������ � ����������.
        /// ����� ��� ����, ����� �������� ��������� � ���������, 
        /// ���� ��������� ��� �������� �� ���� ���������
        /// </summary>
        public void RefreshPatients()
        {
            LoadPatients(_patientList, _patientPath);
        }

        /// <summary>
        /// ��������� ��� ���� ������
        /// </summary>
        /// <param name="passStr">������ � �����</param>
        /// <returns></returns>
        public static long GetHash(string passStr)
        {
            byte[] arr = Encoding.ASCII.GetBytes(passStr);
            long s = 1;
            int d = 1;

            foreach (byte b in arr)
            {
                s *= b | d;
                d++;
            }

            return s;
        }

        /// <summary>
        /// ��������� ��� ������ 
        /// </summary>
        public void LoadData()
        {
            try
            {
                // ���������� ������ � ������ ������, �� ������ ������
                SaveDbFilesToTempFolder();
            }
            catch (Exception ex)
            {
                MessageBox.Show("�� ������� ����������� ��������� ����������� ������ � �������. ��������� �� ������:\r\n" + ex);
            }

            LoadGlobalSettings();
            LoadPatients(_patientList, _patientPath);
            LoadSurgeons();
            LoadScrubNurses();
            LoadOrderlys();
            LoadHeAnestetists();
            LoadSheAnestethists();
            LoadNosologys(_nosologyList, _nosologyPath);
            LoadKSG();
        }

        /// <summary>
        /// ��������� ����� ��� ������ �� ��������� ����� ������������
        /// ���� ��� � ����
        /// </summary>
        private void SaveDbFilesToTempFolder()
        {
            var di = new DirectoryInfo(_dataPath);
            string saveDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SurgeryHelper");
            string newSaveDataLocation = Path.Combine(saveDataPath, ConvertEngine.GetRightDateString(DateTime.Now));

            if (di.GetFiles().Length == 0 || Directory.Exists(newSaveDataLocation))
            {
                return;
            }

            Directory.CreateDirectory(newSaveDataLocation);

            foreach (FileInfo fi in di.GetFiles())
            {
                fi.CopyTo(Path.Combine(newSaveDataLocation, fi.Name));
            }

            // �������� ������ ����� � ����������� ������� (�������� ������ 10 ���������)
            di = new DirectoryInfo(saveDataPath);
            var savedFolders = new List<DateTime>();
            foreach (DirectoryInfo curDi in di.GetDirectories())
            {
                try
                {
                    savedFolders.Add(ConvertEngine.GetDateTimeFromString(curDi.Name));
                }
                catch
                {
                }
            }

            savedFolders.Sort();

            for (int i = 0; i < savedFolders.Count - 10; i++)
            {
                Directory.Delete(Path.Combine(saveDataPath, ConvertEngine.GetRightDateString(savedFolders[i])), true);
            }
        }

        /// <summary>
        /// ��������� ������ ���������
        /// </summary>
        /// <param name="patientList">������ � ����������, � ������� ���� ��������� ���������</param>
        /// <param name="patientPath">���� �� ����� � ����������</param>
        private void LoadPatients(List<PatientClass> patientList, string patientPath)
        {
            if (GetHash(PassStr) != ConfigEngine.InternalData)
            {
                Environment.Exit(0);
            }

            patientList.Clear();
            string allDataStr = GetPackedData(patientPath);

            // �������� ����� ��������
            string[] objectsStr = allDataStr.Split(new[] { ObjSplitStr }, StringSplitOptions.RemoveEmptyEntries);

            // �������� �� ���� ��������
            foreach (string objectStr in objectsStr)
            {
                // ��� ������� ������� �������� ������ �������� � ������ ��������
                string[] patientOperationsArray = objectStr.Split(new[] { OperationSplitStr }, StringSplitOptions.RemoveEmptyEntries);

                // ��� ������� ������� � ������� �������� �������� ������ ��������
                string[] datasStr = patientOperationsArray[0].Split(new[] { DataSplitStr }, StringSplitOptions.RemoveEmptyEntries);
                var patientInfo = new PatientClass();

                foreach (string dataStr in datasStr)
                {
                    string[] keyValue = dataStr.Split(new[] { '=' }, 2);
                    switch (keyValue[0])
                    {
                        case "Age":
                            patientInfo.Age = Convert.ToInt32(keyValue[1]);
                            break;
                        case "Birthday":
                            if (string.IsNullOrEmpty(keyValue[1]))
                            {
                                patientInfo.Birthday = null;
                            }
                            else
                            {
                                patientInfo.Birthday = ConvertEngine.GetDateTimeFromString(keyValue[1]);
                            }

                            break;
                        case "BuildingNumber":
                            patientInfo.BuildingNumber = keyValue[1];
                            break;
                        case "CityName":
                            patientInfo.CityName = keyValue[1];
                            break;
                        case "DeliveryDate":
                            patientInfo.DeliveryDate = ConvertEngine.GetDateTimeFromString(keyValue[1]);
                            break;
                        case "Diagnose":
                            patientInfo.Diagnose = keyValue[1];
                            break;
                        case "DoctorInChargeOfTheCase":
                            patientInfo.DoctorInChargeOfTheCase = keyValue[1];
                            break;
                        case "FlatNumber":
                            patientInfo.FlatNumber = keyValue[1];
                            break;
                        case "WorkPlace":
                            patientInfo.WorkPlace = keyValue[1];
                            break;
                        case "HomeNumber":
                            patientInfo.HomeNumber = keyValue[1];
                            break;
                        case "Id":
                            patientInfo.Id = Convert.ToInt32(keyValue[1]);
                            break;
                        case "LastName":
                            patientInfo.LastName = keyValue[1];
                            break;
                        case "Name":
                            patientInfo.Name = keyValue[1];
                            break;
                        case "Nosology":
                            patientInfo.Nosology = keyValue[1];
                            break;
                        case "NumberOfCaseHistory":
                            patientInfo.NumberOfCaseHistory = keyValue[1];
                            break;
                        case "Patronymic":
                            patientInfo.Patronymic = keyValue[1];
                            break;
                        case "Phone":
                            patientInfo.Phone = keyValue[1];
                            break;
                        case "TypeOfKSG":
                            patientInfo.TypeOfKSG = keyValue[1];
                            break;
                        case "MKB":
                            patientInfo.MKB = keyValue[1];
                            break;
                        case "KSG":
                            patientInfo.KSG = keyValue[1];
                            break;
                        case "ReleaseDate":
                            if (string.IsNullOrEmpty(keyValue[1]))
                            {
                                patientInfo.ReleaseDate = null;
                            }
                            else
                            {
                                patientInfo.ReleaseDate = ConvertEngine.GetDateTimeFromString(keyValue[1]);
                            }

                            break;
                        case "StreetName":
                            patientInfo.StreetName = keyValue[1];
                            break;
                        case "PrivateFolder":
                            patientInfo.PrivateFolder = keyValue[1];
                            break;
                        case "TransferEpicrisAfterOperationPeriod":
                            patientInfo.TransferEpicrisAfterOperationPeriod = keyValue[1];
                            break;
                        case "TransferEpicrisPlan":
                            patientInfo.TransferEpicrisPlan = keyValue[1];
                            break;
                        case "TransferEpicrisWritingDate":
                            patientInfo.TransferEpicrisWritingDate = ConvertEngine.GetDateTimeFromString(keyValue[1]);
                            break;
                        case "TransferEpicrisAdditionalInfo":
                            patientInfo.TransferEpicrisAdditionalInfo = keyValue[1];
                            break;
                        case "TransferEpicrisDisabilityList":
                            patientInfo.TransferEpicrisDisabilityList = keyValue[1];
                            break;
                        case "TransferEpicrisIsIncludeDisabilityList":
                            patientInfo.TransferEpicrisIsIncludeDisabilityList = Convert.ToBoolean(keyValue[1]);
                            break;
                        case "LineOfCommEpicrisAdditionalInfo":
                            patientInfo.LineOfCommEpicrisAdditionalInfo = keyValue[1];
                            break;
                        case "LineOfCommEpicrisPlan":
                            patientInfo.LineOfCommEpicrisPlan = keyValue[1];
                            break;
                        case "LineOfCommEpicrisWritingDate":
                            patientInfo.LineOfCommEpicrisWritingDate = ConvertEngine.GetDateTimeFromString(keyValue[1]);
                            break;
                        case "DischargeEpicrisAfterOperation":
                            patientInfo.DischargeEpicrisAfterOperation = keyValue[1];
                            break;
                        case "DischargeEpicrisConservativeTherapy":
                            patientInfo.DischargeEpicrisConservativeTherapy = keyValue[1];
                            break;
                        case "DischargeEpicrisEkg":
                            patientInfo.DischargeEpicrisEkg = keyValue[1];
                            break;
                        case "DischargeEpicrisOakEritrocits":
                            patientInfo.DischargeEpicrisOakEritrocits = keyValue[1];
                            break;
                        case "DischargeEpicrisOakHb":
                            patientInfo.DischargeEpicrisOakHb = keyValue[1];
                            break;
                        case "DischargeEpicrisOakLekocits":
                            patientInfo.DischargeEpicrisOakLekocits = keyValue[1];
                            break;
                        case "DischargeEpicrisOakSoe":
                            patientInfo.DischargeEpicrisOakSoe = keyValue[1];
                            break;
                        case "DischargeEpicrisOamColor":
                            patientInfo.DischargeEpicrisOamColor = keyValue[1];
                            break;
                        case "DischargeEpicrisOamDensity":
                            patientInfo.DischargeEpicrisOamDensity = keyValue[1];
                            break;
                        case "DischargeEpicrisOamEritrocits":
                            patientInfo.DischargeEpicrisOamEritrocits = keyValue[1];
                            break;
                        case "DischargeEpicrisOamLekocits":
                            patientInfo.DischargeEpicrisOamLekocits = keyValue[1];
                            break;
                        case "DischargeEpicrisBakBillirubin":
                            patientInfo.DischargeEpicrisBakBillirubin = keyValue[1];
                            break;
                        case "DischargeEpicrisBakGeneralProtein":
                            patientInfo.DischargeEpicrisBakGeneralProtein = keyValue[1];
                            break;
                        case "DischargeEpicrisBakPTI":
                            patientInfo.DischargeEpicrisBakPTI = keyValue[1];
                            break;
                        case "DischargeEpicrisBakSugar":
                            patientInfo.DischargeEpicrisBakSugar = keyValue[1];
                            break;
                        case "DischargeEpicrisAdditionalAnalises":
                            patientInfo.DischargeEpicrisAdditionalAnalises = keyValue[1];
                            break;
                        case "DischargeEpicrisRecomendations":
                            patientInfo.DischargeEpicrisRecomendations = ConvertEngine.StringToList(keyValue[1]);
                            break;
                        case "DischargeEpicrisAdditionalRecomendations":
                            patientInfo.DischargeEpicrisAdditionalRecomendations = ConvertEngine.StringToList(keyValue[1]);
                            break;
                        case "TreatmentPlanInspection":
                            patientInfo.TreatmentPlanInspection = keyValue[1];
                            break;
                        case "TreatmentPlanDate":
                            patientInfo.TreatmentPlanDate = ConvertEngine.GetDateTimeFromString(keyValue[1]);
                            break;
                        case "IsTreatmentPlanActiveInOperationProtocol":
                            patientInfo.IsTreatmentPlanActiveInOperationProtocol = Convert.ToBoolean(keyValue[1]);
                            break;
                        case "MedicalInspectionAnamneseTraumaDate":
                            if (!string.IsNullOrEmpty(keyValue[1]))
                            {
                                patientInfo.MedicalInspectionAnamneseTraumaDate = ConvertEngine.GetDateTimeFromString(keyValue[1]);
                            }
                            else
                            {
                                patientInfo.MedicalInspectionAnamneseTraumaDate = null;
                            }

                            break;
                        case "MedicalInspectionAnamneseAnMorbi":
                            patientInfo.MedicalInspectionAnamneseAnMorbi = keyValue[1];
                            break;
                        case "MedicalInspectionAnamneseAnVitae":
                            patientInfo.MedicalInspectionAnamneseAnVitae = ConvertEngine.StringToArrayBool(keyValue[1]);
                            break;
                        case "MedicalInspectionAnamneseCheckboxes":
                            patientInfo.MedicalInspectionAnamneseCheckboxes = ConvertEngine.StringToArrayBool(keyValue[1]);
                            break;
                        case "MedicalInspectionAnamneseTextBoxes":
                            patientInfo.MedicalInspectionAnamneseTextBoxes = ConvertEngine.StringToArray(keyValue[1]);
                            break;
                        case "MedicalInspectionComplaints":
                            patientInfo.MedicalInspectionComplaints = keyValue[1];
                            break;
                        case "MedicalInspectionExpertAnamnese":
                            patientInfo.MedicalInspectionExpertAnamnese = Convert.ToInt32(keyValue[1]);
                            break;
                        case "MedicalInspectionInspectionPlan":
                            patientInfo.MedicalInspectionInspectionPlan = keyValue[1];
                            break;
                        case "MedicalInspectionStLocalisDescription":
                            patientInfo.MedicalInspectionStLocalisDescription = keyValue[1];
                            break;
                        case "MedicalInspectionStLocalisRentgen":
                            patientInfo.MedicalInspectionStLocalisRentgen = keyValue[1];
                            break;
                        case "MedicalInspectionIsAnamneseActive":
                            patientInfo.MedicalInspectionIsAnamneseActive = Convert.ToBoolean(keyValue[1]);
                            break;
                        case "MedicalInspectionIsPlanEnabled":
                            patientInfo.MedicalInspectionIsPlanEnabled = Convert.ToBoolean(keyValue[1]);
                            break;
                        case "MedicalInspectionIsStLocalisPart1Enabled":
                            patientInfo.MedicalInspectionIsStLocalisPart1Enabled = Convert.ToBoolean(keyValue[1]);
                            break;
                        case "MedicalInspectionIsStLocalisPart2Enabled":
                            patientInfo.MedicalInspectionIsStLocalisPart2Enabled = Convert.ToBoolean(keyValue[1]);
                            break;
                        case "MedicalInspectionLnFirstDateStart":
                            patientInfo.MedicalInspectionLnFirstDateStart = ConvertEngine.GetDateTimeFromString(keyValue[1]);
                            break;
                        case "MedicalInspectionLnWithNumberDateEnd":
                            patientInfo.MedicalInspectionLnWithNumberDateEnd = ConvertEngine.GetDateTimeFromString(keyValue[1]);
                            break;
                        case "MedicalInspectionLnWithNumberDateStart":
                            patientInfo.MedicalInspectionLnWithNumberDateStart = ConvertEngine.GetDateTimeFromString(keyValue[1]);
                            break;
                        case "MedicalInspectionStLocalisPart1Fields":
                            patientInfo.MedicalInspectionStLocalisPart1Fields = ConvertEngine.StringToArray(keyValue[1]);
                            break;
                        case "MedicalInspectionStLocalisPart1OppositionFinger":
                            patientInfo.MedicalInspectionStLocalisPart1OppositionFinger = keyValue[1];
                            break;
                        case "MedicalInspectionStLocalisPart2ComboBoxes":
                            patientInfo.MedicalInspectionStLocalisPart2ComboBoxes = ConvertEngine.StringToArray(keyValue[1]);
                            break;
                        case "MedicalInspectionStLocalisPart2LeftHand":
                            patientInfo.MedicalInspectionStLocalisPart2LeftHand = ConvertEngine.StringToArray(keyValue[1]);
                            break;
                        case "MedicalInspectionStLocalisPart2NumericUpDown":
                            patientInfo.MedicalInspectionStLocalisPart2NumericUpDown = Convert.ToInt32(keyValue[1]);
                            break;
                        case "MedicalInspectionStLocalisPart2RightHand":
                            patientInfo.MedicalInspectionStLocalisPart2RightHand = ConvertEngine.StringToArray(keyValue[1]);
                            break;
                        case "MedicalInspectionStLocalisPart2TextBoxes":
                            patientInfo.MedicalInspectionStLocalisPart2TextBoxes = ConvertEngine.StringToArray(keyValue[1]);
                            break;
                        case "MedicalInspectionStLocalisPart2WhichHand":
                            patientInfo.MedicalInspectionStLocalisPart2WhichHand = keyValue[1];
                            break;
                        case "MedicalInspectionStPraesensComboBoxes":
                            patientInfo.MedicalInspectionStPraesensComboBoxes = ConvertEngine.StringToArray(keyValue[1]);
                            break;
                        case "MedicalInspectionStPraesensNumericUpDowns":
                            patientInfo.MedicalInspectionStPraesensNumericUpDowns = ConvertEngine.StringToArrayInt(keyValue[1]);
                            break;
                        case "MedicalInspectionStPraesensOthers":
                            patientInfo.MedicalInspectionStPraesensOthers = keyValue[1];
                            break;
                        case "MedicalInspectionStPraesensTextBoxes":
                            patientInfo.MedicalInspectionStPraesensTextBoxes = ConvertEngine.StringToArray(keyValue[1]);
                            break;
                        case "MedicalInspectionTeoRisk":
                            patientInfo.MedicalInspectionTeoRisk = keyValue[1];
                            break;
                    }
                }

                if (GetHash(PassStr) != ConfigEngine.InternalData)
                {
                    Environment.Exit(0);
                }

                for (int i = 1; i < patientOperationsArray.Length; i++)
                {
                    var operationInfo = new OperationClass();

                    // ��� ������� ������� � ������� �������� �������� ������ ��������
                    datasStr = patientOperationsArray[i].Split(new[] { DataSplitStr }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string dataStr in datasStr)
                    {
                        string[] keyValue = dataStr.Split(new[] { '=' }, 2);
                        switch (keyValue[0])
                        {
                            case "Assistents":
                                operationInfo.Assistents = ConvertEngine.StringToList(keyValue[1]);
                                break;
                            case "HeAnaesthetist":
                                operationInfo.HeAnaesthetist = keyValue[1];
                                break;
                            case "Id":
                                operationInfo.Id = Convert.ToInt32(keyValue[1]);
                                break;
                            case "Name":
                                operationInfo.Name = keyValue[1];
                                break;
                            case "OperationCourse":
                                operationInfo.OperationCourse = keyValue[1];
                                break;
                            case "DataOfOperation":
                                operationInfo.DataOfOperation = ConvertEngine.GetDateTimeFromString(keyValue[1]);
                                break;
                            case "StartTimeOfOperation":
                                operationInfo.StartTimeOfOperation = ConvertEngine.GetDateTimeFromString(keyValue[1]);
                                break;
                            case "EndTimeOfOperation":
                                operationInfo.EndTimeOfOperation = ConvertEngine.GetDateTimeFromString(keyValue[1]);
                                break;
                            case "Orderly":
                                operationInfo.Orderly = keyValue[1];
                                break;
                            case "ScrubNurse":
                                operationInfo.ScrubNurse = keyValue[1];
                                break;
                            case "SheAnaesthetist":
                                operationInfo.SheAnaesthetist = keyValue[1];
                                break;
                            case "Surgeons":
                                operationInfo.Surgeons = ConvertEngine.StringToList(keyValue[1]);
                                break;
                            case "BeforeOperationEpicrisisADFirst":
                                operationInfo.BeforeOperationEpicrisisADFirst = Convert.ToInt32(keyValue[1]);
                                break;
                            case "BeforeOperationEpicrisisADSecond":
                                operationInfo.BeforeOperationEpicrisisADSecond = Convert.ToInt32(keyValue[1]);
                                break;
                            case "BeforeOperationEpicrisisBreath":
                                operationInfo.BeforeOperationEpicrisisBreath = keyValue[1];
                                break;
                            case "BeforeOperationEpicrisisChDD":
                                operationInfo.BeforeOperationEpicrisisChDD = Convert.ToInt32(keyValue[1]);
                                break;
                            case "BeforeOperationEpicrisisComplaints":
                                operationInfo.BeforeOperationEpicrisisComplaints = keyValue[1];
                                break;
                            case "BeforeOperationEpicrisisState":
                                operationInfo.BeforeOperationEpicrisisState = keyValue[1];
                                break;
                            case "BeforeOperationEpicrisisHeartRhythm":
                                operationInfo.BeforeOperationEpicrisisHeartRhythm = keyValue[1];
                                break;
                            case "BeforeOperationEpicrisisHeartSounds":
                                operationInfo.BeforeOperationEpicrisisHeartSounds = keyValue[1];
                                break;
                            case "BeforeOperationEpicrisisIsDairyEnabled":
                                operationInfo.BeforeOperationEpicrisisIsDairyEnabled = Convert.ToBoolean(keyValue[1]);
                                break;
                            case "BeforeOperationEpicrisisPulse":
                                operationInfo.BeforeOperationEpicrisisPulse = Convert.ToInt32(keyValue[1]);
                                break;
                            case "BeforeOperationEpicrisisStLocalis":
                                operationInfo.BeforeOperationEpicrisisStLocalis = keyValue[1];
                                break;
                            case "BeforeOperationEpicrisisStomach":
                                operationInfo.BeforeOperationEpicrisisStomach = keyValue[1];
                                break;
                            case "BeforeOperationEpicrisisStool":
                                operationInfo.BeforeOperationEpicrisisStool = keyValue[1];
                                break;
                            case "BeforeOperationEpicrisisTemperature":
                                operationInfo.BeforeOperationEpicrisisTemperature = keyValue[1];
                                break;
                            case "BeforeOperationEpicrisisTimeWriting":
                                operationInfo.BeforeOperationEpicrisisTimeWriting = ConvertEngine.GetDateTimeFromString(keyValue[1]);
                                break;
                            case "BeforeOperationEpicrisisUrination":
                                operationInfo.BeforeOperationEpicrisisUrination = keyValue[1];
                                break;
                            case "BeforeOperationEpicrisisWheeze":
                                operationInfo.BeforeOperationEpicrisisWheeze = keyValue[1];
                                break;
                        }
                    }

                    patientInfo.Operations.Add(operationInfo);
                }

                patientList.Add(patientInfo);
            }
        }

        /// <summary>
        /// ��������� ������ ��������
        /// </summary>
        private void LoadSurgeons()
        {
            if (GetHash(PassStr) != ConfigEngine.InternalData)
            {
                Environment.Exit(0);
            }

            _surgeonList = new List<SurgeonClass>();
            string allDataStr = GetPackedData(_surgeonPath);

            // �������� ����� ��������
            string[] objectsStr = allDataStr.Split(new[] { ObjSplitStr }, StringSplitOptions.RemoveEmptyEntries);

            // �������� �� ���� ��������
            foreach (string objectStr in objectsStr)
            {
                // ��� ������� ������� �������� ������ ��������
                string[] datasStr = objectStr.Split(new[] { DataSplitStr }, StringSplitOptions.RemoveEmptyEntries);

                var surgeonInfo = new SurgeonClass();
                foreach (string dataStr in datasStr)
                {
                    string[] keyValue = dataStr.Split(new[] { '=' }, 2);
                    switch (keyValue[0])
                    {
                        case "Id":
                            surgeonInfo.Id = Convert.ToInt32(keyValue[1]);
                            break;
                        case "LastNameWithInitials":
                            surgeonInfo.LastNameWithInitials = keyValue[1];
                            break;
                    }
                }

                _surgeonList.Add(surgeonInfo);
            }
        }

        /// <summary>
        /// ��������� ������ ������������ �����
        /// </summary>
        private void LoadScrubNurses()
        {
            if (GetHash(PassStr) != ConfigEngine.InternalData)
            {
                Environment.Exit(0);
            }

            _scrubNurseList = new List<ScrubNurseClass>();
            string allDataStr = GetPackedData(_scrubNursePath);

            // �������� ����� ��������
            string[] objectsStr = allDataStr.Split(new[] { ObjSplitStr }, StringSplitOptions.RemoveEmptyEntries);

            // �������� �� ���� ��������
            foreach (string objectStr in objectsStr)
            {
                // ��� ������� ������� �������� ������ ��������
                string[] datasStr = objectStr.Split(new[] { DataSplitStr }, StringSplitOptions.RemoveEmptyEntries);

                var scrubNurseInfo = new ScrubNurseClass();
                foreach (string dataStr in datasStr)
                {
                    string[] keyValue = dataStr.Split(new[] { '=' }, 2);
                    switch (keyValue[0])
                    {
                        case "Id":
                            scrubNurseInfo.Id = Convert.ToInt32(keyValue[1]);
                            break;
                        case "LastNameWithInitials":
                            scrubNurseInfo.LastNameWithInitials = keyValue[1];
                            break;
                    }
                }

                _scrubNurseList.Add(scrubNurseInfo);
            }
        }

        /// <summary>
        /// ��������� ������ ���������
        /// </summary>
        private void LoadOrderlys()
        {
            if (GetHash(PassStr) != ConfigEngine.InternalData)
            {
                Environment.Exit(0);
            }

            _orderlyList = new List<OrderlyClass>();
            string allDataStr = GetPackedData(_orderlyPath);

            // �������� ����� ��������
            string[] objectsStr = allDataStr.Split(new[] { ObjSplitStr }, StringSplitOptions.RemoveEmptyEntries);

            // �������� �� ���� ��������
            foreach (string objectStr in objectsStr)
            {
                // ��� ������� ������� �������� ������ ��������
                string[] datasStr = objectStr.Split(new[] { DataSplitStr }, StringSplitOptions.RemoveEmptyEntries);

                var orderlyInfo = new OrderlyClass();
                foreach (string dataStr in datasStr)
                {
                    string[] keyValue = dataStr.Split(new[] { '=' }, 2);
                    switch (keyValue[0])
                    {
                        case "Id":
                            orderlyInfo.Id = Convert.ToInt32(keyValue[1]);
                            break;
                        case "LastNameWithInitials":
                            orderlyInfo.LastNameWithInitials = keyValue[1];
                            break;
                    }
                }

                _orderlyList.Add(orderlyInfo);
            }
        }

        /// <summary>
        /// ��������� ������ ��������������
        /// </summary>
        private void LoadHeAnestetists()
        {
            if (GetHash(PassStr) != ConfigEngine.InternalData)
            {
                Environment.Exit(0);
            }

            _heAnestethistList = new List<HeAnestethistClass>();
            string allDataStr = GetPackedData(_heAnestethistPath);

            // �������� ����� ��������
            string[] objectsStr = allDataStr.Split(new[] { ObjSplitStr }, StringSplitOptions.RemoveEmptyEntries);

            // �������� �� ���� ��������
            foreach (string objectStr in objectsStr)
            {
                // ��� ������� ������� �������� ������ ��������
                string[] datasStr = objectStr.Split(new[] { DataSplitStr }, StringSplitOptions.RemoveEmptyEntries);

                var heAnestetistInfo = new HeAnestethistClass();
                foreach (string dataStr in datasStr)
                {
                    string[] keyValue = dataStr.Split(new[] { '=' }, 2);
                    switch (keyValue[0])
                    {
                        case "Id":
                            heAnestetistInfo.Id = Convert.ToInt32(keyValue[1]);
                            break;
                        case "LastNameWithInitials":
                            heAnestetistInfo.LastNameWithInitials = keyValue[1];
                            break;
                    }
                }

                _heAnestethistList.Add(heAnestetistInfo);
            }
        }

        /// <summary>
        /// ��������� ������ ������������
        /// </summary>
        private void LoadSheAnestethists()
        {
            if (GetHash(PassStr) != ConfigEngine.InternalData)
            {
                Environment.Exit(0);
            }

            _sheAnestethistList = new List<SheAnestethistClass>();
            string allDataStr = GetPackedData(_sheAnestethistPath);

            // �������� ����� ��������
            string[] objectsStr = allDataStr.Split(new[] { ObjSplitStr }, StringSplitOptions.RemoveEmptyEntries);

            // �������� �� ���� ��������
            foreach (string objectStr in objectsStr)
            {
                // ��� ������� ������� �������� ������ ��������
                string[] datasStr = objectStr.Split(new[] { DataSplitStr }, StringSplitOptions.RemoveEmptyEntries);

                var sheAnestethistInfo = new SheAnestethistClass();
                foreach (string dataStr in datasStr)
                {
                    string[] keyValue = dataStr.Split(new[] { '=' }, 2);
                    switch (keyValue[0])
                    {
                        case "Id":
                            sheAnestethistInfo.Id = Convert.ToInt32(keyValue[1]);
                            break;
                        case "LastNameWithInitials":
                            sheAnestethistInfo.LastNameWithInitials = keyValue[1];
                            break;
                    }
                }

                _sheAnestethistList.Add(sheAnestethistInfo);
            }
        }

        /// <summary>
        /// ��������� ������ ���������
        /// </summary>
        /// <param name="nosologyList">������ ���������</param>
        /// <param name="nosologyPath">���� �� ����� � �����������</param>
        private void LoadNosologys(List<NosologyClass> nosologyList, string nosologyPath)
        {
            if (GetHash(PassStr) != ConfigEngine.InternalData)
            {
                Environment.Exit(0);
            }

            nosologyList.Clear();
            string allDataStr = GetPackedData(nosologyPath);

            // �������� ����� ��������
            string[] objectsStr = allDataStr.Split(new[] { ObjSplitStr }, StringSplitOptions.RemoveEmptyEntries);

            // �������� �� ���� ��������
            foreach (string objectStr in objectsStr)
            {
                // ��� ������� ������� �������� ������ ��������
                string[] datasStr = objectStr.Split(new[] { DataSplitStr }, StringSplitOptions.RemoveEmptyEntries);

                var nosologyInfo = new NosologyClass();
                foreach (string dataStr in datasStr)
                {
                    string[] keyValue = dataStr.Split(new[] { '=' }, 2);
                    switch (keyValue[0])
                    {
                        case "Id":
                            nosologyInfo.Id = Convert.ToInt32(keyValue[1]);
                            break;
                        case "LastNameWithInitials":
                            nosologyInfo.LastNameWithInitials = keyValue[1];
                            break;
                    }
                }

                nosologyList.Add(nosologyInfo);
            }
        }

        /// <summary>
        /// ��������� ���������� ���������
        /// </summary>
        private void LoadGlobalSettings()
        {
            if (GetHash(PassStr) != ConfigEngine.InternalData)
            {
                Environment.Exit(0);
            }

            string allDataStr = GetPackedData(_globalSettingsPath);

            // ��� ������� ������� �������� ������ ��������
            string[] datasStr = allDataStr.Split(new[] { DataSplitStr }, StringSplitOptions.RemoveEmptyEntries);

            _globalSettings = new GlobalSettingsClass();
            foreach (string dataStr in datasStr)
            {
                string[] keyValue = dataStr.Split(new[] { '=' }, 2);
                switch (keyValue[0])
                {
                    case "BranchManager":
                        _globalSettings.BranchManager = keyValue[1];
                        break;
                    case "DischargeEpicrisisHeaderFileName":
                        _globalSettings.DischargeEpicrisisHeaderFileName = keyValue[1];
                        break;
                    case "DepartmentName":
                        _globalSettings.DepartmentName = keyValue[1];
                        break;
                    case "IsLoggingEnabled":
                        _globalSettings.IsLoggingEnabled = Convert.ToBoolean(keyValue[1]);
                        break;
                }
            }

            Logger = new LoggerEngine(_globalSettings);
        }

        /// <summary>
        /// ��������� ������� � ������ ���� KSG
        /// </summary>
        private void LoadKSG()
        {
            if (GetHash(PassStr) != ConfigEngine.InternalData)
            {
                Environment.Exit(0);
            }

            DayMKBEngine = new MKBEngine(GetPackedData(_dayKSGPath));
            NightMKBEngine = new MKBEngine(GetPackedData(_nightKSGPath));
        }

        #endregion

        #region ����������� �������

        /// <summary>
        /// �������� ����� ��� ��������� ������ � ����
        /// </summary>
        /// <param name="dataStr">������ � �������</param>
        /// <param name="path">���� �� �����</param>
        private static void PackedData(string dataStr, string path)
        {
            var newThread = new Thread(PackedDataRequest);
            newThread.Start(new[] { dataStr, path });
        }

        /// <summary>
        /// ������������ ������ � ����
        /// </summary>
        /// <param name="parameters">������ string �� ������� � ������� � ���� �� �����</param>
        private static void PackedDataRequest(object parameters)
        {
            string path = string.Empty;
            try
            {
                string dataStr = ((string[])parameters)[0];
                path = ((string[])parameters)[1];

                byte[] bytesBuffer = Encoding.GetEncoding("windows-1251").GetBytes(dataStr);
                byte[] rez = PackXml(bytesBuffer);

                using (var fs = new FileStream(path, FileMode.Create))
                {
                    fs.Write(rez, 0, rez.Length);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("������ ��� ���������� ����� " + path + ":\r\n" + ex);
            }
        }

        /// <summary>
        /// �������� ����� �� ������������� �����
        /// </summary>
        /// <param name="path">���� �� �����</param>
        /// <returns></returns>
        private static string GetPackedData(string path)
        {
            string unpackedString = string.Empty;
            try
            {
                if (!File.Exists(path))
                {
                    return unpackedString;
                }

                byte[] bytesBuffer;
                using (var fs = new FileStream(path, FileMode.Open))
                {
                    bytesBuffer = new byte[fs.Length];
                    fs.Read(bytesBuffer, 0, bytesBuffer.Length);
                }

                byte[] rez = UnpackXml(bytesBuffer);
                if (rez.Length == 0)
                {
                    return unpackedString;
                }

                unpackedString = Encoding.GetEncoding("windows-1251").GetString(rez);

            }
            catch (Exception ex)
            {
                MessageBox.Show("������ ��� ������ ����� " + path + ":\r\n" + ex);
            }

            return unpackedString;
        }
        /*
        /// <summary>
        /// ���������� ������ ����
        /// </summary>
        /// <param name="xmlInfo">������ ��� ���������</param>
        /// <returns></returns>
        private static byte[] PackXml(byte[] xmlInfo)
        {
            var newByffer = new byte[xmlInfo.Length * 10];
            var stream = new MemoryStream(newByffer);
            var zStream = new ZOutputStream(stream, zlibConst.Z_DEFAULT_COMPRESSION);
            zStream.Write(xmlInfo, 0, xmlInfo.Length);
            zStream.Close();

            int i = (xmlInfo.Length * 10) - 1;
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
        /// ����������� ������ ����
        /// </summary>
        /// <param name="xmlInfo">������ ��� ����������</param>
        /// <returns></returns>
        private static byte[] UnpackXml(byte[] xmlInfo)
        {
            var newByffer = new byte[xmlInfo.Length * 100];
            var stream = new MemoryStream(newByffer);
            var zStream = new ZOutputStream(stream);

            try
            {
                zStream.Write(xmlInfo, 0, xmlInfo.Length);
            }
            catch
            {
            }

            zStream.Close();

            int i = (xmlInfo.Length * 100) - 1;
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
        }*/
        
        
        /// <summary>
        /// ���������� ������ ����
        /// </summary>
        /// <param name="xmlInfo">������ ��� ���������</param>
        /// <returns></returns>
        private static byte[] PackXml(byte[] xmlInfo)
        {
            using (var input = new MemoryStream(xmlInfo))
            using (var output = new MemoryStream())
            using (var outZStream = new ZOutputStream(output, zlibConst.Z_DEFAULT_COMPRESSION))
            {
                CopyStream(input, outZStream);
                outZStream.finish();
                output.Seek(0, SeekOrigin.Begin);
                return output.ToArray();
            }
        }

        
        /// <summary>
        /// ����������� ������ ����
        /// </summary>
        /// <param name="xmlInfo">������ ��� ����������</param>
        /// <returns></returns>
        private static byte[] UnpackXml(byte[] xmlInfo)
        {            
            using (var output = new MemoryStream())
            using (var outZStream = new ZOutputStream(output))
            using (var input = new MemoryStream(xmlInfo))
            {
                CopyStream(input, outZStream);
                outZStream.finish();
                return output.ToArray();
            }
        }

        public static void CopyStream(Stream input, ZOutputStream output)
        {
            var buffer = new byte[20000];
            int len;
            while ((len = input.Read(buffer, 0, 20000)) > 0)
            {
                output.Write(buffer, 0, len);
            }
            output.Flush();
        }
        

        #endregion
    }
}
