using Entity.DTOs.ProductTransactions;
using Entity.ViewModels.ProductTransactions;

namespace Core.Abstracts;

public interface IProductTransactionService
{
    IEnumerable<ProductTransactionListVm> GetAll();
    ProductTransactionVm GetById(Guid id);

    ProductTransactionVm Add(ProductTransactionAddDto productTransactionAddDto);
    void Update(Guid id, ProductTransactionUpdateDto productTransactionUpdateDto);
    void DeleteById(Guid id);
    IDictionary<Guid, int> GetStockByProductIdList(Guid[] productIdList);


    Task<IEnumerable<ProductTransactionListVm>> GetAllAsync();
    Task<IEnumerable<ProductTransactionListVm>> GetAllByProductIdAsync(Guid id);
    Task<ProductTransactionVm> GetByIdAsync(Guid id);

    Task<ProductTransactionVm> AddAsync(ProductTransactionAddDto productTransactionAddDto);
    Task UpdateAsync(Guid id, ProductTransactionUpdateDto productTransactionUpdateDto);
    Task DeleteByIdAsync(Guid id);
    Task<IDictionary<Guid, int>> GetStockByProductIdListAsync(Guid[] productIdList);

    int GetStockByProductId(Guid id);
    Task<int> GetStockByProductIdAsync(Guid id);
}
