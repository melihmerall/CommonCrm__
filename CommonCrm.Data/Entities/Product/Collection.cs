namespace CommonCrm.Data.Entities.Product;

public class Collection: BaseEntity
{
    public required string Name { get; set; }
    public ICollection<Product> Products { get; set; } = new HashSet<Product>();

}