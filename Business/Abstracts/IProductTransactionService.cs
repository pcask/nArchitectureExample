using Core.Entities;

namespace Business.Abstracts;

public interface IProductTransactionService
{
    IEnumerable<ProductTransaction> GetAll();
    ProductTransaction? GetById(Guid id);

    ProductTransaction Add(ProductTransaction productTransaction);
    ProductTransaction Update(ProductTransaction productTransaction);
    void DeleteById(Guid id);
    IDictionary<Guid, int> GetStockByProductIdList(Guid[] productIdList);


    Task<IEnumerable<ProductTransaction>> GetAllAsync();
    Task<ProductTransaction?> GetByIdAsync(Guid id);

    Task<ProductTransaction> AddAsync(ProductTransaction productTransaction);
    Task<ProductTransaction> UpdateAsync(ProductTransaction productTransaction);
    Task DeleteByIdAsync(Guid id);
    Task<IDictionary<Guid, int>> GetStockByProductIdListAsync(Guid[] productIdList);
}
