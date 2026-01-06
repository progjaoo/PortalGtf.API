using Microsoft.EntityFrameworkCore;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Infrastructure.Repositories;

public class PermissaoRepository : IPermissaoRepository
{
    private readonly PortalGtfNewsDbContext _dbContext;

    public PermissaoRepository(PortalGtfNewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Permissao>> GetAllPermissoesByUsuarioAsync(int usuarioId)
    {
        return await _dbContext.Usuario
            .Where(u => u.Id == usuarioId)
            .SelectMany(u => u.Funcao.FuncaoPermissoes)
            .Select(fp => fp.Permissao)
            .Distinct()
            .ToListAsync();
    }
    public async Task<List<Permissao>> GetAll()
    {
        return await _dbContext.Permissao.AsNoTracking().ToListAsync();
    }
    public async Task<Permissao> GetById(int id)
    {
        return await _dbContext.Permissao.SingleOrDefaultAsync(p =>  p.Id == id);

    }
    public async Task AddAsync(Permissao permissao)
    {
        await _dbContext.Permissao.AddAsync(permissao);
        await _dbContext.SaveChangesAsync();
    }
    public async Task UpdateAsync(Permissao permissao)
    {
        _dbContext.Permissao.Update(permissao);
        await _dbContext.SaveChangesAsync();
    }
    public Task DeleteAsync(Permissao permissao)
    {
        _dbContext.Permissao.Remove(permissao);
        return _dbContext.SaveChangesAsync();
    }
}