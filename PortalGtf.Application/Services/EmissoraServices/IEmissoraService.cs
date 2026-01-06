using PortalGtf.Application.ViewModels.EmissoraVM;

namespace PortalGtf.Application.Services.EmissoraServices;

public interface IEmissoraService
{
    Task<List<EmissoraViewModel>> GetAllAsync();
    Task<EmissoraViewModel?> GetByIdAsync(int id);
    Task<int> CreateAsync(EmissoraCreateViewModel model);
    Task<bool> UpdateAsync(int id, EmissoraCreateViewModel model);
    Task<bool> DeleteAsync(int id);
}