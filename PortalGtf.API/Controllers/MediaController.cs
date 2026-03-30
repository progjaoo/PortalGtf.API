using FluentFTP;
using Microsoft.AspNetCore.Mvc;
using PortalGtf.Application.Services.MidiaServices;

namespace PortalGtf.API.Controllers;

[ApiController]
[Route("api/media")]
public class MediaController : ControllerBase
{
    private readonly IMidiaService _service;
    public MediaController(IMidiaService service) => _service = service;

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file, int usuarioId)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Arquivo inválido");

        using var stream = file.OpenReadStream();
        var result = await _service.UploadAsync(
            stream, file.FileName, file.ContentType, usuarioId);

        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetPaged(int page = 1, int pageSize = 20)
    {
        var result = await _service.GetPagedAsync(page, pageSize);
        return Ok(result);
    }
    
    [HttpGet("{id}/download")]
    public async Task<IActionResult> Download(int id)
    {
        try
        {
            var midia = await _service.DownloadAsync(id);

            return File(
                midia.Stream,
                midia.ContentType,
                midia.FileName);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteAsync(id);
            return NoContent(); 
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}