using PortalGtf.Application.ViewModels.BannerInstitucionalVM;

namespace PortalGtf.Application.Services.BannerInstitucionalServices;

public interface IBannerInstitucionalService
{
    Task<List<BannerInstitucionalViewModel>> GetAllAsync();
    Task<List<BannerInstitucionalViewModel>> GetAtivosPorEmissoraAsync(int emissoraId, string? posicao);
    Task<BannerInstitucionalViewModel?> GetByIdAsync(int id);
    Task<int> CreateAsync(BannerInstitucionalCreateViewModel model);
    Task<bool> UpdateAsync(int id, BannerInstitucionalCreateViewModel model);
    Task<bool> DeleteAsync(int id);
}
