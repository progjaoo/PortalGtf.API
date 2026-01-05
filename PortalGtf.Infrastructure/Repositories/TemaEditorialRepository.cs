using Microsoft.EntityFrameworkCore;
using PortalGtf.Core;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Infrastructure.Repositories;

public class TemaEditorialRepository : ITemaEditorialRepository
{
    private readonly PortalGtfNewsDbContext _context;

    public TemaEditorialRepository(PortalGtfNewsDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TemaEditorial>> GetAllAsync()
    {
        return await _context.TemaEditorial
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<TemaEditorial?> GetByIdAsync(int id)
    {
        return await _context.TemaEditorial
            .Include(t => t.Editoriais)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task AddAsync(TemaEditorial temaEditorial)
    {
        _context.TemaEditorial.Add(temaEditorial);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TemaEditorial temaEditorial)
    {
        _context.TemaEditorial.Update(temaEditorial);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TemaEditorial temaEditorial)
    {
        _context.TemaEditorial.Remove(temaEditorial);
        await _context.SaveChangesAsync();
    }
}