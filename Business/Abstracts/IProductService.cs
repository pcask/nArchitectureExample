using Entity.Entities;

namespace Core.Abstracts;

public interface IProductService
{
    IEnumerable<Product> GetAll();
    Product? GetById(Guid id);

    Product Add(Product product);
    Product Update(Product product);
    void DeleteById(Guid id);


    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product?> GetByIdAsync(Guid id);

    Task<Product> AddAsync(Product product);
    Task<Product> UpdateAsync(Product product);
    Task DeleteByIdAsync(Guid id);
}
