using System.Net;
using System.Net.Http.Json;
using PortalGtf.Application.ViewModels.BannerInstitucionalVM;
using PortalGtf.Application.ViewModels.CidadeVM;
using PortalGtf.Application.ViewModels.EditorialVM;
using PortalGtf.Application.ViewModels.EmissoraVM;
using PortalGtf.Application.ViewModels.EstadoVM;
using PortalGtf.Application.ViewModels.ProgramacaoVM;
using PortalGtf.Application.ViewModels.RegiaoVM;
using PortalGtf.Application.ViewModels.StreamingVM;
using PortalGtf.Application.ViewModels.SubcategoryVM;
using PortalGtf.Application.ViewModels.TemaEditorialVM;
using PortalGtf.Application.Services.EstadoVM;
using PortalGtf.Tests.Infrastructure;

namespace PortalGtf.Tests.Integration;

public class LookupAndCrudControllersTests : ApiIntegrationTestBase
{
    public LookupAndCrudControllersTests(TestWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task RegiaoController_DeveExecutarCrudCompleto()
    {
        var listResponse = await Client.GetAsync("/api/regiao/buscarTodos");
        Assert.Equal(HttpStatusCode.OK, listResponse.StatusCode);
        var regioes = await ReadAsync<List<RegiaoViewModel>>(listResponse);
        Assert.Contains(regioes!, r => r.Id == TestData.RegiaoId);

        var getResponse = await Client.GetAsync($"/api/regiao/{TestData.RegiaoId}/regiaoPorId");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var nome = $"Região Teste {Guid.NewGuid():N}";
        var createResponse = await Client.PostAsJsonAsync("/api/regiao/criarRegiao", new RegiaoCreateViewModel { Nome = nome });
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        var createdId = await WithDbContextAsync(async db =>
            await db.Regiao.Where(r => r.Nome == nome).Select(r => r.Id).SingleAsync());

        var updateResponse = await Client.PutAsJsonAsync($"/api/regiao/{createdId}/atualizarRegiao", new RegiaoCreateViewModel { Nome = $"{nome} Atualizada" });
        Assert.Equal(HttpStatusCode.NoContent, updateResponse.StatusCode);

        var deleteResponse = await Client.DeleteAsync($"/api/regiao/{createdId}/deletarRegiaio");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }

    [Fact]
    public async Task EstadoController_DeveExecutarCrudCompleto()
    {
        var listResponse = await Client.GetAsync("/api/estado/buscarTodos");
        Assert.Equal(HttpStatusCode.OK, listResponse.StatusCode);
        var estados = await ReadAsync<List<EstadoViewModel>>(listResponse);
        Assert.Contains(estados!, e => e.Id == TestData.EstadoId);

        var getResponse = await Client.GetAsync($"/api/estado/{TestData.EstadoId}/buscarPorId");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var nome = $"Estado Teste {Guid.NewGuid():N}";
        var createResponse = await Client.PostAsJsonAsync("/api/estado/criarEstado", new EstadoCreateViewModel
        {
            Nome = nome,
            Sigla = "TT",
            RegiaoId = TestData.RegiaoId
        });
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        var createdId = await WithDbContextAsync(async db =>
            await db.Estado.Where(e => e.Nome == nome).Select(e => e.Id).SingleAsync());

        var updateResponse = await Client.PutAsJsonAsync($"/api/estado/{createdId}/atualizarEstado", new EstadoCreateViewModel
        {
            Nome = $"{nome} Atualizado",
            Sigla = "TA",
            RegiaoId = TestData.RegiaoId
        });
        Assert.Equal(HttpStatusCode.NoContent, updateResponse.StatusCode);

        var deleteResponse = await Client.DeleteAsync($"/api/estado/{createdId}/deletarEstado");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }

    [Fact]
    public async Task CidadeController_DeveExecutarCrudCompleto()
    {
        var listResponse = await Client.GetAsync("/api/cidade/buscarTodos");
        Assert.Equal(HttpStatusCode.OK, listResponse.StatusCode);
        var cidades = await ReadAsync<List<CidadeViewModel>>(listResponse);
        Assert.Contains(cidades!, c => c.Id == TestData.CidadeId);

        var getResponse = await Client.GetAsync($"/api/cidade/{TestData.CidadeId}/buscarPorId");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var nome = $"Cidade Teste {Guid.NewGuid():N}";
        var createResponse = await Client.PostAsJsonAsync("/api/cidade/criarCidade", new CidadeCreateViewModel
        {
            Nome = nome,
            EstadoId = TestData.EstadoId
        });
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        var createdId = await WithDbContextAsync(async db =>
            await db.Cidade.Where(c => c.Nome == nome).Select(c => c.Id).SingleAsync());

        var updateResponse = await Client.PutAsJsonAsync($"/api/cidade/{createdId}/atualizarCidade", new CidadeCreateViewModel
        {
            Nome = $"{nome} Atualizada",
            EstadoId = TestData.EstadoId
        });
        Assert.Equal(HttpStatusCode.NoContent, updateResponse.StatusCode);

        var deleteResponse = await Client.DeleteAsync($"/api/cidade/{createdId}/deletarCidade");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }

    [Fact]
    public async Task EmissoraController_DeveExecutarCrudCompleto()
    {
        var listResponse = await Client.GetAsync("/api/emissora/buscarTodos");
        Assert.Equal(HttpStatusCode.OK, listResponse.StatusCode);
        var emissoras = await ReadAsync<List<EmissoraViewModel>>(listResponse);
        Assert.Contains(emissoras!, e => e.Id == TestData.EmissoraRadio88Id);

        var getResponse = await Client.GetAsync($"/api/emissora/{TestData.EmissoraRadio88Id}/buscarPorId");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var nome = $"Emissora Teste {Guid.NewGuid():N}";
        var createResponse = await Client.PostAsJsonAsync("/api/emissora/criarEmissora", new EmissoraCreateViewModel
        {
            NomeSocial = nome,
            RazaoSocial = $"{nome} LTDA",
            Cep = "20000-999",
            Endereco = "Rua Teste",
            Numero = "1",
            Bairro = "Centro",
            Estado = "RJ",
            Cidade = "Rio de Janeiro",
            Slug = $"emissora-teste-{Guid.NewGuid():N}",
            Logo = "logo.png",
            LogoSmall = "logo-small.png",
            TemaPrincipal = "#123456",
            Ativa = true
        });
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        var createdId = await WithDbContextAsync(async db =>
            await db.Emissora.Where(e => e.NomeSocial == nome).Select(e => e.Id).SingleAsync());

        var updateResponse = await Client.PutAsJsonAsync($"/api/emissora/{createdId}/atualizarEmissora", new EmissoraCreateViewModel
        {
            NomeSocial = $"{nome} Atualizada",
            RazaoSocial = $"{nome} LTDA",
            Cep = "20000-998",
            Endereco = "Rua Teste 2",
            Numero = "2",
            Bairro = "Centro",
            Estado = "RJ",
            Cidade = "Rio de Janeiro",
            Slug = $"emissora-teste-atualizada-{Guid.NewGuid():N}",
            Logo = "logo.png",
            LogoSmall = "logo-small.png",
            TemaPrincipal = "#654321",
            Ativa = false
        });
        Assert.Equal(HttpStatusCode.NoContent, updateResponse.StatusCode);

        var deleteResponse = await Client.DeleteAsync($"/api/emissora/{createdId}/deletarEmissora");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }

    [Fact]
    public async Task EditorialController_DeveExecutarCrudCompleto()
    {
        var listResponse = await Client.GetAsync("/api/editorial/buscarTodos");
        Assert.Equal(HttpStatusCode.OK, listResponse.StatusCode);
        var editoriais = await ReadAsync<List<EditorialViewModel>>(listResponse);
        Assert.Contains(editoriais!, e => e.Id == TestData.EditorialNoticiasId);

        var byEmissoraResponse = await Client.GetAsync($"/api/editorial/emissora/{TestData.EmissoraRadio88Id}/buscarTodos");
        Assert.Equal(HttpStatusCode.OK, byEmissoraResponse.StatusCode);

        var getResponse = await Client.GetAsync($"/api/editorial/buscarPorId/{TestData.EditorialNoticiasId}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var tipoPostagem = $"Editorial Teste {Guid.NewGuid():N}";
        var createResponse = await Client.PostAsJsonAsync("/api/editorial/criarEditorial", new EditorialCreateViewModel
        {
            TipoPostagem = tipoPostagem,
            TemaEditorialId = TestData.TemaReceitasId,
            EmissoraId = TestData.EmissoraRadio88Id
        });
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        var createdId = await WithDbContextAsync(async db =>
            await db.Editorial.Where(e => e.TipoPostagem == tipoPostagem).Select(e => e.Id).SingleAsync());

        var updateResponse = await Client.PutAsJsonAsync($"/api/editorial/{createdId}/atualizarEditorial", new EditorialCreateViewModel
        {
            TipoPostagem = $"{tipoPostagem} Atualizado",
            TemaEditorialId = TestData.TemaFatoPopularId,
            EmissoraId = TestData.EmissoraRadio88Id
        });
        Assert.Equal(HttpStatusCode.NoContent, updateResponse.StatusCode);

        var deleteResponse = await Client.DeleteAsync($"/api/editorial/{createdId}/deletarEditorial");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }

    [Fact]
    public async Task TemaEditorialController_DeveExecutarCrudCompleto()
    {
        var listResponse = await Client.GetAsync("/api/tema-editorial");
        Assert.Equal(HttpStatusCode.OK, listResponse.StatusCode);

        var getResponse = await Client.GetAsync($"/api/tema-editorial/{TestData.TemaNoticiasId}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var descricao = $"Tema Teste {Guid.NewGuid():N}";
        var createResponse = await Client.PostAsJsonAsync("/api/tema-editorial", new TemaEditorialViewModel
        {
            Descricao = descricao,
            CorPrimaria = "#101010",
            CorSecundaria = "#202020",
            CorFonte = "#ffffff",
            Logo = "tema.png"
        });
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        var createdId = await WithDbContextAsync(async db =>
            await db.TemaEditorial.Where(t => t.Descricao == descricao).Select(t => t.Id).SingleAsync());

        var updateResponse = await Client.PutAsJsonAsync($"/api/tema-editorial/{createdId}", new TemaEditorialViewModel
        {
            Descricao = $"{descricao} Atualizado",
            CorPrimaria = "#303030",
            CorSecundaria = "#404040",
            CorFonte = "#ffffff",
            Logo = "tema2.png"
        });
        Assert.Equal(HttpStatusCode.NoContent, updateResponse.StatusCode);

        var deleteResponse = await Client.DeleteAsync($"/api/tema-editorial/{createdId}");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }

    [Fact]
    public async Task StreamingController_DeveExecutarCrudCompleto()
    {
        var listResponse = await Client.GetAsync("/api/streaming/buscarTodos");
        Assert.Equal(HttpStatusCode.OK, listResponse.StatusCode);

        var getResponse = await Client.GetAsync($"/api/streaming/{TestData.StreamingRadio88Id}/streamingPorId");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var url = $"https://stream-{Guid.NewGuid():N}.example.com";
        var createResponse = await Client.PostAsJsonAsync("/api/streaming/criarStreaming", new StreamingCreateViewModel
        {
            EmissoraId = TestData.EmissoraRadio88Id,
            Url = url,
            Porta = "8001",
            TipoStream = "mp3",
            LinkApi = "https://api.example.com/stream"
        });
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        var createdId = await WithDbContextAsync(async db =>
            await db.Streaming.Where(s => s.Url == url).Select(s => s.Id).SingleAsync());

        var updateResponse = await Client.PutAsJsonAsync($"/api/streaming/{createdId}/atualizarStreaming", new StreamingCreateViewModel
        {
            EmissoraId = TestData.EmissoraRadio88Id,
            Url = $"{url}/updated",
            Porta = "8002",
            TipoStream = "aac",
            LinkApi = "https://api.example.com/stream-updated"
        });
        Assert.Equal(HttpStatusCode.NoContent, updateResponse.StatusCode);

        var deleteResponse = await Client.DeleteAsync($"/api/streaming/{createdId}/deletarStreaming");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }

    [Fact]
    public async Task ProgramacaoController_DeveExecutarCrudCompleto()
    {
        var listResponse = await Client.GetAsync("/api/programacao/buscarTodos");
        Assert.Equal(HttpStatusCode.OK, listResponse.StatusCode);

        var byEmissoraResponse = await Client.GetAsync($"/api/programacao/emissora/{TestData.EmissoraRadio88Id}/buscarTodos");
        Assert.Equal(HttpStatusCode.OK, byEmissoraResponse.StatusCode);

        var getResponse = await Client.GetAsync($"/api/programacao/{TestData.ProgramacaoRadio88Id}/buscarPorId");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var nomePrograma = $"Programa Teste {Guid.NewGuid():N}";
        var createResponse = await Client.PostAsJsonAsync("/api/programacao/criarPrograma", new ProgramacaoRadioCreateViewModel
        {
            EmissoraId = TestData.EmissoraRadio88Id,
            NomePrograma = nomePrograma,
            Apresentador = "Apresentador Teste",
            Descricao = "Descrição do programa",
            DiaSemana = 2,
            HoraInicio = new TimeSpan(10, 0, 0),
            HoraFim = new TimeSpan(12, 0, 0),
            Imagem = "programa.png",
            Ativo = true
        });
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        var createdId = await WithDbContextAsync(async db =>
            await db.ProgramacaoRadio.Where(p => p.NomePrograma == nomePrograma).Select(p => p.Id).SingleAsync());

        var updateResponse = await Client.PutAsJsonAsync($"/api/programacao/{createdId}/atualizarPrograma", new ProgramacaoRadioUpdateViewModel
        {
            EmissoraId = TestData.EmissoraRadio88Id,
            NomePrograma = $"{nomePrograma} Atualizado",
            Apresentador = "Apresentador Atualizado",
            Descricao = "Descrição atualizada",
            DiaSemana = 3,
            HoraInicio = new TimeSpan(13, 0, 0),
            HoraFim = new TimeSpan(15, 0, 0),
            Imagem = "programa2.png",
            Ativo = false
        });
        Assert.Equal(HttpStatusCode.NoContent, updateResponse.StatusCode);

        var deleteResponse = await Client.DeleteAsync($"/api/programacao/{createdId}/deletarPrograma");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }

    [Fact]
    public async Task BannerInstitucionalController_DeveExecutarCrudCompleto()
    {
        var listResponse = await Client.GetAsync("/api/banner-institucional/buscarTodos");
        Assert.Equal(HttpStatusCode.OK, listResponse.StatusCode);

        var ativosResponse = await Client.GetAsync($"/api/banner-institucional/ativos?emissoraId={TestData.EmissoraRadio88Id}&posicao=home");
        Assert.Equal(HttpStatusCode.OK, ativosResponse.StatusCode);

        var getResponse = await Client.GetAsync($"/api/banner-institucional/{TestData.BannerHomeId}/buscarPorId");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var titulo = $"Banner Teste {Guid.NewGuid():N}";
        var createResponse = await Client.PostAsJsonAsync("/api/banner-institucional/criar", new BannerInstitucionalCreateViewModel
        {
            Titulo = titulo,
            EmissoraId = TestData.EmissoraRadio88Id,
            MidiaId = TestData.MidiaBannerId,
            LinkUrl = "https://example.com/novo-banner",
            NovaAba = false,
            Posicao = "home",
            Ordem = 2,
            Ativo = true
        });
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        var createdId = await WithDbContextAsync(async db =>
            await db.BannerInstitucional.Where(b => b.Titulo == titulo).Select(b => b.Id).SingleAsync());

        var updateResponse = await Client.PutAsJsonAsync($"/api/banner-institucional/{createdId}/atualizar", new BannerInstitucionalCreateViewModel
        {
            Titulo = $"{titulo} Atualizado",
            EmissoraId = TestData.EmissoraRadio88Id,
            MidiaId = TestData.MidiaImagemId,
            LinkUrl = "https://example.com/banner-atualizado",
            NovaAba = true,
            Posicao = "hero",
            Ordem = 3,
            Ativo = false
        });
        Assert.Equal(HttpStatusCode.NoContent, updateResponse.StatusCode);

        var deleteResponse = await Client.DeleteAsync($"/api/banner-institucional/{createdId}/deletar");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }

    [Fact]
    public async Task SubcategoriaController_DeveExecutarCrudCompleto()
    {
        var listResponse = await Client.GetAsync("/api/subcategorias/buscarTodasSubcategorias");
        Assert.Equal(HttpStatusCode.OK, listResponse.StatusCode);

        var getResponse = await Client.GetAsync($"/api/subcategorias/{TestData.SubcategoriaNoticiasId}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var nome = $"Subcategoria Teste {Guid.NewGuid():N}";
        var createResponse = await Client.PostAsJsonAsync("/api/subcategorias/criarSubcategoria", new CreateSubcategoriaViewModel
        {
            Nome = nome,
            EditorialId = TestData.EditorialNoticiasId
        });
        Assert.Equal(HttpStatusCode.OK, createResponse.StatusCode);

        var createdId = await WithDbContextAsync(async db =>
            await db.Subcategoria.Where(s => s.Nome == nome).Select(s => s.Id).SingleAsync());

        var deleteResponse = await Client.DeleteAsync($"/api/subcategorias/{createdId}/deletarSubcategoria");
        Assert.Equal(HttpStatusCode.OK, deleteResponse.StatusCode);
    }
}
