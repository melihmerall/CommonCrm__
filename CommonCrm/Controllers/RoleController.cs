using System.Security.Claims;
using CommonCrm.Models.RoleVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CommonCrm.Controllers;

public class RolesController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RolesController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    [Route("/rol/listesi")]
    public async Task<IActionResult> Index()
    {
        var model = new RoleViewModel();
    
        var roles = await _roleManager.Roles.ToListAsync();
        var roleClaims = new Dictionary<string, IList<string>>();

        foreach (var role in roles)
        {
            var claims = await _roleManager.GetClaimsAsync(role);
            var claimValues = claims.Select(c => c.Value).ToList();
            roleClaims[role.Name] = claimValues;
        }

        model.RoleClaims = roleClaims;
        model.Roles = roles;

        return View(model);
    }
    
    [HttpGet]
    [Route("/rol/ekle")]

    public async Task<IActionResult> Create()
    {
        var claims = CustomClaimTypes.GetAllClaims()
            .Select(c => new SelectListItem { Value = c.Type, Text = c.Value }).ToList();

        var model = new RoleViewModel
        {
            SelectedClaims = claims
        };
    
        return View(model);
    }
    
    [Route("/rol/ekle")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RoleViewModel model)
    {
        var selectedClaims = new List<Claim>();

        foreach (var selectedId in model.SelectedIds)
        {
            var claims = CustomClaimTypes.GetAllClaims().Where(c => c.Type == selectedId).ToList();
            selectedClaims.AddRange(claims);
        }
        if (ModelState.IsValid)
        {
            // Yeni rolü oluştur
            
            var role = new IdentityRole { Name = model.RoleName };
            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                // Seçilen talepleri al

                // Role taleplerini ata
                foreach (var claim in selectedClaims)
                {
                    var resultClaims = await _roleManager.AddClaimAsync(role, claim);
                    if (!result.Succeeded)
                    {
                        // Hata işleme
                    }
                }

                // Başarılı rol oluşturma işlemi
                return RedirectToAction("Index", "Roles");
            }
            else
            {
                // Rol oluşturma başarısız oldu, hata işleme
                // Hata mesajını döndür veya gerekli işlemleri yap
            }
        }

        // ModelState geçerli değilse veya işleme başarısız olduysa, aynı sayfaya geri dön
        return View(model);

    }

    public async Task<IActionResult> Edit(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var role = await _roleManager.FindByIdAsync(id);

        if (role == null)
        {
            return NotFound();
        }

        return View(role);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(IdentityRole role)
    {
        if (ModelState.IsValid)
        {
            var result = await _roleManager.UpdateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        return View(role);
    }

    public async Task<IActionResult> Delete(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var role = await _roleManager.FindByIdAsync(id);

        if (role == null)
        {
            return NotFound();
        }

        return View(role);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);

        if (role != null)
        {
            await _roleManager.DeleteAsync(role);
        }

        return RedirectToAction("Index");
    }
}