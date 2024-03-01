using Business.Validations.Common;
using Core.CrossCuttingConcerns.Validation;
using Core.Exceptions;
using DataAccess.Abstracts;

namespace Business.Validations.ProductTransactions;

public class ProductTransactionValidations(IProductTransactionRepository productTransactionRepository) : ValidationBase
{

    [ValidationMethod(Priority: 0)]
    public virtual async Task CheckExistenceAsync(Guid id)
    {
        ValidationReturn.Entity = await productTransactionRepository.GetAsync(pt => pt.Id == id) ?? throw new ValidationException("ProductTransaction was not found!");
    }

    //[ValidationMethod(Priority: 1)]
    //private async Task CheckProductIdListAsync(Guid[] productIdList)
    //{
    //    await Task.Run(() =>
    //    {
    //        if (productIdList.Length < 1)
    //            throw new Exception("ProductIdList cannot be empty!");

    //        if (productIdList.Any(id => productTransactionRepository.Get(pt => pt.ProductId == id) == null))
    //            throw new ValidationException("Some products were not found!");
    //    });
    //}
}