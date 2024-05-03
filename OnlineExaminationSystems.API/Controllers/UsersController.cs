using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.API.Model.Dtos.User;
using OnlineExaminationSystems.API.Services.Abstract;

namespace OnlineExaminationSystems.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IValidator<UserUpdateRequestModel> _validatorUserUpdateRequest;

        public UsersController(IUserService userService, IValidator<UserUpdateRequestModel> validatorUserUpdateRequest)
        {
            _userService = userService;
            _validatorUserUpdateRequest = validatorUserUpdateRequest;
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

            if (user == null)
                return NotFound();

            return Ok(user);
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
            var validationResult = await _validatorUserUpdateRequest.ValidateAsync(model);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var user = _userService.UpdateUserWithHashedPassword(id,model);

            return Ok(user);
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