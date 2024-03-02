using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonCrm.Data.Entities.AppUser
{
	public class ApplicationUser: IdentityUser
	{
        public string? Country { get; set; }
		public string? City { get; set; }
		public string? Description { get; set; }
		public bool IsActive { get; set; }
		public bool IsCompany { get; set; }
		public bool IsCustomer { get; set; }
		public string? CompanyTitle { get; set; }
		public string? ImagePath { get; set; }
		public DateTime? BirthDate { get; set; }
		public Gender Gender { get; set; }
		public string? GenderText
		{
			get => Gender.ToString();
			set => Gender = Enum.Parse<Gender>(value);
		}

	}

	public enum Gender
	{
		Male,
		Female,
		Other
	}
}
