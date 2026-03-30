namespace PortalGtf.Application.ViewModels.PostsVM;

public class CriarPostRequest
{
    public string Titulo { get; set; }
    public string Subtitulo { get; set; }
    public string Conteudo { get; set; }
    public string Slug { get; set; }
    public int EditorialId { get; set; }
    public int EmissoraId { get; set; }
    public int CidadeId { get; set; }
    public int SubcategoriaId { get; set; }
    public List<PostMidiaInput> Midias { get; set; } = new();
    public List<string> Tags { get; set; } = new();
}
public class PostMidiaInput
{
    public int MidiaId { get; set; }
    public int Ordem { get; set; }
}