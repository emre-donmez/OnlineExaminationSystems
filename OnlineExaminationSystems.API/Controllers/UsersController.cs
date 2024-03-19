using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.API.Model.Dtos.User;
using OnlineExaminationSystems.API.Model.Entities;
using OnlineExaminationSystems.API.Model.Repository;

namespace OnlineExaminationSystems.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IValidator<UserUpdateRequestModel> _validatorUserUpdateRequest;
        private readonly IValidator<User> _validatorUser;
        private readonly IGenericRepository<User> _repository;
        private readonly IMapper _mapper;

        public UsersController(IValidator<UserUpdateRequestModel> validatorUserUpdateRequest, IValidator<User> validatorUser, IGenericRepository<User> repository, IMapper mapper)
        {
            _validatorUserUpdateRequest = validatorUserUpdateRequest;
            _validatorUser = validatorUser;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = _repository.GetAll();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = _repository.GetById(id);

            if (user is null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserUpdateRequestModel model)
        {
            var validationResult = await _validatorUserUpdateRequest.ValidateAsync(model);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var mappingModel = _mapper.Map<User>(model);

            var result = _repository.Insert(mappingModel);

            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UserUpdateRequestModel model)
        {
            var user = _repository.GetById(id);

            if (user is null)
                return NotFound();

            var validationResult = await _validatorUserUpdateRequest.ValidateAsync(model);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var mappingModel = _mapper.Map<User>(model);
            mappingModel.Id = id;

            var result = _repository.Update(mappingModel);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = _repository.GetById(id);

            if (user is null)
                return NotFound();

            var deleteOperation = _repository.SoftDelete(id);

            if (deleteOperation)
                return Ok();

            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, JsonPatchDocument<User> patchDocument)
        {
            var user = _repository.GetById(id);

            if (user is null)
                return NotFound();

            patchDocument.ApplyTo(user);

            var validationResult = await _validatorUser.ValidateAsync(user);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var result = _repository.Update(user);

            return Ok(result);
        }
    }
}