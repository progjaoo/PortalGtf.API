using PortalGtf.Core.Entities;
using PortalGtf.Core.Enums;

namespace PortalGtf.Core.Interfaces;

public interface IPostRepository
{
    Task<List<Post>> GetAllAsync();
    Task<Post?> GetByIdAsync(int id);
    Task<List<Post>> GetByEditorialAsync(int editorialId);
    Task<List<Post>> GetByUserAsync(string userName);
    Task<List<Post>> GetByStatusAsync(StatusPost status);
    Task<List<Post>> SearchAsync(string query);
    Task AddAsync(Post post);
    Task UpdateAsync(Post post);
    Task DeleteAsync(Post post);
    Task SaveChangesAsync();
}