using Microsoft.AspNetCore.Mvc;
using PortalGtf.Application.Services.BannerInstitucionalServices;
using PortalGtf.Application.ViewModels.BannerInstitucionalVM;

namespace PortalGtf.API.Controllers;

[ApiController]
[Route("api/banner-institucional")]
public class BannerInstitucionalController : ControllerBase
{
    private readonly IBannerInstitucionalService _service;

    public BannerInstitucionalController(IBannerInstitucionalService service)
    {
        _service = service;
    }

    [HttpGet("buscarTodos")]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("ativos")]
    public async Task<IActionResult> GetAtivos([FromQuery] int emissoraId, [FromQuery] string? posicao = null)
        => Ok(await _service.GetAtivosPorEmissoraAsync(emissoraId, posicao));

    [HttpGet("{id:int}/buscarPorId")]
    public async Task<IActionResult> GetById(int id)
    {
        var banner = await _service.GetByIdAsync(id);
        if (banner == null) return NotFound();
        return Ok(banner);
    }

    [HttpPost("criar")]
    public async Task<IActionResult> Create([FromBody] BannerInstitucionalCreateViewModel model)
    {
        var id = await _service.CreateAsync(model);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    [HttpPut("{id:int}/atualizar")]
    public async Task<IActionResult> Update(int id, [FromBody] BannerInstitucionalCreateViewModel model)
    {
        var updated = await _service.UpdateAsync(id, model);
        if (!updated) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:int}/deletar")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
