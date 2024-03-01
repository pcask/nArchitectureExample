using Core.Abstracts;
using Core.Validations;
using DataAccess.Abstracts;
using Entity.Entities;

namespace Core.Concretes;

public class ProductManager(IProductRepository productRepository, ProductValidations productValidations) : IProductService
{
    public Product Add(Product product)
    {
        return productRepository.Add(product);
    }

    public async Task<Product> AddAsync(Product product)
    {
        return await productRepository.AddAsync(product);
    }

    public void DeleteById(Guid id)
    {
        var product = productRepository.Get(c => c.Id == id);

        productValidations.CheckExistence(product);
        productRepository.Delete(product);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var product = await productRepository.GetAsync(c => c.Id == id);

        await productValidations.CheckExistenceAsync(product);
        await productRepository.DeleteAsync(product);
    }

    public IEnumerable<Product> GetAll() => productRepository.GetAll();

    public async Task<IEnumerable<Product>> GetAllAsync() => await productRepository.GetAllAsync();

    public Product? GetById(Guid id) => productRepository.Get(c => c.Id == id);

    public async Task<Product?> GetByIdAsync(Guid id) => await productRepository.GetAsync(c => c.Id == id);

    public Product Update(Product product)
    {
        var _product = productRepository.Get(c => c.Id == product.Id);
        productValidations.CheckExistence(_product);

        return productRepository.Update(product);
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        var _product = await productRepository.GetAsync(c => c.Id == product.Id);
        await productValidations.CheckExistenceAsync(_product);

        return await productRepository.UpdateAsync(product);
    }
}