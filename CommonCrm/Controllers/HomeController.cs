using CommonCrm.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

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
			TempData["CustomMessage"] = $"Giriş başarılı! Hoşgeldin {HttpContext?.User?.Identity?.Name}.";

			return View();
		}

    }
}
