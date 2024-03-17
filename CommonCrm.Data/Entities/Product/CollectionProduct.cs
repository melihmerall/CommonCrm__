namespace CommonCrm.Data.Entities.Product;

public class CollectionProduct
{
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int CategoryId { get; set; }
    public Collection Collection { get; set; }
}