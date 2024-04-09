using CommonCrm.Business.Extensions;
using CommonCrm.Business.Services;
using CommonCrm.Data.DbContexts;
using CommonCrm.Data.Entities.AppUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CommonCrm.Controllers
{
    public class CustomerController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly AttributeService _attributeService;


        public CustomerController(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IdentityContext identityContext, AttributeService attributeService)
        {
            _userManager = userManager;
            _context = context;
            _userManager = userManager;
            _attributeService = attributeService;
            _attributeService = attributeService;
        }

        [Route("/customer/list")]
        [HttpGet]
        public IActionResult CustomerList()
        {
            var currentUser = _userManager.GetUserAsync(User).Result;
            var customers = _userManager.Users?.Where(x => x.OwnerId == currentUser.OwnerId && (x.IsCustomerPerson || x.IsCustomerCompany)).ToList();

            return View(customers);
        }
        
        [Route("/customer/add")]
        [HttpGet]
        public IActionResult CustomerAdd()
        {
            return View();
        }
        
        [Route("/customer/add")]
        [HttpPost]
        public async Task<IActionResult> CustomerAdd(ApplicationUser model)
        {
            var currentUser = _userManager.GetUserAsync(User).Result;
            Random random = new Random();
            var randomnumber = random.Next(0, 10000);

            if (ModelState.IsValid)
            {
                model.OwnerId = currentUser.OwnerId;
                model.CreatedBy = currentUser.Name + " " +currentUser.Surname;
                model.UserName = model.Name + model.Surname + randomnumber;
                model.IsCustomerPerson = true;
                model.IsCustomerCompany = true;

                var result = await _userManager.CreateAsync(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("UserList", "Admin");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            else
            {
                foreach (var modelError in ModelState.Values)
                {
                    foreach (var error in modelError.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.ErrorMessage);
                    }
                }
            }
            return View(model);
        }
        [Route("/attribute/add")]
        [HttpGet]
        public async Task<IActionResult> CreateAttribute()
        {
            return View();
        }

    }
}
