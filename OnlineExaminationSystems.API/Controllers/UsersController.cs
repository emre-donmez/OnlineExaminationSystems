using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.API.Models.Dtos.User;
using OnlineExaminationSystems.API.Models.Entities;
using OnlineExaminationSystems.API.Services.Abstract;

namespace OnlineExaminationSystems.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _userService;
        private readonly IValidator<UserUpdateRequestModel> _validatorUserUpdateRequest;
        private readonly IMapper _mapper;
        private readonly IValidator<User> _validatorUserValidator;

        public UsersController(IUsersService userService, IValidator<UserUpdateRequestModel> validatorUserUpdateRequest, IMapper mapper, IValidator<User> validatorUser)
        {
            _userService = userService;
            _validatorUserUpdateRequest = validatorUserUpdateRequest;
            _validatorUserValidator = validatorUser;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = _userService.GetById(id);

            return user != null ? Ok(user) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(UserUpdateRequestModel model)
        {
            var validationResult = await _validatorUserUpdateRequest.ValidateAsync(model);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var user = _userService.CreateUserWithHashedPassword(model);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, UserUpdateRequestModel model)
        {
            var user = _mapper.Map<User>(model);
            user.Id = id;

            var validationResult = await _validatorUserValidator.ValidateAsync(user);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var result = _userService.UpdateUserWithHashedPassword(user);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _userService.Delete(id);
            return result ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }

        //[HttpPatch("{id}")]
        //public async Task<IActionResult> Patch(int id, JsonPatchDocument<User> patchDocument)
        //{
        //    var user = _repository.GetById(id);

        //    if (user is null)
        //        return NotFound();

        //    patchDocument.ApplyTo(user);

        //    var validationResult = await _validatorUser.ValidateAsync(user);

        //    if (!validationResult.IsValid)
        //        return BadRequest(validationResult.Errors);

        //    var passwordPaths = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "/Password", "Password" };
        //    if (passwordPaths.Any(p => patchDocument.Operations.Any(op => op.path.Equals(p, StringComparison.OrdinalIgnoreCase))))
        //        user.Password = _passwordHashHelper.HashPassword(user.Password);

        //    var result = _repository.Update(user);

        //    return Ok(result);
        //}
    }
}