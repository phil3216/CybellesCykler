using System;

namespace Services
{
    public struct WeatherData
    {

        private string temperature;
        private string city;
        private string forecast;


        public string Forecast
        {
            get => forecast;
            set
            {
                (bool b, string s) = ValidateForecast(value);
                if (!b) throw new ArgumentException(s);

                forecast = value;
            }
        }



        public string Temperature
        {
            get => temperature;
            set
            {
                (bool b, string s) = ValidateTemperature(value);
                if (!b) throw new ArgumentException(s);

                temperature = value;
            }
        }



        public string City
        {
            get => city;
            set
            {
                (bool b, string s) = ValidateCity(value);
                if (!b) throw new ArgumentException(s);

                city = value;
            }
        }

        public static (bool, string) ValidateForecast(string value)
        {
            return (true, String.Empty);
        }
        public static (bool, string) ValidateTemperature(string value)
        {
            return (true, String.Empty);
        }

        public static (bool, string) ValidateCity(string value)
        {
            return (true, String.Empty);
        }

        public WeatherData(string city,string temperature,string forecast)
        {
            this.city = city;
            this.temperature = temperature;
            this.forecast = forecast;
            Forecast = forecast;
            City = city;
            Temperature = temperature;
        }


    }
}