using Microsoft.EntityFrameworkCore;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Infrastructure.Repositories;

public class FuncaoRepository : IFuncaoRepository
{
    private readonly PortalGtfNewsDbContext  _dbContext;

    public FuncaoRepository(PortalGtfNewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Funcao>> GetAllAsync()
    {
        return await _dbContext.Funcao.AsNoTracking().ToListAsync();
    }
    public async Task<Funcao?> GetByIdAsync(int id)
    { 
        return await _dbContext.Funcao.SingleOrDefaultAsync(f => f.Id == id);
    }
    public async Task AddAsync(Funcao funcao)
    {
        await _dbContext.Funcao.AddAsync(funcao);
        await _dbContext.SaveChangesAsync();
    }
    public async Task UpdateAsync(Funcao funcao)
    {
        _dbContext.Funcao.Update(funcao);
        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteAsync(Funcao funcao)
    {
        _dbContext.Funcao.Remove(funcao);
        await _dbContext.SaveChangesAsync();
    }
}