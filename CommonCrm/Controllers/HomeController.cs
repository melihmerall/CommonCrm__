using CommonCrm.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CommonCrm.BackgroundServices;
using CommonCrm.Data.DbContexts;
using CommonCrm.Data.Entities.AppUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace CommonCrm.Controllers
{
	public class HomeController : BaseController
	{
		private readonly ILogger<HomeController> _logger;
		private readonly CurrencyBackgroundService _currencyService;

		public HomeController(ILogger<HomeController> logger, 
			CurrencyBackgroundService currencyService, 
			UserManager<ApplicationUser> _userManager, 
			ApplicationDbContext _context) 
			: base(_userManager, _context)
		{
			_logger = logger;
			_currencyService = currencyService;
			
			
		}

		//[Authorize(Roles = "Admin, Manager, Customer")]
		[Route("/panel/dashboard")]
		public IActionResult Index()
		{
			var model = new LayoutViewModel();
			
			var currentUser = _userManager.GetUserAsync(User);
			var exchangeRates = _context.ExchangeRates.Where(x => x.OwnerId == currentUser.Result.OwnerId).ToList();
			model.ExchangeRates = exchangeRates;
			return View(model);
		}

    }
}
