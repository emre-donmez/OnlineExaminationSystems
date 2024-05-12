using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.API.Models.Dtos;
using OnlineExaminationSystems.API.Services.Abstract;

namespace OnlineExaminationSystems.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly IResultsService _resultsService;
        private readonly IValidator<ResultUpdateRequestModel> _validatorResultUpdateRequest;

        public ResultsController(IResultsService resultsService, IValidator<ResultUpdateRequestModel> validatorResultUpdateRequest)
        {
            _resultsService = resultsService;
            _validatorResultUpdateRequest = validatorResultUpdateRequest;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var results = _resultsService.GetAll();
            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _resultsService.GetById(id);
            return result != null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ResultUpdateRequestModel model)
        {
            var validationResult = await _validatorResultUpdateRequest.ValidateAsync(model);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var result = _resultsService.Create(model);
            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ResultUpdateRequestModel model)
        {
            var validationResult = await _validatorResultUpdateRequest.ValidateAsync(model);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var result = _resultsService.Update(id, model);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _resultsService.Delete(id);
            return result ? Ok() : NotFound();
        }
    }
}
