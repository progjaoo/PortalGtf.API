using PortalGtf.Application.Services.UsuarioEmissoraService;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Application.Services.UsuarioEmissoarServices;

public class UsuarioEmissoraService : IUsuarioEmissoraService
{
    private readonly IUsuarioEmissoraRepository _repository;

    public UsuarioEmissoraService(IUsuarioEmissoraRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<UsuarioEmissoraViewModel>> GetAllAsync()
    {
        var lista = await _repository.GetAllAsync();

        return lista.Select(ue => new UsuarioEmissoraViewModel
        {
            Id = ue.Id,
            UsuarioId = ue.UsuarioId,
            EmissoraId = ue.EmissoraId,
            FuncaoId = ue.FuncaoId
        }).ToList();
    }

    public async Task<int> CreateAsync(UsuarioEmissoraCreateViewModel model)
    {
        var entity = new UsuarioEmissora
        {
            UsuarioId = model.UsuarioId,
            EmissoraId = model.EmissoraId,
            FuncaoId = model.FuncaoId
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