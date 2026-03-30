using PortalGtf.Core.Entities;
using PortalGtf.Core.Enums;

namespace PortalGtf.Core.Interfaces;

public interface IMidiaRepository
{
    Task AddAsync(Midia midia);
    Task<Midia?> GetByIdAsync(int id);
    Task<List<Midia>> GetPagedAsync(int page, int pageSize);
    Task<int> GetTotalCountAsync();   
    Task<List<Midia>> GetByTipoAsync(TipoMidia tipo);
    Task<bool> ExistsAsync(int id);
    Task DeleteAsync(Midia midia);
    Task SaveChangesAsync();

}