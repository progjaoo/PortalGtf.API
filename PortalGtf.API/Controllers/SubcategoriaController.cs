using Microsoft.AspNetCore.Mvc;
using PortalGtf.Application.Services.SubcategoryServices;
using PortalGtf.Application.ViewModels.SubcategoryVM;

namespace PortalGtf.API.Controllers;

[ApiController]
[Route("api/subcategorias")]
public class SubcategoriaController : ControllerBase
{
    private readonly ISubcategoriaService _service;

    public SubcategoriaController(ISubcategoriaService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var subcategorias = await _service.GetByIdAsync(id);
        return Ok(subcategorias);
    }
    [HttpGet("buscarTodasSubcategorias")]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpPost("criarSubcategoria")]
    public async Task<IActionResult> Create([FromBody] CreateSubcategoriaViewModel viewModel)
    {
        await _service.CreateAsync(viewModel.Nome, viewModel.EditorialId);
        return Ok(new { message = "Subcategoria criada com sucesso" });
    }

    [HttpDelete("{id}/deletarSubcategoria")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return Ok(new { message = "Subcategoria removida" });
    }
}