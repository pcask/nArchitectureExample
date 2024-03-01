using Core.Abstracts;
using Core.Validations;
using Entity.Entities;
using DataAccess.Abstracts;

namespace Core.Concretes;

public class CategoryManager(ICategoryRepository categoryRepository, CategoryValidations categoryValidations) : ICategoryService
{
    public Category Add(Category category)
    {
        return categoryRepository.Add(category);
    }

    public async Task<Category> AddAsync(Category category)
    {
        return await categoryRepository.AddAsync(category);
    }

    public void DeleteById(Guid id)
    {
        var category = categoryRepository.Get(c => c.Id == id);

        categoryValidations.CheckExistence(category);
        categoryRepository.Delete(category);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var category = await categoryRepository.GetAsync(c => c.Id == id);

        await categoryValidations.CheckExistenceAsync(category);
        await categoryRepository.DeleteAsync(category);
    }

    public IEnumerable<Category> GetAll() => categoryRepository.GetAll();

    public async Task<IEnumerable<Category>> GetAllAsync() => await categoryRepository.GetAllAsync();

    public Category? GetById(Guid id) => categoryRepository.Get(c => c.Id == id);

    public async Task<Category?> GetByIdAsync(Guid id) => await categoryRepository.GetAsync(c => c.Id == id);

    public Category Update(Category category)
    {
        var _category = categoryRepository.Get(c => c.Id == category.Id);
        categoryValidations.CheckExistence(_category);

        return categoryRepository.Update(category);
    }

    public async Task<Category> UpdateAsync(Category category)
    {
        var _category = await categoryRepository.GetAsync(c => c.Id == category.Id);
        await categoryValidations.CheckExistenceAsync(_category);

        return await categoryRepository.UpdateAsync(category);
    }
}