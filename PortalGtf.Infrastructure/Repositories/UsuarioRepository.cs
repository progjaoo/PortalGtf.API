using Microsoft.EntityFrameworkCore;
using PortalGtf.Core;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly PortalGtfNewsDbContext _context;

    public UsuarioRepository(PortalGtfNewsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Usuario>> GetAllAsync()
    {
        return await _context.Usuario
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Usuario?> GetByIdAsync(int id)
    {
        return await _context.Usuario
            .AsNoTracking()
            .Include(u => u.Funcao)
            .SingleOrDefaultAsync(u => u.Id == id);
    }
    public async Task AddAsync(Usuario usuario)
    {
        _context.Usuario.Add(usuario);
        await _context.SaveChangesAsync();
    }
    public async Task<Usuario?> GetByEmailAndPasswordAsync(string email, string senhaHash)
    {
        return await _context.Usuario
            .Include(u => u.Funcao)
            .FirstOrDefaultAsync(u =>
                u.Email == email &&
                u.SenhaHash == senhaHash);
    }

    public async Task UpdateAsync(Usuario usuario)
    {
        _context.Usuario.Update(usuario);
        await _context.SaveChangesAsync();
    }
}