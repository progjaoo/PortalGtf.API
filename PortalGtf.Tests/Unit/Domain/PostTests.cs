using PortalGtf.Core.Entities;
using PortalGtf.Core.Enums;

namespace PortalGtf.Tests.Unit.Domain;

public class PostTests
{
    [Fact]
    public void Aprovar_DevePublicarPostQuandoEstiverParaAprovacao()
    {
        var post = new Post
        {
            StatusPost = StatusPost.ParaAprovacao
        };

        post.Aprovar();

        Assert.Equal(StatusPost.Publicado, post.StatusPost);
        Assert.NotNull(post.DataAprovacao);
        Assert.NotNull(post.PublicadoEm);
    }

    [Fact]
    public void Aprovar_DeveLancarExcecaoQuandoStatusNaoPermite()
    {
        var post = new Post
        {
            StatusPost = StatusPost.Publicado
        };

        var exception = Assert.Throws<InvalidOperationException>(() => post.Aprovar());

        Assert.Equal("Post não pode ser aprovado.", exception.Message);
    }

    [Fact]
    public void EnviarParaAprovacao_DeveMoverRascunhoParaFila()
    {
        var post = new Post
        {
            StatusPost = StatusPost.Rascunho
        };

        post.EnviarParaAprovacao();

        Assert.Equal(StatusPost.ParaAprovacao, post.StatusPost);
    }
}
