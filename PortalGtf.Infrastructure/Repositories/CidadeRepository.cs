using Microsoft.EntityFrameworkCore;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Infrastructure.Repositories;

public class CidadeRepository : ICidadeRepository
{
    private readonly PortalGtfNewsDbContext _dbContext;

    public CidadeRepository(PortalGtfNewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Cidade>> GetAllAsync()
    {
        return await _dbContext.Cidade.AsNoTracking()
            .Include(c => c.Estado)
            .Include(c => c.Regiao)
            .ToListAsync();
    }

    public async Task<Cidade?> GetByIdAsync(int id)
    {
        return await _dbContext.Cidade.AsNoTracking()
            .Include(c => c.Estado)
            .Include(c => c.Regiao)
            .SingleOrDefaultAsync(c => c.Id == id);
    }
    public async Task AddAsync(Cidade cidade)
    {
        await _dbContext.Cidade.AddAsync(cidade);
        await _dbContext.SaveChangesAsync();
    }
    public async Task UpdateAsync(Cidade cidade)
    {
        _dbContext.Cidade.Update(cidade);
        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteAsync(Cidade cidade)
    {
        _dbContext.Cidade.Remove(cidade);
        await _dbContext.SaveChangesAsync();
    }
}