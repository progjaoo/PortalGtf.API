using PortalGtf.Application.ViewModels.CidadeVM;

namespace PortalGtf.Application.Services.CidadeService;

public interface ICidadeService
{
    Task<List<CidadeViewModel>> GetAllAsync();
    Task<CidadeViewModel?> GetByIdAsync(int id);
    Task<int> CreateAsync(CidadeCreateViewModel model);
    Task<bool> UpdateAsync(int id, CidadeCreateViewModel model);
    Task<bool> DeleteAsync(int id);
}