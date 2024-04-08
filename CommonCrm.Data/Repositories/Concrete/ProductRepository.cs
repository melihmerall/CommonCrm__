using CommonCrm.Data.DbContexts;
using CommonCrm.Data.Entities.Product;
using CommonCrm.Data.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace CommonCrm.Data.Repositories.Concrete;

public class ProductRepository(ApplicationDbContext context) : Repository<Product>(context), IProductRepository
{
    private ApplicationDbContext ApplicationDbContext => context as ApplicationDbContext ?? throw new InvalidOperationException();


    public async Task<List<Product>> GetByOwnerId(Guid? id)
    {
        return await context.Set<Product>()
            .Where(x => x.OwnerId == id)
            .Include(x=>x.Collections)
            .Include(x=>x.Categories)
            .Include(x=>x.Unit)
            .OrderByDescending(x => x.CreatedDate) 
            .ToListAsync() ?? throw new InvalidOperationException();    }
}