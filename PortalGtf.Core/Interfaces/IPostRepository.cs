using PortalGtf.Core.Entities;
using PortalGtf.Core.Enums;

namespace PortalGtf.Core.Interfaces;

public interface IPostRepository
{
    Task<List<Post>> GetAllAsync();
    Task<Post?> GetByIdAsync(int id);
    Task<List<Post>> GetByEditorialAsync(int editorialId);
    Task<(List<Post> Posts, int TotalCount)> GetAllByRegiaoAsync(int regiaoId, int page, int pageSize);
    Task<List<Post>> GetBySubcategoryAsync(int subcategoryId); 
    Task<List<Post>> GetAllByRecent();
    Task<List<Post>> GetAllPostByEmissora(int emissoraId);
    Task<List<Post>> GetByUserAsync(string userName);
    Task<List<Post>> GetByStatusAsync(StatusPost status);
    Task<List<Post>> SearchAsync(string query);
    Task AddAsync(Post post);
    Task UpdateAsync(Post post);
    Task DeleteAsync(Post post);
    Task SaveChangesAsync();
}
