using CommonCrm.Data.DbContexts;
using CommonCrm.Data.Entities.Product;
using CommonCrm.Data.Repositories.Abstract;
using Attribute = CommonCrm.Data.Entities.Product.Attribute;

namespace CommonCrm.Data.Repositories.Concrete;

public class AttributeRepository(ApplicationDbContext context) : Repository<Attribute>(context), IAttributeRepository
{
    
}