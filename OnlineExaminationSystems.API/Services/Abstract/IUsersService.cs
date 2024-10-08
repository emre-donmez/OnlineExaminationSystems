﻿using OnlineExaminationSystems.API.Models.Dtos;
using OnlineExaminationSystems.API.Models.Entities;

namespace OnlineExaminationSystems.API.Services.Abstract
{
    public interface IUsersService : ICrudService<User>
    {
        string? Authenticate(string email, string password);

        User CreateUserWithHashedPassword(object updateRequestModel);

        IEnumerable<UserWithRoleResponseModel> GetAllWithRoles();

        Task<bool> IsUniqueEmailAsync(string email);

        Task<bool> IsUniqueEmailAsync(int id, string email);

        string? Refresh(string token);

        User UpdateUserWithHashedPassword(User user);
    }
}