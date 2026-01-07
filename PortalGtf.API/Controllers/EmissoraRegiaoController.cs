using Microsoft.AspNetCore.Mvc;
using PortalGtf.Application.Services.EmissoraRegiaoServices;
using PortalGtf.Application.ViewModels.EmissoraRegiaoVM;

namespace PortalGtf.API.Controllers;

[ApiController]
[Route("api/emissora-regiao")]
public class EmissoraRegiaoController : ControllerBase
{
    private readonly IEmissoraRegiaoService _service;

    public EmissoraRegiaoController(IEmissoraRegiaoService service)
    {
        _service = service;
    }

    [HttpGet("buscarTodos")]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpPost("criar")]
    public async Task<IActionResult> Create([FromBody] EmissoraRegiaoCreateViewModel model)
    {
        var id = await _service.CreateAsync(model);
        return CreatedAtAction(nameof(GetAll), new { id }, null);
    }

    [HttpDelete("{id:int}/deletar")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteAsync(id);
        if (!success) return NotFound();

        return NoContent();
    }
}
