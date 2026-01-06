using Microsoft.AspNetCore.Mvc;
using PortalGtf.Application.Services.PermissaoServices;
using PortalGtf.Application.ViewModels.PermissaoVM;

namespace PortalGtf.API.Controllers;

[ApiController]
[Route("api/permissao")]
public class PermissaoController : ControllerBase
{
    private readonly IPermissaoService _permissaoService;

    public PermissaoController(IPermissaoService permissaoService)
    {
        _permissaoService = permissaoService;
    }

    /// <summary>
    /// Retorna todas as permissões
    /// </summary>
    [HttpGet("buscarTodasPermissoes")]
    public async Task<ActionResult<List<PermissaoViewmModel>>> GetAll()
    {
        var permissoes = await _permissaoService.GetAllAsync();
        return Ok(permissoes);
    }

    /// <summary>
    /// Retorna uma permissão pelo Id
    /// </summary>
    [HttpGet("{id:int}/buscarPorId")]
    public async Task<ActionResult<PermissaoViewmModel>> GetById(int id)
    {
        var permissao = await _permissaoService.GetByIdAsync(id);

        if (permissao == null)
            return NotFound();

        return Ok(permissao);
    }

    /// <summary>
    /// Retorna todas as permissões de um usuário
    /// </summary>
    [HttpGet("PermissaoPorUsuario/{usuarioId:int}")]
    public async Task<ActionResult<List<PermissaoResponseViewModel>>> GetAllByUsuario(int usuarioId)
    {
        var permissoes = await _permissaoService.GetAllPermissoesByUsuarioAsync(usuarioId);
        return Ok(permissoes);
    }

    /// <summary>
    /// Cria uma nova permissão
    /// </summary>
    [HttpPost("ciarPermissao")]
    public async Task<IActionResult> Create([FromBody] PermissaoCreateViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = await _permissaoService.CreateAsync(model);

        return CreatedAtAction(nameof(GetById), new { id }, null);
    }

    /// <summary>
    /// Atualiza uma permissão existente
    /// </summary>
    [HttpPut("{id:int}/atualizarPermissao")]
    public async Task<IActionResult> Update(int id, [FromBody] PermissaoCreateViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var success = await _permissaoService.UpdateAsync(id, model);

        if (!success)
            return NotFound();

        return NoContent();
    }

    /// <summary>
    /// Remove uma permissão
    /// </summary>
    [HttpDelete("{id:int}/deletarPermissao")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _permissaoService.DeleteAsync(id);

        if (!success)
            return NotFound();

        return NoContent();
    }
}