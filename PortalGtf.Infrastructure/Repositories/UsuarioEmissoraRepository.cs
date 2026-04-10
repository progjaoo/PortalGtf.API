using Microsoft.EntityFrameworkCore;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Infrastructure.Repositories;

public class UsuarioEmissoraRepository : IUsuarioEmissoraRepository
{
    private readonly PortalGtfNewsDbContext _dbContext;

    public UsuarioEmissoraRepository(PortalGtfNewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<UsuarioEmissora>> GetAllAsync()
    {
        return await _dbContext.UsuarioEmissora
            .Include(ue => ue.Usuario)
            .Include(ue => ue.Emissora)
            .Include(ue => ue.Funcao)
            .AsNoTracking()
            .ToListAsync();
    }
    public async Task<UsuarioEmissora?> GetByIdAsync(int id)
    {
        return await _dbContext.UsuarioEmissora
            .Include(ue => ue.Usuario)
            .Include(ue => ue.Emissora)
            .Include(ue => ue.Funcao)
            .AsNoTracking()
            .SingleOrDefaultAsync(ue => ue.Id == id);
    }
    public async Task AddAsync(UsuarioEmissora usuarioEmissora)
    {
        await _dbContext.UsuarioEmissora.AddAsync(usuarioEmissora);
        await _dbContext.SaveChangesAsync();
    }
    public async Task UpdateAsync(UsuarioEmissora usuarioEmissora)
    {
        _dbContext.UsuarioEmissora.Update(usuarioEmissora);
        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteAsync(UsuarioEmissora usuarioEmissora)
    {
        _dbContext.UsuarioEmissora.Remove(usuarioEmissora);
        await _dbContext.SaveChangesAsync();
    }
}
