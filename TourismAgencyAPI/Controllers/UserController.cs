using Microsoft.AspNetCore.Mvc;
using TourismAgencyAPI.Repositories;
using TourismAgencyAPI.Models;

namespace TourismAgencyAPI.Controllers;

[Route("api/users")]
[ApiController]
public class UserController(IUserRepository userRepository) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {
        var user = await userRepository.GetByIdAsync(id);
        if (user == null)
            return NotFound(new { Message = "User not found" });

        return Ok(user);
    }
}