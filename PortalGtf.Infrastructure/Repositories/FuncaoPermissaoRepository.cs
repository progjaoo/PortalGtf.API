using Microsoft.EntityFrameworkCore;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Infrastructure.Repositories;

public class FuncaoPermissaoRepository : IFuncaoPermissaoRepository
{
    private readonly PortalGtfNewsDbContext _dbContext;

    public FuncaoPermissaoRepository(PortalGtfNewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<FuncaoPermissao>> GetAllAsync()
    {
        return await _dbContext.FuncaoPermissao
            .Include(fp => fp.Funcao)
            .Include(fp => fp.Permissao)
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<FuncaoPermissao?> GetByIdAsync(int id)
    {
        return await _dbContext.FuncaoPermissao
            .Include(fp => fp.Funcao)
            .Include(fp => fp.Permissao)
            .SingleOrDefaultAsync(fp => fp.Id == id);
    }
    public async Task AddAsync(FuncaoPermissao entity)
    {
        await _dbContext.FuncaoPermissao.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteAsync(FuncaoPermissao entity)
    {
        _dbContext.FuncaoPermissao.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}