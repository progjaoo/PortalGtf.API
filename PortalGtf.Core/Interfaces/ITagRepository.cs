using PortalGtf.Core.Entities;

namespace PortalGtf.Core.Interfaces;

public interface ITagRepository
{
    Task<Tag?> GetBySlugAsync(string slug);
    Task AddAsync(Tag tag);
}