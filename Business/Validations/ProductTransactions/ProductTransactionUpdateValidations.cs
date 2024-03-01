using Core.CrossCuttingConcerns.Validation;
using Core.Exceptions;
using DataAccess.Abstracts;
using Entity.DTOs.ProductTransactions;

namespace Business.Validations.ProductTransactions;

public class ProductTransactionUpdateValidations(IProductTransactionRepository productTransactionRepository, IProductRepository productRepository)
    : ProductTransactionValidations(productTransactionRepository)
{
    [ValidationMethod(Priority: 0)]
    public async Task CheckProductQuantityAsync(ProductTransactionUpdateDto productTransactionUpdateDto)
    {
        await Task.Run(() =>
        {
            if (productTransactionUpdateDto.Quantity < int.MinValue || productTransactionUpdateDto.Quantity > int.MaxValue)
                throw new ValidationException($"Quantity must be between {int.MinValue} and {int.MaxValue}!");
        });
    }

    [ValidationMethod(Priority: 1)]
    public override async Task CheckExistenceAsync(Guid id)
    {
        await base.CheckExistenceAsync(id);
    }

    [ValidationMethod(Priority: 1)]
    public async Task CheckProductExistenceAsync(ProductTransactionUpdateDto productTransactionUpdateDto)
    {
        _ = await productRepository.GetAsync(p => p.Id == productTransactionUpdateDto.ProductId) ?? throw new ValidationException("Product was not found!");
    }
}
