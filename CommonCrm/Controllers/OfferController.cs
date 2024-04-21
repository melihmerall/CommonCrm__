using CommonCrm.Business.Extensions;
using CommonCrm.Business.Extensions.Utilities;
using CommonCrm.Business.Services;
using CommonCrm.Data.DbContexts;
using CommonCrm.Data.Entities.AppUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CommonCrm.Controllers
{
    public class OfferController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly AttributeService _attributeService;


        public OfferController(UserManager<ApplicationUser> userManager, ApplicationDbContext context,
            IdentityContext identityContext, AttributeService attributeService)
        {
            _userManager = userManager;
            _context = context;
            _userManager = userManager;
            _attributeService = attributeService;
            _attributeService = attributeService;
        }

        [Route("/offer/list")]
        [HttpGet]
        public IActionResult OfferList()
        {
            var currentUser = _userManager.GetUserAsync(User).Result;
            var offers = _userManager.Users
                ?.Where(x => x.OwnerId == currentUser.OwnerId && (x.IsCustomerPerson || x.IsCustomerCompany)).ToList();

            return View(offers);
        }

        [Route("/offer/requests")]
        [HttpGet]
        public IActionResult OfferRequests()
        {
            var currentUser = _userManager.GetUserAsync(User).Result;
            var offers = _userManager.Users
                ?.Where(x => x.OwnerId == currentUser.OwnerId && (x.IsCustomerPerson || x.IsCustomerCompany)).ToList();

            return View(offers);
        }

        [Route("/offer/add")]
        [HttpGet]
        public IActionResult OfferAdd()
        {
            return View();
        }

        [Route("/offer/add")]
        [HttpPost]
        public async Task<IActionResult> OfferAdd(ApplicationUser model)
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
                    return RedirectToAction("OfferList", "Offer");
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

        [Route("/offer/{id}/update")]
        [HttpGet]
        public async Task<IActionResult> OfferUpdate(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            return View(user);
        }

        [Route("/offer/{id}/update")]
        [HttpPost]
        public async Task<IActionResult> OfferUpdate(ApplicationUser model)
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
                    return RedirectToAction("OfferList", "Offer");

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

        public async Task<IActionResult> DeleteOffer(string id)
        {
            var offer = _userManager.FindByIdAsync(id).Result;
            if (offer == null)
            {
                TempData["CustomMessage"] = "User not find.";

            }
            await _userManager.DeleteAsync(offer);
            await _context.SaveChangesAsync();
            TempData["CustomMessage"] = Constants.SuccessDeleted;
            return RedirectToAction("OfferList");

        }
    }
}