using PortalGtf.Core.Enums;

namespace PortalGtf.Core.Entities;

public class Usuario
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public string NomeCompleto { get; set; } = null!;
    public string SenhaHash { get; set; } = null!;
    public StatusUsuario StatusUsuario { get; set; }
    public int FuncaoId { get; set; }
    public Funcao Funcao { get; set; } = null!;

    public DateTime DataCriacao { get; set; }

    public ICollection<UsuarioEmissora> UsuarioEmissoras { get; set; } = new List<UsuarioEmissora>();

    public void AtivarUsuario()
    {
        if (StatusUsuario != StatusUsuario.Pendente && StatusUsuario != StatusUsuario.Bloqueado &&
            StatusUsuario != StatusUsuario.Inativo)
            throw new InvalidOperationException("Usuário não pode ser ativado");
        
        StatusUsuario = StatusUsuario.Ativo;
    }

    public void DesativarUsuario()
    {
    
        StatusUsuario = StatusUsuario.Bloqueado;
    }
}