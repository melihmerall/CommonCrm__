using CommonCrm.Data.Entities.AppUser;
using CommonCrm.Data.Entities.Offer;
using CommonCrm.Data.Entities.Product;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CommonCrm.Business.DTOs;

public class CreateOfferDto
{
    public string? OfferTitle { get; set; }
    public string? OfferDescription { get; set; }
    public string? OfferCode { get; set; }
    public int? Gecerlilik { get; set; }
    public Offer? Offer { get; set; }

    public DateTime OfferStartDate { get; set; }
    public DateTime OfferEndDate { get; set; }
    public bool CurrencyTl { get; set; }
    public bool CurrencyDollar { get; set; }
    public bool CurrencyEuro { get; set; }
    public ApplicationUser? AppUser { get; set; }
    public List<OfferProduct>? OfferProducts { get; set; }
    public IEnumerable<SelectListItem>? AppUsersSelectListItems { get; set; }
    public List<ApplicationUser>? AppUsers { get; set; }
    public List<Product>? Products { get; set; }
    public IEnumerable<SelectListItem>? ProductsSelectListItems { get; set; }
    public int[]? SelectedProduct { get; set; }
    public int? ProductQuantity {get; set; }
    public int? ProductUnit { get; set; }
    public decimal? ProductUnitPrice { get; set; }
    public int? DiscountPercent { get; set; }
    public decimal? DiscountPrice { get; set; }
    public  decimal? TotalPrice { get; set; }
    public string? TerminDuration { get; set; }
    public string? NakliyeMaliyeti { get; set; }
    public string? Incoterms { get; set; }
    public string? OdemeSartlari { get; set; }
    public string? Yukumluluk { get; set; }
}