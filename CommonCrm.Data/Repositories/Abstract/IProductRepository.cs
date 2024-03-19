using CommonCrm.Data.Entities.Product;

namespace CommonCrm.Data.Repositories.Abstract;

public interface IProductRepository: IRepository<Product>
{
    Task<List<Product>> GetByOwnerId(Guid? id);

}