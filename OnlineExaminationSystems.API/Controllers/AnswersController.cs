using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.API.Models.Dtos;
using OnlineExaminationSystems.API.Services.Abstract;
using OnlineExaminationSystems.API.Services.Concrete;

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

        [HttpGet]
        public IActionResult Get()
        {
            var exams = _answersService.GetAll();
            return Ok(exams);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var exam = _answersService.GetById(id);
            return exam != null ? Ok(exam) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AnswerUpdateRequestModel model)
        {
            var validationResult = await _validatorAnswerUpdateRequestModel.ValidateAsync(model);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var exam = _answersService.Create(model);
            return CreatedAtAction(nameof(Get), new { id = exam.Id }, exam);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, AnswerUpdateRequestModel model)
        {
            var validationResult = await _validatorAnswerUpdateRequestModel.ValidateAsync(model);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var exam = _answersService.Update(id, model);
            return Ok(exam);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _answersService.Delete(id);
            return result ? Ok() : NotFound();
        }      
    }
}
