using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;

    public AuthController(IConfiguration config)
    {
        _config = config;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest model)
    {
        // Mock user validation (replace with database check)
        if (model.Username == "admin" && model.Password == "admin123")
        {
            return Ok(new { token = GenerateJwtToken("Admin") });
        }
        else if (model.Username == "user" && model.Password == "user123")
        {
            return Ok(new { token = GenerateJwtToken("User") });
        }

        return Unauthorized("Invalid credentials");
    }

    private string GenerateJwtToken(string role)
    {
        var secret = _config["Jwt:Secret"];
        if (string.IsNullOrEmpty(secret))
        {
            throw new InvalidOperationException("JWT Secret is not configured.");
        }

        var key = Encoding.UTF8.GetBytes(secret);
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, "testUser"),
            new Claim(ClaimTypes.Role, role)
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}

