using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagementAPI.Models;
using TaskManagementAPI.Services;
using TaskManagementAPI.Services.Interfaces;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IAuthenticationService _authService;

    public AuthController(IConfiguration config, IAuthenticationService authService)
    {
        _config = config;
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest model)
    {
        var res = _authService.AuthenticateUserAsync(model);
        if (res != null)
        {
            return Ok(new { token = res });
        }

        return Unauthorized("Invalid credentials");
    }

   
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}

