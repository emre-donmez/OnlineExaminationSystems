using AutoMapper;
using OnlineExaminationSystems.API.Model.Entities;
using OnlineExaminationSystems.API.Model.Helpers;
using OnlineExaminationSystems.API.Model.Repository;

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

        public User UpdateUserWithHashedPassword(object updateRequestModel)
        {
            var user = _mapper.Map<User>(updateRequestModel);

            user.Password = _passwordHashHelper.HashPassword(user.Password);

            return base.Update(user);
        } 

        public async Task<bool> IsUniqueEmailAsync(string email)
        {
            var query = $"SELECT * FROM USERS WHERE email = @Email";
            var parameters = new { Email = email };

            var existingUser = _repository.ExecuteQuery(query, parameters);
            return existingUser.Count() == 0;
        }
    }
}