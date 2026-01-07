using PortalGtf.Application.ViewModels.CidadeVM;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Application.Services.CidadeService;

public class CidadeService : ICidadeService
{
    private readonly ICidadeRepository _cidadeRepository;

    public CidadeService(ICidadeRepository cidadeRepository)
    {
        _cidadeRepository = cidadeRepository;
    }
    public async Task<List<CidadeViewModel>> GetAllAsync()
    {
        var cidades = await _cidadeRepository.GetAllAsync();

        return cidades.Select(c => new CidadeViewModel
        {
            Id = c.Id,
            Nome = c.Nome,
            EstadoId = c.EstadoId,
            EstadoNome = c.Estado.Nome,
            RegiaoId = c.RegiaoId,
            RegiaoNome = c.Regiao?.Nome
        }).ToList();
    }
    public async Task<CidadeViewModel?> GetByIdAsync(int id)
    {
        var cidade = await _cidadeRepository.GetByIdAsync(id);
        if (cidade == null)
            return null;

        return new CidadeViewModel
        {
            Id = cidade.Id,
            Nome = cidade.Nome,
            EstadoId = cidade.EstadoId,
            EstadoNome = cidade.Estado.Nome,
            RegiaoId = cidade.RegiaoId,
            RegiaoNome = cidade.Regiao?.Nome
        };
    }

    public async Task<int> CreateAsync(CidadeCreateViewModel model)
    {
        var cidade = new Cidade
        {
            Nome = model.Nome,
            EstadoId = model.EstadoId,
            RegiaoId = model.RegiaoId
        };

        await _cidadeRepository.AddAsync(cidade);
        return cidade.Id;
    }

    public async Task<bool> UpdateAsync(int id, CidadeCreateViewModel model)
    {
        var cidade = await _cidadeRepository.GetByIdAsync(id);
        if (cidade == null)
            return false;

        cidade.Nome = model.Nome;
        cidade.EstadoId = model.EstadoId;
        cidade.RegiaoId = model.RegiaoId;

        await _cidadeRepository.UpdateAsync(cidade);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var cidade = await _cidadeRepository.GetByIdAsync(id);
        if (cidade == null)
            return false;

        await _cidadeRepository.DeleteAsync(cidade);
        return true;
    }
}