namespace CommonCrm.Data.Entities.Offer;

public class OfferProduct: BaseEntity
{
    public int? ProductQuantity {get; set; }
    public int? ProductUnit { get; set; }
    public decimal? ProductUnitPrice { get; set; }
    public int? DiscountPercent { get; set; }
    public decimal? DiscountPrice { get; set; }
    public  int? kdv { get; set; }
    public  decimal? TotalPrice { get; set; }
    public int OfferId { get; set; }
    
}