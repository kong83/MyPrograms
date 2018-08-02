using System;
using SurgeryHelper.Tools;
using SurgeryHelper.Workers;

namespace SurgeryHelper.Essences
{
    public class CPassportInformation
    {
        /// <summary>
        /// Серия паспотра
        /// </summary>
        public string Series;

        /// <summary>
        /// Номер паспорта
        /// </summary>
        public string Number;

        /// <summary>
        /// Дата выдачи
        /// </summary>
        public DateTime? DeliveryDate;

        /// <summary>
        /// Код подразделения
        /// </summary>
        public string SubdivisionCode;

        /// <summary>
        /// Организация, выдавшая паспорт
        /// </summary>
        public string Organization;

        /// <summary>
        /// Получить дату выдачи паспорта в формате 11/22/2033, если указана дата выдачи
        /// </summary>
        /// <returns></returns>
        public string GetDeliveryDate()
        {
            if (!DeliveryDate.HasValue)
            {
                return string.Empty;
            }

            return DeliveryDate.Value.Day + "/" +
                DeliveryDate.Value.Month + "/" +
                DeliveryDate.Value.Year;
        }

        /// <summary>
        /// Получить строку с паспортными данными
        /// </summary>
        /// <returns></returns>
        public string GetPassInformation()
        {
            return string.Format(
                "сер. {0} № {1} Дата выдачи: {2} Код подразделения: {3} Кем выдан: {4}",
                Series,
                Number,
                DeliveryDate.HasValue ? CConvertEngine.DateTimeToString(DeliveryDate.Value) : string.Empty,
                SubdivisionCode,
                Organization);
        }

        public CPassportInformation()
        {
            DeliveryDate = null;
        }

        public CPassportInformation(string savedStr)
        {
            string[] savedArr = savedStr.Split(new[] { CBaseWorker.ListSplitStr }, StringSplitOptions.None);
            if (savedArr.Length == 1)
            {
                Organization = savedArr[0];
                return;
            }

            if (savedArr.Length != 5)
            {
                return;
            }

            Series = savedArr[0];
            Number = savedArr[1];
            if (!string.IsNullOrEmpty(savedArr[2]))
            {
                DeliveryDate = CConvertEngine.StringToDateTime(savedArr[2]);
            }
            else
            {
                DeliveryDate = null;
            }
            
            SubdivisionCode = savedArr[3];
            Organization = savedArr[4];
        }

        public CPassportInformation(CPassportInformation passportInfo)
        {
            Series = passportInfo.Series;
            Number = passportInfo.Number;
            DeliveryDate = CConvertEngine.CopyDateTime(passportInfo.DeliveryDate);
            SubdivisionCode = passportInfo.SubdivisionCode;
            Organization = passportInfo.Organization;
        }

        public override string ToString()
        {
            return string.Format(
                "{1}{0}{2}{0}{3}{0}{4}{0}{5}",
                CBaseWorker.ListSplitStr,
                Series,
                Number,
                CConvertEngine.DateTimeToString(DeliveryDate),
                SubdivisionCode,
                Organization);
        }
    }
}
