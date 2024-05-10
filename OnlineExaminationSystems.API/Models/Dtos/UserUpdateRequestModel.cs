namespace OnlineExaminationSystems.API.Models.Dtos.User
{
    public record UserUpdateRequestModel(string Name, string Surname, string Email, string Password, int RoleId);
}