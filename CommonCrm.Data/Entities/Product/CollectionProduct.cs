namespace CommonCrm.Data.Entities.Product;

public class CollectionProduct
{
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public int CollectionId { get; set; }
    public Collection Collection { get; set; }
}