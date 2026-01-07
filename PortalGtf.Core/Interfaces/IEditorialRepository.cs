using PortalGtf.Core.Entities;

namespace PortalGtf.Core.Interfaces;

public interface IEditorialRepository
{
    Task<List<Editorial>> GetAllAsync();
    Task<Editorial?> GetByIdAsync(int id);
    Task AddAsync(Editorial editorial);
    Task UpdateAsync(Editorial editorial);
    Task DeleteAsync(Editorial editorial);
}