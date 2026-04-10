using System.Net;
using System.Net.Http.Headers;
using PortalGtf.Application.ViewModels.MidiaVM;
using PortalGtf.Application.ViewModels.PostsVM;
using PortalGtf.Tests.Infrastructure;

namespace PortalGtf.Tests.Integration;

public class MediaControllerTests : ApiIntegrationTestBase
{
    public MediaControllerTests(TestWebApplicationFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task MediaController_DeveListarMidiasPaginadas()
    {
        var response = await Client.GetAsync("/api/media?page=1&pageSize=10");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var payload = await ReadAsync<PagedResult<MidiaDto>>(response);
        Assert.NotNull(payload);
        Assert.NotEmpty(payload!.Data);
    }

    [Fact]
    public async Task MediaController_DeveFazerUploadEDownload()
    {
        using var form = new MultipartFormDataContent();
        var bytes = new ByteArrayContent("arquivo-de-teste"u8.ToArray());
        bytes.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
        form.Add(bytes, "file", "teste-upload.jpg");

        var uploadResponse = await Client.PostAsync($"/api/media/upload?usuarioId={TestData.UsuarioAdminId}", form);
        Assert.Equal(HttpStatusCode.OK, uploadResponse.StatusCode);

        var uploaded = await ReadAsync<MidiaDto>(uploadResponse);
        Assert.NotNull(uploaded);
        Assert.True(uploaded!.Id > 0);

        var downloadResponse = await Client.GetAsync($"/api/media/{uploaded.Id}/download");
        Assert.Equal(HttpStatusCode.OK, downloadResponse.StatusCode);
        Assert.Equal("image/jpeg", downloadResponse.Content.Headers.ContentType?.MediaType);
    }

    [Fact]
    public async Task MediaController_DeveBaixarMidiaSeedEDeletarMidiaCriada()
    {
        var seededDownload = await Client.GetAsync($"/api/media/{TestData.MidiaImagemId}/download");
        Assert.Equal(HttpStatusCode.OK, seededDownload.StatusCode);

        using var form = new MultipartFormDataContent();
        var bytes = new ByteArrayContent("arquivo-para-delete"u8.ToArray());
        bytes.Headers.ContentType = MediaTypeHeaderValue.Parse("image/png");
        form.Add(bytes, "file", "delete-upload.png");

        var uploadResponse = await Client.PostAsync($"/api/media/upload?usuarioId={TestData.UsuarioAdminId}", form);
        var uploaded = await ReadAsync<MidiaDto>(uploadResponse);

        var deleteResponse = await Client.DeleteAsync($"/api/media/{uploaded!.Id}");
        Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
    }
}
