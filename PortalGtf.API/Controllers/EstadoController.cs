using Microsoft.AspNetCore.Mvc;
using PortalGtf.Application.Services.EstadoServices;
using PortalGtf.Application.ViewModels.EstadoVM;

namespace PortalGtf.API.Controllers;

[ApiController]
[Route("api/estado")]
public class EstadoController : ControllerBase
{
    private readonly IEstadoService _estadoService;
    public EstadoController(IEstadoService estadoService)
    {
        _estadoService = estadoService;
    }
    /// <summary>
    /// Retorna todos os estados
    /// </summary>
    [HttpGet("buscarTodos")]
    public async Task<IActionResult> GetAll()
    {
        var estados = await _estadoService.GetAllAsync();
        return Ok(estados);
    }
    /// <summary>
    /// Retorna um estado por Id
    /// </summary>
    [HttpGet("{id:int}/buscarPorId")]
    public async Task<IActionResult> GetById(int id)
    {
        var estado = await _estadoService.GetByIdAsync(id);

        if (estado == null)
            return NotFound();

        return Ok(estado);
    }
    /// <summary>
    /// Cria um novo estado
    /// </summary>
    [HttpPost("criarEstado")]
    public async Task<IActionResult> Create([FromBody] EstadoCreateViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = await _estadoService.CreateAsync(model);

        return CreatedAtAction(nameof(GetById), new { id }, null);
    }
    /// <summary>
    /// Atualiza um estado existente
    /// </summary>
    [HttpPut("{id:int}/atualizarEstado")]
    public async Task<IActionResult> Update(int id, [FromBody] EstadoCreateViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var success = await _estadoService.UpdateAsync(id, model);

        if (!success)
            return NotFound();

        return NoContent();
    }
    /// <summary>
    /// Remove um estado
    /// </summary>
    [HttpDelete("{id:int}/deletarEstado")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _estadoService.DeleteAsync(id);

        if (!success)
            return NotFound();
        
        return NoContent();
    }
}