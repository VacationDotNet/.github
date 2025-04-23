using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Net.Http;
using System.Text;

namespace Assingment5ForReal
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service3" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service3.svc or Service3.svc.cs at the Solution Explorer and start debugging.
    // works good
    public class Service3 : IService3
    {
        public double ConvertUSDToCurrency(double amount, string targetCurrency)
        {
            string apikey = "970cff4aec305cc892d79ac6";
            string url = $"https://v6.exchangerate-api.com/v6/{apikey}/pair/USD/{targetCurrency}";
            
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    dynamic exchangeData = Newtonsoft.Json.JsonConvert.DeserializeObject(data);
                    double exchangeRate = exchangeData.conversion_rate;
                    return amount * exchangeRate;
                }
                else
                {
                    throw new Exception("Error retrieving exchange rate.");
                }
            }
        }
    }
}
