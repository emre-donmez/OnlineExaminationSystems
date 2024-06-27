using System.IdentityModel.Tokens.Jwt;

namespace OnlineExaminationSystems.UI.Helpers
{
    public static class UserHelper
    {
        public static string? GetToken(HttpContext context) => context.Request.Cookies["JWToken"];

        public static string? GetRole(HttpContext context)
        {
            var token = GetToken(context);

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            var role = jwtToken.Claims.Where(c => c.Type == "role").Select(c => c.Value).FirstOrDefault();

            return role;
        }

        public static bool IsInRole(HttpContext context, string role) => GetRole(context) == role;

        public static IApiRequestHelper AddAuthorization(this IApiRequestHelper apiRequestHelper, HttpContext httpContext)
        {
            apiRequestHelper.AddAuthorization(httpContext);
            return apiRequestHelper;
        }
    }
}
