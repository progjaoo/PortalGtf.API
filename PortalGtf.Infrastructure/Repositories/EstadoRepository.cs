using Microsoft.EntityFrameworkCore;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Infrastructure.Repositories;

public class EstadoRepository : IEstadoRepository
{
    private readonly PortalGtfNewsDbContext _dbContext;

    public EstadoRepository(PortalGtfNewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Estado>> GetAllAsync()
    {
        return await _dbContext.Estado
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Estado?> GetByIdAsync(int id)
    {
        return await _dbContext.Estado
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.Id == id);
    }

    public async Task AddAsync(Estado estado)
    {
        await _dbContext.Estado.AddAsync(estado);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Estado estado)
    {
        _dbContext.Estado.Update(estado);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Estado estado)
    {
        _dbContext.Estado.Remove(estado);
        await _dbContext.SaveChangesAsync();
    }
}