using CommonCrm.Business.Extensions;
using CommonCrm.Business.Extensions.Utilities;
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


        public CustomerController(UserManager<ApplicationUser> userManager, ApplicationDbContext context,
            IdentityContext identityContext, AttributeService attributeService)
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
            var customers = _userManager.Users
                ?.Where(x => x.OwnerId == currentUser.OwnerId && (x.IsCustomerPerson || x.IsCustomerCompany)).ToList();

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
                model.CreatedBy = currentUser.Name + " " + currentUser.Surname;
                model.UserName = model.Name + model.Surname + randomnumber;
                if (model.OfficialName != null)
                {
                    model.IsCustomerCompany = true;
                }

                if (model.Name != null)
                {
                    model.IsCustomerPerson = true;
                }

                model.IsActive = true;

                var result = await _userManager.CreateAsync(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("CustomerList", "Customer");
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
        
        [Route("/customer/{id}/update")]
        [HttpGet]
        public async Task<IActionResult> CustomerUpdate(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            
            return View(user);
        }

        [Route("/customer/{id}/update")]
        [HttpPost]
        public async Task<IActionResult> CustomerUpdate(ApplicationUser model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (model.OfficialName != null)
                {
                    user.Title = model.Title;
                    user.PhoneNumber = model.PhoneNumber;
                    user.Email = model.Email;
                    user.Country = model.Country;
                    user.City = model.City;
                    user.OfficialName = model.OfficialName;
                    user.OfficialSurname = model.OfficialSurname;
                }

                if (model.Name != null)
                {
                    user.Name = model.Name;
                    user.Surname = model.Surname;
                    user.TcNo = model.TcNo;
                    user.PhoneNumber = model.PhoneNumber;
                    user.Email = model.Email;
                    user.Country = model.Country;
                    user.City = model.City;
                    user.PostCode = model.PostCode;
                }

                var result = await _userManager.UpdateAsync(user);
                await _context.SaveChangesAsync();
                if (result.Succeeded)
                {
                    TempData["CustomMessage"] = Constants.SuccessUpdated;
                    return RedirectToAction("CustomerList", "Customer");
                    
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    TempData["ErrorMessage"] = $"{error.Description}";
                }
            }
            else
            {
                foreach (var modelError in ModelState.Values)
                {
                    foreach (var error in modelError.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.ErrorMessage);
                        TempData["ErrorMessage"] = $"{error.ErrorMessage}";

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

        public async Task<IActionResult> DeleteCustomer(string id)
        {
            var customer = _userManager.FindByIdAsync(id).Result;
            if (customer == null)
            {
                TempData["CustomMessage"] = "User not find.";

            }
            await _userManager.DeleteAsync(customer);
            await _context.SaveChangesAsync();
            TempData["CustomMessage"] = Constants.SuccessDeleted;
            return RedirectToAction("CustomerList");

        }
    }
}