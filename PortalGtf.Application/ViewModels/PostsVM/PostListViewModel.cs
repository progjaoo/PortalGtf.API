namespace PortalGtf.Application.ViewModels.PostsVM;

public class PostListViewModel
{
    public int Id { get; set; }
    public string Titulo { get; set; } = null!;
    public string? Subtitulo { get; set; }
    public string Conteudo { get; set; } = null!;
    public string Editorial { get; set; } = null!;
    public string Emissora { get; set; } = null!;
    public string? EmissoraSlug { get; set; }
    public string? ImagemCapaUrl { get; set; }
    public int? ImagemCapaId { get; set; }
    public string Status { get; set; } = null!;
    public int StatusPost { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? PublicadoEm { get; set; }
    public int UsuarioCriacaoId { get; set; }
    public string UsuarioCriacao { get; set; }
    public string Cidade { get; set; } = null!;
    public string? MensagemRevisao { get; set; }
    public DateTime? DataMensagemRevisao { get; set; }
    public string? UsuarioMensagemRevisao { get; set; }
    
    //nova prop
    public List<PostImagemViewModel> Midias { get; set; } = new();
}
public class PostImagemViewModel
{
    public int MidiaId { get; set; }
    public string Url { get; set; } = null!;
    public int Ordem { get; set; }
    public string Tipo { get; set; } = null!;
}
