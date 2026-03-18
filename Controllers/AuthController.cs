using Microsoft.AspNetCore.Mvc;
using KucniSavetBackend.Interfaces.Services;
using KucniSavetBackend.DTO.Requests.User;

namespace KucniSavetBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> FacebookLogin([FromBody] CreateUserRequest request)
    {
        var user = await _authService.LoginWithFacebookAsync(request.FacebookAccessToken);

        if (user is null)
            return BadRequest();

        var jwt = _authService.GenerateJwt(user);
        return Ok(new { token = jwt });
    }
}