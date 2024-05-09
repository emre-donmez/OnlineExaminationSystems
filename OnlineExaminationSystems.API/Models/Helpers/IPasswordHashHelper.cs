namespace OnlineExaminationSystems.API.Models.Helpers
{
    public interface IPasswordHashHelper
    {
        string HashPassword(string password);

        bool VerifyPassword(string password, string hash);
    }
}