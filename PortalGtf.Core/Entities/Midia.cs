using PortalGtf.Core.Enums;

namespace PortalGtf.Core.Entities;

public class Midia
{
    public int Id { get; set; }
    public string NomeOriginal { get; set; } = null!;
    public string NomeArquivo { get; set; } = null!;
    public string Url { get; set; } = null!;
    public TipoMidia Tipo { get; set; }
    public DateTime DataUpload { get; set; }
    public int UsuarioUploadId { get; set; }

    public ICollection<PostImagem> PostImagens { get; set; } = new List<PostImagem>();
}