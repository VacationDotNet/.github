using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Assingment5ForReal
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public string GetCurrentTemperature(string cityName)
        {
            string apiKey = "54dcee075c7793ed33ae270eb35cdf16";
            string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={apiKey}&units=imperial";

            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(apiUrl).Result;
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    dynamic weatherData = Newtonsoft.Json.JsonConvert.DeserializeObject(data);
                    double temperature = weatherData.main.temp;
                    return $"The current temperature in {cityName} is {temperature}°F.";
                }
                else
                {
                    return $"Error: Unable to retrieve weather data for {cityName}.";
                }
            }
        }
    }
}
