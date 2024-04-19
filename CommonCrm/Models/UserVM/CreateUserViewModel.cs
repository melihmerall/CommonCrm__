using System.ComponentModel.DataAnnotations;
using CommonCrm.Data.Entities.AppUser;

namespace CommonCrm.Models.UserVM;

public class CreateUserViewModel
{

    [Required(ErrorMessage = "Boş olamaz.")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Boş olamaz.")]
    public string? Surmame { get; set; }
    public string? Address { get; set; }


    [EmailAddress(ErrorMessage = "Mail Adresi formatında girin.(test@test.com)")]
    public string? Email { get; set; }
    
    [Required(ErrorMessage = "Boş olamaz.")]
    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
    [Required(ErrorMessage = "Boş olamaz.")]

    public string ConfirmPassword { get; set; }

    public string? Country { get; set; }
    public string? City { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public string? ImagePath { get; set; }
    public DateTime? BirthDate { get; set; }
    public Gender Gender { get; set; }
    public string PhoneNumber { get; set; }
    public string? LocationFromCompany { get; set; }
    public string? TcNo { get; set; }


}

