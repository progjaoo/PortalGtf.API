using Microsoft.AspNetCore.Mvc;
using PortalGtf.Application.Services.FuncaoPermissaoService;
using PortalGtf.Application.ViewModels.FuncaoPermissaoVM;

namespace PortalGtf.API.Controllers;

[ApiController]
[Route("api/funcao-permissao")]
public class FuncaoPermissaoController : ControllerBase
{
    private readonly IFuncaoPermissaoService _service;

    public FuncaoPermissaoController(IFuncaoPermissaoService service)
    {
        _service = service;
    }

    [HttpGet("buscarTodos")]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpPost("criar")]
    public async Task<IActionResult> Create([FromBody] FuncaoPermissaoCreateViewModel model)
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