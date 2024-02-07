using Core.Entities;

namespace Business.Abstracts;

public interface ICategoryService
{
    IEnumerable<Category> GetAll();
    Category? GetById(Guid id);

    Category Add(Category category);
    Category Update(Category category);
    void DeleteById(Guid id);


    Task<IEnumerable<Category>> GetAllAsync();
    Task<Category?> GetByIdAsync(Guid id);

    Task<Category> AddAsync(Category category);
    Task<Category> UpdateAsync(Category category);
    Task DeleteByIdAsync(Guid id);
}