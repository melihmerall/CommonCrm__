using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonCrm.Business.Extensions
{
	public class GenericAuthorizationRequirement : IAuthorizationRequirement
	{
		public string Operation { get; }
		public string Role { get; }

		public GenericAuthorizationRequirement(string operation, string role)
		{
			Operation = operation;
			Role = role;
		}
	}

}
