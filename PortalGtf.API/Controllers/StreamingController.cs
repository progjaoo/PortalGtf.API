using Microsoft.AspNetCore.Mvc;
using PortalGtf.Application.Services.StreamingServices;
using PortalGtf.Application.ViewModels.StreamingVM;

namespace PortalGtf.API.Controllers;

[ApiController]
[Route("api/streaming")]
public class StreamingController : ControllerBase
{
    private readonly IStreamingService _streamingService;
    public StreamingController(IStreamingService streamingService)
    {
        _streamingService = streamingService;
    }
    [HttpGet("buscarTodos")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _streamingService.GetAllAsync();
        return Ok(result);
    }
    [HttpGet("{id}/streamingPorId")]
    public async Task<IActionResult> GetById(int id)
    {
        var streaming = await _streamingService.GetByIdAsync(id);
        if (streaming == null)
            return NotFound();

        return Ok(streaming);
    }
    [HttpPost("criarStreaming")]
    public async Task<IActionResult> Create([FromBody] StreamingCreateViewModel model)
    {
        await _streamingService.CreateAsync(model);
        return CreatedAtAction(nameof(GetAll), null);
    }
    [HttpPut("{id}/atualizarStreaming")]
    public async Task<IActionResult> Update(int id, [FromBody] StreamingCreateViewModel model)
    {
        await _streamingService.UpdateAsync(id, model);
        return NoContent();
    }
    [HttpDelete("{id}/deletarStreaming")]
    public async Task<IActionResult> Delete(int id)
    {
        await _streamingService.DeleteAsync(id);
        return NoContent();
    }
}