using Newtonsoft.Json.Linq;
using System.Net;

namespace WeatherApp.Services
{
	public class CityToGeoApi : Iapi
	{
		readonly string _baseUrl;
		private string _city { get; set; }
		private string? _country { get; set; }
		public string Name { get; set; }
		public string _key { get; set; }

		public CityToGeoApi(string city, string country = null)
		{
			_baseUrl = "https://eu1.locationiq.com/v1/search?key=";
			_key = "pk.209a54cc9da1a74c04768e2a0443b1d5";
            //_baseUrl = "http://api.openweathermap.org/geo/1.0/direct";
            //_key = "37148c72f05941b0bce74d301bd8c843";
            _city = city;
			_country = country;
		}
		public string UrlBuilder()
		{
			if (_city!=null)
			{
				if (_country!=null)
				{
                    return $"{_baseUrl}{_key}&country={_country},city={_city}&format=json";
                    //return $"{_baseUrl}?q={_city},{_country}&appid={_key}";
                }
				return $"{_baseUrl}{_key}&city={_city}&format=json";
			}
			return "";
			
		}

		public void SetCity(string city) { _city = city; }
		public void SetCountry(string country) { _country = country; }

		public Dictionary<string, string> GetCoordinates()
		{
			string lat= "";
			string lon="";
            using (var client = new HttpClient())
            {
                var endpoint = new Uri(UrlBuilder());
                var apiResult =  client.GetAsync(endpoint).Result.Content.ReadAsStringAsync().Result;
                dynamic data =  JToken.Parse(apiResult);
				if (data != null)
				{
					if(data?[0]["lat"] != null && data?[0]["lon"] != null)
					{
					   lat = data[0]["lat"];
					   lon = data[0]["lon"];
					}
					else throw new Exception("invalid location");
				}
			}
				
            var result = new Dictionary<string, string>();
			result.Add("latitude", lat);
			result.Add("longitude", lon);
			return result;	
		}
	}
}
