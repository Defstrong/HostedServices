using System.Data.Entity.Core;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BusinessLogic;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
namespace Presentation;

public class JwtMiddlware
{
    private readonly RequestDelegate _next;
    private readonly JwtConfig _jwtConfig;
    public JwtMiddlware(RequestDelegate next, IOptionsMonitor<JwtConfig> jwtConfig)
    {
        _next = next;
        _jwtConfig = jwtConfig.CurrentValue;
    }

    public async Task Invoke(HttpContext context, IClientService clientService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if(token != null)
            await AttachUserToContext(context, clientService, token);
        
        await _next(context);
    }

    private async Task AttachUserToContext(HttpContext context, 
        IClientService clientService, string? token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_jwtConfig.Key);
        tokenHandler.ValidateToken(token,
            new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

        JwtSecurityToken jwtToken = (JwtSecurityToken)validatedToken;
        string clientId = jwtToken.Claims.First(x => x.Type == "id").Value;

        context.Items["Client"] = await clientService.GetAsync(clientId);
    }
}