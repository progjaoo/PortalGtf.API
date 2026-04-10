using PortalGtf.Application.ViewModels.PostsVM;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Enums;

namespace PortalGtf.Application.Services.PostServices;

public interface IPostService 
{
    Task<List<PostListViewModel>> GetAllAsync();
    Task<List<PostListViewModel>> GetAllByRecent();
    Task<PagedResult<PostResumeViewModel>> GetPostsByRegiaoAsync(int regiaoId, int page, int pageSize);
    Task<PostDetailViewModel?> GetByIdAsync(int id);
    Task<List<PostListViewModel>> GetByEditorialAsync(int editorialId);
    Task<List<PostListViewModel>> GetBySubcategoryAsync(int subcategoryId); 
    Task<List<PostListViewModel>> GetByUserAsync(string userName);
    Task<List<PostListViewModel>> GetAllPostsByEmissora(int emissoraId);
    Task<List<PostListViewModel>> GetByStatusAsync(StatusPost status);
    Task<List<PostPublicViewModel>> GetAllPublishedAsync();
    Task<List<PostPublicViewModel>> SearchAsync(string query);
    Task<List<PostPublicViewModel>> GetFilteredAsync(PostEnumViewModel filter);
    Task<List<PostPublicViewModel>> GetMostReadAsync(int? emissoraId, int limit, int days);
    Task<List<PostListViewModel>> GetDestaquesAsync();
    Task<List<PostListViewModel>> GetDestaquesbByFatoPopularAsync();
    Task<List<PostListViewModel>> GetDestaquesbBy88FmAsync();
    Task SetDestaqueAsync(int postId, bool destaque);
    Task<List<PostListViewModel>> GetAllPostsByStatusRascunho();
    Task<List<PostListViewModel>> GetAllPostsByStatusRevisao();
    //TODO: PARTE DE SEO
    Task<PostDetailViewModel?> GetBySlugAsync(string slug);
    Task<string> GenerateSitemapAsync();
    Task RegisterViewAsync(int postId, string? ip);
    Task EnviarParaRevisaoAsync(int postId, PostEnviarRevisaoViewModel model);
    Task AprovarPostAsync(int postId);
    Task RejeitarPostAsync(int postId);
    Task EnviarParaAprovacao(int postId);
    Task CreateAsync(PostCreateViewModel model);
    Task UpdateAsync(int id, PostUpdateViewModel model);
    Task DeleteAsync(int id);

    // Task<string> UploadImagemAsync(int postId, Stream fileStream,string fileName, string contentType);
}
