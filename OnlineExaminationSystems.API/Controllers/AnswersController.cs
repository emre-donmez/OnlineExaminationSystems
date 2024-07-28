using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.API.Models.Dtos;
using OnlineExaminationSystems.API.Models.Helpers;
using OnlineExaminationSystems.API.Services.Abstract;

namespace OnlineExaminationSystems.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly IAnswersService _answersService;
        private readonly IValidator<AnswerUpdateRequestModel> _validatorAnswerUpdateRequestModel;

        public AnswersController(IAnswersService answersService, IValidator<AnswerUpdateRequestModel> validatorAnswerUpdateRequestModel)
        {
            _answersService = answersService;
            _validatorAnswerUpdateRequestModel = validatorAnswerUpdateRequestModel;
        }

        [HttpPost("bulk")]
        [Authorize(Roles = Roles.Student)]
        public async Task<IActionResult> BulkInsert(IEnumerable<AnswerUpdateRequestModel> models)
        {
            foreach (var model in models)
            {
                var validationResult = await _validatorAnswerUpdateRequestModel.ValidateAsync(model);
                if (!validationResult.IsValid)
                    return BadRequest(validationResult.Errors);
            }

            var enrollment = _answersService.BulkInsert(models);
            return Ok();
        }
    }
}