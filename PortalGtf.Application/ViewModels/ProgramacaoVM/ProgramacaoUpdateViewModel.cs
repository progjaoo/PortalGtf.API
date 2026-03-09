namespace PortalGtf.Application.ViewModels.ProgramacaoVM;

public class ProgramacaoRadioUpdateViewModel
{
    public string NomePrograma { get; set; }

    public string? Apresentador { get; set; }

    public string? Descricao { get; set; }

    public int DiaSemana { get; set; }

    public TimeSpan HoraInicio { get; set; }

    public TimeSpan HoraFim { get; set; }

    public bool Ativo { get; set; }

    public string? Imagem { get; set; }
}