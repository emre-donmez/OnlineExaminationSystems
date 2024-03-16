using Dapper;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.API.Model.Context;
using OnlineExaminationSystems.API.Model.Dtos.User;
using OnlineExaminationSystems.API.Model.Entities;

namespace OnlineExaminationSystems.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DapperContext _context;
        private readonly IValidator<UserUpdateRequestModel> _validatorUserUpdateRequest;
        private readonly IValidator<User> _validatorUser;

        public UsersController(DapperContext context, IValidator<UserUpdateRequestModel> validatorUserUpdateRequest, IValidator<User> validatorUser)
        {
            _context = context;
            _validatorUserUpdateRequest = validatorUserUpdateRequest;
            _validatorUser = validatorUser;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = "SELECT * FROM Users WHERE is_del = 0";

            using (var connection = _context.CreateConnection())
            {
                var users = await connection.QueryAsync<User>(query);

                return Ok(users);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var query = "SELECT * FROM Users WHERE id = @Id AND is_del = 0";

            var parameters = new
            {
                Id = id
            };

            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<User>(query, parameters);

                if (user is null)
                    return NotFound();

                return Ok(user);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserUpdateRequestModel model)
        {
            var validationResult = await _validatorUserUpdateRequest.ValidateAsync(model);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var query = "INSERT INTO Users (Name, Surname, Email, Password, Role) VALUES (@Name, @Surname, @Email, @Password, @Role)" +
                        "SELECT CAST(SCOPE_IDENTITY() as int)";

            var parameters = new
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Password = model.Password,
                Role = model.Role
            };

            using (var connection = _context.CreateConnection())
            {
                var id = await connection.QuerySingleAsync<int>(query, parameters);

                var result = new User
                {
                    Id = id,
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    Password = model.Password,
                    Role = model.Role
                };

                return CreatedAtAction(nameof(Get), new { id = id }, result);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserUpdateRequestModel model)
        {
            var selectQuery = "SELECT * FROM Users WHERE id = @Id AND is_del = 0";

            var selectParameters = new
            {
                Id = id
            };

            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<User>(selectQuery, selectParameters);

                if (user is null)
                    return NotFound();

                var validationResult = await _validatorUserUpdateRequest.ValidateAsync(model);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var query = "UPDATE Users SET Name = @Name, Surname = @Surname, Email = @Email, Password = @Password, Role = @Role WHERE id = @Id AND is_del = 0";

                var parameters = new
                {
                    Id = id,
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    Password = model.Password,
                    Role = model.Role
                };

                await connection.ExecuteAsync(query, parameters);

                var result = new User
                {
                    Id = id,
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    Password = model.Password,
                    Role = model.Role,
                };

                return Ok(result);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var selectQuery = "SELECT * FROM Users WHERE id = @Id AND is_del = 0";

            var parameters = new
            {
                Id = id
            };

            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<User>(selectQuery, parameters);

                if (user is null)
                    return NotFound();

                var query = "UPDATE Users SET is_del = 1 WHERE id = @Id";

                await connection.ExecuteAsync(query, parameters);

                return Ok();
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, JsonPatchDocument<User> patchDocument)
        {
            var selectQuery = "SELECT * FROM Users WHERE id = @Id AND is_del = 0";

            var selectParameters = new
            {
                Id = id
            };

            using (var connection = _context.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<User>(selectQuery, selectParameters);

                if (user is null)
                    return NotFound();

                patchDocument.ApplyTo(user);

                var validationResult = await _validatorUser.ValidateAsync(user);

                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);

                var updateQuery = "UPDATE Users SET Name = @Name, Surname = @Surname, Email = @Email, Password = @Password, Role = @Role WHERE id = @Id AND is_del = 0";

                var updateParameters = new
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    Password = user.Password,
                    Role = user.Role
                };

                await connection.ExecuteAsync(updateQuery, updateParameters);

                return Ok(updateParameters);
            }
        }
    }
}