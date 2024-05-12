using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.API.Models.Dtos;
using OnlineExaminationSystems.API.Services.Abstract;

namespace OnlineExaminationSystems.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IValidator<LoginRequestModel> _validatorLoginRequestModel;
        private readonly IUsersService _userService;

        public AuthController(IValidator<LoginRequestModel> validatorLoginRequestModel,IUsersService usersService)
        {
            _userService = usersService;
            _validatorLoginRequestModel = validatorLoginRequestModel;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestModel model)
        {
            var validationResult = await _validatorLoginRequestModel.ValidateAsync(model);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var token = _userService.Authenticate(model.Email, model.Password);

            return token == null ? Unauthorized() : Ok(token);
        }
    }
}
