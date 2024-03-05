using CommonCrm.Data.Entities.AppUser;
using CommonCrm.Models.UserVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CommonCrm.Controllers;

public class AuthController: Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    public AuthController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager )
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    [HttpGet]
    [Route("/")]

    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    [Route("/")]
    public async Task<IActionResult> Login(UserLoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                if (result.Succeeded)
                {
                    // Başarılı giriş durumunda yönlendirme yapabilirsiniz
                    return RedirectToAction("Index", "Home");
                }
            }

            // Giriş başarısızsa hata ekleyebilir veya farklı bir işlem yapabilirsiniz
            ModelState.AddModelError(string.Empty, "Giriş denemesi başarısız.");
        }

        // Giriş başarısız olduğunda veya model geçerli değilse aynı sayfaya geri dön
        return View(model);
    }
}