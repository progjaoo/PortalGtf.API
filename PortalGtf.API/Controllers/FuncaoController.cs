using Microsoft.AspNetCore.Mvc;
using PortalGtf.Application.Services.FuncaoServices;
using PortalGtf.Application.ViewModels.FuncaoVM;

namespace PortalGtf.API.Controllers;

[Route("api/funcoes")]
[ApiController]
public class FuncaoController : ControllerBase
{
    private readonly IFuncaoService _funcaoService;

    public FuncaoController(IFuncaoService funcaoService)
    {
        _funcaoService = funcaoService;
    }
    [HttpGet("buscarTodos")]
    public async Task<IActionResult> GetAll()
    {
        var funcoes = await _funcaoService.GetAllAsync();
        return Ok(funcoes);
    }
    [HttpGet("{id}/funcaoPorId")]
    public async Task<IActionResult> GetById(int id)
    {
        var funcao = await _funcaoService.GetByIdAsync(id);
        if (funcao == null)
            return NotFound();

        return Ok(funcao);
    }

    [HttpPost("criarFuncao")]
    public async Task<IActionResult> Create([FromBody] FuncaoCreateViewModel model)
    {
        await _funcaoService.CreateAsync(model);
        return CreatedAtAction(nameof(GetAll), null);
    }

    [HttpPut("{id}/atualizarFuncao")]
    public async Task<IActionResult> Update(int id, [FromBody] FuncaoViewModel model)
    {
        await _funcaoService.UpdateAsync(id, model);
        return NoContent();
    }

    [HttpDelete("{id}/deletarFuncao")]
    public async Task<IActionResult> Delete(int id)
    {
        await _funcaoService.DeleteAsync(id);
        return NoContent();
    }
}