﻿namespace OnlineExaminationSystems.UI.Areas.Admin.Models.User
{
    public class UserWithRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public Role.Role Role { get; set; }
    }
}