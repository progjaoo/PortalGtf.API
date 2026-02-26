using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalGtf.Application.Services.AuthServices;
using PortalGtf.Application.Services.UsuarioServices;
using PortalGtf.Application.ViewModels.LoginVM;
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
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequestViewModel model)
    {
        var result = await _usuarioService.LoginUserAsync(model);

        if (result == null)
            return Unauthorized("Email ou senha inválidos");

        return Ok(result);
    }
}