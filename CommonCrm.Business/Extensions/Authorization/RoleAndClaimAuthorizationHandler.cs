using System.Linq;
using System.Threading.Tasks;
using CommonCrm.Data.Entities.AppUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

public class RoleAndClaimAuthorizationHandler : AuthorizationHandler<IAuthorizationRequirement>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public RoleAndClaimAuthorizationHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, IAuthorizationRequirement requirement)
    {
        var user = await _userManager.GetUserAsync(context.User);

        if (user == null)
        {
            context.Fail();
            return;
        }

        // Roller ve claim'leri kontrol et
        if (context.User.IsInRole("Admin") && context.User.HasClaim(c => c.Type == "Permission" && (c.Value == "CreateProduct" || c.Value == "UpdateProduct" || c.Value == "DeleteProduct")))
        {
            context.Succeed(requirement);
        }
        else if (context.User.IsInRole("User") && context.User.HasClaim(c => c.Type == "Permission" && c.Value == "ViewProduct"))
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
    }
}