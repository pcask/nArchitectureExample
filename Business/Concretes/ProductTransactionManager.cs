using Business.Abstracts;
using Business.Validations;
using Core.Entities;
using DataAccess.Abstracts;

namespace Business.Concretes;

public class ProductTransactionManager(
                                        IProductTransactionRepository productTransactionRepository,
                                        ProductTransactionValidations productTransactionValidations)
    : IProductTransactionService
{
    public ProductTransaction Add(ProductTransaction productTransaction)
    {
        return productTransactionRepository.Add(productTransaction);
    }

    public async Task<ProductTransaction> AddAsync(ProductTransaction productTransaction)
    {
        return await productTransactionRepository.AddAsync(productTransaction);
    }

    public void DeleteById(Guid id)
    {
        var productTransaction = productTransactionRepository.Get(c => c.Id == id);

        productTransactionValidations.CheckExistence(productTransaction);
        productTransactionRepository.Delete(productTransaction);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var productTransaction = await productTransactionRepository.GetAsync(c => c.Id == id);

        await productTransactionValidations.CheckExistenceAsync(productTransaction);
        await productTransactionRepository.DeleteAsync(productTransaction);
    }

    public IEnumerable<ProductTransaction> GetAll() => productTransactionRepository.GetAll();

    public async Task<IEnumerable<ProductTransaction>> GetAllAsync() => await productTransactionRepository.GetAllAsync();

    public ProductTransaction? GetById(Guid id) => productTransactionRepository.Get(c => c.Id == id);

    public async Task<ProductTransaction?> GetByIdAsync(Guid id) => await productTransactionRepository.GetAsync(c => c.Id == id);

    public ProductTransaction Update(ProductTransaction productTransaction)
    {
        var _productTransaction = productTransactionRepository.Get(c => c.Id == productTransaction.Id);
        productTransactionValidations.CheckExistence(_productTransaction);

        return productTransactionRepository.Update(productTransaction);
    }

    public async Task<ProductTransaction> UpdateAsync(ProductTransaction productTransaction)
    {
        var _productTransaction = await productTransactionRepository.GetAsync(c => c.Id == productTransaction.Id);
        await productTransactionValidations.CheckExistenceAsync(_productTransaction);

        return await productTransactionRepository.UpdateAsync(productTransaction);
    }

    public IDictionary<Guid, int> GetStockByProductIdList(Guid[] productIdList)
    {
        productTransactionValidations.CheckProductIdList(productIdList);

        bool doesNotExist = productIdList.Any(id => productTransactionRepository.Get(pt => pt.ProductId == id) == null);
        productTransactionValidations.IfAnyProductDoesNotExist(doesNotExist);

        Dictionary<Guid, int> productDic = [];

        for (int i = 0; i < productIdList.Length; i++)
        {
            int quantity = productTransactionRepository
                           .GetAll(pt => pt.ProductId == productIdList[i])
                           .Sum(pt => pt.Quantity);

            productDic[productIdList[i]] = quantity;
        }

        return productDic;
    }

    public async Task<IDictionary<Guid, int>> GetStockByProductIdListAsync(Guid[] productIdList)
    {
        await productTransactionValidations.CheckProductIdListAsync(productIdList);
        return await Task.Run(() =>
        {
            Dictionary<Guid, int> productDic = [];

            for (int i = 0; i < productIdList.Length; i++)
            {
                int quantity = productTransactionRepository
                               .GetAll(pt => pt.ProductId == productIdList[i])
                               .Sum(pt => pt.Quantity);

                productDic[productIdList[i]] = quantity;
            }

            return productDic;
        });
    }
}