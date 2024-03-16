namespace OnlineExaminationSystems.API.Model.Dtos.User
{
    public class UserUpdateRequestModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}