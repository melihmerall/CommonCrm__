using CommonCrm.Data.DbContexts;
using CommonCrm.Data.Entities.Product;
using CommonCrm.Data.Repositories.Abstract;

namespace CommonCrm.Data.Repositories.Concrete;

public class CategoryRepository(ApplicationDbContext context) : Repository<Category>(context), ICategoryRepository
{
    private ApplicationDbContext ApplicationDbContext => context as ApplicationDbContext ?? throw new InvalidOperationException();
}