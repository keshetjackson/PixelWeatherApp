using WeatherApp.Services;

namespace WeatherApp.Models
{
    public class Manager
    {
        public Weather WeatherModel { get; set; }
        CurrentWeatherAPi WeatherApi { get; set; }
        public  CityToGeoApi GeoApi { get; set; }

        private Dictionary<string,string> KelvinCurrentWeather { get ; set; }
		private Dictionary<string, string> CelsiusCurrentWeather { get; set; }

		public Manager(string city, string country = null) 
        {
            if (WeatherApi == null) { WeatherApi = new CurrentWeatherAPi(city, country); }
            else
            {
                WeatherApi.GetGeoInit(city, country);
            }
            if (WeatherModel == null)
                WeatherModel = new Weather();

        }
        
        public void ApiToModelWeather(Dictionary<string,string> dict)
        {
            var temp = dict["Temperature"];
            var feels = dict["FeelsLike"];
            var vis = dict["Visibilty"];
            var hum = dict["Humidity"];
            var max = dict["MaxTemp"];
            var min = dict["MinTemp"];
            var pres = dict["Pressure"];
            var wind = dict["WindSpeed"];

          

            WeatherModel.SetProperties(temp,pres,hum,feels,max,min,vis, wind);
        }

        public void ModelToKelvinDict()
        {
            if (KelvinCurrentWeather == null) { KelvinCurrentWeather = new Dictionary<string, string>(); }
            KelvinCurrentWeather.Add("Temperature", WeatherModel.Temperature);
            KelvinCurrentWeather.Add("FeelsLike", WeatherModel.FeelsLike);
            KelvinCurrentWeather.Add("Visibilty", WeatherModel.Visibilty);
            KelvinCurrentWeather.Add("Humidity", WeatherModel.Humidity);
            KelvinCurrentWeather.Add("MaxTemp", WeatherModel.MaxTemp);
            KelvinCurrentWeather.Add("MinTemp", WeatherModel.MinTemp);
            KelvinCurrentWeather.Add("Pressure", WeatherModel.Pressure);
            KelvinCurrentWeather.Add("WindSpeed", WeatherModel.WindSpeed);
        }

        public void KelvinDictToCelsiusDict()
        {
            if (CelsiusCurrentWeather == null) { CelsiusCurrentWeather = new Dictionary<string, string>(); }
            CelsiusCurrentWeather.Add("Temperature", KelvinToCelsius(KelvinCurrentWeather["Temperature"]));
            CelsiusCurrentWeather.Add("FeelsLike", KelvinToCelsius(KelvinCurrentWeather["FeelsLike"]));
            CelsiusCurrentWeather.Add("Visibilty", WeatherModel.Visibilty);
			CelsiusCurrentWeather.Add("Humidity", WeatherModel.Humidity);
			CelsiusCurrentWeather.Add("MaxTemp", KelvinToCelsius(KelvinCurrentWeather["MaxTemp"]));
            CelsiusCurrentWeather.Add("MinTemp", KelvinToCelsius(KelvinCurrentWeather["MinTemp"]));
			CelsiusCurrentWeather.Add("Pressure", WeatherModel.Pressure);
			CelsiusCurrentWeather.Add("WindSpeed", WeatherModel.WindSpeed);
		}

		public string KelvinToCelsius(string num)
        {
            double results= Convert.ToDouble(num) - 273.15;

            return results.ToString();
		}

        public Dictionary<string,string> GetWeather(string city, string country = null)
        {
            try
            {
				ApiToModelWeather(WeatherApi.GetValues(city, country));
				ModelToKelvinDict();
                KelvinDictToCelsiusDict();
				
			}
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
			return CelsiusCurrentWeather;
		}
    }
}
