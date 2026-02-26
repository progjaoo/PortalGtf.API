using Microsoft.EntityFrameworkCore;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Interfaces;

public class TagRepository : ITagRepository
{
    private readonly PortalGtfNewsDbContext _context;

    public TagRepository(PortalGtfNewsDbContext context)
    {
        _context = context;
    }
    public async Task<Tag?> GetBySlugAsync(string slug)
    {
        return await _context.Tag
            .SingleOrDefaultAsync(t => t.Slug == slug);
    }

    public async Task AddAsync(Tag tag)
    {
        await _context.Tag.AddAsync(tag);
        await _context.SaveChangesAsync();
    }
}