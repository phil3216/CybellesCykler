using Newtonsoft.Json.Linq;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class GetWeatherForecast
    {
        private readonly string apiKey;
        private readonly string cityName;
        private readonly int cityID;

        public GetWeatherForecast(string cityName,string apiKey)
        {
            cityID = -1;
            this.cityName = cityName;
            this.apiKey = apiKey;
        }

        public GetWeatherForecast(int cityID, string apiKey)
        {
            this.cityID = cityID;
            this.apiKey = apiKey;
        }

        public WeatherData GetTodaysWeather()
        {
            WebClient client = new WebClient();
            string json = client.DownloadString($"http://api.openweathermap.org/data/2.5/weather?{(cityID == -1 ? "q=" + cityName : "id="+ cityID.ToString())}&APPID={apiKey}");
            JObject jObject = JObject.Parse(json);
            return new WeatherData(jObject["name"].ToString(),jObject["main"]["temp"].ToString(),jObject["weather"].First["description"].ToString());
        }

        public WeatherData GetTomorrowsWeather()
        {
            WebClient client = new WebClient();
            string json = client.DownloadString($"http://api.openweathermap.org/data/2.5/forecast?{(cityID == -1 ? "q=" + cityName : "id=" + cityID.ToString())}&APPID={apiKey}");
            JObject jObject = JObject.Parse(json);
            JToken tomorrow = jObject["list"].OrderBy(x => DateTime.Parse(x["dt_txt"].ToString()) - DateTime.Now.AddDays(1)).First();
            return new WeatherData(jObject["city"]["name"].ToString(), tomorrow["main"]["temp"].ToString(), tomorrow["weather"].First["description"].ToString());

        }
    }
}
