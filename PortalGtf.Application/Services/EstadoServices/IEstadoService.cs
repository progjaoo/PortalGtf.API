using PortalGtf.Application.Services.EstadoVM;
using PortalGtf.Application.ViewModels.EstadoVM;

namespace PortalGtf.Application.Services.EstadoServices;

public interface IEstadoService
{
    Task<List<EstadoViewModel>> GetAllAsync();
    Task<EstadoViewModel?> GetByIdAsync(int id);
    Task<int> CreateAsync(EstadoCreateViewModel model);
    Task<bool> UpdateAsync(int id, EstadoCreateViewModel model);
    Task<bool> DeleteAsync(int id);
}