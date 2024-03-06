namespace CommonCrm.Data.Repositories.Abstract;

public interface IUnitOfWork: IDisposable
{
    ICategoryRepository Categories {get;}
    IProductRepository Products {get;} 
    IProductUnitRepository ProductUnits {get;} 
    IAttributeRepository Attributes {get;} 

    void Save();

}