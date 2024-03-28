using CommonCrm.Business.DTOs;
using CommonCrm.Business.Extensions;
using CommonCrm.Business.Services;
using CommonCrm.Data.DbContexts;
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

public class AdminController : BaseController
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ProductService _productService;
    private readonly AttributeService _attributeService;
    private readonly ProductUnitService _productUnitService;
    private readonly CategoryService _categoryService;

    public AdminController(UserManager<ApplicationUser> userManager, ProductService productService, AttributeService attributeService, ProductUnitService productUnitService, CategoryService categoryService, ApplicationDbContext context):base(userManager,context)
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
    [Route("/panel/user-add")]
    public async Task<IActionResult> CreateUser()
    {
        return View();
    }
    [Route("/panel/user-add")]
    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = model.MapTo<ApplicationUser>();
            user.OwnerId = Guid.NewGuid();

            var userMail = _userManager?.FindByEmailAsync(model?.Email).Result;
            if(userMail != null)
            {
                TempData["ErrorMessage"] = $"Hata! Geçersiz mail adresi.";
                return View(model) ;

            }
            var result = await _userManager.CreateAsync(user, model.Password);

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
    [HttpGet]
    [Route("/panel/user-list")]
    public async Task<IActionResult> UserList()
    {
        var users = await _userManager.Users.ToListAsync();
        return View(users);
    }
    #endregion


    #region Attribute Process
    [Route("/attribute/add")]
    [HttpGet]
    public async Task<IActionResult> CreateAttribute()
    {
        
        return View();
    }
    [Route("/attribute/add")]
    [HttpPost]
    public IActionResult CreateAttribute(Attribute entity)
    {
        _attributeService.Create(entity);
        return View();
    }


    #endregion
    

}