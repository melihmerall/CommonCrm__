using CommonCrm.Business.DTOs;
using CommonCrm.Business.Extensions;
using CommonCrm.Business.Extensions.Utilities;
using CommonCrm.Business.Services;
using CommonCrm.Data.DbContexts;
using CommonCrm.Data.Entities.AppUser;
using CommonCrm.Data.Entities.Offer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CommonCrm.Controllers
{
    public class OfferController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly AttributeService _attributeService;

        #region Special Methods

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
        public List<SelectListItem> GetSelectListItems(IEnumerable<object> users, string primaryPropertyName, string secondaryPropertyName, string idPropertyName)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            foreach (var user in users)
            {
                // Öncelikle "Name" özelliğini kontrol edin
                var primaryPropertyValue = user.GetType().GetProperty(primaryPropertyName)?.GetValue(user, null);

                // Eğer "Name" özelliği null veya boş ise, "OfficialName" özelliğini kullanın
                if (primaryPropertyValue == null || string.IsNullOrWhiteSpace(primaryPropertyValue.ToString()))
                {
                    primaryPropertyValue = user.GetType().GetProperty(secondaryPropertyName)?.GetValue(user, null);
                }

                // ID özelliğini alın
                var id = user.GetType().GetProperty(idPropertyName)?.GetValue(user, null);

                // SelectListItems listesine ekleme
                selectListItems.Add(new SelectListItem { Value = id.ToString(), Text = primaryPropertyValue.ToString() });
            }

            return selectListItems;
        }
        #endregion
        public OfferController(UserManager<ApplicationUser> userManager, ApplicationDbContext context,
            IdentityContext identityContext, AttributeService attributeService)
        {
            _userManager = userManager;
            _context = context;
            _userManager = userManager;
            _attributeService = attributeService;
            _attributeService = attributeService;
        }
        public async Task<IActionResult> DeleteOffer(int id)
        {
            var offer = _context.Offers.FirstOrDefaultAsync(x=>x.Id == id).Result;
            if (offer != null)
            {
                _context.Remove(offer);
                await _context.SaveChangesAsync();
                TempData["CustomMessage"] = Constants.SuccessDeleted;

                return RedirectToAction("OfferList");

            }

            TempData["ErrorMessage"] = Constants.UnSuccessDeleted;

            return RedirectToAction("OfferList");
        }
        [Route("/offer/list")]
        [HttpGet]
        public IActionResult OfferList()
        {
            var currentUser = _userManager.GetUserAsync(User).Result;
            var offers = _context.Offers.Where(x => x.OwnerId == currentUser.OwnerId)
                .Include(x=>x.AppUser)
                .Include(x => x.OffersProducts)
                .ToList();

            return View(offers);
        }
        /*
        [Route("/offer/settings")]
        [HttpGet]
        public IActionResult OfferSettings()
        {
            var currentUser = _userManager.GetUserAsync(User).Result;
            var offers = _userManager.Users
                ?.Where(x => x.OwnerId == currentUser.OwnerId && (x.IsCustomerPerson || x.IsCustomerCompany)).ToList();

            return View(offers);
        }
        */
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
        public async Task<IActionResult> OfferAdd()
        {
            var currentUser = _userManager.GetUserAsync(User).Result;
            var model = new CreateOfferDto();
            model.Products =  await _context.Products.Where(x => x.OwnerId == currentUser.OwnerId).ToListAsync();
            model.AppUsers =  await _userManager.Users.Where(x => x.OwnerId == currentUser.OwnerId && (x.IsCustomerPerson || x.IsCustomerCompany) && x.IsCrmOwner == false).ToListAsync();
            model.AppUsersSelectListItems = GetSelectListItems(model.AppUsers, "Name", "Title", "Id");
            model.ProductsSelectListItems = GetSelectListItems(model.Products, "Name","Id");

            return View(model);
        }
        [HttpGet]
        [Route("/Offer/GetUser/{id?}")]
        public IActionResult GetUser(Guid id)
        {
            if (id == null || id.ToString() == "00000000-0000-0000-0000-000000000000")
            {
                return NotFound(); 
            }
            var user =  _userManager.FindByIdAsync(id.ToString()).Result; // Kullanıcıyı veritabanından id'ye göre bulun

            if (user == null)
            {
                return NotFound(); 
            }

            return Ok(user); 
        }
        [Route("/offer/add")]
        [HttpPost]
        public async Task<IActionResult> OfferAdd(CreateOfferDto model)
        {
            var currentUser = _userManager.GetUserAsync(User).Result;
            Random random = new Random();
            var randomnumber = random.Next(0, 1000000);

            var customer = _userManager.FindByEmailAsync(model.AppUser?.Email).Result;

            if (customer == null)
            {
                model.AppUser.OwnerId = currentUser.OwnerId;
                model.AppUser.CreatedBy = currentUser.Name + " " + currentUser.Surname;
                
                if (model.AppUser.Name != null)
                {
                    model.AppUser.UserName = model.AppUser.Name + model.AppUser.Surname + randomnumber;

                    model.AppUser.IsCustomerPerson = true;
                }

                if (model.AppUser.OfficialName != null)
                {
                    model.AppUser.UserName = model.AppUser.OfficialName + model.AppUser.OfficialSurname + randomnumber;

                    model.AppUser.IsCustomerCompany = true;
                }

                model.AppUser.IsActive = true;

                await _userManager.CreateAsync(model.AppUser);
                TempData["CustomMessage"] = Constants.CustomerAddedSuccess;

            }
            
            if (ModelState.IsValid)
            {
                // Mapping  model to offer.
                var offer = model.MapTo<Offer>();
                offer.OwnerId = currentUser.OwnerId;
                offer.CreatedBy = currentUser.Name + " " + currentUser.Surname;
                offer.OffersProducts = model.OfferProducts;

                await _context.Offers.AddAsync(offer);
                await _context.SaveChangesAsync();
                
                TempData["CustomMessage"] = Constants.OfferSuccess;
                return RedirectToAction("OfferList", "Offer");
                
                    
            }
            else
            {
                foreach (var modelError in ModelState.Values)
                {
                    foreach (var error in modelError.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.ErrorMessage);
                        TempData["ErrorMessage"] = error.ErrorMessage;

                    }
                    return View(model);

                }
            }

            return View(model);
        }

        [Route("/offer/{id}/update")]
        [HttpGet]
        public async Task<IActionResult> OfferUpdate(int id)
        {
            var currentUser = _userManager.GetUserAsync(User).Result;
            var model = new CreateOfferDto();
            model.Products =  await _context.Products.Where(x => x.OwnerId == currentUser.OwnerId).ToListAsync();
            model.AppUsers =  await _userManager.Users.Where(x => x.OwnerId == currentUser.OwnerId && (x.IsCustomerPerson || x.IsCustomerCompany) && x.IsCrmOwner == false).ToListAsync();
            model.AppUsersSelectListItems = GetSelectListItems(model.AppUsers, "Name", "Title", "Id");
            model.ProductsSelectListItems = GetSelectListItems(model.Products, "Name","Id");
            model.Offer = _context.Offers.Include(x=>x.AppUser).Include(x=>x.OffersProducts).FirstOrDefault(x => x.Id == id && x.OwnerId == currentUser.OwnerId);
            model.OfferProducts = model.Offer.OffersProducts;
            model.Offer.TotalPrice = 0;
            model.Offer.DiscountPrice = 0;
            
            return View(model);
        }

        [Route("/offer/{id}/update")]
        [HttpPost]
        public async Task<IActionResult> OfferUpdate(CreateOfferDto model)
        {
            var currentUser = _userManager.GetUserAsync(User).Result;
            var customer = _userManager.FindByEmailAsync(model.AppUser?.Email).Result;
            
            //
            var offerProductsToDelete = await _context.OfferProducts
                .Where(x => x.OwnerId == currentUser.OwnerId && x.OfferId == model.Offer.Id).ToListAsync();
            

            _context.OfferProducts.RemoveRange(offerProductsToDelete);
            await _context.SaveChangesAsync();
            //
            var offer = _context.Offers.FirstOrDefault(x => x.Id == model.Offer.Id && x.OwnerId == currentUser.OwnerId);
            if (ModelState.IsValid)
            {
                model.Offer.AppUser = customer;

                // TODO: Mapping  model to offer.
                offer.ModifiedBy = currentUser.Name + " " + currentUser.Surname;
                offer.ModifiedDate = DateTime.Now;
                offer.OwnerId = currentUser.OwnerId;
                offer.CreatedBy = currentUser.Name + " " + currentUser.Surname;
                offer.OffersProducts = model.OfferProducts;
                offer.Gecerlilik = model.Offer.Gecerlilik;
                offer.OfferStartDate = model.Offer.OfferStartDate;
                offer.OfferEndDate = model.Offer.OfferEndDate;
                offer.Incoterms = model.Offer.Incoterms;
                offer.Yukumluluk = model.Offer.Yukumluluk;
                offer.OfferCode = model.Offer.OfferCode;
                offer.NakliyeMaliyeti = model.Offer.NakliyeMaliyeti;
                offer.OdemeSartlari = model.Offer.OdemeSartlari;
                offer.OfferDescription = model.Offer.OfferDescription;
                offer.TerminDuration = model.Offer.TerminDuration;
                offer.OfferTitle = model.Offer.OfferTitle;
                
                if (!model.OfferProducts.IsNullOrEmpty())
                {
                    foreach (var offerProduct in model.OfferProducts)
                    {
                        offerProduct.OfferId = model.Offer.Id;
                        offerProduct.OwnerId = currentUser.OwnerId;
                    }
                    await _context.OfferProducts.AddRangeAsync(model.OfferProducts);
                    await _context.SaveChangesAsync();
                }
                

                _context.Offers.Update(offer);
                await _context.SaveChangesAsync();
                
                TempData["CustomMessage"] = "Teklif güncellendi.";
                return RedirectToAction("OfferList", "Offer");
                    
            }
            else
            {
                foreach (var modelError in ModelState.Values)
                {
                    foreach (var error in modelError.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.ErrorMessage);
                        TempData["ErrorMessage"] = error.ErrorMessage;

                    }
                    return View(model);

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