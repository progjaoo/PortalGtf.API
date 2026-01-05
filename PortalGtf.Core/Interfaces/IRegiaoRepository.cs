using PortalGtf.Core.Entities;

namespace PortalGtf.Core.Interfaces;

public interface IRegiaoRepository
{
    Task<List<Regiao>> GetAllAsync();
    Task<Regiao?> GetByIdAsync(int id);
    Task AddAsync(Regiao regiao);
    Task UpdateAsync(Regiao regiao);
    Task DeleteAsync(Regiao regiao);
}