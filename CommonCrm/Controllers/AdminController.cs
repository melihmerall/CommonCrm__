using CommonCrm.Business.DTOs;
using CommonCrm.Business.Extensions;
using CommonCrm.Business.Services;
using CommonCrm.Data.Entities.AppUser;
using CommonCrm.Data.Entities.Product;
using CommonCrm.Models.UserVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Attribute = CommonCrm.Data.Entities.Product.Attribute;

namespace CommonCrm.Controllers;

public class AdminController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ProductService _productService;
    private readonly AttributeService _attributeService;
    private readonly ProductUnitService _productUnitService;
    private readonly CategoryService _categoryService;

    public AdminController(UserManager<ApplicationUser> userManager, ProductService productService, AttributeService attributeService, ProductUnitService productUnitService, CategoryService categoryService)
    {
        _userManager = userManager;
        _productService = productService;
        _attributeService = attributeService;
        _productUnitService = productUnitService;
        _categoryService = categoryService;
    }

    #region Special Methods

    private List<SelectListItem?> GetSelectListItems(IEnumerable<object> entities, string textPropertyName, string valuePropertyName)
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


    #endregion
    
    #region User Process
    [HttpGet]
    [Route("/panel/kullanici-ekle")]
    public async Task<IActionResult> CreateUser()
    {
        return View();
    }
    [Route("/panel/kullanici-ekle")]
    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = model.MapTo<ApplicationUser>();

            var userMail = _userManager?.FindByEmailAsync(model?.Email);
            if(userMail != null)
            {
                TempData["ErrorMessage"] = $"Hata! Geçersiz mail adresi.";
                return View(model) ;

            }
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                
                return RedirectToAction("Index", "Home"); 
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
    [HttpGet]
    [Route("/panel/kullanici-liste")]
    public async Task<IActionResult> UserList()
    {
        var users = await _userManager.Users.ToListAsync();
        return View(users);
    }
    #endregion

    #region Product Process
    [Route("/urun/ekle")]
    [HttpGet]
    public async  Task<IActionResult> CreateProduct()
    {
        var model = new CreateProductDto();
        
        var productUnits = await _productUnitService.GetAll();
        model.ProductUnits = GetSelectListItems(productUnits, "Name", "Id");
        
        var attributes = await _attributeService.GetAll();
        model.Attributes = GetSelectListItems(attributes, "Name", "Id");
        
        var categories = await _categoryService.GetAll();
        model.Categories = GetSelectListItems(categories, "Name", "Id");
        
        
        return View(model);
    }
    [Route("/urun/ekle")]
    [HttpPost]
    public IActionResult CreateProduct(CreateProductDto model)
    {
        // Sadece "Admin" rolüne sahip ve "CreateProduct" yetkisi olan kullanıcılar bu eyleme erişebilir
        
        return View();
    }

    

    #endregion

    #region Product Unit Process
    [Route("/birim/ekle")]
    [HttpGet]
    public async Task<IActionResult> CreateProductUnit()
    {
        
        return View();
    }
    [Route("/birim/ekle")]
    [HttpPost]
    public IActionResult CreateProductUnit(ProductUnit entity)
    {
        _productUnitService.Create(entity);
        return View();
    }
    

    #endregion

    #region Attribute Process
    [Route("/ozellik/ekle")]
    [HttpGet]
    public async Task<IActionResult> CreateAttribute()
    {
        
        return View();
    }
    [Route("/ozellik/ekle")]
    [HttpPost]
    public IActionResult CreateAttribute(Attribute entity)
    {
        _attributeService.Create(entity);
        return View();
    }


    #endregion

    [Route("/urun/listesi")]

    public IActionResult Product()
    {
        return View();
    }

    [Route("/teklifler/teklifler")]
    public IActionResult Offers()
    {
        return View();
    }

    [Route("/teklifler/talepleri")]
    public IActionResult CreateOffers()
    {
        return View();
    }
    [Route("/teklif/olustur")]
    public IActionResult OfferRequest()
    {
        return View();
    }

    [Route("/musteriler/musteriler")]
    public IActionResult Customers()
    {
        return View();
    }
}