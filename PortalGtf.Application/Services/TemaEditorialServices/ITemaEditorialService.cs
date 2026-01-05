using PortalGtf.Application.ViewModels.TemaEditorialVM;

namespace PortalGtf.Application.Services.TemaEditorialServices;

public interface ITemaEditorialService
{
    Task<IEnumerable<TemaEditorialResponseViewModel>> GetAllAsync();
    Task<TemaEditorialResponseViewModel> GetByIdAsync(int id);
    Task CreateAsync(TemaEditorialViewModel model);
    Task UpdateAsync(int id, TemaEditorialViewModel model);
    Task DeleteAsync(int id);
}