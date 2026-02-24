using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DojoManager.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
   
    private const string TokenKey = "SuaChaveSuperSecretaDeBelem2026_PRO_SISTEMA"; 

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
       
        if (request.Email == "admin@dojo.com" && request.Senha == "123456")
        {
            var token = GerarJwtToken(request.Email);
            return Ok(new { token, user = "Mestre" });
        }

        return Unauthorized("Email ou senha inválidos.");
    }

    private string GerarJwtToken(string email)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(TokenKey);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, email) }),
            Expires = DateTime.UtcNow.AddHours(8), 
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

public class LoginRequest { public string Email { get; set; } = ""; public string Senha { get; set; } = ""; }