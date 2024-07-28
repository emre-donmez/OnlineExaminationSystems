using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.API.Models.Dtos;
using OnlineExaminationSystems.API.Models.Entities;
using OnlineExaminationSystems.API.Services.Abstract;
using OnlineExaminationSystems.API.Models.Helpers;

namespace OnlineExaminationSystems.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUsersService _userService;
    private readonly IValidator<UserUpdateRequestModel> _validatorUserUpdateRequest;
    private readonly IMapper _mapper;
    private readonly IValidator<User> _validatorUserValidator;
    private readonly ILessonsService _lessonsService;
    private readonly IEnrollmentsService _enrollmentsService;

    public UsersController(IUsersService userService, IValidator<UserUpdateRequestModel> validatorUserUpdateRequest, IMapper mapper, IValidator<User> validatorUser, ILessonsService lessonsService, IEnrollmentsService enrollmentsService)
    {
        _userService = userService;
        _validatorUserUpdateRequest = validatorUserUpdateRequest;
        _validatorUserValidator = validatorUser;
        _mapper = mapper;
        _lessonsService = lessonsService;
        _enrollmentsService = enrollmentsService;
    }

    [HttpGet]
    [Authorize(Roles = Roles.Admin)]
    public IActionResult Get()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = Roles.Admin)]
    public IActionResult Get(int id)
    {
        var user = _userService.GetById(id);

        return user != null ? Ok(user) : NotFound();
    }

    [HttpPost]
    [Authorize(Roles = Roles.Admin)]
    public async Task<IActionResult> CreateAsync(UserUpdateRequestModel model)
    {
        var validationResult = await _validatorUserUpdateRequest.ValidateAsync(model);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var user = _userService.CreateUserWithHashedPassword(model);
        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = Roles.Admin)]
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
    [Authorize(Roles = Roles.Admin)]
    public IActionResult Delete(int id)
    {
        var result = _userService.Delete(id);
        return result ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
    }

    [HttpGet("{id}/lessons")]
    [Authorize(Roles = Roles.Academician)]
    public IActionResult GetLessonsByUserId(int id)
    {
        var lessons = _lessonsService.GetLessonsByUserId(id);
        return Ok(lessons);
    }

    [HttpGet("{id}/enrollments-with-lesson")]
    [Authorize(Roles = Roles.Student)]
    public IActionResult GetEnrollmentsWithLessonByUserId(int id)
    {
        var enrollmentWithLessons = _enrollmentsService.GetEnrollmentsWithLessonByUserId(id);
        return Ok(enrollmentWithLessons);
    }

    [HttpGet("with-roles")]
    [Authorize(Roles = Roles.Admin)]
    public IActionResult GetWithRoles()
    {
        var users = _userService.GetAllWithRoles();
        return Ok(users);
    }
}