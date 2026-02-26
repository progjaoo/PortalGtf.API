using PortalGtf.Core.Entities;

namespace PortalGtf.Core.Interfaces;

public interface IStreamingRepository
{
    Task<List<Streaming>> GetAllAsync();
    Task<Streaming> GetByIdAsync(int id);
    Task AddAsync(Streaming streaming);
    Task UpdateAsync(Streaming streaming);
    Task DeleteAsync(Streaming streaming);
}
    