using PortalGtf.Core.Entities;

namespace PortalGtf.Core.Interfaces;

public interface IBannerInstitucionalRepository
{
    Task<List<BannerInstitucional>> GetAllAsync();
    Task<List<BannerInstitucional>> GetAtivosPorEmissoraAsync(int emissoraId, string? posicao);
    Task<BannerInstitucional?> GetByIdAsync(int id);
    Task AddAsync(BannerInstitucional banner);
    Task UpdateAsync(BannerInstitucional banner);
    Task DeleteAsync(BannerInstitucional banner);
}
