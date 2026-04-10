using System.Net;
using System.Net.Http.Json;
using PortalGtf.Application.ViewModels.PostsVM;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Enums;
using PortalGtf.Tests.Infrastructure;

namespace PortalGtf.Tests.Integration;

public class PostControllerTests : ApiIntegrationTestBase
{
    public PostControllerTests(TestWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task PostController_DeveRetornarEndpointsDeLeitura()
    {
        Assert.Equal(HttpStatusCode.OK, (await Client.GetAsync("/api/posts")).StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await Client.GetAsync("/api/posts/buscarTodosRascunhos")).StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await Client.GetAsync("/api/posts/buscarTodasRevisoes")).StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await Client.GetAsync($"/api/posts/regiao/{TestData.RegiaoId}?page=1&pageSize=10")).StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await Client.GetAsync("/api/posts/mais-recentes")).StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await Client.GetAsync("/api/posts/mais-lidas")).StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await Client.GetAsync($"/api/posts/{TestData.PostPublicadoFatoId}")).StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await Client.GetAsync($"/api/posts/slug/post-publicado-fato-popular")).StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await Client.GetAsync($"/api/posts/editorial/{TestData.EditorialNoticiasId}")).StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await Client.GetAsync("/api/posts/user/Administrador%20Teste")).StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await Client.GetAsync("/api/posts/public")).StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await Client.GetAsync("/api/posts/destaques")).StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await Client.GetAsync("/api/posts/destaques88fm")).StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await Client.GetAsync("/api/posts/destaquesFatoPopular")).StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await Client.GetAsync("/api/posts/search?query=receita")).StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await Client.GetAsync($"/api/posts/status?statusPost={(int)StatusPost.Publicado}")).StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await Client.GetAsync($"/api/posts/buscarPorEmissora/{TestData.EmissoraRadio88Id}")).StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await Client.GetAsync($"/api/posts/buscarPorSubcategory/{TestData.SubcategoriaReceitasId}")).StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await Client.GetAsync("/api/posts/filtro")).StatusCode);
    }

    [Fact]
    public async Task PostController_DeveRetornarSearchInvalidaComoBadRequest()
    {
        var response = await Client.GetAsync("/api/posts/search?query=");
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task PostController_DeveRetornarArquivosSeo()
    {
        var sitemapResponse = await Client.GetAsync("/sitemap.xml");
        Assert.Equal(HttpStatusCode.OK, sitemapResponse.StatusCode);
        var sitemap = await sitemapResponse.Content.ReadAsStringAsync();
        Assert.Contains("post-publicado-fato-popular", sitemap);

        var robotsResponse = await Client.GetAsync("/robots.txt");
        Assert.Equal(HttpStatusCode.OK, robotsResponse.StatusCode);
        var robots = await robotsResponse.Content.ReadAsStringAsync();
        Assert.Contains("Sitemap", robots);
    }

    [Fact]
    public async Task PostController_DeveRegistrarVisualizacao()
    {
        var response = await Client.PostAsync($"/api/posts/{TestData.PostPublicadoRadioId}/visualizacao", null);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        var totalViews = await WithDbContextAsync(async db =>
            await db.PostVisualizacao.CountAsync(v => v.PostId == TestData.PostPublicadoRadioId));

        Assert.True(totalViews >= 3);
    }

    [Fact]
    public async Task PostController_DeveCriarAtualizarEDestacarPost()
    {
        var slug = $"post-teste-{Guid.NewGuid():N}";
        var createResponse = await Client.PostAsJsonAsync("/api/posts/criarPost", new PostCreateViewModel
        {
            Titulo = "Novo Post Teste",
            Subtitulo = "Subtítulo do novo post",
            Conteudo = "<p>Conteúdo de teste</p>",
            ImagemCapaId = TestData.MidiaImagemId,
            Slug = slug,
            EditorialId = TestData.EditorialReceitasId,
            SubcategoriaId = TestData.SubcategoriaReceitasId,
            EmissoraId = TestData.EmissoraRadio88Id,
            CidadeId = TestData.CidadeId,
            UsuarioCriacaoId = TestData.UsuarioAdminId,
            Tags = new List<string> { "teste", "api" }
        });
        Assert.Equal(HttpStatusCode.OK, createResponse.StatusCode);

        var createdId = await WithDbContextAsync(async db =>
            await db.Post.Where(p => p.Slug == slug).Select(p => p.Id).SingleAsync());

        var updateResponse = await Client.PutAsJsonAsync($"/api/posts/{createdId}/updatePost", new PostUpdateViewModel
        {
            Titulo = "Novo Post Atualizado",
            Subtitulo = "Subtítulo atualizado",
            Conteudo = "<p>Conteúdo atualizado</p>",
            ImagemCapaId = TestData.MidiaBannerId,
            Slug = $"{slug}-atualizado",
            EditorialId = TestData.EditorialReceitasId,
            SubcategoriaId = TestData.SubcategoriaReceitasId,
            EmissoraId = TestData.EmissoraRadio88Id,
            CidadeId = TestData.CidadeId,
            UsuarioCriacaoId = TestData.UsuarioAdminId,
            Tags = new List<string> { "atualizado" }
        });
        Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);

        var destaqueResponse = await Client.PutAsync($"/api/posts/{createdId}/destaque?destaque=true", null);
        Assert.Equal(HttpStatusCode.NoContent, destaqueResponse.StatusCode);

        var destacado = await WithDbContextAsync(async db =>
            await db.Post.Where(p => p.Id == createdId).Select(p => p.Destaque).SingleAsync());

        Assert.True(destacado);
    }

    [Fact]
    public async Task PostController_DeveExecutarFluxoEditorial()
    {
        var postId = await SeedPostAsync(StatusPost.Rascunho);

        var revisaoResponse = await Client.PutAsJsonAsync($"/api/posts/{postId}/enviar-revisao", new PostEnviarRevisaoViewModel
        {
            UsuarioId = TestData.UsuarioAdminId,
            Mensagem = "Ajustar chamada principal."
        });
        Assert.Equal(HttpStatusCode.OK, revisaoResponse.StatusCode);

        var aprovarResponse = await Client.PutAsync($"/api/posts/{postId}/aprovar", null);
        Assert.Equal(HttpStatusCode.OK, aprovarResponse.StatusCode);

        var rejeitarId = await SeedPostAsync(StatusPost.EmRevisao);
        var rejeitarResponse = await Client.PutAsync($"/api/posts/{rejeitarId}/rejeitar", null);
        Assert.Equal(HttpStatusCode.OK, rejeitarResponse.StatusCode);

        var aprovacoesId = await SeedPostAsync(StatusPost.Rascunho);
        var enviarAprovacaoResponse = await Client.PutAsync($"/api/posts/{aprovacoesId}/enviarParaAprovacao", null);
        Assert.Equal(HttpStatusCode.OK, enviarAprovacaoResponse.StatusCode);
    }

    [Fact]
    public async Task PostController_DeveDeletarPost()
    {
        var postId = await SeedPostAsync(StatusPost.Rascunho);

        var deleteResponse = await Client.DeleteAsync($"/api/posts/{postId}/deletePost");
        Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);

        var exists = await WithDbContextAsync(async db =>
            await db.Post.AnyAsync(p => p.Id == postId));

        Assert.False(exists);
    }

    private Task<int> SeedPostAsync(StatusPost status)
    {
        return WithDbContextAsync(async db =>
        {
            var post = new Post
            {
                Titulo = $"Post Seed {Guid.NewGuid():N}",
                Subtitulo = "Subtítulo seed",
                Conteudo = "<p>Conteúdo seed</p>",
                Slug = $"post-seed-{Guid.NewGuid():N}",
                EditorialId = TestData.EditorialReceitasId,
                EmissoraId = TestData.EmissoraRadio88Id,
                CidadeId = TestData.CidadeId,
                SubcategoriaId = TestData.SubcategoriaReceitasId,
                UsuarioCriacaoId = TestData.UsuarioAdminId,
                StatusPost = status,
                DataCriacao = DateTime.UtcNow,
                PublicadoEm = status == StatusPost.Publicado ? DateTime.UtcNow : null,
                ImagemCapaId = TestData.MidiaImagemId
            };

            db.Post.Add(post);
            await db.SaveChangesAsync();
            return post.Id;
        });
    }
}
