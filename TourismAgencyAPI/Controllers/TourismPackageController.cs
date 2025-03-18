using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TourismAgencyAPI.Data;
using TourismAgencyAPI.Models;
using Microsoft.IdentityModel.JsonWebTokens;

namespace TourismAgencyAPI.Controllers;

[Route("api/packages")]
[ApiController]
public class TourismPackageController(AppDbContext context) : ControllerBase
{
    [HttpGet("all")]
    public async Task<IActionResult> GetAllPackages()
    {
        var packages = await context.TourismPackages.ToListAsync();

        var result = packages.Select(p => new
        {
            p.Id,
            p.Name,
            p.Description,
            p.Price,
            ImageBase64 = Convert.ToBase64String(p.ImageData)
        });

        return Ok(result);
    }

    [Authorize]
    [HttpPost("purchase/{packageId}")]
    public async Task<IActionResult> PurchasePackage(Guid packageId)
    {
        var userEmailClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst(JwtRegisteredClaimNames.Sub);

        if (userEmailClaim == null)
        {
            return Unauthorized(new { Message = "Invalid token or user not found" });
        }

        string userEmail = userEmailClaim.Value;

        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
        if (user == null)
        {
            return Unauthorized(new { Message = "User not found in database" });
        }

        var package = await context.TourismPackages.FindAsync(packageId);
        if (package == null)
        {
            return NotFound(new { Message = "Package not found" });
        }

        var transaction = new Transaction
        {
            UserId = user.Id,
            PackageId = package.Id,
            Timestamp = DateTime.UtcNow
        };

        await context.Transactions.AddAsync(transaction);
        await context.SaveChangesAsync();

        return Ok(new { Message = $"Package {package.Name} purchased successfully!" });
    }
}