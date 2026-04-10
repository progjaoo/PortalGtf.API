using Microsoft.EntityFrameworkCore;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Infrastructure.Repositories;

public class EditorialRepository : IEditorialRepository
{
    private readonly PortalGtfNewsDbContext _dbContext;
    
    public EditorialRepository(PortalGtfNewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Editorial>> GetAllAsync()
    {
        return await _dbContext.Editorial
            .AsNoTracking()
            .Include(e => e.Emissora)
            .ToListAsync();
    }

    public async Task<List<Editorial>> GetByEmissoraAsync(int emissoraId)
    {
        return await _dbContext.Editorial
            .AsNoTracking()
            .Include(e => e.Emissora)
            .Where(e => e.EmissoraId == emissoraId)
            .ToListAsync();
    }
    public async Task<Editorial?> GetByIdAsync(int id)
    {
        return await _dbContext.Editorial
            .Include(e => e.Emissora)
            .SingleOrDefaultAsync(e => e.Id == id); 
    }
    public async Task AddAsync(Editorial editorial)
    {
        await _dbContext.Editorial.AddAsync(editorial);
        await _dbContext.SaveChangesAsync();
    }
    public async Task UpdateAsync(Editorial editorial)
    {
        _dbContext.Editorial.Update(editorial);
        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteAsync(Editorial editorial)
    {
        _dbContext.Editorial.Remove(editorial);
        await _dbContext.SaveChangesAsync();
    }
}
