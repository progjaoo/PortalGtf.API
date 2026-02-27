using Microsoft.EntityFrameworkCore;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Enums;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Infrastructure.Repositories;

public class MidiaRepository : IMidiaRepository
{
    private readonly PortalGtfNewsDbContext _context;
    public MidiaRepository(PortalGtfNewsDbContext context)
    {
        _context = context;
    }
    public async Task AddAsync(Midia midia)
        => await _context.Midia.AddAsync(midia);

    public async Task<Midia?> GetByIdAsync(int id)
        => await _context.Midia.FindAsync(id);

    public async Task<List<Midia>> GetPagedAsync(int page, int pageSize)
        => await _context.Midia
            .OrderByDescending(m => m.DataUpload)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

    public async Task<List<Midia>> GetByTipoAsync(TipoMidia tipo)
        => await _context.Midia
            .Where(m => m.Tipo == tipo)
            .OrderByDescending(m => m.DataUpload)
            .ToListAsync();

    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}