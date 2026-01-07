using Microsoft.AspNetCore.Mvc;
using PortalGtf.Application.Services.EditorialServices;
using PortalGtf.Application.ViewModels.EditorialVM;

namespace PortalGtf.API.Controllers;

[ApiController]
[Route("api/editorial")]
public class EditorialController : ControllerBase
{
    private readonly IEditorialService _editorialService;

    public EditorialController(IEditorialService editorialService)
    {
        _editorialService = editorialService;
    }
    /// <summary>
    /// Retorna uma Editorial pelo Id
    /// </summary>
    [HttpGet("{id:int}/buscarPorId")]
    public async Task<ActionResult<EditorialViewModel>> GetById(int id)
    {
        var editorial = await _editorialService.GetByIdAsync(id);

        if (editorial == null)
            return NotFound();

        return Ok(editorial);
    }

    /// <summary>
    /// Retorna todas as Editorial 
    /// </summary>
    [HttpGet("buscarTodos")]
    public async Task<IActionResult> GetAll()
    {
        var editorial = await _editorialService.GetAllAsync();
        return Ok(editorial);
    }
    /// <summary>
    /// Cria um novo Editorial
    /// </summary>
    [HttpPost("criarEditorial")]
    public async Task<IActionResult> Create([FromBody] EditorialCreateViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var id = await _editorialService.CreateAsync(model);

        return CreatedAtAction(nameof(GetById), new { id }, null);
    }
    /// <summary>
    /// Atualiza um editorial existente
    /// </summary>
    [HttpPut("{id:int}/atualizarEditorial")]
    public async Task<IActionResult> Update(int id, [FromBody] EditorialCreateViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var success = await _editorialService.UpdateAsync(id, model);

        if (!success)
            return NotFound();

        return NoContent();
    }
    /// <summary>
    /// Remove um editorial
    /// </summary>
    [HttpDelete("{id:int}/deletarEditorial")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _editorialService.DeleteAsync(id);

        if (!success)
            return NotFound();

        return NoContent();
    }
}