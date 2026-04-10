using PortalGtf.Core.Enums;

namespace PortalGtf.Core.Entities;

public class ProgramacaoRadio
{
    public int Id { get; set; }
    public int EmissoraId { get; set; }
    public Emissora Emissora { get; set; } = null!;
    public string NomePrograma { get; set; }
    public string? Apresentador { get; set; }
    public string? Descricao { get; set; }
    public DiaSemanaEnum DiaSemana { get; set; }
    public TimeSpan HoraInicio { get; set; }
    public TimeSpan HoraFim { get; set; }
    public string? Imagem { get; set; }
    public bool Ativo { get; set; }
}
