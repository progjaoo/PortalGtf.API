using PortalGtf.Application.ViewModels.MidiaVM;
using PortalGtf.Application.ViewModels.PostsVM;
using PortalGtf.Core.Entities;

namespace PortalGtf.Application.Services.MidiaServices;

public interface IMidiaService
{
    Task<MidiaDto> UploadAsync(
        Stream fileStream,
        string fileName,
        string contentType,
        int usuarioId);    

    Task<PagedResult<MidiaDto>> GetPagedAsync(int page, int pageSize);
    Task<MidiaDownloadViewModel> DownloadAsync(int id);
    Task DeleteAsync(int id);
}