using CommonCrm.Data.Entities.Product;
using CommonCrm.Data.Repositories.Abstract;
using Attribute = CommonCrm.Data.Entities.Product.Attribute;

namespace CommonCrm.Business.Services;

public class AttributeService
{
    private readonly IUnitOfWork _unitofwork;
    public AttributeService(IUnitOfWork unitofwork)
    {
        _unitofwork = unitofwork;
    }

    public void Create(Attribute entity)
    {
        _unitofwork.Attributes.Create(entity);
        _unitofwork.Save();
    }

    public void Delete(Attribute entity)
    {
        _unitofwork.Attributes.Delete(entity);
        _unitofwork.Save();
    }

    public async Task<List<Attribute>> GetAll()
    {
        return await _unitofwork.Attributes.GetAll();
    }

    public async Task<Attribute> GetById(int id)
    {
        return await _unitofwork.Attributes.GetById(id);
    }
    
    public void Update(Attribute entity)
    {
        _unitofwork.Attributes.Update(entity);
        _unitofwork.Save();
    }
}