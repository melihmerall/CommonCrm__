using System.Reflection;
using System.Security.Claims;
using CommonCrm.Data.Entities.AppUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CommonCrm.Models.RoleVM;

public class RoleViewModel
{
    public string RoleName { get; set; }
    public string Description { get; set; }
    public List<ClaimViewModel>? Claims { get; set; }
    public List<SelectListItem>? SelectedClaims { get; set; }
    public string[]? SelectedIds { get; set; }
    public Dictionary<string, IList<string>>? RoleClaims { get; set; }
    public List<ApplicationRole>? Roles { get; set; } 
}
public class ClaimViewModel
{
    public string Type { get; set; }
    public string Value { get; set; }
    public bool IsSelected { get; set; }
}
public static class CustomClaimTypes
{
    //Product
    public const string AddProduct = "UrunEkle";
    public const string DeleteProduct = "UrunSil";
    public const string EditProduct = "UrunDüzenle";
    //
    //User
    public const string AddUser = "KullaniciEkle";
    public const string DeleteUser = "KullaniciSil";
    public const string EditUser = "KullaniciDüzenle";
    //
    //Role
    public const string AddRole = "RolEkle";
    public const string DeleteRole = "RolSil";
    public const string EditRole = "RolDüzenle";
    //
    public static List<Claim> GetAllClaims()
    {
        List<Claim> claims = new List<Claim>();

        Type type = typeof(CustomClaimTypes);
        FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

        foreach (FieldInfo field in fields)
        {
            // Claim türü ve claim değeri ile yeni bir Claim nesnesi oluşturun
            claims.Add(new Claim(field.Name, (string)field.GetValue(null)));
        }

        return claims;
    }

}