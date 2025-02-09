using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagementAPI.Models;
using TaskManagementAPI.Services;
using TaskManagementAPI.Services.Interfaces;

namespace TaskManagementAPI.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(IConfiguration config, IAuthenticationService authService) : ControllerBase
{
    private readonly IConfiguration _config = config;
    private readonly IAuthenticationService _authService = authService;

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest model)
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
    public string Username { get; set; }= string.Empty;
    public string Password { get; set; }= string.Empty;
}

