using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WeatherApp.Models;

namespace WeatherApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private Manager? _manager { get; set; }

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}
		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Weather (string city, string country = null)
		{
			if (_manager == null)
			_manager = new Manager(city, country);
			return View("Index",_manager.GetWeather(city,country));
		}


		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}