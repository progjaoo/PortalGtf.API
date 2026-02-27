using PortalGtf.Application.ViewModels.PostsVM;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Enums;
using PortalGtf.Core.Interfaces;
using PostResumeViewModel = PortalGtf.Application.ViewModels.PostsVM.PostResumeViewModel;

namespace PortalGtf.Application.Services.PostServices;
public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly ITagRepository _tagRepository;
    private readonly ISubcategoriaRepository _subcategoriaRepository;
    public PostService(IPostRepository postRepository,ITagRepository tagRepository,  ISubcategoriaRepository subcategoriaRepository)
    {
        _postRepository = postRepository;
        _tagRepository = tagRepository;
        _subcategoriaRepository = subcategoriaRepository;
    }
    private static PostListViewModel MapToListVM(Post p)
    {
        return new PostListViewModel
        {
            Id = p.Id,
            Titulo = p.Titulo,
            Conteudo = p.Conteudo,
            Editorial = p.Editorial?.TipoPostagem,
            Imagem = p.Imagem,
            Emissora = p.Emissora?.NomeSocial,
            Status = p.StatusPost.ToString(),
            DataCriacao = p.DataCriacao,
            PublicadoEm = p.PublicadoEm,
            UsuarioCriacao = p.UsuarioCriacao?.NomeCompleto ?? "",
            UsuarioCriacaoId = p.UsuarioCriacaoId,
            Cidade = p.Cidade?.Nome ?? ""
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
            Slug = p.Slug,
            Imagem = p.Imagem,
            PublicadoEm = p.PublicadoEm,
            Editorial = p.Editorial?.TipoPostagem,
            CorTema = p.Editorial?.TemaEditorial?.CorPrimaria,
            Emissora = p.Emissora?.NomeSocial,
            UsuarioCriacao = p.UsuarioCriacao?.NomeCompleto ?? "",
            UsuarioCriacaoId = p.UsuarioCriacaoId,
            Cidade = p.Cidade?.Nome ?? "",
        };
    }
    // TODO: Realizar todos métodos services de GET`s de POST`s
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
            Imagem = p.Imagem,
            PublicadoEm = p.PublicadoEm,
        
            Editorial = p.Editorial?.TipoPostagem, 
            CorTema = p.Editorial?.TemaEditorial?.CorPrimaria, 
            UsuarioCriacaoId = p.UsuarioCriacaoId,
            UsuarioCriacao = p.UsuarioCriacao?.NomeCompleto, 
            Emissora = p.Emissora?.NomeSocial,
            Cidade = p.Cidade?.Nome,
        
            EmissoraNome = p.Emissora?.NomeSocial,
            EmissoraSlug = p.Emissora?.Slug,
            EmissoraLogo = p.Emissora?.LogoSmall
        }).ToList();

        return new PagedResult<PostResumeViewModel>(result, totalCount, page, pageSize);
    }
    public async Task<List<PostListViewModel>> GetAllAsync()
    {
        var posts = await _postRepository.GetAllAsync();
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
            Conteudo = post.Conteudo,
            Imagem = post.Imagem,
            Status = post.StatusPost,
            Editorial = post.Editorial?.TipoPostagem,
            Emissora = post.Emissora?.NomeSocial,
            Subcategoria =  post.Subcategoria?.Nome,
            UsuarioCriacao = post.UsuarioCriacao?.NomeCompleto ?? "",
            UsuarioCriacaoId = post.UsuarioCriacaoId,
            Cidade = post.Cidade?.Nome ?? "",
            DataCriacao = post.DataCriacao,
            PublicadoEm = post.PublicadoEm,
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
    // TODO: Revisado
    public async Task EnviarParaRevisaoAsync(int postId)
    {
        var post = await _postRepository.GetByIdAsync(postId);

        if (post == null)
            throw new Exception("Post não encontrado");

        if (post.StatusPost != StatusPost.Rascunho)
            throw new Exception("Apenas posts em Rascunho podem ser enviados para revisão.");
        
        post.StatusPost = StatusPost.EmRevisao;
        post.DataCriacao = DateTime.UtcNow;
        
        await _postRepository.UpdateAsync(post);
        await _postRepository.SaveChangesAsync();
    }


    public async Task AprovarPostAsync(int postId)
    {
        var post = await _postRepository.GetByIdAsync(postId);

        if (post == null)
            throw new KeyNotFoundException("Post não encontrado");

        post.Aprovar();

        await _postRepository.UpdateAsync(post);
    }

    public async Task RejeitarPostAsync(int postId)
    {
        var post = await _postRepository.GetByIdAsync(postId);

        if (post == null)
            throw new KeyNotFoundException("Post não encontrado");

        if (post.StatusPost != StatusPost.EmRevisao)
            throw new InvalidOperationException("Post não está em revisão");

        post.StatusPost = StatusPost.Rascunho;
        post.DataEdicao = DateTime.UtcNow;

        await _postRepository.UpdateAsync(post);
    }
    public async Task<string> UploadImagemAsync(
        int postId,
        Stream fileStream,
        string fileName,
        string contentType
    )
    {
        var post = await _postRepository.GetByIdAsync(postId);

        if (post == null)
            throw new Exception("Post não encontrado");

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

        var url = $"http://localhost:5091/uploads/{newFileName}";

        post.Imagens.Add(new PostImagem
        {
            Ordem = post.Imagens.Count + 1,
            Tipo = TipoMidia.Imagem
        });

        await _postRepository.UpdateAsync(post);
        await _postRepository.SaveChangesAsync();

        return url;
    }
    public async Task CreateAsync(PostCreateViewModel model)
    {
        if (model.SubcategoriaId.HasValue)
        {
            var subcategoria = await _subcategoriaRepository
                .GetByIdAsync(model.SubcategoriaId.Value);

            if (subcategoria == null)
                throw new Exception("Subcategoria não encontrada.");

            if (subcategoria.EditorialId != model.EditorialId)
                throw new Exception("Subcategoria não pertence ao Editorial informado.");
        }
        var post = new Post
        {
            Titulo = model.Titulo,
            Subtitulo = model.Subtitulo,
            Conteudo = model.Conteudo,
            Imagem = model.Imagem,
            Slug = model.Slug,
            EditorialId = model.EditorialId,
            EmissoraId = model.EmissoraId,
            SubcategoriaId = model.SubcategoriaId,
            CidadeId = model.CidadeId,
            UsuarioCriacaoId = model.UsuarioCriacaoId,
            UsuarioAprovacaoId = null,
            StatusPost = StatusPost.Rascunho,
            DataCriacao = DateTime.UtcNow
        };
        
        foreach (var tagNome in model.Tags)
        {
            var slug = tagNome.ToLower().Replace(" ", "-");

            var existingTag = await _tagRepository.GetBySlugAsync(slug);

            Tag tag;

            if (existingTag == null)
            {
                tag = new Tag
                {
                    Nome = tagNome,
                    Slug = slug
                };

                await _tagRepository.AddAsync(tag);
            }
            else
            {
                tag = existingTag;
            }

            post.PostTags.Add(new PostTag
            {
                TagId = tag.Id
            });
        }
        foreach (var midia in model.Midias)
        {
            post.Imagens.Add(new PostImagem
            {
                MidiaId = midia.MidiaId,
                Ordem = midia.Ordem,
                Tipo = midia.Tipo
            });
        }
        await _postRepository.AddAsync(post);
        await _postRepository.SaveChangesAsync();
    }
    public async Task UpdateAsync(int id, PostUpdateViewModel model)
    {
        var post = await _postRepository.GetByIdAsync(id);

        if (post == null)
            throw new Exception("Post não encontrado");

        post.Titulo = model.Titulo;
        post.Subtitulo = model.Subtitulo;
        post.Conteudo = model.Conteudo;
        post.Imagem = model.Imagem;
        post.EditorialId = model.EditorialId;
        post.SubcategoriaId = model.SubcategoriaId;
        post.StatusPost = model.StatusPost;
        post.DataEdicao = DateTime.UtcNow;

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
}