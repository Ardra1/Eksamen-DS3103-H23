namespace FormulaOneAPI.Controllers;

using FORMULAONEAPI.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]

public class ImageUploadController : ControllerBase
{

private readonly IWebHostEnvironment environment;

public ImageUploadController(IWebHostEnvironment _environment)
{
    environment = _environment;
}

//LASTER OPP BILDE AV DRIVER
[HttpPost]
 public async Task<ActionResult<Drivers>> PostUploadDriverImage(IFormFile file)
 {
    try
    {
        string webRootPath = environment.WebRootPath;
        string absolutePath = Path.Combine($"{webRootPath}/images/drivers/{file.FileName}");
        using(var fileStream = new FileStream(absolutePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }
        return Ok("Bilde er lastet opp - fra ImageUploadControlleren (Drivers)");
    }
    catch
    {
        return StatusCode(500);
    }
 }

 //LASTER OPP BILDE AV CAR
[HttpPost]
[Route("[action]")]
 public async Task<ActionResult<Teams>> PostUploadCarImage(IFormFile file)
 {
    try
    {
        string webRootPath = environment.WebRootPath;
        string absolutePath = Path.Combine($"{webRootPath}/images/cars/{file.FileName}");
        using(var fileStream = new FileStream(absolutePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }
        return Ok("Bilde er lastet opp - fra ImageUploadControlleren (Cars)");
    }
    catch
    {
        return StatusCode(500);
    }
 }
}