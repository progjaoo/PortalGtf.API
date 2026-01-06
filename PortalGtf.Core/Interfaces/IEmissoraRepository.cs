using PortalGtf.Core.Entities;

namespace PortalGtf.Core.Interfaces;

public interface IEmissoraRepository
{
    Task<List<Emissora>> GetAllAsync();
    Task<Emissora?> GetByIdAsync(int id);
    Task AddAsync(Emissora emissora);
    Task UpdateAsync(Emissora emissora);
    Task DeleteAsync(Emissora emissora);
}