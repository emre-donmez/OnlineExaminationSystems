namespace OnlineExaminationSystems.API.Models.Helpers
{
    public class PasswordHashHelper : IPasswordHashHelper
    {
        private readonly string _salt;

        public PasswordHashHelper(IConfiguration configuration)
        {
            _salt = configuration["Salt"];
            if (string.IsNullOrEmpty(_salt))
                throw new ArgumentException("Salt configuration is missing or empty.");
        }

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, _salt);
        }

        public bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}