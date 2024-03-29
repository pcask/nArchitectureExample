﻿using Entity.Entities;

namespace Entity.ViewModels.Users;

public class UserVm
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public static UserVm GetModel(User user)
    {
        return new()
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }
}