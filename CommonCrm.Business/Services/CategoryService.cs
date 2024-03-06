using CommonCrm.Data.Entities.Product;
using CommonCrm.Data.Repositories.Abstract;

namespace CommonCrm.Business.Services;

public class CategoryService
{
    private readonly IUnitOfWork _unitofwork;
    public CategoryService(IUnitOfWork unitofwork)
    {
        _unitofwork = unitofwork;
    }

    public void Create(Category entity)
    {
        _unitofwork.Categories.Create(entity);
        _unitofwork.Save();
    }

    public void Delete(Category entity)
    {
        _unitofwork.Categories.Delete(entity);
        _unitofwork.Save();
    }

    public async Task<List<Category>> GetAll()
    {
        return await _unitofwork.Categories.GetAll();
    }

    public async Task<Category> GetById(int id)
    {
        return await _unitofwork.Categories.GetById(id);
    }
    
    public void Update(Category entity)
    {
        _unitofwork.Categories.Update(entity);
        _unitofwork.Save();
    }
}