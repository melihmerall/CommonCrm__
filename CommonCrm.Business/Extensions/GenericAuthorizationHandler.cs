using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonCrm.Business.Extensions
{
	public class GenericAuthorizationHandler : IAuthorizationHandler
	{
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly UserManager<IdentityUser> _userManager;

		public GenericAuthorizationHandler(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
		{
			_roleManager = roleManager;
			_userManager = userManager;
			
		}
		
		public async Task HandleAsync(AuthorizationHandlerContext context)
		{
			var resource = context.Resource;
			var requirement = context.Requirements.First() as GenericAuthorizationRequirement;

			var role = await _roleManager.FindByNameAsync(requirement.Role);
			if (role == null)
			{
				context.Fail();
				return;
			}

			var hasRoleClaim = context.User.Claims.Any(c => c.Type == "role" && c.Value == requirement.Role);
			var hasOperationClaim = context.User.Claims.Any(c => c.Type == "operation" && c.Value == requirement.Operation);

			if (hasRoleClaim && hasOperationClaim)
			{
				context.Succeed(requirement);
			}
			else
			{
				context.Fail();
			}
		}
	}

}
