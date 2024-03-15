using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineExaminationSystems.API.Model.Context;
using OnlineExaminationSystems.API.Model.Entities;

namespace OnlineExaminationSystems.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DapperContext _context;

        public UserController(DapperContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = "SELECT * FROM Users";
            using (var connection = _context.CreateConnection())
            {
                var companies = await connection.QueryAsync<User>(query);
                return Ok(companies.ToList());
            }
        }
    }
}
