namespace SurgeryHelper.Entities
{
    public class ServiceClass
    {
        /// <summary>
        /// Название услуги
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Код услуги
        /// </summary>
        public string ServiceCode { get; set; }

        /// <summary>
        /// Код КСГ
        /// </summary>
        public string KsgCode { get; set; }

        /// <summary>
        /// Расшифровка кода КСГ
        /// </summary>
        public string KsgDecoding { get; set; }


        public ServiceClass()
        {
            Initialize(new string[0]);
        }

        public ServiceClass(string[] serviceData)
        {
            Initialize(serviceData);
        }

        public ServiceClass(string serviceData)
        {
            string[] serviceDataArr = serviceData.Split(';');
            Initialize(serviceDataArr);
        }

        private void Initialize(string[] serviceData)
        {
            ServiceName = serviceData.Length > 0 ? serviceData[0] : string.Empty;
            ServiceCode = serviceData.Length > 1 ? serviceData[1] : string.Empty;
            KsgCode = serviceData.Length > 2 ? serviceData[2] : string.Empty;
            KsgDecoding = serviceData.Length > 3 ? serviceData[3] : string.Empty;
        }

        public override string ToString()
        {
            return string.Format(
                "{0};{1};{2};{3}",
                ServiceName,
                ServiceCode,
                KsgCode,
                KsgDecoding);
        }
    }    
}
