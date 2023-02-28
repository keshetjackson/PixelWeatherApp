namespace WeatherApp.Services
{
	public interface Iapi
	{
		string _key { get; set; }
		string UrlBuilder();
	}
}
