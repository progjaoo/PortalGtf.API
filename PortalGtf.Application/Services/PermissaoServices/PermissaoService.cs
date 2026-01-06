using PortalGtf.Application.ViewModels.PermissaoVM;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Application.Services.PermissaoServices;

public class PermissaoService : IPermissaoService
{
    private readonly IPermissaoRepository _permissaoRepository;

    public PermissaoService(IPermissaoRepository permissaoRepository)
    {
        _permissaoRepository = permissaoRepository;
    }
    public async Task<List<PermissaoResponseViewModel>> GetAllPermissoesByUsuarioAsync(int usuarioId)
    {
        var permissoes = await _permissaoRepository.GetAllPermissoesByUsuarioAsync(usuarioId);

        return permissoes.Select(p => new PermissaoResponseViewModel
        {
            Id = p.Id,
            TipoPermissao = p.TipoPermissao
        }).ToList();
    }
    public async Task<List<PermissaoViewmModel>> GetAllAsync()
    {
        var permissoes = await _permissaoRepository.GetAll();

        return permissoes.Select(p => new PermissaoViewmModel
        {
            Id = p.Id,
            TipoPermissao = p.TipoPermissao
        }).ToList();
    }
    public async Task<PermissaoViewmModel?> GetByIdAsync(int id)
    {
        var permissao = await _permissaoRepository.GetById(id);
        
        return new PermissaoViewmModel
        {
            Id = permissao.Id,
            TipoPermissao = permissao.TipoPermissao
        };
    }
    public async Task<int> CreateAsync(PermissaoCreateViewModel model)
    {
        var permissao = new  Permissao
        {
            TipoPermissao = model.TipoPermissao
        };
        await _permissaoRepository.AddAsync(permissao);
        return permissao.Id;
    }
    public async Task<bool> UpdateAsync(int id, PermissaoCreateViewModel model)
    {
        var permissao = await _permissaoRepository.GetById(id);
        
        permissao.TipoPermissao = model.TipoPermissao;
        await _permissaoRepository.UpdateAsync(permissao);
        return true;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var permissao = await _permissaoRepository.GetById(id);
        await _permissaoRepository.DeleteAsync(permissao);
        return true;
    }
}