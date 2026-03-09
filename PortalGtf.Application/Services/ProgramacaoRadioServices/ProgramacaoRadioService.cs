using PortalGtf.Application.ViewModels.ProgramacaoVM;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Enums;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Application.Services.ProgramacaoRadioServices;

public class ProgramacaoRadioService : IProgramacaoRadioService
{
    private readonly IProgramacaoRadiorRepository _repository;

    public ProgramacaoRadioService(IProgramacaoRadiorRepository repository)
    {
        _repository = repository;
    }
    public async Task<List<ProgramacaoRadioViewModel>> GetAllAsync()
    {
        var programacoes = await _repository.GetAllAsync();

        return programacoes.Select(p => new ProgramacaoRadioViewModel
        {
            Id = p.Id,
            NomePrograma = p.NomePrograma,
            Apresentador = p.Apresentador,
            Descricao = p.Descricao,
            DiaSemana = (int)p.DiaSemana,
            HoraInicio = p.HoraInicio,
            HoraFim = p.HoraFim,
            Imagem = p.Imagem,
            Ativo = p.Ativo,
        }).ToList();
    }

    public async Task<ProgramacaoRadioViewModel?> GetByIdAsync(int id)
    {
        var programaca = await _repository.GetByIdAsync(id);

        if (programaca == null)
            return null;

        return new ProgramacaoRadioViewModel
        {
            Id = programaca.Id,
            NomePrograma = programaca.NomePrograma,
            Apresentador = programaca.Apresentador,
            Descricao = programaca.Descricao,
            DiaSemana = (int)programaca.DiaSemana,
            HoraInicio = programaca.HoraInicio,
            HoraFim = programaca.HoraFim,
            Imagem = programaca.Imagem,
            Ativo = programaca.Ativo,
        };
    }

    public async Task CreateAsync(ProgramacaoRadioCreateViewModel model)
    {
        var programacao = new ProgramacaoRadio
        {
            NomePrograma = model.NomePrograma,
            Apresentador = model.Apresentador,
            Descricao = model.Descricao,
            DiaSemana = (DiaSemanaEnum)model.DiaSemana,
            HoraInicio = model.HoraInicio,
            HoraFim = model.HoraFim,
            Imagem = model.Imagem,
            Ativo = true
        };

        await _repository.AddAsync(programacao);
    }
    public async Task UpdateAsync(int id, ProgramacaoRadioUpdateViewModel model)
    {
        var programacao = await _repository.GetByIdAsync(id);

        if (programacao == null)
            throw new Exception("Programação não encontrada.");

        programacao.NomePrograma = model.NomePrograma;
        programacao.Apresentador = model.Apresentador;
        programacao.Descricao = model.Descricao;
        programacao.DiaSemana = (DiaSemanaEnum)model.DiaSemana;
        programacao.HoraInicio = model.HoraInicio;
        programacao.HoraFim = model.HoraFim;
        programacao.Imagem = model.Imagem;
        programacao.Ativo = model.Ativo;

        await _repository.UpdateAsync(programacao);
    }
    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}