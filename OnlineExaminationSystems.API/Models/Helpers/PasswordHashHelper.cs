namespace OnlineExaminationSystems.API.Model.Helpers
{
    public class PasswordHashHelper : IPasswordHashHelper
    {
        private readonly String _salt;

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