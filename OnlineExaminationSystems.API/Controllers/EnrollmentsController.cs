using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.API.Models.Dtos;
using OnlineExaminationSystems.API.Services.Abstract;
using OnlineExaminationSystems.API.Models.Helpers;

namespace OnlineExaminationSystems.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EnrollmentsController : ControllerBase
{
    private readonly IEnrollmentsService _enrollmentsService;
    private readonly IValidator<EnrollmentUpdateRequestModel> _validatorEnrollmentUpdateRequest;

    public EnrollmentsController(IEnrollmentsService enrollmentsService, IValidator<EnrollmentUpdateRequestModel> validatorEnrollmentUpdateRequest)
    {
        _enrollmentsService = enrollmentsService;
        _validatorEnrollmentUpdateRequest = validatorEnrollmentUpdateRequest;
    }

    [HttpGet]
    [Authorize(Roles = Roles.Admin)]
    public IActionResult Get()
    {
        var enrollments = _enrollmentsService.GetAll();
        return Ok(enrollments);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = Roles.Admin)]
    public IActionResult Get(int id)
    {
        var enrollment = _enrollmentsService.GetById(id);
        return enrollment != null ? Ok(enrollment) : NotFound();
    }

    [HttpPost]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> Create(EnrollmentUpdateRequestModel model)
    {
        var validationResult = await _validatorEnrollmentUpdateRequest.ValidateAsync(model);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var enrollment = _enrollmentsService.Create(model);
        return CreatedAtAction(nameof(Get), new { id = enrollment.Id }, enrollment);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> Update(int id, EnrollmentUpdateRequestModel model)
    {
        var validationResult = await _validatorEnrollmentUpdateRequest.ValidateAsync(model);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var enrollment = _enrollmentsService.Update(id, model);
        return Ok(enrollment);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = Roles.Admin)]
    public IActionResult Delete(int id)
    {
        var result = _enrollmentsService.Delete(id);
        return result ? Ok() : NotFound();
    }
}