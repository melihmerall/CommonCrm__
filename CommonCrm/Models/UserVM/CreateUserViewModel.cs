using System.ComponentModel.DataAnnotations;

namespace CommonCrm.Models.UserVM;

public class CreateUserViewModel
{
    [Microsoft.Build.Framework.Required]
    public string UserName { get; set; }

    [Microsoft.Build.Framework.Required]
    [EmailAddress]
    public string Email { get; set; }

    [Microsoft.Build.Framework.Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Microsoft.Build.Framework.Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
    public string ConfirmPassword { get; set; }
}