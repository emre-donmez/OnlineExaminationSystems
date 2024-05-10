using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.API.Models.Dtos;
using OnlineExaminationSystems.API.Services.Abstract;

namespace OnlineExaminationSystems.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesService;
        private readonly IValidator<RoleUpdateRequestModel> _validatorRoleUpdateRequest;     

        public RolesController(IRolesService rolesService, IValidator<RoleUpdateRequestModel> validatorRoleUpdateRequest)
        {
            _rolesService = rolesService;
            _validatorRoleUpdateRequest = validatorRoleUpdateRequest;         
        }

        [HttpGet]
        public IActionResult Get()
        {
            var roles = _rolesService.GetAll();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var role = _rolesService.GetById(id);

            return role != null ? Ok(role) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleUpdateRequestModel model)
        {
            var validationResult = await _validatorRoleUpdateRequest.ValidateAsync(model);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var role = _rolesService.Create(model);
            return CreatedAtAction(nameof(Get), new { id = role.Id }, role);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, RoleUpdateRequestModel model)
        {
            var validationResult = await _validatorRoleUpdateRequest.ValidateAsync(model);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var lesson = _rolesService.Update(id, model);
            return Ok(lesson);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _rolesService.Delete(id);
            return result ? Ok() : NotFound();
        }
    }
}
