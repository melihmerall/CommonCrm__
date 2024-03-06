using CommonCrm.Data.DbContexts;
using CommonCrm.Data.Repositories.Abstract;

namespace CommonCrm.Data.Repositories.Concrete;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }
        
    private CategoryRepository _categoryRepository;
    private ProductRepository _productRepository;
    private ProductUnitRepository _productUnitRepository;
    private AttributeRepository _attributeRepository;

    
    public ICategoryRepository Categories => 
        _categoryRepository = _categoryRepository ?? new CategoryRepository(_context);             
    public IProductRepository Products => 
        _productRepository = _productRepository ?? new ProductRepository(_context);
    public IProductUnitRepository ProductUnits => 
        _productUnitRepository = _productUnitRepository ?? new ProductUnitRepository(_context);
    public IAttributeRepository Attributes => 
        _attributeRepository = _attributeRepository ?? new AttributeRepository(_context);
    public  void Dispose()
    {
        _context.Dispose();
    }

    public  void Save()
    {
        _context.SaveChanges();
    }
}