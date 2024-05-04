using OnlineExaminationSystems.API.Model.Entities;

namespace OnlineExaminationSystems.API.Services.Abstract
{
    public interface IUserService : ICrudService<User>
    {
        User CreateUserWithHashedPassword(object updateRequestModel);

        Task<bool> IsUniqueEmailAsync(string email);

        Task<bool> IsUniqueEmailAsync(int id, string email);

        User UpdateUserWithHashedPassword(User user);
    }
}