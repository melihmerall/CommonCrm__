using CommonCrm.Data.DbContexts;
using CommonCrm.Data.Entities.AppUser;
using CommonCrm.Data.Entities.Product;
using CommonCrm.Data.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;

namespace CommonCrm.Business.Services;

public class ProductService(IUnitOfWork unitofwork)
{
    private readonly IUnitOfWork _unitofwork = unitofwork;
    public bool Create(Product entity)
    {
            _unitofwork.Products.Create(entity);
            _unitofwork.Save();
            return true;
    }

    public void Delete(Product entity)
    {
        _unitofwork.Products.Delete(entity);
        _unitofwork.Save();
    }

    public async Task<List<Product>> GetAll()
    {            
        var products = await _unitofwork.Products.GetAll();
        return products.OrderByDescending(p => p.CreatedDate).ToList();    
    }

    public async Task<Product> GetById(int id)
    {
        return await _unitofwork.Products.GetById(id);
    }

    public void Update(Product entity)
    {
        _unitofwork.Products.Update(entity);
        _unitofwork.Save();
    }
    
    public async Task<List<Product>> GetByOwnerId(Guid? id)
    {
        return await _unitofwork.Products.GetByOwnerId(id);
    }

}