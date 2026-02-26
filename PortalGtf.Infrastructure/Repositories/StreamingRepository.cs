using Microsoft.EntityFrameworkCore;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Infrastructure.Repositories;

public class StreamingRepository : IStreamingRepository
{
    public StreamingRepository(PortalGtfNewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private readonly PortalGtfNewsDbContext _dbContext;
    
    public async Task<List<Streaming>> GetAllAsync()
    {
        return await _dbContext.Streaming.ToListAsync();
    }

    public async Task<Streaming> GetByIdAsync(int id)
    {
        return await _dbContext.Streaming.SingleOrDefaultAsync(s => s.Id == id);
    }

    public async Task AddAsync(Streaming streaming)
    {
        await _dbContext.Streaming.AddAsync(streaming);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Streaming streaming)
    {
        _dbContext.Streaming.Update(streaming);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Streaming streaming)
    {
        _dbContext.Streaming.Remove(streaming);
        await _dbContext.SaveChangesAsync();
    }
}