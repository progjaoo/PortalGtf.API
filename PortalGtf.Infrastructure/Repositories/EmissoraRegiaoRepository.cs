using Microsoft.EntityFrameworkCore;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Infrastructure.Repositories;

public class EmissoraRegiaoRepository : IEmissoraRegiaoRepository
{
    private readonly PortalGtfNewsDbContext _dbContext;

    public EmissoraRegiaoRepository(PortalGtfNewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<EmissoraRegiao>> GetAllAsync()
    {
        return await _dbContext.EmissoraRegiao
            .AsNoTracking()
            .Include(e => e.Regiao)
            .Include(e => e.Emissora)
            .ToListAsync();
    }
    public async Task<EmissoraRegiao?> GetByIdAsync(int id)
    {
        return await _dbContext.EmissoraRegiao
            .AsNoTracking()
            .Include(e => e.Regiao)
            .Include(e => e.Emissora)
            .SingleOrDefaultAsync(er => er.Id == id);
    }
    public async Task AddAsync(EmissoraRegiao emissoraRegiao)
    {
        await _dbContext.EmissoraRegiao.AddAsync(emissoraRegiao);
        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteAsync(EmissoraRegiao emissoraRegiao)
    {
        _dbContext.EmissoraRegiao.Remove(emissoraRegiao);
        await _dbContext.SaveChangesAsync();
    }
}