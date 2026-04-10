using PortalGtf.Application.Services.EstadoVM;
using PortalGtf.Application.ViewModels.EstadoVM;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Application.Services.EstadoServices;

public class EstadoService : IEstadoService
{
    private readonly IEstadoRepository _estadoRepository;

    public EstadoService(IEstadoRepository estadoRepository)
    {
        _estadoRepository = estadoRepository;
    }

    public async Task<List<EstadoViewModel>> GetAllAsync()
    {
        var estados = await _estadoRepository.GetAllAsync();

        return estados.Select(e => new EstadoViewModel
        {
            Id = e.Id,
            Nome = e.Nome,
            Sigla = e.Sigla,
            RegiaoId = e.RegiaoId
        }).ToList();
    }

    public async Task<EstadoViewModel?> GetByIdAsync(int id)
    {
        var estado = await _estadoRepository.GetByIdAsync(id);

        if (estado == null)
            return null;

        return new EstadoViewModel
        {
            Id = estado.Id,
            Nome = estado.Nome,
            Sigla = estado.Sigla,
            RegiaoId = estado.RegiaoId
        };
    }

    public async Task<int> CreateAsync(EstadoCreateViewModel model)
    {
        var estado = new Estado
        {
            Nome = model.Nome,
            Sigla = model.Sigla,
            RegiaoId = model.RegiaoId
        };

        await _estadoRepository.AddAsync(estado);
        return estado.Id;
    }

    public async Task<bool> UpdateAsync(int id, EstadoCreateViewModel model)
    {
        var estado = await _estadoRepository.GetByIdAsync(id);

        if (estado == null)
            return false;

        estado.Nome = model.Nome;
        estado.Sigla = model.Sigla;
        estado.RegiaoId = model.RegiaoId;

        await _estadoRepository.UpdateAsync(estado);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var estado = await _estadoRepository.GetByIdAsync(id);

        if (estado == null)
            return false;

        await _estadoRepository.DeleteAsync(estado);
        return true;
    }
}
