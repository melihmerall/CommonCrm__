using System.ComponentModel.DataAnnotations;
using CommonCrm.Data.Entities.AppUser;

namespace CommonCrm.Models.UserVM;

public class CreateUserViewModel
{
    public string UserName { get; set; }

    [EmailAddress]
    public string Email { get; set; }

    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
    public string ConfirmPassword { get; set; }

    public string Country { get; set; }
    public string City { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public bool IsCompany { get; set; }
    public bool IsCustomer { get; set; }
    public string? CompanyTitle { get; set; }
    public string? ImagePath { get; set; }
    public DateTime? BirthDate { get; set; }
    public Gender Gender { get; set; }

}

