using AutoMapper;
using OnlineExaminationSystems.API.Model.Entities;
using OnlineExaminationSystems.API.Model.Helpers;
using OnlineExaminationSystems.API.Model.Repository;
using OnlineExaminationSystems.API.Services.Abstract;

namespace OnlineExaminationSystems.API.Services
{
    public class UserService : CrudService<User>, IUserService
    {
        private readonly IPasswordHashHelper _passwordHashHelper;

        public UserService(IGenericRepository<User> repository, IMapper mapper, IPasswordHashHelper passwordHashHelper) : base(repository, mapper)
        {
            _passwordHashHelper = passwordHashHelper;
        }

        public User CreateUserWithHashedPassword(object updateRequestModel)
        {
            var user = _mapper.Map<User>(updateRequestModel);

            user.Password = _passwordHashHelper.HashPassword(user.Password);

            return base.Create(user);
        }

        public User UpdateUserWithHashedPassword(User user)
        {
            user.Password = _passwordHashHelper.HashPassword(user.Password);

            return base.Update(user);
        }

        public async Task<bool> IsUniqueEmailAsync(string email)
        {
            var query = $"SELECT 1 FROM USERS WHERE email = @Email";
            var parameters = new { Email = email };

            var existingUser = _repository.ExecuteQuery(query, parameters);
            return existingUser.Count() == 0;
        }

        public async Task<bool> IsUniqueEmailAsync(int id, string email)
        {
            var query = $"SELECT 1 FROM USERS WHERE email = @Email and id != @Id";
            var parameters = new { Email = email, Id = id };

            var existingUser = _repository.ExecuteQuery(query, parameters);
            return existingUser.Count() == 0;
        }
    }
}