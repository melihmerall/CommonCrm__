using System.Globalization;
using AutoMapper;
using CommonCrm.Business.Extensions;
using CommonCrm.Business.Extensions.Utilities;
using CommonCrm.Business.Services;
using CommonCrm.Data.DbContexts;
using CommonCrm.Data.Entities;
using CommonCrm.Data.Entities.AppUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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

        // public async Task<List<Tuple<string, string>>> GetCountry()
        // {
        //     List<Tuple<string, string>> countriesWithNames = new List<Tuple<string, string>>();
        //
        //     HttpClient client = new HttpClient();
        //     HttpResponseMessage response =
        //         await client.GetAsync($"https://countriesnow.space/api/v0.1/countries/iso");
        //     response.EnsureSuccessStatusCode();
        //     string responseBody = await response.Content.ReadAsStringAsync();
        //
        //     dynamic countryData = JsonConvert.DeserializeObject(responseBody);
        //
        //     // foreach (var country in countryData.data)
        //     // {
        //     //     string countryName = country.name.ToString();
        //     //     string iso3 = country.iso3.ToString();
        //     //
        //     //     foreach (var state in country.states)
        //     //     {
        //     //         string stateName = state.name.ToString();
        //     //         string stateCode = state.state_code.ToString();
        //     //
        //     //         // Veritabanına ekleme işlemi
        //     //         var countryState = new CountryStates
        //     //         {
        //     //             CountryName = countryName,
        //     //             ISO3 = iso3,
        //     //             StateName = stateName,
        //     //             StateCode = stateCode
        //     //         };
        //     //
        //     //         _context.CountryStates.Add(countryState);
        //     //     }
        //     // }
        //     foreach (var country in countryData.data)
        //     {
        //         string countryName = country.name.ToString();
        //         string iso2 = country.Iso2.ToString();
        //         string iso3 = country.Iso3.ToString();
        //
        //         // Yeni ülke oluşturma
        //         var newCountry = new Country
        //         {
        //             Name = countryName,
        //             Iso2 = iso2,
        //             Iso3 = iso3
        //         };
        //
        //         // Oluşturulan ülkeyi veritabanına ekleme
        //         _context.Countries.Add(newCountry);
        //     }
        //
        //     _context.SaveChanges();
        //     return countriesWithNames;
        // }

        private List<SelectListItem?> GetSelectListItems(IEnumerable<object> entities, string textPropertyName,
            string valuePropertyName)
        {
            return entities
                .Select(entity =>
                {
                    var textProperty = entity.GetType().GetProperty(textPropertyName);
                    var valueProperty = entity.GetType().GetProperty(valuePropertyName);

                    if (textProperty != null && valueProperty != null)
                    {
                        return new SelectListItem
                        {
                            Text = textProperty.GetValue(entity)?.ToString(),
                            Value = valueProperty.GetValue(entity)?.ToString()
                        };
                    }

                    return null;
                })
                .Where(item => item != null)
                .ToList();
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
        public async Task<IActionResult> CustomerAdd()
        {
            // Şehir listesini al
            var countries = await _context.Countries.ToListAsync();
            // SelectListItem listesine dönüştür
            List<SelectListItem> selectListItemsCountry = countries.Select(country => new SelectListItem
            {
                Text = country.Name,
                Value = country.Name
            }).ToList();
            ViewBag.CountryList = selectListItemsCountry;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetCities(string countryName)
        {
            var cities = await _context.CountryStates.Where(c => c.CountryName == countryName).ToListAsync();
            return Json(cities);
        }
        [Route("/customer/add")]
        [HttpPost]
        public async Task<IActionResult> CustomerAdd(ApplicationUser model)
        {
            var currentUser = _userManager.GetUserAsync(User).Result;
            Random random = new Random();
            var randomnumber = random.Next(0, 1000000);
            var userMail = _userManager?.FindByEmailAsync(model?.Email).Result;
            if (userMail != null)
            {
                TempData["ErrorMessage"] = $"Hata! E-Posta Adresi Kullanılıyor.";
                return View(model);
            }

            if (ModelState.IsValid)
            {
                model.OwnerId = currentUser.OwnerId;
                model.CreatedBy = currentUser.Name + " " + currentUser.Surname;

                if (model.Name != null)
                {
                    model.UserName = model.Name + model.Surname + randomnumber;
                    model.UserName = RemoveTurkishCharacters(model.UserName);

                    model.IsCustomerPerson = true;
                }

                if (model.OfficialName != null)
                {
                    model.UserName = model.OfficialName + model.OfficialSurname + randomnumber;
                    model.UserName = RemoveTurkishCharacters(model.UserName);

                    model.IsCustomerCompany = true;
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

        public static string RemoveTurkishCharacters(string input)
        {
            // Türkçe karakterlerin İngilizce karşılıkları
            string[] turkishChars = { "Ğ", "ğ", "Ü", "ü", "Ş", "ş", "İ", "ı", "Ö", "ö", "Ç", "ç", " ", "ı", "İ" };
            string[] englishChars = { "G", "g", "U", "u", "S", "s", "I", "i", "O", "o", "C", "c", "", "i", "i" };

            // Türkçe karakterleri ve boşlukları değiştirme
            for (int i = 0; i < turkishChars.Length; i++)
            {
                input = input.Replace(turkishChars[i], englishChars[i]);
            }

            return input;
        }

        [Route("/customer/{id}/update")]
        [HttpGet]
        public async Task<IActionResult> CustomerUpdate(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            // Şehir listesini al
            var countries = await _context.Countries.ToListAsync();
            // SelectListItem listesine dönüştür
            List<SelectListItem> selectListItemsCountry = countries.Select(country => new SelectListItem
            {
                Text = country.Name,
                Value = country.Name
            }).ToList();
            
            
            
            ViewBag.CountryList = selectListItemsCountry;
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