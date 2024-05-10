using OnlineExaminationSystems.API.Models.Entities;

namespace OnlineExaminationSystems.API.Services.Abstract
{
    public interface IUsersService : ICrudService<User>
    {
        User CreateUserWithHashedPassword(object updateRequestModel);

        Task<bool> IsUniqueEmailAsync(string email);

        Task<bool> IsUniqueEmailAsync(int id, string email);

        User UpdateUserWithHashedPassword(User user);
    }
}