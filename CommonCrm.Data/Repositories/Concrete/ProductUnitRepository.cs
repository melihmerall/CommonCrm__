using CommonCrm.Data.DbContexts;
using CommonCrm.Data.Entities.Product;
using CommonCrm.Data.Repositories.Abstract;

namespace CommonCrm.Data.Repositories.Concrete;

public class ProductUnitRepository(ApplicationDbContext context) : Repository<ProductUnit>(context), IProductUnitRepository
{
    private ApplicationDbContext ApplicationDbContext => context as ApplicationDbContext ?? throw new InvalidOperationException();

}