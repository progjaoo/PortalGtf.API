using FluentFTP;
using Microsoft.AspNetCore.Mvc;

namespace PortalGtf.API.Controllers;

[ApiController]
[Route("api/media")]
public class MediaController : ControllerBase
{
    private readonly IWebHostEnvironment _env;

    public MediaController(IWebHostEnvironment env)
    {
        _env = env;
    }
    [HttpPost("upload")]
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
    // [HttpPost("upload")]
    // public async Task<string> UploadViaFtp(IFormFile file)
    // {
    //     var fileName = $"{Guid.NewGuid()}_{file.FileName}";
    //     var tempPath = Path.GetTempFileName();
    //
    //     using (var stream = new FileStream(tempPath, FileMode.Create))
    //     {
    //         await file.CopyToAsync(stream);
    //     }
    //
    //     using var client = new FtpClient("ftp.grupogtf.com.br", "grupogtf1", "");
    //     client.Connect();
    //
    //     var remotePath = $"/public_html/uploads/{fileName}";
    //
    //     client.UploadFile(tempPath, remotePath);
    //
    //     client.Disconnect();
    //
    //     return $"https://grupogtf.com.br/uploads/{fileName}";
    // }
}