using PortalGtf.Application.ViewModels.EmissoraVM;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Application.Services.EmissoraServices;

public class EmissoraService : IEmissoraService
{
    private readonly IEmissoraRepository _emissoraRepository;
    
    public EmissoraService(IEmissoraRepository emissoraRepository)
    {
        _emissoraRepository = emissoraRepository;
    }
    public async Task<List<EmissoraViewModel>> GetAllAsync()
    {
        var emissoras = await _emissoraRepository.GetAllAsync();
        
        return emissoras.Select(e => new EmissoraViewModel
        {
            Id = e.Id,
            NomeSocial =  e.NomeSocial,
            RazaoSocial =  e.RazaoSocial,
            Cep = e.Cep,
            Endereco = e.Endereco,
            Numero = e.Numero,
            Bairro = e.Bairro,
            Estado = e.Estado,
            Cidade = e.Cidade,
            Ativa = e.Ativa
        }).ToList();
    }
    public async Task<EmissoraViewModel?> GetByIdAsync(int id)
    {
        var emissora = await _emissoraRepository.GetByIdAsync(id);
        
        if (emissora == null)
            return null;

        return new EmissoraViewModel
        {
            Id = emissora.Id,
            NomeSocial = emissora.NomeSocial,
            RazaoSocial = emissora.RazaoSocial,
            Cep = emissora.Cep,
            Endereco = emissora.Endereco,
            Numero = emissora.Numero,
            Bairro = emissora.Bairro,
            Estado = emissora.Estado,
            Cidade = emissora.Cidade,
            Ativa = emissora.Ativa
        };
    }

    public async Task<int> CreateAsync(EmissoraCreateViewModel model)
    {
        var emissora = new Emissora
        {
            NomeSocial = model.NomeSocial,
            RazaoSocial = model.RazaoSocial,
            Cep = model.Cep,
            Endereco = model.Endereco,
            Numero = model.Numero,
            Bairro = model.Bairro,
            Estado = model.Estado,
            Cidade = model.Cidade,
            Ativa = model.Ativa
        };
        await _emissoraRepository.AddAsync(emissora);
        return emissora.Id;
    }

    public async Task<bool> UpdateAsync(int id, EmissoraCreateViewModel model)
    {
        var emissora = await _emissoraRepository.GetByIdAsync(id);  
        if (emissora == null)
            return false;
        
        emissora.NomeSocial = model.NomeSocial;
        emissora.RazaoSocial = model.RazaoSocial;
        emissora.Cep = model.Cep;
        emissora.Endereco = model.Endereco;
        emissora.Numero = model.Numero;
        emissora.Bairro = model.Bairro;
        emissora.Estado = model.Estado;
        emissora.Cidade = model.Cidade;
        emissora.Ativa = model.Ativa;
        
        await _emissoraRepository.UpdateAsync(emissora);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var emissora = await _emissoraRepository.GetByIdAsync(id);
        
        if(emissora == null)
            return false;
        
        await _emissoraRepository.DeleteAsync(emissora);
        return true;
    }
}