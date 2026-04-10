using System.Text;
using PortalGtf.Application.ViewModels.PostsVM;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Enums;
using PortalGtf.Core.Interfaces;
using PostResumeViewModel = PortalGtf.Application.ViewModels.PostsVM.PostResumeViewModel;

namespace PortalGtf.Application.Services.PostServices;
public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly IMidiaRepository _midiaRepository;
    private readonly ITagRepository _tagRepository;
    private readonly ISubcategoriaRepository _subcategoriaRepository;
    private readonly ICidadeRepository _cidadeRepository;
    private readonly IEmissoraRepository _emissoraRepository;
    private readonly IEditorialRepository _editorialRepository;
    public PostService(
        IPostRepository postRepository,
        ITagRepository tagRepository,
        IMidiaRepository midiaRepository,
        ISubcategoriaRepository subcategoriaRepository,
        ICidadeRepository cidadeRepository,
        IEmissoraRepository emissoraRepository,
        IEditorialRepository editorialRepository)
    {
        _postRepository = postRepository;
        _tagRepository = tagRepository;
        _midiaRepository = midiaRepository;
        _subcategoriaRepository = subcategoriaRepository;
        _cidadeRepository = cidadeRepository;
        _emissoraRepository = emissoraRepository;
        _editorialRepository = editorialRepository;
    }

    private static PostHistorico? GetUltimoHistoricoRevisao(Post post)
    {
        return post.Historicos?
            .Where(h => h.Acao == "ENVIADO_PARA_REVISAO")
            .OrderByDescending(h => h.DataAcao)
            .FirstOrDefault();
    }

    private static PostListViewModel MapToListVM(Post p)
    {
        var ultimaRevisao = GetUltimoHistoricoRevisao(p);

        return new PostListViewModel
        {
            Id = p.Id,
            Titulo = p.Titulo,
            Subtitulo = p.Subtitulo,
            Conteudo = p.Conteudo,
            Editorial = p.Editorial?.TipoPostagem,
            Emissora = p.Emissora?.NomeSocial,
            EmissoraSlug = p.Emissora?.Slug,
            ImagemCapaId = p.ImagemCapaId,
            ImagemCapaUrl = p.ImagemCapa?.Url,
            Status = p.StatusPost.ToString(),
            StatusPost = (int)p.StatusPost,
            DataCriacao = p.DataCriacao,
            PublicadoEm = p.PublicadoEm,
            UsuarioCriacao = p.UsuarioCriacao?.NomeCompleto ?? "",
            UsuarioCriacaoId = p.UsuarioCriacaoId,
            Cidade = p.Cidade?.Nome ?? "",
            MensagemRevisao = ultimaRevisao?.Mensagem,
            DataMensagemRevisao = ultimaRevisao?.DataAcao,
            UsuarioMensagemRevisao = ultimaRevisao?.Usuario?.NomeCompleto,
            Midias = p.Imagens?.Select(i => new PostImagemViewModel
            {
                MidiaId = i.MidiaId,
                Url = i.Midia?.Url ?? "", 
                Ordem = i.Ordem,
                Tipo = i.Tipo.ToString()
            }).ToList() ?? new List<PostImagemViewModel>()
        };
    }
    private static PostPublicViewModel MapToPublicVM(Post p)
    {
        return new PostPublicViewModel
        {
            Id = p.Id,
            Titulo = p.Titulo,
            Conteudo = p.Conteudo,
            Subtitulo = p.Subtitulo,
            ImagemCapaId = p.ImagemCapaId,
            ImagemCapaUrl = p.ImagemCapa?.Url,
            Slug = p.Slug,
            PublicadoEm = p.PublicadoEm,
            Editorial = p.Editorial?.TipoPostagem,
            CorTema = p.Editorial?.TemaEditorial?.CorPrimaria,
            Emissora = p.Emissora?.NomeSocial,
            EmissoraSlug = p.Emissora?.Slug,
            UsuarioCriacao = p.UsuarioCriacao?.NomeCompleto ?? "",
            UsuarioCriacaoId = p.UsuarioCriacaoId,
            Cidade = p.Cidade?.Nome ?? "",
            TotalVisualizacoes = p.Visualizacoes?.Count ?? 0,
            Midias = p.Imagens?.Select(i => new PostImagemViewModel
            {
                MidiaId = i.MidiaId,
                Url = i.Midia?.Url ?? "", 
                Ordem = i.Ordem,
                Tipo = i.Tipo.ToString()
            }).ToList() ?? new List<PostImagemViewModel>()
        };
    }

    #region QUERIES PRINCIPAIS - CONSULTAS DO SISTEMA
    // QUERIES PRINCIPAIS
    public async Task<PagedResult<PostResumeViewModel>> GetPostsByRegiaoAsync(int regiaoId, int page, int pageSize)
    {
        if (page <= 0) page = 1;
        if (pageSize <= 0 || pageSize > 50) pageSize = 10;

        var (posts, totalCount) = await _postRepository.GetAllByRegiaoAsync(regiaoId, page, pageSize);

        var result = posts.Select(p => new PostResumeViewModel
        {
            Id = p.Id,
            Titulo = p.Titulo,
            Subtitulo = p.Subtitulo,
            Slug = p.Slug,
            PublicadoEm = p.PublicadoEm,
            ImagemCapaId = p.ImagemCapaId,
            ImagemCapaUrl = p.ImagemCapa?.Url,
            Editorial = p.Editorial?.TipoPostagem, 
            CorTema = p.Editorial?.TemaEditorial?.CorPrimaria, 
            UsuarioCriacaoId = p.UsuarioCriacaoId,
            UsuarioCriacao = p.UsuarioCriacao?.NomeCompleto, 
            Emissora = p.Emissora?.NomeSocial,
            Cidade = p.Cidade?.Nome,
        
            EmissoraNome = p.Emissora?.NomeSocial,
            EmissoraSlug = p.Emissora?.Slug,
            EmissoraLogo = p.Emissora?.LogoSmall,
            
            Midias = p.Imagens?.Select(i => new PostImagemViewModel
            {
                MidiaId = i.MidiaId,
                Url = i.Midia?.Url ?? "", 
                Ordem = i.Ordem,
                Tipo = i.Tipo.ToString()
            }).ToList() ?? new List<PostImagemViewModel>()
            
        }).ToList();

        return new PagedResult<PostResumeViewModel>(result, totalCount, page, pageSize);
    }
    // TODO: SEO ENGINNER PARA PÁGINAS OTIMIZADAS DE PESQUISAS...

    public async Task<PostDetailViewModel?> GetBySlugAsync(string slug)
    {
        var post = await _postRepository.GetBySlugAsync(slug);

        if (post == null)
            return null;

        return new PostDetailViewModel
        {
            Id = post.Id,
            Titulo = post.Titulo,
            Subtitulo = post.Subtitulo,
            Conteudo = post.Conteudo,
            Slug = post.Slug,
            ImagemCapaId = post.ImagemCapaId,
            ImagemCapaUrl = post.ImagemCapa?.Url,
            PublicadoEm = post.PublicadoEm,
            Emissora = post.Emissora.NomeSocial,
            EmissoraSlug = post.Emissora.Slug,
            Cidade = post.Cidade.Nome,
            Editorial = post.Editorial.TipoPostagem,
            Subcategoria = post.Subcategoria?.Nome ?? "",
            UsuarioCriacao = post.UsuarioCriacao?.NomeCompleto ?? "",
            UsuarioCriacaoId = post.UsuarioCriacaoId,
            TotalVisualizacoes = post.Visualizacoes?.Count ?? 0,
            Midias = post.Imagens?.Select(i => new PostImagemViewModel
            {
                MidiaId = i.MidiaId,
                Url = i.Midia?.Url ?? "", 
                Ordem = i.Ordem,
                Tipo = i.Tipo.ToString()
            }).ToList() ?? new List<PostImagemViewModel>()
        };
    }
    public async Task<string> GenerateSitemapAsync()
    {
        var posts = await _postRepository.GetAllPublishedForSitemapAsync();

        var sb = new StringBuilder();

        sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.AppendLine("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");

        foreach (var post in posts)
        {
            sb.AppendLine("<url>");
            sb.AppendLine($"<loc>https://portalgtf.com.br/noticia/{post.Slug}</loc>"); // TODO: Depois de publicado ajustar URL
            sb.AppendLine($"<lastmod>{post.PublicadoEm:yyyy-MM-dd}</lastmod>");
            sb.AppendLine("<changefreq>daily</changefreq>");
            sb.AppendLine("<priority>0.8</priority>");
            sb.AppendLine("</url>");
        }

        sb.AppendLine("</urlset>");

        return sb.ToString();
    }
    public async Task<List<PostListViewModel>> GetAllAsync()
    {
        var posts = await _postRepository.GetAllAsync();
        return posts.Select(MapToListVM).ToList();
    }
    public async Task<List<PostListViewModel>> GetAllPostsByStatusRascunho()
    {
        var posts = await _postRepository.GetAllPostsByStatusRascunho();
        return posts.Select(MapToListVM).ToList();
    }
    public async Task<List<PostListViewModel>> GetAllPostsByStatusRevisao()
    {
        var posts = await _postRepository.GetAllPostsByStatusRevisao();
        return posts.Select(MapToListVM).ToList();
    }
    public async Task<List<PostListViewModel>> GetAllByRecent()
    {
        var post = await _postRepository.GetAllByRecent();
        return post.Select(MapToListVM).ToList();  
    } 
    public async Task<PostDetailViewModel?> GetByIdAsync(int id)
    {
        var post = await _postRepository.GetByIdAsync(id);
        
        if (post == null) return null;
        
        return new PostDetailViewModel
        {
            Id = post.Id,
            Titulo = post.Titulo,
            Subtitulo = post.Subtitulo,
            Slug =  post.Slug,
            Conteudo = post.Conteudo,
            Status = post.StatusPost,
            ImagemCapaId = post.ImagemCapaId,
            ImagemCapaUrl = post.ImagemCapa?.Url,
            Editorial = post.Editorial?.TipoPostagem,
            Emissora = post.Emissora?.NomeSocial,
            EmissoraSlug = post.Emissora?.Slug,
            Subcategoria =  post.Subcategoria?.Nome,
            UsuarioCriacao = post.UsuarioCriacao?.NomeCompleto ?? "",
            UsuarioCriacaoId = post.UsuarioCriacaoId,
            Cidade = post.Cidade?.Nome ?? "",
            TotalVisualizacoes = post.Visualizacoes?.Count ?? 0,
            DataCriacao = post.DataCriacao,
            PublicadoEm = post.PublicadoEm,
            
            Midias = post.Imagens?.Select(i => new PostImagemViewModel
            {
                MidiaId = i.MidiaId,
                Url = i.Midia?.Url ?? "", 
                Ordem = i.Ordem,
                Tipo = i.Tipo.ToString()
            }).ToList() ?? new List<PostImagemViewModel>(),
            
            Tags = post.PostTags.Select(pt => pt.Tag.Nome).ToList()
        };
    }
    public async Task<List<PostListViewModel>> GetByEditorialAsync(int editorialId)
    {
        var posts = await _postRepository.GetByEditorialAsync(editorialId);
        return posts.Select(MapToListVM).ToList();
    }

    public async Task<List<PostListViewModel>> GetBySubcategoryAsync(int subcategoryId)
    {
        var post = await _postRepository.GetBySubcategoryAsync(subcategoryId);
        
        return post.Select(MapToListVM).ToList();
    }
    public async Task<List<PostListViewModel>> GetByUserAsync(string userName)
    {
        var posts = await _postRepository.GetByUserAsync(userName);
        return posts.Select(MapToListVM).ToList();
    }

    public async Task<List<PostListViewModel>> GetAllPostsByEmissora(int emissoraId)
    {
        var posts = await _postRepository.GetAllPostByEmissora(emissoraId);
        return posts.Select(MapToListVM).ToList(); 
    }

    public async Task<List<PostListViewModel>> GetByStatusAsync(StatusPost status)
    {
        var posts = await _postRepository.GetByStatusAsync(status);
        return posts.Select(MapToListVM).ToList();
    }

    public async Task<List<PostPublicViewModel>> GetAllPublishedAsync()
    {
        var posts = await _postRepository.GetByStatusAsync(StatusPost.Publicado);
        return posts.Select(MapToPublicVM).ToList();
    }

    public async Task<List<PostPublicViewModel>> SearchAsync(string query)
    {
        var posts = await _postRepository.SearchAsync(query);
        return posts.Select(MapToPublicVM).ToList();
    }
    public async Task<List<PostPublicViewModel>> GetFilteredAsync(PostEnumViewModel filter)
    {
        var posts = await _postRepository
            .GetFilteredAsync(filter.FiltroData, filter.OrdenarPor);

        return posts.Select(MapToPublicVM).ToList();
    }
    public async Task<List<PostPublicViewModel>> GetMostReadAsync(int? emissoraId, int limit, int days)
    {
        var posts = await _postRepository.GetMostReadAsync(emissoraId, limit, days);
        return posts.Select(MapToPublicVM).ToList();
    }
    public async Task<List<PostListViewModel>> GetDestaquesAsync()
    {
        var post = await _postRepository.GetDestaquesAsync();

        return post.Select(p => new PostListViewModel()
        {
            Id = p.Id,
            Titulo = p.Titulo,
            Subtitulo = p.Subtitulo,
            Conteudo = p.Conteudo,
            Editorial = p.Editorial?.TipoPostagem,
            Emissora = p.Emissora?.NomeSocial,
            EmissoraSlug = p.Emissora?.Slug,
            Status = p.StatusPost.ToString(),
            DataCriacao = p.DataCriacao,
            PublicadoEm = p.PublicadoEm,
            UsuarioCriacao = p.UsuarioCriacao?.NomeCompleto ?? "",
            UsuarioCriacaoId = p.UsuarioCriacaoId,
            Cidade = p.Cidade?.Nome ?? "",
            ImagemCapaId = p.ImagemCapaId,
            ImagemCapaUrl = p.ImagemCapa?.Url,
            Midias = p.Imagens?.Select(i => new PostImagemViewModel
            {
                MidiaId = i.MidiaId,
                Url = i.Midia?.Url ?? "", 
                Ordem = i.Ordem,
                Tipo = i.Tipo.ToString()
            }).ToList() ?? new List<PostImagemViewModel>()
        }).ToList();
    }
    public async Task<List<PostListViewModel>> GetDestaquesbBy88FmAsync()
    {
        var post = await _postRepository.GetDestaquesBy88FmAsync();

        return post.Select(p => new PostListViewModel()
        {
            Id = p.Id,
            Titulo = p.Titulo,
            Subtitulo = p.Subtitulo,
            Conteudo = p.Conteudo,
            Editorial = p.Editorial?.TipoPostagem,
            Emissora = p.Emissora?.NomeSocial,
            EmissoraSlug = p.Emissora?.Slug,
            Status = p.StatusPost.ToString(),
            DataCriacao = p.DataCriacao,
            PublicadoEm = p.PublicadoEm,
            UsuarioCriacao = p.UsuarioCriacao?.NomeCompleto ?? "",
            UsuarioCriacaoId = p.UsuarioCriacaoId,
            Cidade = p.Cidade?.Nome ?? "",
            ImagemCapaId = p.ImagemCapaId,
            ImagemCapaUrl = p.ImagemCapa?.Url,
            Midias = p.Imagens?.Select(i => new PostImagemViewModel
            {
                MidiaId = i.MidiaId,
                Url = i.Midia?.Url ?? "", 
                Ordem = i.Ordem,
                Tipo = i.Tipo.ToString()
            }).ToList() ?? new List<PostImagemViewModel>()
        }).ToList();
    }
    public async Task<List<PostListViewModel>> GetDestaquesbByFatoPopularAsync()
    {
        var post = await _postRepository.GetDestaquesByFatoPopularAsync();

        return post.Select(p => new PostListViewModel()
        {
            Id = p.Id,
            Titulo = p.Titulo,
            Subtitulo = p.Subtitulo,
            Conteudo = p.Conteudo,
            Editorial = p.Editorial?.TipoPostagem,
            Emissora = p.Emissora?.NomeSocial,
            EmissoraSlug = p.Emissora?.Slug,
            Status = p.StatusPost.ToString(),
            DataCriacao = p.DataCriacao,
            PublicadoEm = p.PublicadoEm,
            UsuarioCriacao = p.UsuarioCriacao?.NomeCompleto ?? "",
            UsuarioCriacaoId = p.UsuarioCriacaoId,
            Cidade = p.Cidade?.Nome ?? "",
            ImagemCapaId = p.ImagemCapaId,
            ImagemCapaUrl = p.ImagemCapa?.Url,
            Midias = p.Imagens?.Select(i => new PostImagemViewModel
            {
                MidiaId = i.MidiaId,
                Url = i.Midia?.Url ?? "", 
                Ordem = i.Ordem,
                Tipo = i.Tipo.ToString()
            }).ToList() ?? new List<PostImagemViewModel>()
        }).ToList();
    }
    #endregion
        
    #region COMMANDS - AÇÕES NO SISTEMA
    // COMMANDS PRINCIPAIS
    
    public async Task EnviarParaRevisaoAsync(int postId, PostEnviarRevisaoViewModel model)
    {
        var post = await _postRepository.GetByIdAsync(postId);

        if (post == null)
            throw new Exception("Post não encontrado");

        if (string.IsNullOrWhiteSpace(model.Mensagem))
            throw new Exception("É obrigatório informar a mensagem de revisão.");
        
        post.StatusPost = StatusPost.EmRevisao;
        post.DataEdicao = DateTime.UtcNow;
        
        await _postRepository.UpdateAsync(post);
        await _postRepository.AddHistoricoAsync(new PostHistorico
        {
            PostId = post.Id,
            UsuarioId = model.UsuarioId,
            Acao = "ENVIADO_PARA_REVISAO",
            Mensagem = model.Mensagem.Trim(),
            DataAcao = DateTime.UtcNow,
        });
    }


    public async Task AprovarPostAsync(int postId)
    {
        var post = await _postRepository.GetByIdAsync(postId);

        if (post == null)
            throw new KeyNotFoundException("Post não encontrado");

        post.Aprovar();

        await _postRepository.UpdateAsync(post);
    }
    public async Task EnviarParaAprovacao(int postId)
    {
        var post = await _postRepository.GetByIdAsync(postId);

        if (post == null)
            throw new KeyNotFoundException("Post não encontrado");

        post.EnviarParaAprovacao();

        await _postRepository.UpdateAsync(post);
    }
    public async Task RejeitarPostAsync(int postId)
    {
        var post = await _postRepository.GetByIdAsync(postId);

        if (post == null)
            throw new KeyNotFoundException("Post não encontrado");

        post.StatusPost = StatusPost.Rejeitado;
        post.DataEdicao = DateTime.UtcNow;

        await _postRepository.UpdateAsync(post);
    }
    public async Task SetDestaqueAsync(int postId, bool destaque)
    {
        await _postRepository.SetDestaqueAsync(postId, destaque);
    }
    public async Task RegisterViewAsync(int postId, string? ip)
    {
        await _postRepository.AddViewAsync(postId, ip);
    }
    
    public async Task CreateAsync(PostCreateViewModel model)
    {
    if (model.EmissoraId <= 0)
        throw new Exception("Selecione uma emissora válida.");

    if (model.CidadeId <= 0)
        throw new Exception("Selecione uma cidade válida.");

    var emissora = await _emissoraRepository.GetByIdAsync(model.EmissoraId);
    if (emissora == null)
        throw new Exception("Emissora não encontrada.");

    var cidade = await _cidadeRepository.GetByIdAsync(model.CidadeId);
    if (cidade == null)
        throw new Exception("Cidade não encontrada.");

    var editorial = await _editorialRepository.GetByIdAsync(model.EditorialId);
    if (editorial == null)
        throw new Exception("Editorial não encontrado.");

    if (editorial.EmissoraId != model.EmissoraId)
        throw new Exception("O editorial selecionado não pertence à emissora informada.");

    // Valida subcategoria
    if (model.SubcategoriaId.HasValue)
    {
        var subcategoria = await _subcategoriaRepository
            .GetByIdAsync(model.SubcategoriaId.Value);

        if (subcategoria == null)
            throw new Exception("Subcategoria não encontrada.");

        if (subcategoria.EditorialId != model.EditorialId)
            throw new Exception("Subcategoria não pertence ao Editorial informado.");
    }

    // Valida capa: se informada, precisa existir na tabela Midia
    if (model.ImagemCapaId.HasValue)
    {
        var midiaExiste = await _midiaRepository.ExistsAsync(model.ImagemCapaId.Value);
        if (!midiaExiste)
            throw new Exception("Mídia de capa não encontrada.");
    }

    var post = new Post
    {
        Titulo = model.Titulo,
        Subtitulo = model.Subtitulo,
        Conteudo = model.Conteudo,
        Slug = model.Slug,
        EditorialId = model.EditorialId,
        EmissoraId = model.EmissoraId,
        SubcategoriaId = model.SubcategoriaId,
        CidadeId = model.CidadeId,
        UsuarioCriacaoId = model.UsuarioCriacaoId,
        UsuarioAprovacaoId = null,
        StatusPost = StatusPost.Rascunho,
        DataCriacao = DateTime.UtcNow,
        ImagemCapaId = model.ImagemCapaId  // <-- capa vinculada aqui
    };

    // Tags
    foreach (var tagNome in model.Tags)
    {
        var slug = tagNome.ToLower().Replace(" ", "-");
        var existingTag = await _tagRepository.GetBySlugAsync(slug);

        Tag tag;
        if (existingTag == null)
        {
            tag = new Tag { Nome = tagNome, Slug = slug };
            await _tagRepository.AddAsync(tag);
        }
        else
        {
            tag = existingTag;
        }

        post.PostTags.Add(new PostTag { TagId = tag.Id });
    }
    
    // Mídias do corpo do post (galeria / vídeos)
    // foreach (var midia in model.Midias)
    // {
    //     post.Imagens.Add(new PostImagem
    //     {
    //         MidiaId = midia.MidiaId,
    //         Ordem = midia.Ordem,
    //         Tipo = midia.Tipo
    //     });
    // }
    await _postRepository.AddAsync(post);
    await _postRepository.SaveChangesAsync();
}
    public async Task UpdateAsync(int id, PostUpdateViewModel model)
    {
        var post = await _postRepository.GetByIdAsync(id);

        if (post == null)
            throw new Exception("Post não encontrado.");

        if (model.EmissoraId <= 0)
            throw new Exception("Selecione uma emissora válida.");

        if (model.CidadeId <= 0)
            throw new Exception("Selecione uma cidade válida.");

        var emissora = await _emissoraRepository.GetByIdAsync(model.EmissoraId);
        if (emissora == null)
            throw new Exception("Emissora não encontrada.");

        var cidade = await _cidadeRepository.GetByIdAsync(model.CidadeId);
        if (cidade == null)
            throw new Exception("Cidade não encontrada.");

        var editorial = await _editorialRepository.GetByIdAsync(model.EditorialId);
        if (editorial == null)
            throw new Exception("Editorial não encontrado.");

        if (editorial.EmissoraId != model.EmissoraId)
            throw new Exception("O editorial selecionado não pertence à emissora informada.");

        if (model.SubcategoriaId.HasValue)
        {
            var subcategoria = await _subcategoriaRepository
                .GetByIdAsync(model.SubcategoriaId.Value);

            if (subcategoria == null)
                throw new Exception("Subcategoria não encontrada.");

            if (subcategoria.EditorialId != model.EditorialId)
                throw new Exception("Subcategoria não pertence ao Editorial informado.");
        }

        post.Titulo = model.Titulo;
        post.Subtitulo = model.Subtitulo;
        post.Conteudo = model.Conteudo;
        post.Slug = model.Slug;
        post.ImagemCapaId = model.ImagemCapaId;
        post.EditorialId = model.EditorialId;
        post.SubcategoriaId = model.SubcategoriaId;
        post.EmissoraId = model.EmissoraId;
        post.CidadeId = model.CidadeId;
        post.UsuarioCriacaoId = model.UsuarioCriacaoId;
        post.DataEdicao = DateTime.UtcNow;

        // -------- TAGS --------

        post.PostTags.Clear();
        await _postRepository.SaveChangesAsync(); 

        foreach (var tagNome in model.Tags)
        {
            var slug = tagNome.ToLower().Trim().Replace(" ", "-");
            var existingTag = await _tagRepository.GetBySlugAsync(slug);

            Tag tag;
            if (existingTag == null)
            {
                tag = new Tag { Nome = tagNome, Slug = slug };
                await _tagRepository.AddAsync(tag);
                await _tagRepository.SaveChangesAsync(); 
            }
            else
            {
                tag = existingTag;
            }

            post.PostTags.Add(new PostTag 
            { 
                PostId = post.Id, 
                TagId = tag.Id 
            });
        }
        await _postRepository.UpdateAsync(post);
        await _postRepository.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var post = await _postRepository.GetByIdAsync(id);

        if (post == null)
            throw new Exception("Post não encontrado");

        await _postRepository.DeleteAsync(post);
        await _postRepository.SaveChangesAsync();
    }
    #endregion
}
