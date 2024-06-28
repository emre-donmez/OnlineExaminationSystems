using FluentValidation;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = Roles.Admin)]
        public IActionResult Get()
        {
            var roles = _rolesService.GetAll();
            return Ok(roles);
        }
    }
}