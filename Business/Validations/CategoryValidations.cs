﻿using Business.Exceptions;
using Entity.Entities;

namespace Business.Validations;

public class CategoryValidations
{
    public void CheckExistence(Category? category)
    {
        if (category == null) throw new ValidationException("Category not found");
    }

    public async Task CheckExistenceAsync(Category? category)
    {
        await Task.Run(() =>
        {
            if (category == null)
                throw new ValidationException("Category not found");
        });
    }
}
