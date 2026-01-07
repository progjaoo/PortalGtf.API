using PortalGtf.Application.ViewModels.EmissoraRegiaoVM;

namespace PortalGtf.Application.Services.EmissoraRegiaoServices;

public interface IEmissoraRegiaoService
{
    Task<List<EmissoraRegiaoViewModel>> GetAllAsync();
    Task<int> CreateAsync(EmissoraRegiaoCreateViewModel model);
    Task<bool> DeleteAsync(int id);
}