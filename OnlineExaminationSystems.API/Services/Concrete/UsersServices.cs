using AutoMapper;
using OnlineExaminationSystems.API.Data.Repository;
using OnlineExaminationSystems.API.Models.Entities;
using OnlineExaminationSystems.API.Models.Helpers;
using OnlineExaminationSystems.API.Services.Abstract;

namespace OnlineExaminationSystems.API.Services.Concrete
{
    public class UsersService : CrudService<User>, IUsersService
    {
        private readonly IPasswordHashHelper _passwordHashHelper;
        private readonly IAuthHelper _authHelper;

        public UsersService(IGenericRepository<User> repository, IMapper mapper, IPasswordHashHelper passwordHashHelper, IAuthHelper authHelper) : base(repository, mapper)
        {
            _passwordHashHelper = passwordHashHelper;
            _authHelper = authHelper;
        }

        public User CreateUserWithHashedPassword(object updateRequestModel)
        {
            var user = _mapper.Map<User>(updateRequestModel);

            user.Password = _passwordHashHelper.HashPassword(user.Password);

            return Create(user);
        }

        public User UpdateUserWithHashedPassword(User user)
        {
            var existedUser = _repository.GetById(user.Id);

            if(!existedUser.Password.Equals(user.Password))
                user.Password = _passwordHashHelper.HashPassword(user.Password);

            return Update(user);
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

        public string? Authenticate(string email, string password)
        {
            password = _passwordHashHelper.HashPassword(password);

            var query = $"SELECT id AS Id,Name,Surname,Email,Password,role_id AS RoleId FROM USERS WHERE email = @Email and password = @Password";
            var parameters = new { Email = email, Password = password };

            var user = _repository.ExecuteQueryFirstOrDefault(query, parameters);

            return user != null ? _authHelper.GenerateJWTToken(user) : null;
        }

        public string? Refresh(string token)
        {
            return _authHelper.RefreshJWTToken(token);
        }
    }
}