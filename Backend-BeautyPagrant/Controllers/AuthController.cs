using Microsoft.AspNetCore.Mvc;
using Backend_BeautyPagrant.Dto;
using Backend_BeautyPagrant.Services;
using Microsoft.AspNetCore.Authorization;

namespace Backend_BeautyPagrant.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] 
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpGet("check-token")]
    public IActionResult CheckToken()
    {
        string? authHeader = Request.Headers["Authorization"];
        if (string.IsNullOrWhiteSpace(authHeader))
        {
            return Unauthorized(new { message = "No Authorization header found" });
        }

        string token = authHeader.StartsWith("Bearer ")
            ? authHeader.Substring("Bearer ".Length).Trim()
            : authHeader.Trim();

        try
        {
            (DateTime Expiration, int RemainingMinutes, IEnumerable<System.Security.Claims.Claim> Claims) result =
                _authService.CheckToken(token);

            return Ok(new
            {
                exp = result.Expiration,
                remainingMinutes = result.RemainingMinutes,
                claims = result.Claims.Select(c => new { c.Type, c.Value })
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterDto request)
    {
        try
        {
            UserDto user = await _authService.RegisterAsync(request, "system");
            return Ok(user);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDto request)
    {
        try
        {
            (string token, UserDto user) result = await _authService.LoginAsync(request);

            return Ok(new
            {
                status = 200,
                message = "Success",
                bearerToken = result.token,
                user = result.user
            });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    [HttpPost("login-refresh")]
    [AllowAnonymous]
    public async Task<IActionResult> LoginWithRefresh([FromBody] LoginDto request)
    {
        try
        {
            (string accessToken, string refreshToken, UserDto user) result =
                await _authService.LoginWithRefreshAsync(request);

            return Ok(new
            {
                accessToken = result.accessToken,
                refreshToken = result.refreshToken,
                user = result.user
            });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<IActionResult> Refresh([FromBody] string refreshToken)
    {
        try
        {
            string newAccessToken = await _authService.RefreshAccessTokenAsync(refreshToken);
            return Ok(new { accessToken = newAccessToken });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

}
