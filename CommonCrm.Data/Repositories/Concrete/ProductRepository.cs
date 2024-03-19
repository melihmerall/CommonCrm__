using CommonCrm.Data.DbContexts;
using CommonCrm.Data.Entities.Product;
using CommonCrm.Data.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CommonCrm.Data.Repositories.Concrete;

public class ProductRepository(ApplicationDbContext context) : Repository<Product>(context), IProductRepository
{
    private ApplicationDbContext ApplicationDbContext => context as ApplicationDbContext ?? throw new InvalidOperationException();
    public async Task<List<Product>> GetByOwnerId(Guid? id)
    {
        return await context.Set<Product>()
            .Where(x => x.OwnerId == id)
            .OrderByDescending(x => x.CreatedDate) // CreatedDate özelliğine göre azalan sıralama ile gelir. son eklenen veri en üstte...
            .ToListAsync() ?? throw new InvalidOperationException();    }
}