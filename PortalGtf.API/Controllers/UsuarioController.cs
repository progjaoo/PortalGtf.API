using Microsoft.AspNetCore.Mvc;
using PortalGtf.Application.Services.UsuarioServices;
using PortalGtf.Application.ViewModels.UsuarioVM;

namespace PortalGtf.Api.Controllers;

[ApiController]
[Route("api/usuarios")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioService _usuarioService;

    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioResponseViewModel>>> GetAll()
    {
        var usuarios = await _usuarioService.GetAllAsync();
        return Ok(usuarios);
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UsuarioResponseViewModel>> GetById(int id)
    {
        var usuario = await _usuarioService.GetByIdAsync(id);
        if (usuario == null)
            return NotFound();

        return Ok(usuario);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UsuarioCreateViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _usuarioService.CreateAsync(model);

        return StatusCode(StatusCodes.Status201Created);
    }
}