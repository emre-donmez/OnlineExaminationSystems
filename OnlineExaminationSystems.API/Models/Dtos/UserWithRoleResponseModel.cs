using OnlineExaminationSystems.API.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineExaminationSystems.API.Models.Dtos
{
    public class UserWithRoleResponseModel : IEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int RoleId { get; set; }
        public Role? Role { get; set; }
    }
}