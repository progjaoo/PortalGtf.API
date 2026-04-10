using System.Net;
using System.Net.Http.Json;
using PortalGtf.Application.Services.UsuarioEmissoraService;
using PortalGtf.Application.ViewModels.EmissoraRegiaoVM;
using PortalGtf.Application.ViewModels.FuncaoPermissaoVM;
using PortalGtf.Application.ViewModels.FuncaoVM;
using PortalGtf.Application.ViewModels.LoginVM;
using PortalGtf.Application.ViewModels.PermissaoVM;
using PortalGtf.Application.ViewModels.UsuarioVM;
using PortalGtf.Core.Enums;
using PortalGtf.Tests.Infrastructure;

namespace PortalGtf.Tests.Integration;

public class AccessControllersTests : ApiIntegrationTestBase
{
    public AccessControllersTests(TestWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task FuncaoController_DeveExecutarCrudCompleto()
    {
        var listResponse = await Client.GetAsync("/api/funcoes/buscarTodos");
        Assert.Equal(HttpStatusCode.OK, listResponse.StatusCode);

        var getResponse = await Client.GetAsync($"/api/funcoes/{TestData.FuncaoAdministradorId}/funcaoPorId");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var nome = $"Funcao Teste {Guid.NewGuid():N}";
        var createResponse = await Client.PostAsJsonAsync("/api/funcoes/criarFuncao", new FuncaoCreateViewModel
        {
            TipoFuncao = nome
        });
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        var createdId = await WithDbContextAsync(async db =>
            await db.Funcao.Where(f => f.TipoFuncao == nome).Select(f => f.Id).SingleAsync());

        var updateResponse = await Client.PutAsJsonAsync($"/api/funcoes/{createdId}/atualizarFuncao", new FuncaoViewModel
        {
            Id = createdId,
            TipoFuncao = $"{nome} Atualizada"
        });
        Assert.Equal(HttpStatusCode.NoContent, updateResponse.StatusCode);

        var deleteResponse = await Client.DeleteAsync($"/api/funcoes/{createdId}/deletarFuncao");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }

    [Fact]
    public async Task PermissaoController_DeveExecutarCrudCompleto()
    {
        var listResponse = await Client.GetAsync("/api/permissao/buscarTodasPermissoes");
        Assert.Equal(HttpStatusCode.OK, listResponse.StatusCode);

        var getResponse = await Client.GetAsync($"/api/permissao/{TestData.PermissaoPostsId}/buscarPorId");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var byUsuarioResponse = await Client.GetAsync($"/api/permissao/PermissaoPorUsuario/{TestData.UsuarioAdminId}");
        Assert.Equal(HttpStatusCode.OK, byUsuarioResponse.StatusCode);

        var tipoPermissao = $"permissao.teste.{Guid.NewGuid():N}";
        var createResponse = await Client.PostAsJsonAsync("/api/permissao/ciarPermissao", new PermissaoCreateViewModel
        {
            TipoPermissao = tipoPermissao
        });
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        var createdId = await WithDbContextAsync(async db =>
            await db.Permissao.Where(p => p.TipoPermissao == tipoPermissao).Select(p => p.Id).SingleAsync());

        var updateResponse = await Client.PutAsJsonAsync($"/api/permissao/{createdId}/atualizarPermissao", new PermissaoCreateViewModel
        {
            TipoPermissao = $"{tipoPermissao}.updated"
        });
        Assert.Equal(HttpStatusCode.NoContent, updateResponse.StatusCode);

        var deleteResponse = await Client.DeleteAsync($"/api/permissao/{createdId}/deletarPermissao");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }

    [Fact]
    public async Task FuncaoPermissaoController_DeveExecutarCrudCompleto()
    {
        var listResponse = await Client.GetAsync("/api/funcao-permissao/buscarTodos");
        Assert.Equal(HttpStatusCode.OK, listResponse.StatusCode);

        var createResponse = await Client.PostAsJsonAsync("/api/funcao-permissao/criar", new FuncaoPermissaoCreateViewModel
        {
            FuncaoId = TestData.FuncaoRedatorId,
            PermissaoId = TestData.PermissaoPostsId
        });
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        var createdId = await WithDbContextAsync(async db =>
            await db.FuncaoPermissao
                .Where(fp => fp.FuncaoId == TestData.FuncaoRedatorId && fp.PermissaoId == TestData.PermissaoPostsId)
                .OrderByDescending(fp => fp.Id)
                .Select(fp => fp.Id)
                .FirstAsync());

        var deleteResponse = await Client.DeleteAsync($"/api/funcao-permissao/{createdId}/deletar");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }

    [Fact]
    public async Task UsuarioController_DeveExecutarCrudELogin()
    {
        var listResponse = await Client.GetAsync("/api/usuarios");
        Assert.Equal(HttpStatusCode.OK, listResponse.StatusCode);

        var getResponse = await Client.GetAsync($"/api/usuarios/{TestData.UsuarioAdminId}");
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var createEmail = $"usuario-{Guid.NewGuid():N}@portalgtf.com";
        var createResponse = await Client.PostAsJsonAsync("/api/usuarios", new UsuarioCreateViewModel
        {
            Email = createEmail,
            NomeCompleto = "Usuário Integração",
            Senha = "123456",
            FuncaoId = TestData.FuncaoRedatorId
        });
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        var createdId = await WithDbContextAsync(async db =>
            await db.Usuario.Where(u => u.Email == createEmail).Select(u => u.Id).SingleAsync());

        var updateResponse = await Client.PutAsJsonAsync($"/api/usuarios/{createdId}/atualizarUsuario", new UsuarioUpdateViewModel
        {
            Email = createEmail,
            NomeCompleto = "Usuário Integração Atualizado",
            FuncaoId = TestData.FuncaoAdministradorId
        });
        Assert.Equal(HttpStatusCode.NoContent, updateResponse.StatusCode);

        var ativarResponse = await Client.PutAsync($"/api/usuarios/ativarUsuario/{createdId}", null);
        Assert.Equal(HttpStatusCode.OK, ativarResponse.StatusCode);

        var desativarResponse = await Client.PutAsync($"/api/usuarios/desativarUsuario/{createdId}", null);
        Assert.Equal(HttpStatusCode.OK, desativarResponse.StatusCode);

        var loginResponse = await Client.PostAsJsonAsync("/api/usuarios/login", new LoginRequestViewModel
        {
            Email = "admin@portalgtf.com",
            Senha = "123456"
        });
        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);

        var loginPayload = await ReadAsync<LoginResponseViewModel>(loginResponse);
        Assert.Equal(TestData.UsuarioAdminId, loginPayload!.UsuarioId);
        Assert.False(string.IsNullOrWhiteSpace(loginPayload.Token));
    }

    [Fact]
    public async Task UsuarioEmissoraController_DeveExecutarCrudBasico()
    {
        var listResponse = await Client.GetAsync("/api/usuario-emissora/buscarTodos");
        Assert.Equal(HttpStatusCode.OK, listResponse.StatusCode);

        var createResponse = await Client.PostAsJsonAsync("/api/usuario-emissora/criar", new UsuarioEmissoraCreateViewModel
        {
            UsuarioId = TestData.UsuarioAdminId,
            EmissoraId = TestData.EmissoraFatoPopularId,
            FuncaoId = TestData.FuncaoAdministradorId
        });
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        var createdId = await WithDbContextAsync(async db =>
            await db.UsuarioEmissora
                .Where(ue => ue.UsuarioId == TestData.UsuarioAdminId && ue.EmissoraId == TestData.EmissoraFatoPopularId)
                .OrderByDescending(ue => ue.Id)
                .Select(ue => ue.Id)
                .FirstAsync());

        var deleteResponse = await Client.DeleteAsync($"/api/usuario-emissora/{createdId}/deletar");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }

    [Fact]
    public async Task EmissoraRegiaoController_DeveExecutarCrudBasico()
    {
        var listResponse = await Client.GetAsync("/api/emissora-regiao/buscarTodos");
        Assert.Equal(HttpStatusCode.OK, listResponse.StatusCode);

        var createResponse = await Client.PostAsJsonAsync("/api/emissora-regiao/criar", new EmissoraRegiaoCreateViewModel
        {
            EmissoraId = TestData.EmissoraRadio88Id,
            RegiaoId = TestData.RegiaoId
        });
        Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

        var createdId = await WithDbContextAsync(async db =>
            await db.EmissoraRegiao
                .Where(er => er.EmissoraId == TestData.EmissoraRadio88Id && er.RegiaoId == TestData.RegiaoId)
                .OrderByDescending(er => er.Id)
                .Select(er => er.Id)
                .FirstAsync());

        var deleteResponse = await Client.DeleteAsync($"/api/emissora-regiao/{createdId}/deletar");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }
}
