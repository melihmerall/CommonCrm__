using Microsoft.AspNetCore.Identity;

namespace CommonCrm.Data.Entities.AppUser;

public class ApplicationRole: IdentityRole
{
    public Guid? OwnerId { get; set; }
}