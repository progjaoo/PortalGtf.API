using PortalGtf.Core.Entities;

namespace PortalGtf.Core.Interfaces;

public interface ITemaEditorialRepository
{
    Task<IEnumerable<TemaEditorial>> GetAllAsync();
    Task<TemaEditorial?> GetByIdAsync(int id);
    Task AddAsync(TemaEditorial temaEditorial);
    Task UpdateAsync(TemaEditorial temaEditorial);
    Task DeleteAsync(TemaEditorial temaEditorial);
}