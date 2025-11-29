using System.Threading.Tasks;
using AnimalShelter.Dto;
using AnimalShelter.Mappers;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Services;
using Domain.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnimalShelter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        private const string RefreshTokenCookieName = "refreshToken";

        [HttpPost("login")]
        public async Task<ActionResult<AuthResultDto>> Login([FromBody] LoginRequest request)
        {
            try
            {
                AuthResult authResult = await authService.LoginAsync(request.Email, request.Password);
                SetRefreshTokenInCookie(authResult.RefreshToken);
                return Ok(authResult.ToDto());
            }
            catch (InvalidEmailException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (InvalidPasswordException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                UserRegisterCommand command = new(request.Name, request.Age, request.Email, request.Password);
                await authService.RegisterAsync(command);
                return Ok(new { message = "Registration successful. Please check your email to verify your account." });
            }
            catch (UserAlreadyExistsException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpGet("verify")]
        public async Task<ActionResult> Verify([FromQuery] string token)
        {
            try
            {
                await authService.VerifyAsync(token);
                return Ok(new { message = "Email verified successfully." });
            }
            catch (VerificationTokenExpiredException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (VerificationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<AuthResultDto>> Refresh()
        {
            string? refreshToken = Request.Cookies[RefreshTokenCookieName];

            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized(new { message = "Session expired." });
            }

            try
            {
                AuthResult authResult = await authService.RefreshAsync(refreshToken);
                SetRefreshTokenInCookie(authResult.RefreshToken);
                return Ok(authResult.ToDto());
            }
            catch (ExpiredRefreshTokenException ex)
            {
                ClearRefreshTokenFromCookie();
                return Unauthorized(new { message = ex.Message });
            }
        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            string? refreshToken = Request.Cookies[RefreshTokenCookieName];

            if (!string.IsNullOrEmpty(refreshToken))
            {
                try
                {
                    await authService.LogoutAsync(refreshToken);
                }
                catch (ExpiredRefreshTokenException)
                {
                }
            }

            ClearRefreshTokenFromCookie();
            return Ok(new { message = "Logged out successfully." });
        }

        private void SetRefreshTokenInCookie(RefreshToken refreshToken)
        {
            CookieOptions cookieOptions = new()
            {
                HttpOnly = true,
                Expires = refreshToken.Expires,
                Secure = false,
                SameSite = SameSiteMode.Lax
            };

            Response.Cookies.Append(RefreshTokenCookieName, refreshToken.Token, cookieOptions);
        }

        private void ClearRefreshTokenFromCookie()
        {
            CookieOptions cookieOptions = new()
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(-1),
                Secure = false,
                SameSite = SameSiteMode.Lax
            };

            Response.Cookies.Append(RefreshTokenCookieName, string.Empty, cookieOptions);
        }
    }
}