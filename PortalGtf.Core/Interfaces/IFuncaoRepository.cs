using PortalGtf.Core.Entities;

namespace PortalGtf.Core.Interfaces;

public interface IFuncaoRepository
{
    Task<List<Funcao>> GetAllAsync();
    Task<Funcao?> GetByIdAsync(int id);
    Task AddAsync(Funcao funcao);
    Task UpdateAsync(Funcao funcao);
    Task DeleteAsync(Funcao funcao);
}