using PortalGtf.Application.ViewModels.RegiaoVM;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Application.Services.RegiaoServices;

public class RegiaoService : IRegiaoService
{
    private readonly IRegiaoRepository _regiaoRepository;
    public RegiaoService(IRegiaoRepository regiaoRepository)
    {
        _regiaoRepository = regiaoRepository;
    }

    public async Task<List<RegiaoViewModel>> GetAllAsync()
    {
        var regioes = await _regiaoRepository.GetAllAsync();

        return regioes.Select(r => new RegiaoViewModel
        {
            Id = r.Id,
            Nome = r.Nome
        }).ToList();
    }
    public async Task<RegiaoViewModel?> GetByIdAsync(int id)
    {
        var regiao = await _regiaoRepository.GetByIdAsync(id);

        if (regiao == null)
            return null;

        return new RegiaoViewModel
        {
            Id = regiao.Id,
            Nome = regiao.Nome
        };
    }

    public async Task<int> CreateAsync(RegiaoCreateViewModel model)
    {
        var regiao = new Regiao
        {
            Nome = model.Nome
        };

        await _regiaoRepository.AddAsync(regiao);
        return regiao.Id;
    }

    public async Task<bool> UpdateAsync(int id, RegiaoCreateViewModel model)
    {
        var regiao = await _regiaoRepository.GetByIdAsync(id);

        if (regiao == null)
            return false;

        regiao.Nome = model.Nome;

        await _regiaoRepository.UpdateAsync(regiao);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var regiao = await _regiaoRepository.GetByIdAsync(id);

        if (regiao == null)
            return false;

        await _regiaoRepository.DeleteAsync(regiao);
        return true;
    }
}