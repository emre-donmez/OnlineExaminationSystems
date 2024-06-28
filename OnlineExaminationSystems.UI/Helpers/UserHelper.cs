using System.IdentityModel.Tokens.Jwt;

namespace OnlineExaminationSystems.UI.Helpers;

public static class UserHelper
{
    private static JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

    public static string? GetToken(HttpContext context) => context.Request.Cookies["JWToken"];

    private static string? GetClaimValue(HttpContext context, string claimType)
    {
        var token = GetToken(context);

        if (token == null)
            return null;

        var jwtToken = tokenHandler.ReadJwtToken(token);
        var claimValue = jwtToken.Claims.FirstOrDefault(c => c.Type == claimType)?.Value;

        return claimValue;
    }

    public static string? GetRole(HttpContext context) => GetClaimValue(context, "role");
    public static string? GetUserName(HttpContext context) => GetClaimValue(context, "unique_name");
    public static int GetUserId(HttpContext context) => Convert.ToInt32(GetClaimValue(context, "nameid"));
    public static bool IsInRole(HttpContext context, string role) => GetRole(context) == role;

}
