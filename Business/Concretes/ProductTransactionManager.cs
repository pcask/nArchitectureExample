using Core.Abstracts;
using Entity.Entities;
using DataAccess.Abstracts;
using Entity.DTOs.ProductTransactions;
using Core.CrossCuttingConcerns.Validation;
using Entity.ViewModels.ProductTransactions;
using Core.Aspects.Autofac.Validation;
using Business.Validations.ProductTransactions;
using Core.Exceptions;
using Business.Concretes.Common;

namespace Core.Concretes;

public class ProductTransactionManager(IProductTransactionRepository productTransactionRepository) : ManagerBase, IProductTransactionService
{
    public ProductTransactionVm Add(ProductTransactionAddDto productTransactionAddDto)
    {
        var pt = productTransactionAddDto.GetEntity();
        pt.CreatedDate = DateTime.Now;

        return ProductTransactionVm.GetModel(productTransactionRepository.Add(pt));
    }

    public async Task<ProductTransactionVm> AddAsync(ProductTransactionAddDto productTransactionAddDto)
    {
        var pt = productTransactionAddDto.GetEntity();
        pt.CreatedDate = DateTime.Now;

        return ProductTransactionVm.GetModel(await productTransactionRepository.AddAsync(pt));
    }

    [ValidationAspect(typeof(ProductTransactionDeleteValidations))]
    public void DeleteById(Guid id)
    {
        productTransactionRepository.Delete(ValidationReturn.Entity);
    }

    [ValidationAspect(typeof(ProductTransactionDeleteValidations))]
    public async Task DeleteByIdAsync(Guid id)
    {
        await productTransactionRepository.DeleteAsync(ValidationReturn.Entity);
    }

    public IEnumerable<ProductTransactionListVm> GetAll()
    {
        return ProductTransactionListVm.GetModels(productTransactionRepository.GetAll());
    }

    public async Task<IEnumerable<ProductTransactionListVm>> GetAllAsync()
    {
        return ProductTransactionListVm.GetModels(await productTransactionRepository.GetAllAsync());
    }

    public async Task<IEnumerable<ProductTransactionListVm>> GetAllByProductIdAsync(Guid id)
    {
        return ProductTransactionListVm.GetModels(await productTransactionRepository.GetAllAsync(pt => pt.ProductId == id));
    }

    public ProductTransactionVm GetById(Guid id)
    {
        return ProductTransactionVm.GetModel(productTransactionRepository.Get(c => c.Id == id));
    }

    public async Task<ProductTransactionVm> GetByIdAsync(Guid id)
    {
        return ProductTransactionVm.GetModel(await productTransactionRepository.GetAsync(c => c.Id == id));
    }

    [ValidationAspect(typeof(ProductTransactionUpdateValidations))]
    public void Update(Guid id, ProductTransactionUpdateDto productTransactionUpdateDto)
    {
        ProductTransaction pt = ValidationReturn.Entity;

        pt.ProductId = productTransactionUpdateDto.ProductId;
        pt.Quantity = productTransactionUpdateDto.Quantity;

        productTransactionRepository.Update(pt);
    }

    [ValidationAspect(typeof(ProductTransactionUpdateValidations))]
    public async Task UpdateAsync(Guid id, ProductTransactionUpdateDto productTransactionUpdateDto)
    {
        ProductTransaction pt = ValidationReturn.Entity;

        pt.ProductId = productTransactionUpdateDto.ProductId;
        pt.Quantity = productTransactionUpdateDto.Quantity;

        await productTransactionRepository.UpdateAsync(pt);
    }

    [ValidationAspect(typeof(ProductTransactionValidations))]
    public IDictionary<Guid, int> GetStockByProductIdList(Guid[] productIdList)
    {
        Dictionary<Guid, int> productStocks = [];

        for (int i = 0; i < productIdList.Length; i++)
        {
            int quantity = productTransactionRepository
                           .GetAll(pt => pt.ProductId == productIdList[i])
                           .Sum(pt => pt.Quantity);

            productStocks[productIdList[i]] = quantity;
        }

        return productStocks;
    }

    [ValidationAspect(typeof(ProductTransactionValidations))]
    public async Task<IDictionary<Guid, int>> GetStockByProductIdListAsync(Guid[] productIdList)
    {
        return await Task.Run(() =>
        {
            Dictionary<Guid, int> productStocks = [];

            for (int i = 0; i < productIdList.Length; i++)
            {
                int quantity = productTransactionRepository
                               .GetAll(pt => pt.ProductId == productIdList[i])
                               .Sum(pt => pt.Quantity);

                productStocks[productIdList[i]] = quantity;
            }

            return productStocks;
        });
    }

    public int GetStockByProductId(Guid id)
    {
        var productTransactions = productTransactionRepository.GetAll(pt => pt.ProductId == id);
        if (!productTransactions.Any())
            throw new ValidationException("The product was not found in stock!");

        return productTransactions.Sum(pt => pt.Quantity);
    }

    public async Task<int> GetStockByProductIdAsync(Guid id)
    {
        var productTransactions = await productTransactionRepository.GetAllAsync(pt => pt.ProductId == id);
        if (!productTransactions.Any())
            throw new ValidationException("The product was not found in stock!");

        return productTransactions.Sum(pt => pt.Quantity);
    }
}