using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.API.Models.Dtos;
using OnlineExaminationSystems.API.Services.Abstract;

namespace OnlineExaminationSystems.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamsController : ControllerBase
    {
        private readonly IExamsService _examsService;
        private readonly IValidator<ExamUpdateRequestModel> _validatorExamUpdateRequestModel;
        private readonly IQuestionsService _questionsService;

        public ExamsController(IExamsService examsService, IValidator<ExamUpdateRequestModel> validatorExamUpdateRequestModel, IQuestionsService questionsService)
        {
            _examsService = examsService;
            _validatorExamUpdateRequestModel = validatorExamUpdateRequestModel;
            _questionsService = questionsService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var exams = _examsService.GetAll();
            return Ok(exams);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var exam = _examsService.GetById(id);
            return exam != null ? Ok(exam) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ExamUpdateRequestModel model)
        {
            var validationResult = await _validatorExamUpdateRequestModel.ValidateAsync(model);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var exam = _examsService.Create(model);
            return CreatedAtAction(nameof(Get), new { id = exam.Id }, exam);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ExamUpdateRequestModel model)
        {
            var validationResult = await _validatorExamUpdateRequestModel.ValidateAsync(model);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var exam = _examsService.Update(id, model);
            return Ok(exam);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _examsService.Delete(id);
            return result ? Ok() : NotFound();
        }

        [HttpGet("{examId}/questions")]
        public IActionResult GetQuestions(int examId)
        {
            var questions = _questionsService.GetQuestionsByExamId(examId);
            return Ok(questions);
        }

        //add question to exam
        //[HttpPost("{examId}/questions")]
        //public async Task<IActionResult> AddQuestion(int examId, QuestionUpdateRequestModel model)
        //{
        //    var validationResult = await _validatorExamUpdateRequestModel.ValidateAsync(model);

        //    if (!validationResult.IsValid)
        //        return BadRequest(validationResult.Errors);

        //    var question = _examsService.AddQuestion(examId, model);
        //    return CreatedAtAction(nameof(GetQuestions), new { examId = examId }, question);
        //}

        ////update question
        //[HttpPut("{examId}/questions/{questionId}")]
        //public async Task<IActionResult> UpdateQuestion(int examId, int questionId, QuestionUpdateRequestModel model)
        //{
        //    var validationResult = await _validatorExamUpdateRequestModel.ValidateAsync(model);

        //    if (!validationResult.IsValid)
        //        return BadRequest(validationResult.Errors);

        //    var question = _examsService.UpdateQuestion(examId, questionId, model);
        //    return Ok(question);
        //}

        ////delete question
        //[HttpDelete("{examId}/questions/{questionId}")]
        //public IActionResult DeleteQuestion(int examId, int questionId)
        //{
        //    _examsService.DeleteQuestion(examId, questionId);
        //    return NoContent();
        //}
    }
}