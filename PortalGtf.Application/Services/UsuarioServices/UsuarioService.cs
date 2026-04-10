using PortalGtf.Application.Services.AuthServices;
using PortalGtf.Application.ViewModels.LoginVM;
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
            StatusUsuario = u.StatusUsuario,
            NomeFuncao = u.Funcao.TipoFuncao    
        });
    }
    
    public async Task<LoginResponseViewModel?> LoginUserAsync(LoginRequestViewModel model)
    {
        return await _authService.LoginAsync(model);
    }

    public async Task AtivarUsuarioAsync(int usuarioId)
    {
        var user = await _usuarioRepository.GetByIdAsync(usuarioId);

        if (user == null)
            throw new Exception("Usuário não encontrado");
        
        user.AtivarUsuario();
        await _usuarioRepository.UpdateAsync(user);
    }
    public async Task DesativarUsuarioAsync(int usuarioId)
    {
        var user = await _usuarioRepository.GetByIdAsync(usuarioId);

        if (user == null)
            throw new Exception("Usuário não encontrado");
        
        user.DesativarUsuario();
        await _usuarioRepository.UpdateAsync(user);
    }

    public async Task UpdateAsync(int id, UsuarioUpdateViewModel model)
    {
        var user = await _usuarioRepository.GetByIdAsync(id);
        
        user.Email = model.Email;
        user.NomeCompleto = model.NomeCompleto;
        user.FuncaoId = model.FuncaoId;
        
        await _usuarioRepository.UpdateAsync(user);
        await _usuarioRepository.SaveChangesAsync();
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
            StatusUsuario = usuario.StatusUsuario,
            FuncaoId =  usuario.FuncaoId,
            NomeFuncao = usuario.Funcao.TipoFuncao,
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