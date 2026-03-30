using Microsoft.Extensions.Configuration;
using PortalGtf.Application.ViewModels.MidiaVM;
using PortalGtf.Application.ViewModels.PostsVM;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Enums;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Application.Services.MidiaServices;

public class MidiaService : IMidiaService
{
    private readonly IMidiaRepository _repository;
    private readonly IConfiguration _config;

    public MidiaService(IMidiaRepository repository, IConfiguration config)
    {
        _repository = repository;
        _config = config;
    }

    public async Task<MidiaDto> UploadAsync(
        Stream fileStream,
        string fileName,
        string contentType,
        int usuarioId)
    {
        var uploadsFolder = Path.Combine(
            Directory.GetCurrentDirectory(), "wwwroot", "uploads");

        Directory.CreateDirectory(uploadsFolder);

        var newFileName = $"{Guid.NewGuid()}_{SanitizeFileName(fileName)}";
        var filePath = Path.Combine(uploadsFolder, newFileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await fileStream.CopyToAsync(stream);

        var baseUrl = _config["App:BaseUrl"]?.TrimEnd('/')
            ?? throw new InvalidOperationException("App:BaseUrl não configurado");

        var url = $"{baseUrl}/uploads/{newFileName}";

        var tipo = contentType.StartsWith("video", StringComparison.OrdinalIgnoreCase)
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

        return new MidiaDto
        {
            Id = midia.Id,
            NomeOriginal = fileName,
            Url = url,
            Tipo = tipo,
            DataUpload = midia.DataUpload
        };
    }
    public async Task<MidiaDownloadViewModel> DownloadAsync(int id)
    {
        var midia = await _repository.GetByIdAsync(id);
        if (midia == null)
            throw new KeyNotFoundException("Mídia não encontrada");

        var uploadsFolder = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            "uploads");

        var filePath = Path.Combine(uploadsFolder, midia.NomeArquivo);

        if (!File.Exists(filePath))
            throw new FileNotFoundException("Arquivo não encontrado no servidor");

        var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

        var contentType = GetContentType(midia.NomeArquivo);

        return new MidiaDownloadViewModel
        {
            Stream = stream,
            ContentType = contentType,
            FileName = midia.NomeArquivo
        };
    }
    private string GetContentType(string fileName)
    {
        var ext = Path.GetExtension(fileName).ToLowerInvariant();

        return ext switch
        {
            ".jpg" => "image/jpeg",
            ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".webp" => "image/webp",
            ".gif" => "image/gif",
            ".mp4" => "video/mp4",
            ".webm" => "video/webm",
            _ => "application/octet-stream"
        };
    }
    public async Task<PagedResult<MidiaDto>> GetPagedAsync(int page, int pageSize)
    {
        var midias = await _repository.GetPagedAsync(page, pageSize);
        var total = await _repository.GetTotalCountAsync();

        var dtos = midias.Select(m => new MidiaDto
        {
            Id = m.Id,
            NomeOriginal = m.NomeOriginal,
            Url = m.Url,
            Tipo = m.Tipo,
            DataUpload = m.DataUpload,
            ThumbnailUrl = m.Tipo == TipoMidia.Imagem ? m.Url : null
        }).ToList();

        return new PagedResult<MidiaDto>(dtos, total, page, pageSize);
    }
    
    public async Task DeleteAsync(int id)
    {
        var midia = await _repository.GetByIdAsync(id);

        if (midia == null)
            throw new KeyNotFoundException("Mídia não encontrada.");

        // Remove o arquivo físico da VPS
        var filePath = Path.Combine(
            Directory.GetCurrentDirectory(), "wwwroot", "uploads", midia.NomeArquivo);

        if (File.Exists(filePath))
            File.Delete(filePath);

        await _repository.DeleteAsync(midia);
        await _repository.SaveChangesAsync();
    }
    private static string SanitizeFileName(string name)
        => string.Concat(name.Split(Path.GetInvalidFileNameChars()));
    private TipoMidia DetectTipo(string contentType)
    {
        if (contentType.StartsWith("image"))
            return TipoMidia.Imagem;

        if (contentType.StartsWith("video"))
            return TipoMidia.Video;

        return TipoMidia.Video;
    }
}