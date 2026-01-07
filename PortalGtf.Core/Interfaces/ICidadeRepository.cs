using PortalGtf.Core.Entities;

namespace PortalGtf.Core.Interfaces;

public interface ICidadeRepository
{
    Task<List<Cidade>> GetAllAsync();
    Task<Cidade?> GetByIdAsync(int id);
    Task AddAsync(Cidade cidade);
    Task UpdateAsync(Cidade cidade);
    Task DeleteAsync(Cidade cidade);
}