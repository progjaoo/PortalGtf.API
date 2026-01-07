using PortalGtf.Application.ViewModels.EmissoraRegiaoVM;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Application.Services.EmissoraRegiaoServices;

public class EmissoraRegiaoService : IEmissoraRegiaoService
{
    private readonly IEmissoraRegiaoRepository _repository;

    public EmissoraRegiaoService(IEmissoraRegiaoRepository repository)
    {
        _repository = repository;
    }
    public async Task<List<EmissoraRegiaoViewModel>> GetAllAsync()
    {
        var lista = await _repository.GetAllAsync();

        return lista.Select(er => new EmissoraRegiaoViewModel
        {
            Id = er.Id,
            EmissoraId = er.EmissoraId,
            RegiaoId = er.RegiaoId,
            NomeRegiao = er.Regiao.Nome,
            NomeEmissora = er.Emissora.NomeSocial
        }).ToList();
    }

    public async Task<int> CreateAsync(EmissoraRegiaoCreateViewModel model)
    {
        var entity = new EmissoraRegiao
        {
            EmissoraId = model.EmissoraId,
            RegiaoId = model.RegiaoId
            
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