using System.Reflection.Metadata;
using System.Security.Claims;
using CommonCrm.BackgroundServices;
using CommonCrm.Business.DTOs;
using CommonCrm.Business.Extensions;
using CommonCrm.Business.Extensions.Utilities.FileManage;
using CommonCrm.Business.Services;
using CommonCrm.Data.DbContexts;
using CommonCrm.Data.Entities;
using CommonCrm.Data.Entities.AppUser;
using CommonCrm.Data.Entities.Product;
using CommonCrm.Data.Entities.Product.Enums;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.VisualBasic;
using Collection = CommonCrm.Data.Entities.Product.Collection;
using Constants = CommonCrm.Business.Extensions.Utilities.Constants;

namespace CommonCrm.Controllers
{
    public class ProductController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly ProductService _productService;
        private readonly AttributeService _attributeService;
        private readonly ProductUnitService _productUnitService;
        private readonly CategoryService _categoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(UserManager<ApplicationUser> userManager, ProductService productService,
            AttributeService attributeService, ProductUnitService productUnitService, CategoryService categoryService,
            IWebHostEnvironment webHostEnvironment, ApplicationDbContext context) : base(userManager, context)
        {
            _userManager = userManager;
            _productService = productService;
            _attributeService = attributeService;
            _productUnitService = productUnitService;
            _categoryService = categoryService;
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }

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
                return RedirectToAction("Index", "Home");
            }

            var model = new List<GetProductsDto>();
            var products = _productService.GetByOwnerId(currentUser.OwnerId).Result;
            foreach (var product in products)
            {
                var mappedProduct = product.MapTo<GetProductsDto>();
                mappedProduct.ProductCollections = _context.CollectionProducts.Where(x => x.ProductId == product.Id)
                    .Select(x => x.Collection).ToList();
                mappedProduct.Categories = _context.CategoryProducts.Where(x => x.ProductId == product.Id)
                    .Select(x => x.Category).ToList();
                model.Add(mappedProduct);
            }

            return View(model);
        }

        [Route("/product/add")]
        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            ViewBag.PriceKdvEnumList = Enum.GetValues(typeof(PriceKdvEnum)).Cast<PriceKdvEnum>();
            var currentUser = _userManager.GetUserAsync(User).Result;
            if (currentUser == null)
            {
                TempData["ErrorMessage"] = Constants.WrongUserAuth;
                return View();
            }

            var model = new CreateProductDto();

            var productUnits = await _productUnitService.GetAll();
            var units = productUnits.Where(x => x.OwnerId == currentUser.OwnerId).ToList();
            model.Units = GetSelectListItems(units, "Name", "Id");


            var categories = await _categoryService.GetAll();
            var ownerCategories = categories.Where(x => x.OwnerId == currentUser.OwnerId).ToList();
            model.Categories = GetSelectListItems(ownerCategories, "Name", "Id");

            var productCollections = await _context.Collections.ToListAsync();
            var ownerCollections = productCollections.Where(x => x.OwnerId == currentUser.OwnerId);
            model.ProductCollections = GetSelectListItems(ownerCollections, "Name", "Id");


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
            product.CreatedBy = (currentUser.Name + currentUser.Surname).ToOneLineStringJustNotNulls(emptyValue: "");
            if (formFile != null)
            {
                var subPath = "products";
                var filePath = await formFile.SaveToWwwRootAsync(_webHostEnvironment, model.Name, subPath);
                product.ImagePath = filePath;
            }

            var result = _productService.Create(product);

            if (model.CategoryIds != null)
                foreach (var i in model.CategoryIds)
                {
                    var productCategories = new CategoryProduct()
                    {
                        CategoryId = i,
                        ProductId = product.Id
                    };
                    await _context.CategoryProducts.AddAsync(productCategories);
                    await _context.SaveChangesAsync();
                }

            if (model.CollectionIds != null)
            {
                foreach (var i in model.CollectionIds)
                {
                    var productCollections = new CollectionProduct()
                    {
                        CollectionId = i,
                        ProductId = product.Id
                    };
                    await _context.CollectionProducts.AddAsync(productCollections);
                    await _context.SaveChangesAsync();
                }
            }


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
                return RedirectToAction("WrongOwner", "Auth", new { errorMessage = "Erişim Engellendi" });
            }

            var model = new CreateProductDto();
            model = entity.MapTo<CreateProductDto>();
            model.Id = entity.Id;
            var productUnits = await _productUnitService.GetAll();
            productUnits.Where(x => x.OwnerId == currentUser.OwnerId);
            productUnits.Where(x => x.Id == entity.UnitId);
            model.Units = GetSelectListItems(productUnits, "Name", "Id");

            var categories = await _categoryService.GetAll();
            categories.Where(x => x.OwnerId == currentUser.OwnerId);
            var productCategories = await _context.CategoryProducts
                .Where(x => x.ProductId == entity.Id)
                .Select(x => x.CategoryId)
                .ToListAsync();
            model.CategoryIds = productCategories.ToArray();
            model.Categories = GetSelectListItems(categories, "Name", "Id");

            var collections = await _context.Collections.Where(x => x.OwnerId == currentUser.OwnerId).ToListAsync();
            var productCollections = await _context.CollectionProducts
                .Where(x => x.ProductId == entity.Id)
                .Select(x => x.CollectionId)
                .ToListAsync();
            model.CollectionIds = productCollections.ToArray();
            model.ProductCollections = GetSelectListItems(collections, "Name", "Id");

            return View(model);
        }

        [Route("/product/{id?}/update")]
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(CreateProductDto model, IFormFile formFile)
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


            var product = _context.Products.FirstOrDefault(x => x.Id == model.Id && x.OwnerId == currentUser.OwnerId);
            if (model.CategoryIds != null)
                foreach (var i in model.CategoryIds)
                {
                    var productCategories = new CategoryProduct()
                    {
                        CategoryId = i,
                        ProductId = product.Id
                    };
                    _context.CategoryProducts.Update(productCategories);
                    await _context.SaveChangesAsync();
                }

            if (model.CollectionIds != null)
            {
                foreach (var i in model.CollectionIds)
                {
                    var productCollections = new CollectionProduct()
                    {
                        CollectionId = i,
                        ProductId = product.Id
                    };
                    _context.CollectionProducts.Update(productCollections);
                    await _context.SaveChangesAsync();
                }
            }

            product.Packet = model.Height;
            product.OwnerId = currentUser.OwnerId;
            product.Code = model.Code;
            product.Depth = model.Depth;
            product.Height = model.Height;
            product.Description = model.Description;
            product.Packet = model.Packet;
            product.Name = model.Name;
            product.Unit = model.ProductUnit;
            product.Volume = model.Volume;
            product.Weight = model.Weight;
            product.Width = model.Width;
            product.CurrencyDollar = model.CurrencyDollar;
            product.CurrencyEuro = model.CurrencyEuro;
            product.EnglishDescription = model.EnglishDescription;
            product.EnglishName = model.EnglishName;
            product.KdvDolar = model.KdvDolar;
            product.KdvEuro = model.KdvEuro;
            product.KdvTL = model.KdvTL;
            product.OfferDescription = model.OfferDescription;
            product.OfferQuantity = model.OfferQuantity;
            product.TotalDolar = model.TotalDolar;
            product.TotalEuro = model.TotalEuro;
            product.TotalTL = model.TotalTL;
            product.SalesPriceEuro = model.SalesPriceEuro;
            product.SalesPriceDolar = model.SalesPriceDolar;
            product.SalesPriceTL = model.SalesPriceTL;
            product.IsEuroDefault = model.IsEuroDefault;
            product.IsDolarDefault = model.IsDolarDefault;
            product.IsTlDefault = model.IsTlDefault;
            product.ModifiedBy = currentUser.Name + " " + currentUser.Surname;
            product.ModifiedDate = DateTime.Now;
            product.CreatedBy = (currentUser.Name + currentUser.Surname).ToOneLineStringJustNotNulls();
            if (formFile != null)
            {
                var subPath = "products";
                var filePath = await formFile.SaveToWwwRootAsync(_webHostEnvironment, model.Name, subPath);
                product.ImagePath = filePath;
            }

            _productService.Update(product);

            TempData["CustomMessage"] = Constants.ProductSuccessUpdated;
            return RedirectToAction("ProductList");
        }

        #endregion

        #region Product Unit Process

        [Route("/units/add")]
        [HttpGet]
        public async Task<IActionResult> CreateProductUnit()
        {
            return View();
        }

        [Route("/units/add")]
        [HttpPost]
        public IActionResult CreateProductUnit(ProductUnit entity)
        {
            _productUnitService.Create(entity);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CreateProductUnitPartial()
        {
            return PartialView("CreateProductUnitPartial");
        }

        [HttpGet]
        public async Task<IActionResult> CreateCategoryPartial()
        {
            return PartialView("CreateCategoryPartial");
        }

        [HttpGet]
        public async Task<IActionResult> CreateCollectionPartial()
        {
            return PartialView("CreateCollectionPartial");
        }

        [HttpPost]
        public IActionResult SaveProductUnit(CreateProductDto entity)
        {
            var check = OwnerCheck();
            var user = GetCurrentUser();
            if (entity.ProductUnit?.Name == null)
            {
                TempData["ErrorMessage"] = "Koleksiyon ismi boş olamaz.";
                return Json(new { success = false, message = "Değişiklik izni yok." });
            }

            if (check == false)
            {
                TempData["ErrorMessage"] = Constants.WrongUserAuth;
                return Json(new { success = false, message = "Değişiklik izni yok." });
            }

            try
            {
                var unit = new ProductUnit
                {
                    Name = entity.ProductUnit.Name,
                    OwnerId = user.OwnerId
                };
                _productUnitService.Create(unit);
                TempData["SuccessMessage"] = Constants.SuccessAdded;
                return Json(new { success = true, message = "Değişiklik izni yok." });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveCategoryPartial(CreateProductDto entity)
        {
            var check = OwnerCheck();
            var user = GetCurrentUser();
            if (entity.Category?.Name == null)
            {
                TempData["ErrorMessage"] = "Koleksiyon ismi boş olamaz.";
                return Json(new { success = false, message = "Değişiklik izni yok." });
            }

            if (check == false)
            {
                TempData["ErrorMessage"] = Constants.WrongUserAuth;
                return Json(new { success = false, message = "Değişiklik izni yok." });
            }

            try
            {
                var category = new Category
                {
                    Name = entity.Category?.Name,
                    OwnerId = user.OwnerId
                };
                await _context.Categories.AddAsync(category);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = Constants.SuccessAdded;
                return Json(new { success = true, message = "Ekleme Başarılı." });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveCollectionPartial(CreateProductDto entity)
        {
            var check = OwnerCheck();
            var user = GetCurrentUser();
            if (entity.Collection?.Name == null)
            {
                TempData["ErrorMessage"] = "Koleksiyon ismi boş olamaz.";
                return Json(new { success = false, message = "Değişiklik izni yok." });
            }

            {
            }
            if (check == false)
            {
                TempData["ErrorMessage"] = Constants.WrongUserAuth;
                return Json(new { success = false, message = "Değişiklik izni yok." });
            }

            try
            {
                var collection = new Collection
                {
                    Name = entity.Collection.Name,
                    OwnerId = user.OwnerId,
                };
                await _context.Collections.AddAsync(collection);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = Constants.SuccessAdded;
                return Json(new { success = true, message = "Ekleme Başarılı." });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message + "-Beklenmeyen bir hata oluştu.";
                return Json(new { success = false, message = ex.Message });
            }
        }

        public bool OwnerCheck()
        {
            var currentUser = _userManager.GetUserAsync(User).Result;
            if (currentUser == null)
            {
                TempData["ErrorMessage"] = Constants.WrongUserAuth;
                return false;
            }

            if (currentUser.OwnerId == null)
            {
                TempData["ErrorMessage"] = Constants.OwnerIdNull;
                return false;
            }

            return true;
        }

        public ApplicationUser? GetCurrentUser()
        {
            var currentUser = _userManager.GetUserAsync(User).Result;
            return currentUser;
        }

        #endregion

        [HttpPost]
        public async Task<IActionResult> CurrencyChange(decimal newValue, string code)
        {
            var currentUser = GetCurrentUser();
            var currentRate =
                _context.ExchangeRates.FirstOrDefault(x => x.OwnerId == currentUser.OwnerId && x.CurrencyCode == code);

            if (currentRate == null)
            {
                var t = new ExchangeRate()
                {
                    CurrencyCode = code,
                    Rate = newValue,
                    OwnerId = currentUser.OwnerId,
                    CreatedBy = currentUser.Name + currentUser.Surname + " - Email:" + currentUser.Email
                };
                await _context.ExchangeRates.AddAsync(t);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                currentRate.Rate = newValue;
                currentRate.CurrencyCode = code;
                currentRate.OwnerId = currentUser.OwnerId;
                currentRate.ModifiedBy = currentUser.Name + currentUser.Surname + " - Email:" + currentUser.Email;

                _context.ExchangeRates.Update(currentRate);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CurrencyRealValue()
        {
            var exchangeRatesdlr =
                CurrencyBackgroundService.SharedData._exchangeRates?.FirstOrDefault(x => x.CurrencyCode == "DLR");
            var exchangeRateseur =
                CurrencyBackgroundService.SharedData._exchangeRates?.FirstOrDefault(x => x.CurrencyCode == "EUR");
            var currentUser = GetCurrentUser();
            var currentRate = _context.ExchangeRates.Where(x => x.OwnerId == currentUser.OwnerId).ToList();
            if (currentUser == null)
            {
                return Json(new { success = false, message = "Değişiklik izni yok." });
            }

            if (currentRate.IsNullOrEmpty())
            {
                var rateList = new List<ExchangeRate>()
                {
                    new()
                    {
                        CurrencyCode = "dolar",
                        Rate = exchangeRatesdlr.Rate,
                        CreatedBy = currentUser.Name + " " + currentUser.Surname,
                        OwnerId = currentUser.OwnerId
                    },
                    new()
                    {
                        CurrencyCode = "euro",
                        Rate = exchangeRateseur.Rate,
                        CreatedBy = currentUser.Name + " " + currentUser.Surname,
                        OwnerId = currentUser.OwnerId
                    }
                };

                await _context.ExchangeRates.AddRangeAsync(rateList);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var i in currentRate)
                {
                    if (i.CurrencyCode == "dolar")
                    {
                        i.Rate = exchangeRatesdlr.Rate;
                    }

                    if (i.CurrencyCode == "euro")
                    {
                        i.Rate = exchangeRateseur.Rate;
                    }

                    i.ModifiedBy = currentUser.Name + " " + currentUser.Surname;

                    _context.ExchangeRates.Update(i);
                }

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}