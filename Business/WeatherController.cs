using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class WeatherController
    {
        private readonly string apiKey;

        public WeatherController(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public WeatherData GetTodaysWeather(string city)
        {
            using (WebClient client = new WebClient() { Encoding = new UTF8Encoding() })
            {
                string json = client.DownloadString($"http://api.openweathermap.org/data/2.5/weather?q={city}&APPID={apiKey}");
                JObject jObject = JObject.Parse(json);
                return new WeatherData(jObject["name"].ToString(), String.Format("{0:#.##}", (double.Parse(jObject["main"]["temp"].ToString()) - 272.15)), jObject["weather"].First["description"].ToString());

            }
        }

        public WeatherData GetTomorrowsWeather(string city)
        {
            using (WebClient client = new WebClient() { Encoding = new UTF8Encoding() })
            {
                string json = client.DownloadString($"http://api.openweathermap.org/data/2.5/forecast?q={city}&APPID={apiKey}");
                JObject jObject = JObject.Parse(json);
                JToken tomorrow = jObject["list"].OrderBy(x => DateTime.Parse(x["dt_txt"].ToString()) - DateTime.Now.AddDays(1)).First();
                return new WeatherData(jObject["city"]["name"].ToString(), String.Format("{0:#.##}", double.Parse(tomorrow["main"]["temp"].ToString()) - 272.15), tomorrow["weather"].First["description"].ToString());
            }

        }
    }
}
