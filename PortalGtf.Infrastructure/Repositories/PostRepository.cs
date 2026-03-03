using Microsoft.EntityFrameworkCore;
using PortalGtf.Application.ViewModels.PostsVM;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Enums;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly PortalGtfNewsDbContext _dbContext;

    public PostRepository(PortalGtfNewsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    private IQueryable<Post> BaseQuery()
    {
        return _dbContext.Post
            .Include(p => p.PostTags)
            .ThenInclude(pt => pt.Tag) 
            .Include(p => p.Subcategoria)
            .Include(p => p.Editorial)
            .ThenInclude(e => e.TemaEditorial)
            .Include(p => p.Emissora)
            .Include(p => p.UsuarioCriacao)
            .Include(p => p.Cidade)
            .OrderByDescending(p => p.DataCriacao);
    }
    public async Task<List<Post>> GetAllAsync()
    {
        return await BaseQuery().ToListAsync();
    }
    public async Task<Post?> GetByIdAsync(int id)
    {
        return await BaseQuery()
            .SingleOrDefaultAsync(p => p.Id == id);
    }
    public async Task<(List<Post> Posts, int TotalCount)> GetAllByRegiaoAsync(int regiaoId, int page, int pageSize)
    {
        var query = _dbContext.Post
            .AsNoTracking()
            .Include(p => p.Editorial)
            .ThenInclude(e => e.TemaEditorial) 
            .Include(p => p.Emissora)
            .Include(p => p.Cidade)
            .Include(p => p.UsuarioCriacao)
            .Where(p => 
                p.StatusPost == StatusPost.Publicado && 
                p.Emissora.Ativa && 
                p.Emissora.EmissoraRegioes.Any(er => er.RegiaoId == regiaoId)
            );

        var totalCount = await query.CountAsync();

        var posts = await query
            .OrderByDescending(p => p.PublicadoEm)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(); 

        return (posts, totalCount);
    }
    public async Task<List<Post>> GetByEditorialAsync(int editorialId)
    {
        return await BaseQuery()
            .Where(p => p.EditorialId == editorialId)
            .ToListAsync();
    }

    public async Task<List<Post>> GetBySubcategoryAsync(int subcategoryId)
    {
        return await BaseQuery()
            .Where(p => p.SubcategoriaId == subcategoryId)
            .ToListAsync();
    }
    public async Task<List<Post>> GetAllByRecent()
    {
        return await _dbContext.Post
            .Include(p => p.PostTags)
            .ThenInclude(pt => pt.Tag) 
            .Include(p => p.Subcategoria)
            .Include(p => p.Editorial)
            .ThenInclude(e => e.TemaEditorial)
            .Include(p => p.Emissora)
            .Include(p => p.UsuarioCriacao)
            .Include(p => p.Cidade)
            .OrderByDescending(p => p.PublicadoEm).ToListAsync(); 
    }
    public async Task<List<Post>> GetAllPostByEmissora(int emissoraId)
    {
        return await BaseQuery()
            .Where(p => p.EmissoraId == emissoraId)
            .ToListAsync();
    }
    public async Task<List<Post>> GetByUserAsync(string userName)
    {
        return await BaseQuery()
            .Where(p => p.UsuarioCriacao.NomeCompleto == userName)
            .ToListAsync();
    }
    public async Task<List<Post>> GetByStatusAsync(StatusPost status)
    {
        return await BaseQuery()
            .Where(p => p.StatusPost == status)
            .ToListAsync();
    }
    public async Task<List<Post>> SearchAsync(string query)
    {
        query = query.ToLower();

        return await BaseQuery()
            .Where(p =>
                p.StatusPost == StatusPost.Publicado &&
                (p.Titulo.ToLower().Contains(query) ||
                 p.Subtitulo.ToLower().Contains(query) ||
                 p.Conteudo.ToLower().Contains(query)))
            .OrderByDescending(p => p.PublicadoEm)
            .ToListAsync();
    }
    public async Task<List<Post>> GetFilteredAsync(FiltroPostEnum filterData, OrdenaPostEnum ordenaPost)
    {
        var query = _dbContext.Post
            .AsNoTracking()
            .Include(p => p.Editorial)
            .ThenInclude(e => e.TemaEditorial)
            .Include(p => p.Emissora)
            .Include(p => p.UsuarioCriacao)
            .Include(p => p.Cidade)
            .Where(p => p.StatusPost == StatusPost.Publicado);

        // FILTRO DE DATA
        if (filterData != FiltroPostEnum.QualquerData)
        {
            var now = DateTime.UtcNow;

            DateTime startDate = filterData switch
            {
                FiltroPostEnum.UltimaHora => now.AddHours(-1),
                FiltroPostEnum.UltimaSemana => now.AddDays(-7),
                FiltroPostEnum.UltimoMes => now.AddMonths(-1),
                FiltroPostEnum.UltimaAno => now.AddYears(-1),
                _ => DateTime.MinValue
            };

            query = query.Where(p => p.PublicadoEm >= startDate);
        }

        // ORDENAÇÃO
        query = ordenaPost switch
        {
            OrdenaPostEnum.MaisAntigos => query.OrderBy(p => p.PublicadoEm),
            _ => query.OrderByDescending(p => p.PublicadoEm)
        };

        return await query.ToListAsync();
    }

    public async Task<Post?> GetBySlugAsync(string slug)
    {
        return await _dbContext.Post
            .AsNoTracking()
            .Include(p => p.Editorial)
            .ThenInclude(e => e.TemaEditorial)
            .Include(p => p.Emissora)
            .Include(p => p.UsuarioCriacao)
            .Include(p => p.Cidade)
            .FirstOrDefaultAsync(p =>
                p.Slug == slug &&
                p.StatusPost == StatusPost.Publicado);
    }
    public async Task<List<Post>> GetAllPublishedForSitemapAsync()
    {
        return await _dbContext.Post
            .AsNoTracking()
            .Where(p => p.StatusPost == StatusPost.Publicado)
            .OrderByDescending(p => p.PublicadoEm)
            .Select(p => new Post
            {
                Slug = p.Slug,
                PublicadoEm = p.PublicadoEm
            })
            .ToListAsync();
    }
    public async Task AddAsync(Post post)
    {
        await _dbContext.Post.AddAsync(post);
        await _dbContext.SaveChangesAsync();    
    }
    public async Task UpdateAsync(Post post)
    {
       _dbContext.Post.Update(post);
       await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteAsync(Post post)
    { 
        _dbContext.Post.Remove(post);
        await _dbContext.SaveChangesAsync();
    }
    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}