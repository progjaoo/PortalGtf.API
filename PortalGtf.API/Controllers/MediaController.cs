using FluentFTP;
using Microsoft.AspNetCore.Mvc;
using PortalGtf.Application.Services.MidiaServices;

namespace PortalGtf.API.Controllers;

[ApiController]
[Route("api/media")]
public class MediaController : ControllerBase
{
    private readonly IWebHostEnvironment _env;
    private readonly IMidiaService _service;
    public MediaController(IWebHostEnvironment env,  IMidiaService service)
    {
        _env = env;
        _service = service;
    }
    [HttpPost("uploadAntigo")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Arquivo inválido.");

        var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");

        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        var filePath = Path.Combine(uploadsFolder, fileName);

        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        var url = $"{Request.Scheme}://{Request.Host}/uploads/{fileName}";

        return Ok(new { url });
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file, int usuarioId)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Arquivo inválido");

        using var stream = file.OpenReadStream();

        var url = await _service.UploadAsync(
            stream,
            file.FileName,
            file.ContentType,
            usuarioId
        );

        return Ok(new { url });
    }

    [HttpGet]
    public async Task<IActionResult> GetPaged(int page = 1, int pageSize = 20)
    {
        var result = await _service.GetPagedAsync(page, pageSize);
        return Ok(result);
    }
}