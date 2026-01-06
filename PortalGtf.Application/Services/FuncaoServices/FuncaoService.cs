using PortalGtf.Application.ViewModels.FuncaoVM;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Application.Services.FuncaoServices;

public class FuncaoService : IFuncaoService
{
    private readonly IFuncaoRepository _funcaoRepository;

    public FuncaoService(IFuncaoRepository funcaoRepository)
    {
        _funcaoRepository = funcaoRepository;
    }

    public async Task<List<FuncaoViewModel>> GetAllAsync()
    {
        var funcoes =  await _funcaoRepository.GetAllAsync();
        
        return funcoes.Select(f =>new  FuncaoViewModel
        {
            Id = f.Id,
            TipoFuncao = f.TipoFuncao,
        }).ToList();
    }
    public async Task<FuncaoViewModel?> GetByIdAsync(int id)
    {
        var funcao = await _funcaoRepository.GetByIdAsync(id);

        if (funcao == null)
            return null;

        return new FuncaoViewModel
        {
            Id = funcao.Id,
            TipoFuncao = funcao.TipoFuncao,
        };
    }
    public async Task<int> CreateAsync(FuncaoCreateViewModel model)
    {
        var funcao = new Funcao
        {
            TipoFuncao = model.TipoFuncao
        };
        
        await _funcaoRepository.AddAsync(funcao);
        return funcao.Id;
    }
    public async Task<bool> UpdateAsync(int id, FuncaoViewModel model)
    {
        var funcao = await _funcaoRepository.GetByIdAsync(id);
        
        if (funcao == null)
            return false;
        
        funcao.TipoFuncao = model.TipoFuncao;
        
        await _funcaoRepository.UpdateAsync(funcao);
        return true;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var funcao = await _funcaoRepository.GetByIdAsync(id);
        
        if(funcao == null)
            return false;   
        
        await _funcaoRepository.DeleteAsync(funcao);
        return true;
    }
}