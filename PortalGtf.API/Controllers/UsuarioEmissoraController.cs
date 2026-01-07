using Microsoft.AspNetCore.Mvc;
using PortalGtf.Application.Services.UsuarioEmissoarServices;
using PortalGtf.Application.Services.UsuarioEmissoraService;

namespace PortalGtf.API.Controllers;

[ApiController]
[Route("api/usuario-emissora")]
public class UsuarioEmissoraController : ControllerBase
{
    private readonly IUsuarioEmissoraService _service;

    public UsuarioEmissoraController(IUsuarioEmissoraService service)
    {
        _service = service;
    }   
    [HttpGet("buscarTodos")]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpPost("criar")]
    public async Task<IActionResult> Create([FromBody] UsuarioEmissoraCreateViewModel model)
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