using Microsoft.AspNetCore.Mvc;
using PortalGtf.Application.Services.RegiaoServices;
using PortalGtf.Application.ViewModels.RegiaoVM;
using PortalGtf.Infrastructure.Repositories;

namespace PortalGtf.API.Controllers;

[ApiController] 
[Route("api/regiao")]
public class RegiaoController : ControllerBase
{
    private readonly RegiaoService _regiaoService;

    public RegiaoController(RegiaoService regiaoService)
    {
        _regiaoService = regiaoService;
    }
    [HttpGet("buscarTodos")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _regiaoService.GetAllAsync();
        return Ok(result);
    }
    [HttpGet("{id}/regiaoPorId")]
    public async Task<IActionResult> GetById(int id)
    {
        var regiao = await _regiaoService.GetByIdAsync(id);
        if (regiao == null)
            return NotFound();

        return Ok(regiao);
    }

    [HttpPost("criarRegiao")]
    public async Task<IActionResult> Create([FromBody] RegiaoCreateViewModel model)
    {
        await _regiaoService.CreateAsync(model);
        return CreatedAtAction(nameof(GetAll), null);
    }

    [HttpPut("{id}/atualizarRegiao")]
    public async Task<IActionResult> Update(int id, [FromBody] RegiaoCreateViewModel model)
    {
        await _regiaoService.UpdateAsync(id, model);
        return NoContent();
    }
    [HttpDelete("{id}/deletarRegiaio")]
    public async Task<IActionResult> Delete(int id)
    {
        await _regiaoService.DeleteAsync(id);
        return NoContent();
    }
}