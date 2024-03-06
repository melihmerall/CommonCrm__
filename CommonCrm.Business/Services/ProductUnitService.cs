using CommonCrm.Data.Entities.Product;
using CommonCrm.Data.Repositories.Abstract;

namespace CommonCrm.Business.Services;

public class ProductUnitService(IUnitOfWork unitofwork)
{
    private readonly IUnitOfWork _unitofwork = unitofwork;
    public bool Create(ProductUnit entity)
    {
        _unitofwork.ProductUnits.Create(entity);
        _unitofwork.Save();
        return true;
    }

    public void Delete(ProductUnit entity)
    {
        _unitofwork.ProductUnits.Delete(entity);
        _unitofwork.Save();
    }

    public async Task<List<ProductUnit>> GetAll()
    {            
        return await _unitofwork.ProductUnits.GetAll();
    }

    public async Task<ProductUnit> GetById(int id)
    {
        return await _unitofwork.ProductUnits.GetById(id);
    }

    public void Update(ProductUnit entity)
    {
        _unitofwork.ProductUnits.Update(entity);
        _unitofwork.Save();
    }
}