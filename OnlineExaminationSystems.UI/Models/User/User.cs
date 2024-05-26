using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineExaminationSystems.UI.Models.User
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role? Role { get; internal set; }
    }
}
