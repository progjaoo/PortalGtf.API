using Microsoft.AspNetCore.Mvc;
using PortalGtf.Application.Services.ProgramacaoRadioServices;
using PortalGtf.Application.ViewModels.ProgramacaoVM;

namespace PortalGtf.API.Controllers;

[ApiController]
[Route("api/programacao")]
public class ProgramacaoRadioController : ControllerBase
{
    private readonly IProgramacaoRadioService _service;
    public ProgramacaoRadioController(IProgramacaoRadioService service)
    {
        _service = service;
    }
    /// <summary>
    /// Retorna toda programação da rádio
    /// </summary>
    [HttpGet("buscarTodos")]
    public async Task<IActionResult> GetAll()
    {
        var programacao = await _service.GetAllAsync();
        return Ok(programacao);
    }

    /// <summary>
    /// Retorna um programa específico
    /// </summary>
    [HttpGet("{id}/buscarPorId")]
    public async Task<IActionResult> GetById(int id)
    {
        var programa = await _service.GetByIdAsync(id);

        if (programa == null)
            return NotFound();

        return Ok(programa);
    }

    /// <summary>
    /// Cria um novo programa
    /// </summary>
    [HttpPost("criarPrograma")]
    public async Task<IActionResult> Create([FromBody] ProgramacaoRadioCreateViewModel model)
    {
        await _service.CreateAsync(model);

        return Created("", model);
    }
    /// <summary>
    /// Atualiza um programa
    /// </summary>
    [HttpPut("{id}/atualizarPrograma")]
    public async Task<IActionResult> Update(int id, [FromBody] ProgramacaoRadioUpdateViewModel model)
    {
        await _service.UpdateAsync(id, model);

        return NoContent();
    }
    /// <summary>
    /// Remove um programa
    /// </summary>
    [HttpDelete("{id}/deletarPrograma")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}