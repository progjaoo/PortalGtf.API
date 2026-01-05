using Microsoft.AspNetCore.Mvc;
using PortalGtf.Application.Services.TemaEditorialServices;
using PortalGtf.Application.ViewModels.TemaEditorialVM;

namespace PortalGtf.API.Controllers;

[ApiController]
[Route("api/tema-editorial")]
public class TemaEditorialController : ControllerBase
{
    private readonly ITemaEditorialService _service;

    public TemaEditorialController(ITemaEditorialService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var temas = await _service.GetAllAsync();
        return Ok(temas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var tema = await _service.GetByIdAsync(id);
        if (tema == null)
            return NotFound();

        return Ok(tema);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TemaEditorialViewModel model)
    {
        await _service.CreateAsync(model);
        return CreatedAtAction(nameof(GetAll), null);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TemaEditorialViewModel model)
    {
        await _service.UpdateAsync(id, model);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}