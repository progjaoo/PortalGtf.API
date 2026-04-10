using Microsoft.EntityFrameworkCore;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Infrastructure.Repositories;

public class BannerInstitucionalRepository : IBannerInstitucionalRepository
{
    private readonly PortalGtfNewsDbContext _dbContext;

    public BannerInstitucionalRepository(PortalGtfNewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    private IQueryable<BannerInstitucional> BaseQuery()
    {
        return _dbContext.BannerInstitucional
            .Include(b => b.Emissora)
            .Include(b => b.Midia)
            .OrderBy(b => b.Ordem)
            .ThenByDescending(b => b.DataCriacao);
    }

    public async Task<List<BannerInstitucional>> GetAllAsync()
    {
        return await BaseQuery().ToListAsync();
    }

    public async Task<List<BannerInstitucional>> GetAtivosPorEmissoraAsync(int emissoraId, string? posicao)
    {
        var query = BaseQuery()
            .Where(b => b.Ativo && b.EmissoraId == emissoraId);

        if (!string.IsNullOrWhiteSpace(posicao))
        {
            query = query.Where(b => b.Posicao == posicao);
        }

        return await query.ToListAsync();
    }

    public async Task<BannerInstitucional?> GetByIdAsync(int id)
    {
        return await BaseQuery().SingleOrDefaultAsync(b => b.Id == id);
    }

    public async Task AddAsync(BannerInstitucional banner)
    {
        await _dbContext.BannerInstitucional.AddAsync(banner);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(BannerInstitucional banner)
    {
        _dbContext.BannerInstitucional.Update(banner);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(BannerInstitucional banner)
    {
        _dbContext.BannerInstitucional.Remove(banner);
        await _dbContext.SaveChangesAsync();
    }
}
