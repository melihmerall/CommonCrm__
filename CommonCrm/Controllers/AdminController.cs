using CommonCrm.Data.Entities.AppUser;
using CommonCrm.Models.UserVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CommonCrm.Controllers;

public class AdminController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    public IActionResult CreateUser()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser(){ UserName = model.UserName, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Kullanıcı başarıyla oluşturuldu, diğer işlemler
                return RedirectToAction("Index", "Home"); // Örnek olarak ana sayfaya yönlendiriyoruz
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(model);
    }
    [Route("/urun/ekle")]
    public IActionResult CreateProduct()
    {
        // Sadece "Admin" rolüne sahip ve "CreateProduct" yetkisi olan kullanıcılar bu eyleme erişebilir
        
        return View();
    }
}