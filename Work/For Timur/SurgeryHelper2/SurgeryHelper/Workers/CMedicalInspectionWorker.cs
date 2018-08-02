using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using SurgeryHelper.Essences;
using SurgeryHelper.Tools;

namespace SurgeryHelper.Workers
{
    public class CMedicalInspectionWorker : CBaseWorker
    {
        private List<CMedicalInspection> _medicalInspectionList;
        private readonly string _medicalInspectionPath;

        public CMedicalInspectionWorker(string dataPath)
        {
            _medicalInspectionPath = Path.Combine(dataPath, "medical_inspection.save");
            Load();
        }


        /// <summary>
        /// Обновить информацию об осмотре в отделении
        /// </summary>
        /// <param name="medicalInspectionInfo">Информация об осмотре в отделении</param>
        public void Update(CMedicalInspection medicalInspectionInfo)
        {
            int n = GetIndexFromList(medicalInspectionInfo.HospitalizationId);
            medicalInspectionInfo.NotInDatabase = false;
            _medicalInspectionList[n] = new CMedicalInspection(medicalInspectionInfo);
            Save();
        }


        /// <summary>
        /// Удалить осмотр в отделении
        /// </summary>
        /// <param name="hospitalizationId">id госпитализации</param>
        public void Remove(int hospitalizationId)
        {
            int index = 0;
            while (index < _medicalInspectionList.Count)
            {
                if (_medicalInspectionList[index].HospitalizationId == hospitalizationId)
                {
                    _medicalInspectionList.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }

            Save();
        }


        /// <summary>
        /// Сохранить список осмотров в отделении
        /// </summary>
        private void Save()
        {
            var medicalInspectionsStr = new StringBuilder();

            foreach (CMedicalInspection medicalInspectionInfo in _medicalInspectionList)
            {
                medicalInspectionsStr.Append(
                    "HospitalizationId=" + medicalInspectionInfo.HospitalizationId + DataSplitStr +
                    "AnamneseAnMorbi=" + medicalInspectionInfo.AnamneseAnMorbi + DataSplitStr +
                    "StLocalisDescription=" + medicalInspectionInfo.StLocalisDescription + DataSplitStr +
                    "StLocalisRentgen=" + medicalInspectionInfo.StLocalisRentgen + DataSplitStr +
                    "AnamneseAnVitae=" + CConvertEngine.ListToString(medicalInspectionInfo.AnamneseAnVitae) + DataSplitStr +
                    "AnamneseCheckboxes=" + CConvertEngine.ListToString(medicalInspectionInfo.AnamneseCheckboxes) + DataSplitStr +
                    "AnamneseTextBoxes=" + CConvertEngine.ListToString(medicalInspectionInfo.AnamneseTextBoxes) + DataSplitStr +
                    "Complaints=" + medicalInspectionInfo.Complaints + DataSplitStr +
                    "ExpertAnamnese=" + medicalInspectionInfo.ExpertAnamnese + DataSplitStr +
                    "InspectionPlan=" + medicalInspectionInfo.InspectionPlan + DataSplitStr +
                    "IsAnamneseActive=" + medicalInspectionInfo.IsAnamneseActive + DataSplitStr +
                    "IsPlanEnabled=" + medicalInspectionInfo.IsPlanEnabled + DataSplitStr +
                    "IsStLocalisPart1Enabled=" + medicalInspectionInfo.IsStLocalisPart1Enabled + DataSplitStr +
                    "IsStLocalisPart2Enabled=" + medicalInspectionInfo.IsStLocalisPart2Enabled + DataSplitStr +
                    "LnFirstDateStart=" + CConvertEngine.DateTimeToString(medicalInspectionInfo.LnFirstDateStart) + DataSplitStr +
                    "LnWithNumberDateEnd=" + CConvertEngine.DateTimeToString(medicalInspectionInfo.LnWithNumberDateEnd) + DataSplitStr +
                    "LnWithNumberDateStart=" + CConvertEngine.DateTimeToString(medicalInspectionInfo.LnWithNumberDateStart) + DataSplitStr +
                    "StLocalisPart1Fields=" + CConvertEngine.ListToString(medicalInspectionInfo.StLocalisPart1Fields) + DataSplitStr +
                    "StLocalisPart1OppositionFinger=" + medicalInspectionInfo.StLocalisPart1OppositionFinger + DataSplitStr +
                    "StLocalisPart2ComboBoxes=" + CConvertEngine.ListToString(medicalInspectionInfo.StLocalisPart2ComboBoxes) + DataSplitStr +
                    "StLocalisPart2LeftHand=" + CConvertEngine.ListToString(medicalInspectionInfo.StLocalisPart2LeftHand) + DataSplitStr +
                    "StLocalisPart2NumericUpDown=" + medicalInspectionInfo.StLocalisPart2NumericUpDown + DataSplitStr +
                    "StLocalisPart2RightHand=" + CConvertEngine.ListToString(medicalInspectionInfo.StLocalisPart2RightHand) + DataSplitStr +
                    "StLocalisPart2TextBoxes=" + CConvertEngine.ListToString(medicalInspectionInfo.StLocalisPart2TextBoxes) + DataSplitStr +
                    "StLocalisPart2WhichHand=" + medicalInspectionInfo.StLocalisPart2WhichHand + DataSplitStr +
                    "StPraesensComboBoxes=" + CConvertEngine.ListToString(medicalInspectionInfo.StPraesensComboBoxes) + DataSplitStr +
                    "StPraesensNumericUpDowns=" + CConvertEngine.ListToString(medicalInspectionInfo.StPraesensNumericUpDowns) + DataSplitStr +
                    "StPraesensTextBoxes=" + CConvertEngine.ListToString(medicalInspectionInfo.StPraesensTextBoxes) + DataSplitStr +
                    "TeoRisk=" + medicalInspectionInfo.TeoRisk + ObjSplitStr);
            }

            CDatabaseEngine.PackText(medicalInspectionsStr.ToString(), _medicalInspectionPath);
        }


        /// <summary>
        /// Загрузить список осмотров в отделении
        /// </summary>
        private void Load()
        {
            _medicalInspectionList = new List<CMedicalInspection>();
            string allDataStr = CDatabaseEngine.UnpackText(_medicalInspectionPath);

            // Получаем набор объектов
            string[] objectsStr = allDataStr.Split(new[] { ObjSplitStr }, StringSplitOptions.RemoveEmptyEntries);

            // Проходим по всем объектам
            foreach (string objectStr in objectsStr)
            {
                // Для каждого объекта получаем список значений
                string[] datasStr = objectStr.Split(new[] { DataSplitStr }, StringSplitOptions.RemoveEmptyEntries);

                var medicalInspectionInfo = new CMedicalInspection();
                foreach (string dataStr in datasStr)
                {
                    string[] keyValue = dataStr.Split(new[] { '=' }, 2);
                    switch (keyValue[0])
                    {
                        case "HospitalizationId":
                            medicalInspectionInfo.HospitalizationId = Convert.ToInt32(keyValue[1]);
                            break;
                        case "AnamneseAnMorbi":
                            medicalInspectionInfo.AnamneseAnMorbi = keyValue[1];
                            break;
                        case "AnamneseAnVitae":
                            medicalInspectionInfo.AnamneseAnVitae = CConvertEngine.StringToBoolArray(keyValue[1]);
                            break;
                        case "AnamneseCheckboxes":
                            medicalInspectionInfo.AnamneseCheckboxes = CConvertEngine.StringToBoolArray(keyValue[1]);
                            break;
                        case "AnamneseTextBoxes":
                            medicalInspectionInfo.AnamneseTextBoxes = CConvertEngine.StringToStringArray(keyValue[1]);
                            break;
                        case "Complaints":
                            medicalInspectionInfo.Complaints = keyValue[1];
                            break;
                        case "ExpertAnamnese":
                            medicalInspectionInfo.ExpertAnamnese = Convert.ToInt32(keyValue[1]);
                            break;
                        case "InspectionPlan":
                            medicalInspectionInfo.InspectionPlan = keyValue[1];
                            break;
                        case "StLocalisDescription":
                            medicalInspectionInfo.StLocalisDescription = keyValue[1];
                            break;
                        case "StLocalisRentgen":
                            medicalInspectionInfo.StLocalisRentgen = keyValue[1];
                            break;
                        case "IsAnamneseActive":
                            medicalInspectionInfo.IsAnamneseActive = Convert.ToBoolean(keyValue[1]);
                            break;
                        case "IsPlanEnabled":
                            medicalInspectionInfo.IsPlanEnabled = Convert.ToBoolean(keyValue[1]);
                            break;
                        case "IsStLocalisPart1Enabled":
                            medicalInspectionInfo.IsStLocalisPart1Enabled = Convert.ToBoolean(keyValue[1]);
                            break;
                        case "IsStLocalisPart2Enabled":
                            medicalInspectionInfo.IsStLocalisPart2Enabled = Convert.ToBoolean(keyValue[1]);
                            break;
                        case "LnFirstDateStart":
                            medicalInspectionInfo.LnFirstDateStart = CConvertEngine.StringToDateTime(keyValue[1]);
                            break;
                        case "LnWithNumberDateEnd":
                            medicalInspectionInfo.LnWithNumberDateEnd = CConvertEngine.StringToDateTime(keyValue[1]);
                            break;
                        case "LnWithNumberDateStart":
                            medicalInspectionInfo.LnWithNumberDateStart = CConvertEngine.StringToDateTime(keyValue[1]);
                            break;
                        case "StLocalisPart1Fields":
                            medicalInspectionInfo.StLocalisPart1Fields = CConvertEngine.StringToStringArray(keyValue[1]);
                            break;
                        case "StLocalisPart1OppositionFinger":
                            medicalInspectionInfo.StLocalisPart1OppositionFinger = keyValue[1];
                            break;
                        case "StLocalisPart2ComboBoxes":
                            medicalInspectionInfo.StLocalisPart2ComboBoxes = CConvertEngine.StringToStringArray(keyValue[1]);
                            break;
                        case "StLocalisPart2LeftHand":
                            medicalInspectionInfo.StLocalisPart2LeftHand = CConvertEngine.StringToStringArray(keyValue[1]);
                            break;
                        case "StLocalisPart2NumericUpDown":
                            medicalInspectionInfo.StLocalisPart2NumericUpDown = Convert.ToInt32(keyValue[1]);
                            break;
                        case "StLocalisPart2RightHand":
                            medicalInspectionInfo.StLocalisPart2RightHand = CConvertEngine.StringToStringArray(keyValue[1]);
                            break;
                        case "StLocalisPart2TextBoxes":
                            medicalInspectionInfo.StLocalisPart2TextBoxes = CConvertEngine.StringToStringArray(keyValue[1]);
                            break;
                        case "StLocalisPart2WhichHand":
                            medicalInspectionInfo.StLocalisPart2WhichHand = keyValue[1];
                            break;
                        case "StPraesensComboBoxes":
                            medicalInspectionInfo.StPraesensComboBoxes = CConvertEngine.StringToStringArray(keyValue[1]);
                            break;
                        case "StPraesensNumericUpDowns":
                            medicalInspectionInfo.StPraesensNumericUpDowns = CConvertEngine.StringToIntArray(keyValue[1]);
                            break;
                        case "StPraesensTextBoxes":
                            medicalInspectionInfo.StPraesensTextBoxes = CConvertEngine.StringToStringArray(keyValue[1]);
                            break;
                        case "TeoRisk":
                            medicalInspectionInfo.TeoRisk = keyValue[1];
                            break;
                    }
                }

                _medicalInspectionList.Add(medicalInspectionInfo);
            }
        }


        /// <summary>
        /// Получить индекс осмотра в отделении в списке для указанного id госпитализации
        /// </summary>
        /// <param name="hospitalizationId">id госпитализации</param>
        /// <returns></returns>
        private int GetIndexFromList(int hospitalizationId)
        {
            int n = 0;
            while (n < _medicalInspectionList.Count &&
                _medicalInspectionList[n].HospitalizationId != hospitalizationId)
            {
                n++;
            }

            return n;
        }


        /// <summary>
        /// Проверить, существует ли осмотр в отделении для указанного id госпитализации
        /// </summary>
        /// <param name="hospitalizationId">id госпитализации</param>
        /// <returns></returns>
        public bool IsExists(int hospitalizationId)
        {
            return GetIndexFromList(hospitalizationId) != _medicalInspectionList.Count;
        }



        /// <summary>
        /// Получить осмотр в отделении по переданному id госпитализации
        /// </summary>
        /// <param name="hospitalizationId">id госпитализации</param>
        /// <returns></returns>
        public CMedicalInspection GetByHospitalizationId(int hospitalizationId)
        {
            int n = GetIndexFromList(hospitalizationId);

            if (n == _medicalInspectionList.Count)
            {
                var newMedicalInspectionInfo = new CMedicalInspection(hospitalizationId)
                {
                    NotInDatabase = true
                };
                _medicalInspectionList.Add(newMedicalInspectionInfo);
                return new CMedicalInspection(newMedicalInspectionInfo);
            }

            return new CMedicalInspection(_medicalInspectionList[n]);
        }


        /// <summary>
        /// Скопировать все осмотры в отделении для указанной госпитализации
        /// </summary>
        /// <param name="hospitalizationId">Id копируемой госпитализации</param>
        /// <param name="newHospitalizationId">Id нового госпитализации</param>
        public void CopyByHospitalizationId(int hospitalizationId, int newHospitalizationId)
        {
            CMedicalInspection newMedicalInspection = GetByHospitalizationId(hospitalizationId);
            newMedicalInspection.HospitalizationId = newHospitalizationId;
            _medicalInspectionList.Add(newMedicalInspection);
            Save();
        }


        /// <summary>
        /// Получить все осмотры в отделении с неправильным id госпитализации
        /// </summary>
        /// <param name="hospitalizationWorker">Объект для работы с госпитализациями</param>
        /// <returns></returns>
        public List<CMedicalInspection> GetWrongMedicalInspections(CHospitalizationWorker hospitalizationWorker)
        {
            var wrongMedicalInspections = new List<CMedicalInspection>();
            foreach (CMedicalInspection medicalInspection in _medicalInspectionList)
            {
                try
                {
                    hospitalizationWorker.GetById(medicalInspection.HospitalizationId);
                }
                catch
                {
                    wrongMedicalInspections.Add(medicalInspection);
                }
            }

            return wrongMedicalInspections;
        }
    }
}
