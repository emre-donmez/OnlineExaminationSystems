using OnlineExaminationSystems.API.Model.Dtos.User;
using OnlineExaminationSystems.API.Model.Entities;

namespace OnlineExaminationSystems.API.Services.Abstract
{
    public interface IUserService : ICrudService<User>
    {
        User CreateUserWithHashedPassword(object updateRequestModel);
        Task<bool> IsUniqueEmailAsync(string email);
        User UpdateUserWithHashedPassword(int id, object updateRequestModel);
    }
}