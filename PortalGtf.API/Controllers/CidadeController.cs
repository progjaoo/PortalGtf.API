using Microsoft.AspNetCore.Mvc;
using PortalGtf.Application.Services.CidadeService;
using PortalGtf.Application.ViewModels.CidadeVM;

namespace PortalGtf.API.Controllers;

[ApiController]
[Route("api/cidade")]
public class CidadeController : ControllerBase
{
    private readonly ICidadeService _cidadeService;

    public CidadeController(ICidadeService cidadeService)
    {
        _cidadeService = cidadeService;
    }
    [HttpGet("buscarTodos")]
    public async Task<IActionResult> GetAll()
    {
        var cidades = await _cidadeService.GetAllAsync();
        return Ok(cidades);
    }
    [HttpGet("{id:int}/buscarPorId")]
    public async Task<IActionResult> GetById(int id)
    {
        var cidade = await _cidadeService.GetByIdAsync(id);
        if (cidade == null)
            return NotFound();

        return Ok(cidade);
    }
    [HttpPost("criarCidade")]
    public async Task<IActionResult> Create([FromBody] CidadeCreateViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = await _cidadeService.CreateAsync(model);
        return CreatedAtAction(nameof(GetById), new { id }, null);
    }
    [HttpPut("{id:int}/atualizarCidade")]
    public async Task<IActionResult> Update(int id, [FromBody] CidadeCreateViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var success = await _cidadeService.UpdateAsync(id, model);
        if (!success)
            return NotFound();

        return NoContent();
    }
    [HttpDelete("{id:int}/deletarCidade")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _cidadeService.DeleteAsync(id);
        if (!success)
            return NotFound();

        return NoContent();
    }
}