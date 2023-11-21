using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BusinessLogic;
using Microsoft.IdentityModel.Tokens;
namespace Presentation;

public static class ClientHelper
{
    public static string GenerateJwtToken(this IConfiguration configuration, ClientDto client)
    {
        JwtSecurityTokenHandler? tokenHandler = new ();

        byte[]? key = Encoding.ASCII.GetBytes(configuration["JwtConfig:Key"]);
        SecurityTokenDescriptor tokenDescriptor = new ()
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", client.Id) }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
