using System.Security.Claims;
using CommonCrm.Business.DTOs;
using CommonCrm.Business.Extensions;
using CommonCrm.Business.Services;
using CommonCrm.Data.Entities.AppUser;
using CommonCrm.Data.Entities.Product;
using CommonCrm.Data.Entities.Product.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CommonCrm.Controllers
{

    public class ProductController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ProductService _productService;
        private readonly AttributeService _attributeService;
        private readonly ProductUnitService _productUnitService;
        private readonly CategoryService _categoryService;

        public ProductController(UserManager<ApplicationUser> userManager, ProductService productService, AttributeService attributeService, ProductUnitService productUnitService, CategoryService categoryService)
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

        #region Product Process

        [Route("/product/list")]
        [HttpGet]
        public IActionResult ProductList()
        {
            return View();
        }

        [Route("/product/add")]
        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.PriceKdvEnumList = Enum.GetValues(typeof(PriceKdvEnum)).Cast<PriceKdvEnum>();

            var model = new CreateProductDto();

            var productUnits = await _productUnitService.GetAll();
            model.Units = GetSelectListItems(productUnits, "Name", "Id");
 

            var categories = await _categoryService.GetAll();
            model.Categories = GetSelectListItems(categories, "Name", "Id");


            return View(model);
        }
        [Route("/product/add")]
        [HttpPost]
        public IActionResult CreateProduct(CreateProductDto model)
        {
            var currentUser = _userManager.GetUserAsync(User).Result;
            var product = model.MapTo<Product>();
            

            return View();
        }



        #endregion

        #region Product Unit Process
        [Route("/unit/add")]
        [HttpGet]
        public async Task<IActionResult> CreateProductUnit()
        {

            return View();
        }
        [Route("/unit/add")]
        [HttpPost]
        public IActionResult CreateProductUnit(ProductUnit entity)
        {
            _productUnitService.Create(entity);
            return View();
        }


        #endregion
    }
}
