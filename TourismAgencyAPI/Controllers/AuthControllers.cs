using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using TourismAgencyAPI.Models;
using TourismAgencyAPI.Services;

namespace TourismAgencyAPI.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController(IAuthService authService, IValidator<User?> validator) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User? user)
    {
        if (user != null)
        {
            user.Id = Guid.NewGuid();

            ValidationResult validationResult = await validator.ValidateAsync(user);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            await authService.RegisterAsync(user, user.Password);
        }

        return Ok(new { Message = "User registered successfully" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] User user)
    {
        var token = await authService.LoginAsync(user.Email, user.Password);

        return Ok(new { Token = token });
    }
}