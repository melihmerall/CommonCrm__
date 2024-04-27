using CommonCrm.Business.Extensions;
using CommonCrm.Business.Extensions.Utilities;
using CommonCrm.Business.Services;
using CommonCrm.Data.DbContexts;
using CommonCrm.Data.Entities.AppUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CommonCrm.Controllers
{
    public class ShopController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly AttributeService _attributeService;


        public ShopController(UserManager<ApplicationUser> userManager, ApplicationDbContext context,
            IdentityContext identityContext, AttributeService attributeService)
        {
            _userManager = userManager;
            _context = context;
            _userManager = userManager;
            _attributeService = attributeService;
            _attributeService = attributeService;
        }

        [Route("/shop/main")]
        [HttpGet]
        public IActionResult ShopMain()
        {
            var currentUser = _userManager.GetUserAsync(User).Result;
            var offers = _userManager.Users
                ?.Where(x => x.OwnerId == currentUser.OwnerId && (x.IsCustomerPerson || x.IsCustomerCompany)).ToList();

            return View(offers);
        }

        [Route("/attribute/add")]
        [HttpGet]
        public async Task<IActionResult> CreateAttribute()
        {
            return View();
        }

        public async Task<IActionResult> DeleteShop(string id)
        {
            var shops = _userManager.FindByIdAsync(id).Result;
            if (shops == null)
            {
                TempData["CustomMessage"] = "User not find.";

            }
            await _userManager.DeleteAsync(shops);
            await _context.SaveChangesAsync();
            TempData["CustomMessage"] = Constants.SuccessDeleted;
            return RedirectToAction("ShopList");

        }
    }
}