using Microsoft.EntityFrameworkCore;

namespace PortalGtf.Core.Entities;

public class PortalGtfNewsDbContext : DbContext
{
    public PortalGtfNewsDbContext(DbContextOptions<PortalGtfNewsDbContext> options)
        : base(options) { }

    public DbSet<Cidade> Cidade => Set<Cidade>();
    public DbSet<Comentario> Comentario => Set<Comentario>();
    public DbSet<Editorial> Editoriai => Set<Editorial>();
    public DbSet<Emissora> Emissora => Set<Emissora>();
    public DbSet<EmissoraRegiao> EmissoraRegioe => Set<EmissoraRegiao>();
    public DbSet<Estado> Estado => Set<Estado>();
    public DbSet<Funcao> Funcoe => Set<Funcao>();
    public DbSet<FuncaoPermissao> FuncaoPermissoe => Set<FuncaoPermissao>();
    public DbSet<Newsletter> Newsletter => Set<Newsletter>();
    public DbSet<Notificacao> Notificacoe => Set<Notificacao>();
    public DbSet<Permissao> Permissoe => Set<Permissao>();
    public DbSet<Post> Post => Set<Post>();
    public DbSet<PostComentario> PostComentario => Set<PostComentario>();
    public DbSet<PostHistorico> PostHistorico => Set<PostHistorico>();
    public DbSet<PostImagem> PostImagen => Set<PostImagem>();
    public DbSet<PostTag> PostTag => Set<PostTag>();
    public DbSet<PostVisualizacao> PostVisualizacoe => Set<PostVisualizacao>();
    public DbSet<Regiao> Regiao => Set<Regiao>();
    public DbSet<Streaming> Streaming => Set<Streaming>();
    public DbSet<Tag> Tag => Set<Tag>();
    public DbSet<TemaEditorial> TemaEditorial => Set<TemaEditorial>();
    public DbSet<Usuario> Usuario => Set<Usuario>();
    public DbSet<UsuarioEmissora> UsuarioEmissora => Set<UsuarioEmissora>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<FuncaoPermissao>()
            .HasKey(fp => new { fp.FuncaoId, fp.PermissaoId });

        modelBuilder.Entity<FuncaoPermissao>()
            .HasOne(fp => fp.Funcao)
            .WithMany(f => f.FuncaoPermissoes)
            .HasForeignKey(fp => fp.FuncaoId);

        modelBuilder.Entity<FuncaoPermissao>()
            .HasOne(fp => fp.Permissao)
            .WithMany(p => p.FuncaoPermissoes)
            .HasForeignKey(fp => fp.PermissaoId);

        /* ============================
         * USUÁRIO
         * ============================ */
        modelBuilder.Entity<Usuario>()
            .HasOne(u => u.Funcao)
            .WithMany(f => f.Usuarios)
            .HasForeignKey(u => u.FuncaoId);
        /* ============================
         * USUÁRIO / EMISSORA
         * ============================ */
        modelBuilder.Entity<UsuarioEmissora>()
            .HasOne(ue => ue.Usuario)
            .WithMany(u => u.UsuarioEmissoras)
            .HasForeignKey(ue => ue.UsuarioId);

        modelBuilder.Entity<UsuarioEmissora>()
            .HasOne(ue => ue.Emissora)
            .WithMany(e => e.UsuarioEmissoras)
            .HasForeignKey(ue => ue.EmissoraId);

        modelBuilder.Entity<UsuarioEmissora>()
            .HasOne(ue => ue.Funcao)
            .WithMany()
            .HasForeignKey(ue => ue.FuncaoId);
        
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.Property(u => u.StatusUsuario)
                .HasConversion<int>()
                .IsRequired();
        });
        
        /* ============================
         * LOCALIZAÇÃO
         * ============================ */
        modelBuilder.Entity<Cidade>()
            .HasOne(c => c.Estado)
            .WithMany(e => e.Cidades)
            .HasForeignKey(c => c.EstadoId);

        modelBuilder.Entity<Cidade>()
            .HasOne(c => c.Regiao)
            .WithMany(r => r.Cidades)
            .HasForeignKey(c => c.RegiaoId)
            .OnDelete(DeleteBehavior.Restrict);

        /* ============================
         * EMISSORA / REGIÃO (N:N)
         * ============================ */
        modelBuilder.Entity<EmissoraRegiao>()
            .HasKey(er => new { er.EmissoraId, er.RegiaoId });

        modelBuilder.Entity<EmissoraRegiao>()
            .HasOne(er => er.Emissora)
            .WithMany(e => e.EmissoraRegioes)
            .HasForeignKey(er => er.EmissoraId);

        modelBuilder.Entity<EmissoraRegiao>()
            .HasOne(er => er.Regiao)
            .WithMany(r => r.EmissoraRegioes)
            .HasForeignKey(er => er.RegiaoId);

        /* ============================
         * STREAMING
         * ============================ */
        modelBuilder.Entity<Streaming>()
            .HasOne(s => s.Emissora)
            .WithMany(e => e.Streamings)
            .HasForeignKey(s => s.EmissoraId);

        /* ============================
         * POST
         * ============================ */
        modelBuilder.Entity<Post>()
            .HasOne(p => p.Editorial)
            .WithMany(e => e.Posts)
            .HasForeignKey(p => p.EditorialId);

        modelBuilder.Entity<Post>()
            .HasOne(p => p.Emissora)
            .WithMany(e => e.Posts)
            .HasForeignKey(p => p.EmissoraId);

        modelBuilder.Entity<Post>()
            .HasOne(p => p.Cidade)
            .WithMany(c => c.Posts)
            .HasForeignKey(p => p.CidadeId);

        modelBuilder.Entity<Post>()
            .HasOne(p => p.UsuarioCriacao)
            .WithMany()
            .HasForeignKey(p => p.UsuarioCriacaoId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Post>()
            .HasOne(p => p.UsuarioAprovacao)
            .WithMany()
            .HasForeignKey(p => p.UsuarioAprovacaoId)
            .OnDelete(DeleteBehavior.Restrict);

        /* ============================
         * POST / TAG (N:N)
         * ============================ */
        modelBuilder.Entity<PostTag>()
            .HasKey(pt => new { pt.PostId, pt.TagId });

        modelBuilder.Entity<PostTag>()
            .HasOne(pt => pt.Post)
            .WithMany(p => p.PostTags)
            .HasForeignKey(pt => pt.PostId);

        modelBuilder.Entity<PostTag>()
            .HasOne(pt => pt.Tag)
            .WithMany(t => t.PostTags)
            .HasForeignKey(pt => pt.TagId);

        /* ============================
         * POST IMAGEM
         * ============================ */
        modelBuilder.Entity<PostImagem>()
            .HasOne(pi => pi.Post)
            .WithMany(p => p.Imagens)
            .HasForeignKey(pi => pi.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        /* ============================
         * POST HISTÓRICO
         * ============================ */
        
        /* modelBuilder.Entity<PostHistorico>()
            .HasOne(ph => ph.Post)
            .WithMany(p => p.Historicos)
            .HasForeignKey(ph => ph.PostId);*/

        modelBuilder.Entity<PostHistorico>()
            .HasOne(ph => ph.Usuario)
            .WithMany()
            .HasForeignKey(ph => ph.UsuarioId);

        /* ============================
         * COMENTÁRIO
         * ============================ */
        modelBuilder.Entity<Comentario>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.Property(c => c.Conteudo)
                .IsRequired()
                .HasMaxLength(1000);

            entity.Property(c => c.Status)
                .IsRequired()
                .HasMaxLength(20);

            entity.HasOne(c => c.Post)
                .WithMany(p => p.Comentarios) 
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(c => c.Usuario)
                .WithMany()
                .HasForeignKey(c => c.UsuarioId)
                .OnDelete(DeleteBehavior.SetNull);
        });


        modelBuilder.Entity<Comentario>()
            .HasOne(c => c.Usuario)
            .WithMany()
            .HasForeignKey(c => c.UsuarioId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<PostComentario>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasOne(e => e.Post)
                .WithMany(p => p.PostComentarios)
                .HasForeignKey(e => e.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.Comentario)
                .WithMany(c => c.PostComentarios)
                .HasForeignKey(e => e.ComentarioId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        /* ============================
         * NOTIFICAÇÃO
         * ============================ */
       /* modelBuilder.Entity<Notificacao>()
            .HasOne(n => n.Usuario)
            .WithMany(u => u.Notificacoes)
            .HasForeignKey(n => n.UsuarioId);*/

        /* ============================
         * POST VISUALIZAÇÃO
         * ============================ */
        modelBuilder.Entity<PostVisualizacao>()
            .HasOne(pv => pv.Post)
            .WithMany(p => p.Visualizacoes)
            .HasForeignKey(pv => pv.PostId);
    }
}