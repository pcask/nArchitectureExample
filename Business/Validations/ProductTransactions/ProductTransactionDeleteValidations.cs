using DataAccess.Abstracts;

namespace Business.Validations.ProductTransactions;

public class ProductTransactionDeleteValidations(IProductTransactionRepository productTransactionRepository) : ProductTransactionValidations(productTransactionRepository)
{
    
}
