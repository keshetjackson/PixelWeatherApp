using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WeatherApp.Models
{
    public class Weather
    {
        string? _temperature;
        string? _pressure;
        string? _humidity;
        string? _feelsLike;
        string? _maxTemp;
        string? _minTemp;
        string? _visibilty;
        string? _windSpeed;

        public string Temperature { get { return _temperature; } set { _temperature = value; } }
        public string Pressure { get { return _pressure; } set { _pressure = value; } }
        public string Humidity { get { return _humidity; } set { _humidity = value; } }
        [Display(Name = "Feels Like")]
        public string FeelsLike { get { return _feelsLike; } set { _feelsLike = value; } }
        [Display(Name = "Maximum Temperature")]
        public string MaxTemp { get { return _maxTemp;} set { _maxTemp = value; } }
        [Display(Name = "Minimum Temperature")]
        public string MinTemp { get { return _minTemp; } set { _minTemp = value; } }
        public string Visibilty { get { return _visibilty;} set { _visibilty = value;}  }
        [Display(Name = "Wind Speed")]
        public string WindSpeed { get { return _windSpeed; } set { _windSpeed = value; } }
    



        public void SetProperties(string temp, string pres, string hum, string feel, string max, string min, string vis, string wind)
        {
            Temperature = temp;
            Pressure = pres;
            Humidity = hum;
            FeelsLike = feel;
            MaxTemp = max;
            MinTemp = min;
            Visibilty = vis;
            WindSpeed = wind;
        }
    }
}
