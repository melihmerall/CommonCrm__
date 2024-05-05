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
		public string? Name { get; set; }
		public string? Zip { get; set; }
        public string? Surname { get; set; }
		public string? Title { get; set; }
		public string? Address { get; set; }
		public string? OfficialName { get; set; }
		public string? OfficialSurname { get; set; }
        public string? Country { get; set; }
		public string? City { get; set; }
		public string? Description { get; set; }
		public bool IsActive { get; set; }
		public bool? IsPersonnel { get; set; }
		public bool IsCustomerPerson { get; set; }
		public bool IsCustomerCompany { get; set; }
		//
		//Owner is Crm customer uniq ID. If you dont know what is this. get contact with developer.
		public bool IsOwner { get; set; }
		public Guid? OwnerId { get; set; }
		public string? TaxOffice { get; set; }
		public string? TIN { get; set; } // Vergi yükümlülük kimlik numarası
		public string? Fax { get; set; }
		public string? PostCode { get; set; }
		//
		public string? TcNo { get; set; }

        public string? CompanyTitle { get; set; }
		public string? ImagePath { get; set; }
		public DateTime? BirthDate { get; set; }
		public Gender Gender { get; set; }

		public string? CreatedBy { get; set; }
		
		public bool IsCrmOwner { get; set; }
		
		public string? LocationFromCompany { get; set; }
		public string? PaidPrice { get; set; }
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
