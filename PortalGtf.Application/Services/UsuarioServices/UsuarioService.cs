using PortalGtf.Application.Services.AuthServices;
using PortalGtf.Application.ViewModels.UsuarioVM;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Enums;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Application.Services.UsuarioServices;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IAuthService _authService;

    public UsuarioService(
        IUsuarioRepository usuarioRepository,
        IAuthService authService)
    {
        _usuarioRepository = usuarioRepository;
        _authService = authService;
    }

    public async Task<IEnumerable<UsuarioResponseViewModel>> GetAllAsync()
    {
        var usuarios = await _usuarioRepository.GetAllAsync();

        return usuarios.Select(u => new UsuarioResponseViewModel
        {
            Id = u.Id,
            Email = u.Email,
            NomeCompleto = u.NomeCompleto,
            StatusUsuario = u.StatusUsuario
        });
    }

    public async Task<UsuarioResponseViewModel?> GetByIdAsync(int id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);
        if (usuario == null) return null;

        return new UsuarioResponseViewModel
        {
            Id = usuario.Id,
            Email = usuario.Email,
            NomeCompleto = usuario.NomeCompleto,
            StatusUsuario = usuario.StatusUsuario
        };
    }

    public async Task CreateAsync(UsuarioCreateViewModel model)
    {
        var senhaHash = _authService.ComputeSha256Hash(model.Senha);

        var usuario = new Usuario
        {
            Email = model.Email,
            NomeCompleto = model.NomeCompleto,
            SenhaHash = senhaHash,
            FuncaoId = model.FuncaoId,
            StatusUsuario = StatusUsuario.Pendente,
            DataCriacao = DateTime.UtcNow
        };

        await _usuarioRepository.AddAsync(usuario);
    }
}