using System.Reflection.Metadata;
using System.Security.Claims;
using CommonCrm.Business.DTOs;
using CommonCrm.Business.Extensions;
using CommonCrm.Business.Extensions.Utilities.FileManage;
using CommonCrm.Business.Services;
using CommonCrm.Data.Entities.AppUser;
using CommonCrm.Data.Entities.Product;
using CommonCrm.Data.Entities.Product.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
using Constants = CommonCrm.Business.Extensions.Utilities.Constants;

namespace CommonCrm.Controllers
{

    public class ProductController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ProductService _productService;
        private readonly AttributeService _attributeService;
        private readonly ProductUnitService _productUnitService;
        private readonly CategoryService _categoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(UserManager<ApplicationUser> userManager, ProductService productService, AttributeService attributeService, ProductUnitService productUnitService, CategoryService categoryService, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _productService = productService;
            _attributeService = attributeService;
            _productUnitService = productUnitService;
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
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
            var currentUser = _userManager.GetUserAsync(User).Result;
            if (currentUser == null)
            {
                TempData["ErrorMessage"] = Constants.WrongUserAuth;
                return RedirectToAction("Index","Home");
            }
            var model = new List<GetProductsDto>();
            var products = _productService.GetByOwnerId(currentUser.OwnerId).Result;
            foreach (var product in products)
            {
                var mappedProduct = product.MapTo<GetProductsDto>();
                model.Add(mappedProduct);
            }
            return View(model);
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
            
            var productCollections = await _categoryService.GetAll();
            model.ProductCollections = GetSelectListItems(categories, "Name", "Id");


            return View(model);
        }
        [Route("/product/add")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto model, IFormFile? formFile)
        {
            var currentUser = _userManager.GetUserAsync(User).Result;
            if (currentUser == null)
            {
                TempData["ErrorMessage"] = Constants.WrongUserAuth;
                return View(model);
            }
            if (currentUser.OwnerId == null)
            {
                TempData["ErrorMessage"] = Constants.OwnerIdNull;
                return View(model);
            }
            var product = model.MapTo<Product>();
            product.OwnerId = currentUser.OwnerId;
            product.CreatedBy = (currentUser.Name + currentUser.Surname).ToOneLineStringJustNotNulls(emptyValue:"");
            if (formFile != null)
            {
                var subPath = "products";
                var filePath = await formFile.SaveToWwwRootAsync(_webHostEnvironment,model.Name , subPath);
                product.ImagePath = filePath;
            } 
            var result = _productService.Create(product);
            if (result)
            {
                TempData["CustomMessage"] = Constants.ProductSuccessCreated;
                return RedirectToAction("ProductList");
            }
            return View();
        }
        
        [Route("/product/{id?}/update")]
        [HttpGet]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id)
        {
            ViewBag.PriceKdvEnumList = Enum.GetValues(typeof(PriceKdvEnum)).Cast<PriceKdvEnum>();
            var currentUser = _userManager.GetUserAsync(User).Result;

            var entity = await _productService.GetById(id);

            if (currentUser?.OwnerId != entity.OwnerId)
            {
                return RedirectToAction("WrongOwner", "Auth", new { errorMessage = "Erisim Engellendi" });
            }
            var model = new UpdateProductDto();
            model.CurrentUser = currentUser;
            model = entity.MapTo<UpdateProductDto>();
            
            var productUnits = await _productUnitService.GetAll();
            model.Units = GetSelectListItems(productUnits, "Name", "Id");
            
            var categories = await _categoryService.GetAll();
            model.Categories = GetSelectListItems(categories, "Name", "Id");
            
            var productCollections = await _categoryService.GetAll();
            model.ProductCollections = GetSelectListItems(categories, "Name", "Id");
            
            return View(model);
        }
        [Route("/product/{id?}/update")]
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto model, IFormFile formFile)
        {
            var currentUser = _userManager.GetUserAsync(User).Result;

            model.CurrentUser = currentUser;
            if (currentUser == null)
            {
                TempData["ErrorMessage"] = Constants.WrongUserAuth;
                return View(model);
            }
            if (currentUser.OwnerId == null)
            {
                TempData["ErrorMessage"] = Constants.OwnerIdNull;
                return View(model);
            }
            var product = model.MapTo<Product>();
            product.OwnerId = currentUser.OwnerId;
            product.CreatedBy = (currentUser.Name + currentUser.Surname).ToOneLineStringJustNotNulls();
            if (formFile != null)
            {
                var subPath = "products";
                var filePath = await formFile.SaveToWwwRootAsync(_webHostEnvironment,model.Name , subPath);
                product.ImagePath = filePath;
            } 
            var result = _productService.Create(product);
            if (result)
            {
                TempData["CustomMessage"] = Constants.ProductSuccessCreated;
                return RedirectToAction("ProductList");
            }
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
