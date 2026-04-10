using System.Security.Cryptography;
using System.Text;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Enums;

namespace PortalGtf.Tests.Infrastructure;

public static class TestDataSeeder
{
    public static async Task SeedAsync(PortalGtfNewsDbContext context)
    {
        if (context.Regiao.Any())
            return;

        await SeedLocalFilesAsync();

        var regiao = new Regiao
        {
            Id = TestData.RegiaoId,
            Nome = "Sudeste"
        };

        var estado = new Estado
        {
            Id = TestData.EstadoId,
            Nome = "Rio de Janeiro",
            Sigla = "RJ",
            RegiaoId = TestData.RegiaoId
        };

        var cidade = new Cidade
        {
            Id = TestData.CidadeId,
            Nome = "Rio de Janeiro",
            EstadoId = TestData.EstadoId
        };

        var cidadeSecundaria = new Cidade
        {
            Id = TestData.CidadeSecundariaId,
            Nome = "Niterói",
            EstadoId = TestData.EstadoId
        };

        var funcaoAdmin = new Funcao
        {
            Id = TestData.FuncaoAdministradorId,
            TipoFuncao = "Administrador"
        };

        var funcaoRedator = new Funcao
        {
            Id = TestData.FuncaoRedatorId,
            TipoFuncao = "Redator"
        };

        var permissaoPosts = new Permissao
        {
            Id = TestData.PermissaoPostsId,
            TipoPermissao = "posts.manage"
        };

        var permissaoUsuarios = new Permissao
        {
            Id = TestData.PermissaoUsuariosId,
            TipoPermissao = "usuarios.manage"
        };

        var usuarioAdmin = new Usuario
        {
            Id = TestData.UsuarioAdminId,
            Email = "admin@portalgtf.com",
            NomeCompleto = "Administrador Teste",
            SenhaHash = Sha256("123456"),
            StatusUsuario = StatusUsuario.Ativo,
            FuncaoId = TestData.FuncaoAdministradorId,
            DataCriacao = DateTime.UtcNow.AddDays(-10)
        };

        var usuarioRedator = new Usuario
        {
            Id = TestData.UsuarioRedatorId,
            Email = "redator@portalgtf.com",
            NomeCompleto = "Redator Teste",
            SenhaHash = Sha256("123456"),
            StatusUsuario = StatusUsuario.Ativo,
            FuncaoId = TestData.FuncaoRedatorId,
            DataCriacao = DateTime.UtcNow.AddDays(-9)
        };

        var emissoraRadio = new Emissora
        {
            Id = TestData.EmissoraRadio88Id,
            NomeSocial = "Radio 88 FM",
            RazaoSocial = "Radio 88 FM LTDA",
            Cep = "20000-000",
            Endereco = "Rua da Radio",
            Numero = "88",
            Bairro = "Centro",
            Estado = "RJ",
            Cidade = "Rio de Janeiro",
            Slug = "radio-88-fm",
            Logo = "radio88.png",
            LogoSmall = "radio88-small.png",
            TemaPrincipal = "#ffb400",
            Ativa = true
        };

        var emissoraFato = new Emissora
        {
            Id = TestData.EmissoraFatoPopularId,
            NomeSocial = "Fato Popular",
            RazaoSocial = "Fato Popular LTDA",
            Cep = "20000-001",
            Endereco = "Rua do Portal",
            Numero = "100",
            Bairro = "Centro",
            Estado = "RJ",
            Cidade = "Rio de Janeiro",
            Slug = "fato-popular-88-fm",
            Logo = "fatopopular.png",
            LogoSmall = "fatopopular-small.png",
            TemaPrincipal = "#e53935",
            Ativa = true
        };

        var temaNoticias = new TemaEditorial
        {
            Id = TestData.TemaNoticiasId,
            Descricao = "Noticias",
            CorPrimaria = "#E83C25",
            CorSecundaria = "#E83C25",
            CorFonte = "#FFFFFF",
            Logo = "noticias.png"
        };

        var temaReceitas = new TemaEditorial
        {
            Id = TestData.TemaReceitasId,
            Descricao = "Receitas",
            CorPrimaria = "#06AA48",
            CorSecundaria = "#06AA48",
            CorFonte = "#FFFFFF",
            Logo = "receitas.png"
        };

        var temaFatoPopular = new TemaEditorial
        {
            Id = TestData.TemaFatoPopularId,
            Descricao = "Fato Popular",
            CorPrimaria = "#132D52",
            CorSecundaria = "#132D52",
            CorFonte = "#FFFFFF",
            Logo = "fato.png"
        };

        var editorialNoticias = new Editorial
        {
            Id = TestData.EditorialNoticiasId,
            TipoPostagem = "Notícias",
            TemaEditorialId = TestData.TemaNoticiasId,
            EmissoraId = TestData.EmissoraFatoPopularId
        };

        var editorialReceitas = new Editorial
        {
            Id = TestData.EditorialReceitasId,
            TipoPostagem = "Receitas",
            TemaEditorialId = TestData.TemaReceitasId,
            EmissoraId = TestData.EmissoraRadio88Id
        };

        var editorialFatoPopularRadio = new Editorial
        {
            Id = TestData.EditorialFatoPopularRadioId,
            TipoPostagem = "Fato Popular",
            TemaEditorialId = TestData.TemaFatoPopularId,
            EmissoraId = TestData.EmissoraRadio88Id
        };

        var subcategoriaNoticias = new Subcategoria
        {
            Id = TestData.SubcategoriaNoticiasId,
            Nome = "Geral",
            Slug = "geral",
            EditorialId = TestData.EditorialNoticiasId
        };

        var subcategoriaReceitas = new Subcategoria
        {
            Id = TestData.SubcategoriaReceitasId,
            Nome = "Doces",
            Slug = "doces",
            EditorialId = TestData.EditorialReceitasId
        };

        var midiaImagem = new Midia
        {
            Id = TestData.MidiaImagemId,
            NomeOriginal = "imagem.jpg",
            NomeArquivo = "imagem-seed.jpg",
            Url = "http://localhost/uploads/imagem-seed.jpg",
            Tipo = TipoMidia.Imagem,
            DataUpload = DateTime.UtcNow.AddDays(-5),
            UsuarioUploadId = TestData.UsuarioAdminId
        };

        var midiaBanner = new Midia
        {
            Id = TestData.MidiaBannerId,
            NomeOriginal = "banner.jpg",
            NomeArquivo = "banner-seed.jpg",
            Url = "http://localhost/uploads/banner-seed.jpg",
            Tipo = TipoMidia.Imagem,
            DataUpload = DateTime.UtcNow.AddDays(-4),
            UsuarioUploadId = TestData.UsuarioAdminId
        };

        var bannerHome = new BannerInstitucional
        {
            Id = TestData.BannerHomeId,
            Titulo = "Banner Hero",
            EmissoraId = TestData.EmissoraRadio88Id,
            MidiaId = TestData.MidiaBannerId,
            LinkUrl = "https://example.com/banner",
            NovaAba = true,
            Posicao = "home",
            Ordem = 1,
            Ativo = true,
            DataCriacao = DateTime.UtcNow.AddDays(-3)
        };

        var streaming = new Streaming
        {
            Id = TestData.StreamingRadio88Id,
            EmissoraId = TestData.EmissoraRadio88Id,
            Url = "https://stream.example.com",
            Porta = "8000",
            TipoStream = "aac",
            LinkApi = "https://api.example.com"
        };

        var programacao = new ProgramacaoRadio
        {
            Id = TestData.ProgramacaoRadio88Id,
            EmissoraId = TestData.EmissoraRadio88Id,
            NomePrograma = "Manhã 88",
            Apresentador = "Equipe 88",
            Descricao = "Programa matinal",
            DiaSemana = DiaSemanaEnum.Segunda,
            HoraInicio = new TimeSpan(8, 0, 0),
            HoraFim = new TimeSpan(10, 0, 0),
            Imagem = "manha88.png",
            Ativo = true
        };

        var tagNoticias = new Tag
        {
            Id = 1,
            Nome = "Portal",
            Slug = "portal"
        };

        var tagReceitas = new Tag
        {
            Id = 2,
            Nome = "Receita",
            Slug = "receita"
        };

        var postPublicadoFato = new Post
        {
            Id = TestData.PostPublicadoFatoId,
            Titulo = "Post publicado Fato Popular",
            Subtitulo = "Subtítulo do portal de notícias",
            Conteudo = "<p>Conteúdo do post publicado.</p>",
            Slug = "post-publicado-fato-popular",
            EditorialId = TestData.EditorialNoticiasId,
            EmissoraId = TestData.EmissoraFatoPopularId,
            CidadeId = TestData.CidadeId,
            SubcategoriaId = TestData.SubcategoriaNoticiasId,
            UsuarioCriacaoId = TestData.UsuarioAdminId,
            StatusPost = StatusPost.Publicado,
            DataCriacao = DateTime.UtcNow.AddDays(-2),
            DataAprovacao = DateTime.UtcNow.AddDays(-2),
            PublicadoEm = DateTime.UtcNow.AddDays(-2),
            Destaque = true,
            ImagemCapaId = TestData.MidiaImagemId
        };

        var postPublicadoRadio = new Post
        {
            Id = TestData.PostPublicadoRadioId,
            Titulo = "Receita da 88 FM",
            Subtitulo = "Subtítulo da receita",
            Conteudo = "<p>Conteúdo de receita.</p>",
            Slug = "receita-da-88-fm",
            EditorialId = TestData.EditorialReceitasId,
            EmissoraId = TestData.EmissoraRadio88Id,
            CidadeId = TestData.CidadeId,
            SubcategoriaId = TestData.SubcategoriaReceitasId,
            UsuarioCriacaoId = TestData.UsuarioRedatorId,
            StatusPost = StatusPost.Publicado,
            DataCriacao = DateTime.UtcNow.AddDays(-1),
            DataAprovacao = DateTime.UtcNow.AddDays(-1),
            PublicadoEm = DateTime.UtcNow.AddDays(-1),
            Destaque = true,
            ImagemCapaId = TestData.MidiaImagemId
        };

        var postRascunho = new Post
        {
            Id = TestData.PostRascunhoId,
            Titulo = "Rascunho 88",
            Subtitulo = "Subtítulo rascunho",
            Conteudo = "<p>Rascunho.</p>",
            Slug = "rascunho-88",
            EditorialId = TestData.EditorialReceitasId,
            EmissoraId = TestData.EmissoraRadio88Id,
            CidadeId = TestData.CidadeId,
            SubcategoriaId = TestData.SubcategoriaReceitasId,
            UsuarioCriacaoId = TestData.UsuarioRedatorId,
            StatusPost = StatusPost.Rascunho,
            DataCriacao = DateTime.UtcNow.AddHours(-10),
            Destaque = false,
            ImagemCapaId = TestData.MidiaImagemId
        };

        var postEmRevisao = new Post
        {
            Id = TestData.PostEmRevisaoId,
            Titulo = "Post em revisão",
            Subtitulo = "Subtítulo revisão",
            Conteudo = "<p>Em revisão.</p>",
            Slug = "post-em-revisao",
            EditorialId = TestData.EditorialReceitasId,
            EmissoraId = TestData.EmissoraRadio88Id,
            CidadeId = TestData.CidadeId,
            SubcategoriaId = TestData.SubcategoriaReceitasId,
            UsuarioCriacaoId = TestData.UsuarioRedatorId,
            StatusPost = StatusPost.EmRevisao,
            DataCriacao = DateTime.UtcNow.AddHours(-8),
            DataEdicao = DateTime.UtcNow.AddHours(-6),
            Destaque = false,
            ImagemCapaId = TestData.MidiaImagemId
        };

        var postParaAprovacao = new Post
        {
            Id = TestData.PostParaAprovacaoId,
            Titulo = "Post para aprovação",
            Subtitulo = "Subtítulo aprovação",
            Conteudo = "<p>Para aprovação.</p>",
            Slug = "post-para-aprovacao",
            EditorialId = TestData.EditorialReceitasId,
            EmissoraId = TestData.EmissoraRadio88Id,
            CidadeId = TestData.CidadeId,
            SubcategoriaId = TestData.SubcategoriaReceitasId,
            UsuarioCriacaoId = TestData.UsuarioRedatorId,
            StatusPost = StatusPost.ParaAprovacao,
            DataCriacao = DateTime.UtcNow.AddHours(-5),
            DataEdicao = DateTime.UtcNow.AddHours(-4),
            Destaque = false,
            ImagemCapaId = TestData.MidiaImagemId
        };

        var postRejeitado = new Post
        {
            Id = TestData.PostRejeitadoId,
            Titulo = "Post rejeitado",
            Subtitulo = "Subtítulo rejeitado",
            Conteudo = "<p>Rejeitado.</p>",
            Slug = "post-rejeitado",
            EditorialId = TestData.EditorialReceitasId,
            EmissoraId = TestData.EmissoraRadio88Id,
            CidadeId = TestData.CidadeId,
            SubcategoriaId = TestData.SubcategoriaReceitasId,
            UsuarioCriacaoId = TestData.UsuarioRedatorId,
            StatusPost = StatusPost.Rejeitado,
            DataCriacao = DateTime.UtcNow.AddHours(-3),
            DataEdicao = DateTime.UtcNow.AddHours(-2),
            Destaque = false,
            ImagemCapaId = TestData.MidiaImagemId
        };

        var funcaoPermissoes = new[]
        {
            new FuncaoPermissao { Id = 1, FuncaoId = TestData.FuncaoAdministradorId, PermissaoId = TestData.PermissaoPostsId },
            new FuncaoPermissao { Id = 2, FuncaoId = TestData.FuncaoAdministradorId, PermissaoId = TestData.PermissaoUsuariosId }
        };

        var emissoraRegioes = new[]
        {
            new EmissoraRegiao { Id = 1, EmissoraId = TestData.EmissoraRadio88Id, RegiaoId = TestData.RegiaoId },
            new EmissoraRegiao { Id = 2, EmissoraId = TestData.EmissoraFatoPopularId, RegiaoId = TestData.RegiaoId }
        };

        var usuarioEmissoras = new[]
        {
            new UsuarioEmissora { Id = 1, UsuarioId = TestData.UsuarioRedatorId, EmissoraId = TestData.EmissoraRadio88Id, FuncaoId = TestData.FuncaoRedatorId }
        };

        var postTags = new[]
        {
            new PostTag { PostId = TestData.PostPublicadoFatoId, TagId = 1 },
            new PostTag { PostId = TestData.PostPublicadoRadioId, TagId = 2 }
        };

        var historicos = new[]
        {
            new PostHistorico
            {
                Id = 1,
                PostId = TestData.PostEmRevisaoId,
                UsuarioId = TestData.UsuarioAdminId,
                Acao = "ENVIADO_PARA_REVISAO",
                Mensagem = "Ajustar subtítulo e legenda da foto.",
                DataAcao = DateTime.UtcNow.AddHours(-6)
            }
        };

        var visualizacoes = new[]
        {
            new PostVisualizacao { Id = 1, PostId = TestData.PostPublicadoRadioId, Ip = "127.0.0.1", DataVisualizacao = DateTime.UtcNow.AddHours(-1) },
            new PostVisualizacao { Id = 2, PostId = TestData.PostPublicadoRadioId, Ip = "127.0.0.2", DataVisualizacao = DateTime.UtcNow.AddHours(-2) },
            new PostVisualizacao { Id = 3, PostId = TestData.PostPublicadoFatoId, Ip = "127.0.0.3", DataVisualizacao = DateTime.UtcNow.AddHours(-1) }
        };

        context.Regiao.Add(regiao);
        context.Estado.Add(estado);
        context.Cidade.AddRange(cidade, cidadeSecundaria);
        context.Funcao.AddRange(funcaoAdmin, funcaoRedator);
        context.Permissao.AddRange(permissaoPosts, permissaoUsuarios);
        context.Usuario.AddRange(usuarioAdmin, usuarioRedator);
        context.Emissora.AddRange(emissoraRadio, emissoraFato);
        context.TemaEditorial.AddRange(temaNoticias, temaReceitas, temaFatoPopular);
        context.Editorial.AddRange(editorialNoticias, editorialReceitas, editorialFatoPopularRadio);
        context.Subcategoria.AddRange(subcategoriaNoticias, subcategoriaReceitas);
        context.Midia.AddRange(midiaImagem, midiaBanner);
        context.BannerInstitucional.Add(bannerHome);
        context.Streaming.Add(streaming);
        context.ProgramacaoRadio.Add(programacao);
        context.Tag.AddRange(tagNoticias, tagReceitas);
        context.Post.AddRange(postPublicadoFato, postPublicadoRadio, postRascunho, postEmRevisao, postParaAprovacao, postRejeitado);
        context.FuncaoPermissao.AddRange(funcaoPermissoes);
        context.EmissoraRegiao.AddRange(emissoraRegioes);
        context.UsuarioEmissora.AddRange(usuarioEmissoras);
        context.PostTag.AddRange(postTags);
        context.PostHistorico.AddRange(historicos);
        context.PostVisualizacao.AddRange(visualizacoes);

        await context.SaveChangesAsync();
    }

    private static async Task SeedLocalFilesAsync()
    {
        var uploadsPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
        Directory.CreateDirectory(uploadsPath);

        await File.WriteAllBytesAsync(Path.Combine(uploadsPath, "imagem-seed.jpg"), "seed-image"u8.ToArray());
        await File.WriteAllBytesAsync(Path.Combine(uploadsPath, "banner-seed.jpg"), "seed-banner"u8.ToArray());
    }

    private static string Sha256(string input)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
        var builder = new StringBuilder();
        foreach (var value in bytes)
            builder.Append(value.ToString("x2"));
        return builder.ToString();
    }
}
