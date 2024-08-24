using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using HuiLianMedical.Share.Data;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HuilianMedical.Backend.Models;

public class TokenActionFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var bearer = context.HttpContext.Request.Headers.Authorization.FirstOrDefault();
        if (string.IsNullOrEmpty(bearer) || !bearer.Contains("Bearer")) return;
        var jwt = bearer.Split(' ');
        var tokenObj = new JwtSecurityToken(jwt[1]);

        var claimsIdentity = new ClaimsIdentity(tokenObj.Claims);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
        context.HttpContext.User = claimsPrincipal;
    }
}

public static class TokenHelper
{
    public static UserModel? GetUser(this ClaimsPrincipal? claimsPrincipal)
    {
        var claimId = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
        var claimRole = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);
        var claimName = claimsPrincipal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
        if (claimId.IsNull() || claimRole.IsNull() || claimName.IsNull())
        {
            return null;
        }

        return new UserModel()
        {
            Id = claimId!.Value,
            Identity = claimRole!.Value,
            UserName = claimName!.Value
        };
    }

    private static bool IsNull(this Claim? claim)
        => claim == null || string.IsNullOrEmpty(claim.Value);
}