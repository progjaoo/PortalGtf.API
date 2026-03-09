using Microsoft.EntityFrameworkCore;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Infrastructure.Repositories;

public class ProgramacaoRadiorRepository : IProgramacaoRadiorRepository
{
    private readonly PortalGtfNewsDbContext _dbContext;
    public ProgramacaoRadiorRepository(PortalGtfNewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<ProgramacaoRadio>> GetAllAsync()
    {
        return await _dbContext.ProgramacaoRadio.OrderBy(p => p.HoraInicio).ToListAsync();
    }
    public async Task<ProgramacaoRadio> GetByIdAsync(int id)
    {
        return await _dbContext.ProgramacaoRadio.SingleOrDefaultAsync(p => p.Id == id); 
    }
    public async Task AddAsync(ProgramacaoRadio programacaoRadio)
    {
        await _dbContext.ProgramacaoRadio.AddAsync(programacaoRadio);
        await _dbContext.SaveChangesAsync();
    }
    public async Task UpdateAsync(ProgramacaoRadio programacaoRadio)
    {
        _dbContext.ProgramacaoRadio.Update(programacaoRadio);
        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        _dbContext.ProgramacaoRadio.Remove(await _dbContext.ProgramacaoRadio.SingleOrDefaultAsync(p => p.Id == id));
        await _dbContext.SaveChangesAsync();
    }
}