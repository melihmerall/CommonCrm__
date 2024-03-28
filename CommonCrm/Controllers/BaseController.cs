using CommonCrm.Data.DbContexts;
using CommonCrm.Data.Entities.AppUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CommonCrm.Controllers;

public class BaseController: Controller
{
    public readonly UserManager<ApplicationUser> _userManager;
    public readonly ApplicationDbContext _context;

    public BaseController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }
    
    
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        var currentUser = _userManager.GetUserAsync(User);
        if (currentUser.Result != null)
        {
            ViewBag.exchangeRates = _context.ExchangeRates.Where(x => x.OwnerId == currentUser.Result.OwnerId).ToList();

        }
        base.OnActionExecuting(filterContext);
    }
}