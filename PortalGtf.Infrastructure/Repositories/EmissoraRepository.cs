using Microsoft.EntityFrameworkCore;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Infrastructure.Repositories;

public class EmissoraRepository : IEmissoraRepository
{
    private readonly PortalGtfNewsDbContext _dbContext;

    public EmissoraRepository(PortalGtfNewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Emissora>> GetAllAsync()
    {
        return await _dbContext.Emissora.AsNoTracking().ToListAsync();
    }

    public async Task<Emissora?> GetByIdAsync(int id)
    {
        return await _dbContext.Emissora.
            AsNoTracking().
            SingleOrDefaultAsync(e => e.Id == id);
    }

    public async Task AddAsync(Emissora emissora)
    {
        await _dbContext.Emissora.AddAsync(emissora);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Emissora emissora)
    {
        _dbContext.Emissora.Update(emissora);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Emissora emissora)
    {
        _dbContext.Emissora.Remove(emissora);
        await _dbContext.SaveChangesAsync();
    }
}