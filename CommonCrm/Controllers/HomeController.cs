using CommonCrm.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CommonCrm.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
			
		}
		[Route("/panel/dashboard")]
		public IActionResult Index()
		{
			return View();
		}
        public IActionResult Testss()
        {
            return View();
        }

    }
}
