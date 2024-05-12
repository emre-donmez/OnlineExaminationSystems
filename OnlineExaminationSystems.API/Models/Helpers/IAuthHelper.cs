using OnlineExaminationSystems.API.Models.Entities;

namespace OnlineExaminationSystems.API.Models.Helpers
{
    public interface IAuthHelper
    {
        string GenerateJWTToken(User user);
    }
}
