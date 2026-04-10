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
    Task<List<Post>> GetFilteredAsync(FiltroPostEnum filterData, OrdenaPostEnum ordenaPost);
    Task<List<Post>> GetDestaquesAsync();
    Task<List<Post>> GetDestaquesByFatoPopularAsync();
    Task<List<Post>> GetDestaquesBy88FmAsync();
    Task<List<Post>> GetMostReadAsync(int? emissoraId, int limit, int days);
    Task<List<Post>> GetAllPostsByStatusRascunho();
    Task<List<Post>> GetAllPostsByStatusRevisao();
    Task SetDestaqueAsync(int postId, bool destaque);
    Task AddViewAsync(int postId, string? ip);
    Task AddHistoricoAsync(PostHistorico historico);
    //TODO: PARTE DE SEO
    Task<Post?> GetBySlugAsync(string slug);
    Task<List<Post>> GetAllPublishedForSitemapAsync(); // NÃO ESTÁ IMPLEMENTADO...
    Task AddAsync(Post post);
    Task UpdateAsync(Post post);
    Task DeleteAsync(Post post);
    Task SaveChangesAsync();
}
