using PortalGtf.Core.Entities;

namespace PortalGtf.Application.Services.MidiaServices;

public interface IMidiaService
{
    Task<string> UploadAsync(Stream fileStream, string fileName, string contentType, int usuarioId);
    Task<List<Midia>> GetPagedAsync(int page, int pageSize);
}