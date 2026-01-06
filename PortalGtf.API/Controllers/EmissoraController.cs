using Microsoft.AspNetCore.Mvc;
using PortalGtf.Application.Services.EmissoraServices;
using PortalGtf.Application.ViewModels.EmissoraVM;

namespace PortalGtf.API.Controllers;
 
[ApiController]
[Route("api/emissora")]
public class EmissoraController : ControllerBase
{
    private readonly IEmissoraService _emissoraService;

    public EmissoraController(IEmissoraService emissoraService)
    {
        _emissoraService = emissoraService;
    }
    /// <summary>
    /// Retorna uma emissora pelo Id
    /// </summary>
    [HttpGet("{id:int}/buscarPorId")]
    public async Task<ActionResult<EmissoraViewModel>> GetById(int id)
    {
        var emissora = await _emissoraService.GetByIdAsync(id);

        if (emissora == null)
            return NotFound();

        return Ok(emissora);
    }

    /// <summary>
    /// Retorna todas as emissoras 
    /// </summary>
    [HttpGet("buscarTodos")]
    public async Task<IActionResult> GetAll()
    {
        var emissora = await _emissoraService.GetAllAsync();
        return Ok(emissora);
    }
    /// <summary>
    /// Cria uma nova emissora
    /// </summary>
    [HttpPost("criarEmissora")]
    public async Task<IActionResult> Create([FromBody] EmissoraCreateViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = await _emissoraService.CreateAsync(model);

        return CreatedAtAction(nameof(GetById), new { id }, null);
    }
    /// <summary>
    /// Atualiza uma emissora existente
    /// </summary>
    [HttpPut("{id:int}/atualizarEmissora")]
    public async Task<IActionResult> Update(int id, [FromBody] EmissoraCreateViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var success = await _emissoraService.UpdateAsync(id, model);

        if (!success)
            return NotFound();

        return NoContent();
    }
    /// <summary>
    /// Remove uma emissora
    /// </summary>
    [HttpDelete("{id:int}/deletarEmissora")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _emissoraService.DeleteAsync(id);

        if (!success)
            return NotFound();

        return NoContent();
    }
}