using Microsoft.EntityFrameworkCore;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Infrastructure.Repositories;
public class RegiaoRepository : IRegiaoRepository
{
    private readonly PortalGtfNewsDbContext _dbContext;

    public RegiaoRepository(PortalGtfNewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Regiao>> GetAllAsync()
    {
        return await _dbContext.Regiao
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Regiao?> GetByIdAsync(int id)
    {
        return await _dbContext.Regiao
            .Include(r => r.Cidades)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task AddAsync(Regiao regiao)
    {
        _dbContext.Regiao.Add(regiao);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Regiao regiao)
    {
        _dbContext.Regiao.Update(regiao);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Regiao regiao)
    {
        _dbContext.Regiao.Remove(regiao);
        await _dbContext.SaveChangesAsync();
    }
}