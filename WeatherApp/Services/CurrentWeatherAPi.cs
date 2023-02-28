using Newtonsoft.Json.Linq;

namespace WeatherApp.Services
{
	public class CurrentWeatherAPi : Iapi
	{
		public string BaseUrl = "https://api.openweathermap.org/data/2.5/weather";
		public string _key { get; set; }
		private string _latitude { get; set; }
		private string _longitude { get; set; }

		private CityToGeoApi GetGeo;

		public CurrentWeatherAPi(string city, string country = null) 
		{

		   GetGeo = new CityToGeoApi(city, country);
			_key = "37148c72f05941b0bce74d301bd8c843";
		}


		public string UrlBuilder()
		{
			return $"{BaseUrl}?lat={_latitude}&lon={_longitude}&appid={_key}";
		}

		public Dictionary<string,string> GetValues(string city, string country = null)
		{
			GetGeoInit(city, country);
			SetCoordinates();
			Dictionary<string,string> dict = new Dictionary<string,string>();

            using (var client = new HttpClient())
            {
                var endpoint = new Uri(UrlBuilder());
                var apiResult = client.GetAsync(endpoint).Result.Content.ReadAsStringAsync().Result;
                dynamic data = JToken.Parse(apiResult);
                Console.WriteLine(data);
				dict.Add("Temperature",(String)data.main.temp);
                dict.Add("Pressure", (String)data.main.pressure);
                dict.Add("Humidity", (String)data.main.humidity);
                dict.Add("FeelsLike", (String)data.main.feels_like);
                dict.Add("MaxTemp", (String)data.main.temp_max);
                dict.Add("MinTemp", (String)data.main.temp_min);
                dict.Add("Visibilty", (String)data.visibility);
                dict.Add("WindSpeed", (String)data.wind.speed);	
            }
			return dict;
        }

		public void SetCoordinates()
		{
            Dictionary<string, string> coordinates = GetGeo.GetCoordinates();
            _latitude = coordinates["latitude"];
            _longitude = coordinates["longitude"];
        }

		public void GetGeoInit(string city,string country)
		{
            if (GetGeo == null) GetGeo = new CityToGeoApi(city, country);
            else
            {
                GetGeo.SetCity(city);
                if (country != null) GetGeo.SetCountry(country);
            }
        }
	}
}
