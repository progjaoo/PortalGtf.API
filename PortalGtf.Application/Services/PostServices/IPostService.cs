using PortalGtf.Application.ViewModels.PostsVM;
using PortalGtf.Core.Enums;

namespace PortalGtf.Application.Services.PostServices;

public interface IPostService 
{
    Task<List<PostListViewModel>> GetAllAsync();
    Task<PostDetailViewModel?> GetByIdAsync(int id);
    Task<List<PostListViewModel>> GetByEditorialAsync(int editorialId);
    Task<List<PostListViewModel>> GetByUserAsync(string userName);
    Task<List<PostListViewModel>> GetByStatusAsync(StatusPost status);
    Task<List<PostPublicViewModel>> GetAllPublishedAsync();
    Task<List<PostPublicViewModel>> SearchAsync(string query);
    Task EnviarParaRevisaoAsync(int postId);
    Task AprovarPostAsync(int postId);
    Task RejeitarPostAsync(int postId);
    Task CreateAsync(PostCreateViewModel model);
    Task UpdateAsync(int id, PostUpdateViewModel model);
    Task DeleteAsync(int id);

    Task<string> UploadImagemAsync(int postId, Stream fileStream,string fileName, string contentType);
}