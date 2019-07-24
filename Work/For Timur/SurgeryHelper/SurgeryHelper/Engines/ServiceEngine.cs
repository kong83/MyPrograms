using System;
using System.Collections.Generic;
using System.Text;
using SurgeryHelper.Entities;

namespace SurgeryHelper.Engines
{
    public class ServiceEngine
    {
        public List<ServiceClass> Services { get; private set; }

        private const string SplitStr = "^";

        public ServiceEngine()
        {
            Services = new List<ServiceClass>();
        }

        public ServiceEngine(string serviceData)
        {
            Services = new List<ServiceClass>();

            string[] servicesDate = serviceData.Split(new[] { SplitStr }, StringSplitOptions.RemoveEmptyEntries);
            if (servicesDate.Length == 0)
            {
                return;
            }

            Array.Sort(servicesDate);

            // Пропускаем одинаковые
            string prevItem = servicesDate[0];
            Services.Add(new ServiceClass(prevItem));
            for (int i = 1; i < servicesDate.Length; i++)
            {
                if (servicesDate[i] != prevItem)
                {
                    prevItem = servicesDate[i];
                    Services.Add(new ServiceClass(prevItem));
                }
            }
        }

        /// <summary>
        /// Записать все данные в строку для подготовки данных к запаковке
        /// </summary>
        /// <returns></returns>
        public string PrepareDataToPack()
        {
            var serviceData = new StringBuilder();
            foreach (ServiceClass service in Services)
            {
                serviceData.Append(service.ToString() + SplitStr);
            }

            return serviceData.ToString();
        }
    }
}
