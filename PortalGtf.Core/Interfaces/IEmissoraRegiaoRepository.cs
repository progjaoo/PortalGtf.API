using PortalGtf.Core.Entities;

namespace PortalGtf.Core.Interfaces;

public interface IEmissoraRegiaoRepository
{
    Task<List<EmissoraRegiao>> GetAllAsync();
    Task<EmissoraRegiao?> GetByIdAsync(int id);
    Task AddAsync(EmissoraRegiao entity);
    Task DeleteAsync(EmissoraRegiao entity);
}