using PortalGtf.Application.ViewModels.EditorialVM;

namespace PortalGtf.Application.Services.EditorialServices;

public interface IEditorialService
{
    Task<List<EditorialViewModel>> GetAllAsync();
    Task<List<EditorialViewModel>> GetByEmissoraAsync(int emissoraId);
    Task<EditorialViewModel?> GetByIdAsync(int id);
    Task<int> CreateAsync(EditorialCreateViewModel model);
    Task<bool> UpdateAsync(int id, EditorialCreateViewModel model);
    Task<bool> DeleteAsync(int id);
}
