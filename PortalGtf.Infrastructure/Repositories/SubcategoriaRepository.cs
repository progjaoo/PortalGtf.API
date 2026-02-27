using Microsoft.EntityFrameworkCore;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Infrastructure.Repositories;

public class SubcategoriaRepository : ISubcategoriaRepository
{
    private readonly PortalGtfNewsDbContext _dbContext;

    public SubcategoriaRepository(PortalGtfNewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Subcategoria>> GetAllAsync()
        => await _dbContext.Subcategoria
            .Include(s => s.Editorial)
            .ToListAsync();

    public async Task<Subcategoria?> GetByIdAsync(int id)
        => await _dbContext.Subcategoria
            .Include(s => s.Editorial)
            .SingleOrDefaultAsync(s => s.Id == id);

    public async Task AddAsync(Subcategoria subcategoria)
        => await _dbContext.Subcategoria.AddAsync(subcategoria);

    public Task UpdateAsync(Subcategoria subcategoria)
    {
        _dbContext.Subcategoria.Update(subcategoria);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Subcategoria subcategoria)
    {
        _dbContext.Subcategoria.Remove(subcategoria);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
        => await _dbContext.SaveChangesAsync();
}