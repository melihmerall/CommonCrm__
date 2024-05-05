using CommonCrm.Data.Entities.AppUser;

namespace CommonCrm.Data.Entities.Offer;

public class Offer: BaseEntity
{
    public string OfferTitle { get; set; }
    public string? OfferDescription { get; set; }
    public string? OfferCode { get; set; }
    public DateTime OfferStartDate { get; set; }
    public int? Gecerlilik { get; set; }
    public DateTime OfferEndDate { get; set; }
    public bool CurrencyTl { get; set; }
    public bool CurrencyDollar { get; set; }
    public bool CurrencyEuro { get; set; }
    public decimal? TotalPrice { get; set; }
    public decimal? DiscountPrice { get; set; }
    public ApplicationUser? AppUser { get; set; }
    public List<Product.Product>? Products { get; set; }
    public List<OfferProduct>? OffersProducts { get; set; }
    public string? TerminDuration { get; set; }
    public string? NakliyeMaliyeti { get; set; }
    public string? Incoterms { get; set; }
    public string? OdemeSartlari { get; set; }
    public string? Yukumluluk { get; set; }
    
}