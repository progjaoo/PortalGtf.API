using PortalGtf.Core.Entities;
using PortalGtf.Core.Enums;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Application.Services.MidiaServices;

public class MidiaService : IMidiaService
{
    private readonly IMidiaRepository _repository;

    public MidiaService(IMidiaRepository repository)
    {
        _repository = repository;
    }

    public async Task<string> UploadAsync(
        Stream fileStream,
        string fileName,
        string contentType,
        int usuarioId)
    {
        var uploadsFolder = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            "uploads"
        );

        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var newFileName = $"{Guid.NewGuid()}_{fileName}";
        var filePath = Path.Combine(uploadsFolder, newFileName);

        using var stream = new FileStream(filePath, FileMode.Create);
        await fileStream.CopyToAsync(stream);

        var url = $"/uploads/{newFileName}";

        var tipo = contentType.StartsWith("video")
            ? TipoMidia.Video
            : TipoMidia.Imagem;

        var midia = new Midia
        {
            NomeOriginal = fileName,
            NomeArquivo = newFileName,
            Url = url,
            Tipo = tipo,
            DataUpload = DateTime.UtcNow,
            UsuarioUploadId = usuarioId
        };

        await _repository.AddAsync(midia);
        await _repository.SaveChangesAsync();

        return url;
    }

    public async Task<List<Midia>> GetPagedAsync(int page, int pageSize)
        => await _repository.GetPagedAsync(page, pageSize);
}