using System;
using System.Collections.Generic;

namespace SurgeryHelper.Entities
{
    /// <summary>
    /// Класс с данными по операции
    /// </summary>
    public class OperationClass
    {
        /// <summary>
        /// Открытая для этой операции форма с данными, если она есть
        /// </summary>
        public OperationViewForm OpenedOperationViewForm;

        /// <summary>
        /// Открытая для этой операции форма с протоколом операции, если она есть
        /// </summary>
        public OperationProtocolForm OpenedOperationProtocolForm;

        /// <summary>
        /// Уникальный id
        /// </summary>
        public int Id;

        /// <summary>
        /// Дата операции
        /// </summary>
        public DateTime DataOfOperation;        

        /// <summary>
        /// Время начала операции
        /// </summary>
        public DateTime StartTimeOfOperation;

        /// <summary>
        /// Время окончания операции
        /// </summary>
        public DateTime EndTimeOfOperation;

        /// <summary>
        /// Название операции
        /// </summary>
        public string Name;

        /// <summary>
        /// Список хирургов
        /// </summary>
        public List<string> Surgeons;

        /// <summary>
        /// Список ассистентов
        /// </summary>
        public List<string> Assistents;

        /// <summary>
        /// Анестезист операции
        /// </summary>
        public string HeAnaesthetist;

        /// <summary>
        /// Анестезистка операции
        /// </summary>
        public string SheAnaesthetist;

        /// <summary>
        /// Операционная мед. сестра
        /// </summary>
        public string ScrubNurse;

        /// <summary>
        /// Санитар операции
        /// </summary>
        public string Orderly;


        /// <summary>
        /// Время написания эпискриза
        /// </summary>
        public DateTime BeforeOperationEpicrisisTimeWriting;

        /// <summary>
        /// Активен ли дневник
        /// </summary>
        public bool BeforeOperationEpicrisisIsDairyEnabled;

        /// <summary>
        /// Температура тела
        /// </summary>
        public string BeforeOperationEpicrisisTemperature;

        /// <summary>
        /// Жалобы пациента
        /// </summary>
        public string BeforeOperationEpicrisisComplaints;

        /// <summary>
        /// Состояние пациента
        /// </summary>
        public string BeforeOperationEpicrisisState;
        
        /// <summary>
        /// Пульс пациента
        /// </summary>
        public int BeforeOperationEpicrisisPulse;

        /// <summary>
        /// Первое значение АД
        /// </summary>
        public int BeforeOperationEpicrisisADFirst;

        /// <summary>
        /// Второе значение АД
        /// </summary>
        public int BeforeOperationEpicrisisADSecond;

        /// <summary>
        /// ЧДД пациента
        /// </summary>
        public int BeforeOperationEpicrisisChDD;

        /// <summary>
        /// Дыхание пациента
        /// </summary>
        public string BeforeOperationEpicrisisBreath;

        /// <summary>
        /// Хрипы пациента
        /// </summary>
        public string BeforeOperationEpicrisisWheeze;

        /// <summary>
        /// Тоны сердца
        /// </summary>
        public string BeforeOperationEpicrisisHeartSounds;

        /// <summary>
        /// Ритм сердца
        /// </summary>
        public string BeforeOperationEpicrisisHeartRhythm;

        /// <summary>
        /// Живот пациента
        /// </summary>
        public string BeforeOperationEpicrisisStomach;

        /// <summary>
        /// Мочеиспускание пациента
        /// </summary>
        public string BeforeOperationEpicrisisUrination;

        /// <summary>
        /// Стул пациента
        /// </summary>
        public string BeforeOperationEpicrisisStool;

        /// <summary>
        /// St. Localis
        /// </summary>
        public string BeforeOperationEpicrisisStLocalis;

        
        /// <summary>
        /// Ход операции
        /// </summary>
        public string OperationCourse;

        public OperationClass()
        {
            Id = 0;
            Surgeons = new List<string>();
            Assistents = new List<string>();

            BeforeOperationEpicrisisIsDairyEnabled = true;
            BeforeOperationEpicrisisTemperature = "N";
            BeforeOperationEpicrisisPulse = 76;
            BeforeOperationEpicrisisADFirst = 120;
            BeforeOperationEpicrisisADSecond = 70;
            BeforeOperationEpicrisisChDD = 18;
            BeforeOperationEpicrisisWheeze = "нет";
            BeforeOperationEpicrisisStomach = "мягкий, безболезненный";
            BeforeOperationEpicrisisUrination = "свободное, регулярное";
            BeforeOperationEpicrisisStool = "регулярный, без особенностей";
            BeforeOperationEpicrisisState = "удовлетворительное";
            BeforeOperationEpicrisisBreath = "везикулярное";
            BeforeOperationEpicrisisHeartSounds = "ясные";
            BeforeOperationEpicrisisHeartRhythm = "правильный";
        }

        public OperationClass(OperationClass operationInfo)
        {
            Id = operationInfo.Id;
            DataOfOperation = new DateTime(operationInfo.DataOfOperation.Year, operationInfo.DataOfOperation.Month, operationInfo.DataOfOperation.Day, operationInfo.DataOfOperation.Hour, operationInfo.DataOfOperation.Minute, operationInfo.DataOfOperation.Second);
            StartTimeOfOperation = new DateTime(operationInfo.StartTimeOfOperation.Year, operationInfo.StartTimeOfOperation.Month, operationInfo.StartTimeOfOperation.Day, operationInfo.StartTimeOfOperation.Hour, operationInfo.StartTimeOfOperation.Minute, operationInfo.StartTimeOfOperation.Second);
            EndTimeOfOperation = new DateTime(operationInfo.EndTimeOfOperation.Year, operationInfo.EndTimeOfOperation.Month, operationInfo.EndTimeOfOperation.Day, operationInfo.EndTimeOfOperation.Hour, operationInfo.EndTimeOfOperation.Minute, operationInfo.EndTimeOfOperation.Second);
            Name = operationInfo.Name;
            HeAnaesthetist = operationInfo.HeAnaesthetist;
            SheAnaesthetist = operationInfo.SheAnaesthetist;
            OperationCourse = operationInfo.OperationCourse;

            Surgeons = new List<string>();
            foreach (string surgeon in operationInfo.Surgeons)
            {
                Surgeons.Add(surgeon);
            }

            Assistents = new List<string>();
            foreach (string assistent in operationInfo.Assistents)
            {
                Assistents.Add(assistent);
            }

            ScrubNurse = operationInfo.ScrubNurse;
            Orderly = operationInfo.Orderly;

            BeforeOperationEpicrisisADFirst = operationInfo.BeforeOperationEpicrisisADFirst;
            BeforeOperationEpicrisisADSecond = operationInfo.BeforeOperationEpicrisisADSecond;
            BeforeOperationEpicrisisBreath = operationInfo.BeforeOperationEpicrisisBreath;
            BeforeOperationEpicrisisChDD = operationInfo.BeforeOperationEpicrisisChDD;
            BeforeOperationEpicrisisComplaints = operationInfo.BeforeOperationEpicrisisComplaints;
            BeforeOperationEpicrisisState = operationInfo.BeforeOperationEpicrisisState;
            BeforeOperationEpicrisisHeartRhythm = operationInfo.BeforeOperationEpicrisisHeartRhythm;
            BeforeOperationEpicrisisHeartSounds = operationInfo.BeforeOperationEpicrisisHeartSounds;
            BeforeOperationEpicrisisIsDairyEnabled = operationInfo.BeforeOperationEpicrisisIsDairyEnabled;
            BeforeOperationEpicrisisPulse = operationInfo.BeforeOperationEpicrisisPulse;
            BeforeOperationEpicrisisStLocalis = operationInfo.BeforeOperationEpicrisisStLocalis;
            BeforeOperationEpicrisisStomach = operationInfo.BeforeOperationEpicrisisStomach;
            BeforeOperationEpicrisisStool = operationInfo.BeforeOperationEpicrisisStool;
            BeforeOperationEpicrisisTemperature = operationInfo.BeforeOperationEpicrisisTemperature;
            BeforeOperationEpicrisisTimeWriting = new DateTime(operationInfo.BeforeOperationEpicrisisTimeWriting.Year, operationInfo.BeforeOperationEpicrisisTimeWriting.Month, operationInfo.BeforeOperationEpicrisisTimeWriting.Day, operationInfo.BeforeOperationEpicrisisTimeWriting.Hour, operationInfo.BeforeOperationEpicrisisTimeWriting.Minute, operationInfo.BeforeOperationEpicrisisTimeWriting.Second);            
            BeforeOperationEpicrisisUrination = operationInfo.BeforeOperationEpicrisisUrination;
            BeforeOperationEpicrisisWheeze = operationInfo.BeforeOperationEpicrisisWheeze;
        }

        /// <summary>
        /// Скопировать данные об операции в указанную операцию (без использования new)
        /// </summary>
        /// <param name="operationInfo">Информация об операции</param>
        public void Copy(OperationClass operationInfo)
        {
            if (operationInfo == this)
            {
                return;
            }

            operationInfo.Id = Id;
            operationInfo.DataOfOperation = new DateTime(DataOfOperation.Year, DataOfOperation.Month, DataOfOperation.Day, DataOfOperation.Hour, DataOfOperation.Minute, DataOfOperation.Second);
            operationInfo.StartTimeOfOperation = new DateTime(StartTimeOfOperation.Year, StartTimeOfOperation.Month, StartTimeOfOperation.Day, StartTimeOfOperation.Hour, StartTimeOfOperation.Minute, StartTimeOfOperation.Second);
            operationInfo.EndTimeOfOperation = new DateTime(EndTimeOfOperation.Year, EndTimeOfOperation.Month, EndTimeOfOperation.Day, EndTimeOfOperation.Hour, EndTimeOfOperation.Minute, EndTimeOfOperation.Second);
            operationInfo.Name = Name;
            operationInfo.HeAnaesthetist = HeAnaesthetist;
            operationInfo.SheAnaesthetist = SheAnaesthetist;
            operationInfo.OperationCourse = OperationCourse;

            operationInfo.Surgeons = new List<string>();
            foreach (string surgeon in Surgeons)
            {
                operationInfo.Surgeons.Add(surgeon);
            }

            operationInfo.Assistents = new List<string>();
            foreach (string assistent in Assistents)
            {
                operationInfo.Assistents.Add(assistent);
            }

            operationInfo.ScrubNurse = ScrubNurse;
            operationInfo.Orderly = Orderly;

            operationInfo.BeforeOperationEpicrisisADFirst = BeforeOperationEpicrisisADFirst;
            operationInfo.BeforeOperationEpicrisisADSecond = BeforeOperationEpicrisisADSecond;
            operationInfo.BeforeOperationEpicrisisBreath = BeforeOperationEpicrisisBreath;
            operationInfo.BeforeOperationEpicrisisChDD = BeforeOperationEpicrisisChDD;
            operationInfo.BeforeOperationEpicrisisComplaints = BeforeOperationEpicrisisComplaints;
            operationInfo.BeforeOperationEpicrisisState = BeforeOperationEpicrisisState;
            operationInfo.BeforeOperationEpicrisisHeartRhythm = BeforeOperationEpicrisisHeartRhythm;
            operationInfo.BeforeOperationEpicrisisHeartSounds = BeforeOperationEpicrisisHeartSounds;
            operationInfo.BeforeOperationEpicrisisIsDairyEnabled = BeforeOperationEpicrisisIsDairyEnabled;
            operationInfo.BeforeOperationEpicrisisPulse = BeforeOperationEpicrisisPulse;
            operationInfo.BeforeOperationEpicrisisStLocalis = BeforeOperationEpicrisisStLocalis;
            operationInfo.BeforeOperationEpicrisisStomach = BeforeOperationEpicrisisStomach;
            operationInfo.BeforeOperationEpicrisisStool = BeforeOperationEpicrisisStool;
            operationInfo.BeforeOperationEpicrisisTemperature = BeforeOperationEpicrisisTemperature;
            operationInfo.BeforeOperationEpicrisisTimeWriting = new DateTime(BeforeOperationEpicrisisTimeWriting.Year, BeforeOperationEpicrisisTimeWriting.Month, BeforeOperationEpicrisisTimeWriting.Day, BeforeOperationEpicrisisTimeWriting.Hour, BeforeOperationEpicrisisTimeWriting.Minute, BeforeOperationEpicrisisTimeWriting.Second);
            operationInfo.BeforeOperationEpicrisisUrination = BeforeOperationEpicrisisUrination;
            operationInfo.BeforeOperationEpicrisisWheeze = BeforeOperationEpicrisisWheeze;
            operationInfo.OpenedOperationProtocolForm = OpenedOperationProtocolForm;
            operationInfo.OpenedOperationViewForm = OpenedOperationViewForm;
        }

        public static int Compare(OperationClass operationInfo1, OperationClass operationInfo2)
        {
            return DateTime.Compare(operationInfo1.DataOfOperation, operationInfo2.DataOfOperation);
        }
    }
}
