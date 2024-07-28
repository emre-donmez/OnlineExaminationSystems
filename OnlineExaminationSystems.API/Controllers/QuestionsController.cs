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
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionsService _questionsService;
        private readonly IValidator<QuestionUpdateRequestModel> _validatorQuestionUpdateRequest;

        public QuestionsController(IQuestionsService questionsService, IValidator<QuestionUpdateRequestModel> validatorQuestionUpdateRequest)
        {
            _questionsService = questionsService;
            _validatorQuestionUpdateRequest = validatorQuestionUpdateRequest;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = Roles.Academician)]
        public IActionResult Get(int id)
        {
            var question = _questionsService.GetById(id);
            return question != null ? Ok(question) : NotFound();
        }

        [HttpPost]
        [Authorize(Roles = Roles.Academician)]
        public async Task<IActionResult> Create(QuestionUpdateRequestModel model)
        {
            var validationResult = await _validatorQuestionUpdateRequest.ValidateAsync(model);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var question = _questionsService.Create(model);
            return CreatedAtAction(nameof(Get), new { id = question.Id }, question);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Roles.Academician)]
        public async Task<IActionResult> Update(int id, QuestionUpdateRequestModel model)
        {
            var validationResult = await _validatorQuestionUpdateRequest.ValidateAsync(model);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var question = _questionsService.Update(id, model);
            return Ok(question);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Roles.Academician)]
        public IActionResult Delete(int id)
        {
            var result = _questionsService.Delete(id);
            return result ? Ok() : NotFound();
        }
    }
}