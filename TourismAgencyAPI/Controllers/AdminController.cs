using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TourismAgencyAPI.Data;
using TourismAgencyAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace TourismAgencyAPI.Controllers;

[Route("api/admin")]
[ApiController]
[Authorize(Roles = "Admin")]
public class AdminController(AppDbContext context) : ControllerBase
{
    public class TourismPackageDto
    {
        [FromForm, Required] public string Name { get; set; } = string.Empty;
        [FromForm, Required] public string Description { get; set; } = string.Empty;
        [FromForm, Required] public decimal Price { get; set; }
        [FromForm, Required] public IFormFile Image { get; set; } = default!;
    }

    [HttpPost("add-package")]
    public async Task<IActionResult> AddPackage([FromForm] TourismPackageDto packageDto)
    {
        if (packageDto.Image.Length == 0)
            return BadRequest(new { Message = "Image file is required" });

        using var memoryStream = new MemoryStream();
        await packageDto.Image.CopyToAsync(memoryStream);
        var imageBytes = memoryStream.ToArray();

        var package = new TourismPackage
        {
            Name = packageDto.Name,
            Description = packageDto.Description,
            Price = packageDto.Price,
            ImageData = imageBytes
        };

        await context.TourismPackages.AddAsync(package);
        await context.SaveChangesAsync();

        return Ok(new { Message = "Package added successfully", PackageId = package.Id });
    }
}