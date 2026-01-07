using PortalGtf.Application.ViewModels.FuncaoPermissaoVM;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Application.Services.FuncaoPermissaoService;

public class FuncaoPermissaoService : IFuncaoPermissaoService
{
    private readonly IFuncaoPermissaoRepository _repository;

    public FuncaoPermissaoService(IFuncaoPermissaoRepository repository)
    {
        _repository = repository;
    }
    public async Task<List<FuncaoPermissaoViewModel>> GetAllAsync()
    {
        var lista = await _repository.GetAllAsync();

        return lista.Select(fp => new FuncaoPermissaoViewModel
        {
            Id = fp.Id,
            FuncaoId = fp.FuncaoId,
            PermissaoId = fp.PermissaoId
        }).ToList();
    }
    public async Task<int> CreateAsync(FuncaoPermissaoCreateViewModel model)
    {
        var entity = new FuncaoPermissao
        {
            FuncaoId = model.FuncaoId,
            PermissaoId = model.PermissaoId
        };

        await _repository.AddAsync(entity);
        return entity.Id;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _repository.GetByIdAsync(id);
        if (entity == null) return false;

        await _repository.DeleteAsync(entity);
        return true;
    }
}