using PortalGtf.Core.Entities;

namespace PortalGtf.Core.Interfaces;

public interface ISubcategoriaRepository
{
    Task<List<Subcategoria>> GetAllAsync();
    Task<Subcategoria?> GetByIdAsync(int id);
    Task AddAsync(Subcategoria subcategoria);
    Task UpdateAsync(Subcategoria subcategoria);
    Task DeleteAsync(Subcategoria subcategoria);
    Task SaveChangesAsync();
}