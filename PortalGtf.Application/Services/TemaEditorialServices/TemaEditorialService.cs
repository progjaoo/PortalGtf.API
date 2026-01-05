using PortalGtf.Application.ViewModels.TemaEditorialVM;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Application.Services.TemaEditorialServices;

public class TemaEditorialService : ITemaEditorialService
{
    private readonly ITemaEditorialRepository _repository;

    public TemaEditorialService(ITemaEditorialRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TemaEditorialResponseViewModel>> GetAllAsync()
    {
        var temas = await _repository.GetAllAsync();

        return temas.Select(t => new TemaEditorialResponseViewModel
        {
            Id = t.Id,
            Descricao = t.Descricao,
            CorPrimaria = t.CorPrimaria,
            CorSecundaria = t.CorSecundaria,
            CorFonte = t.CorFonte,
            Logo = t.Logo
        });
    }

    public async Task<TemaEditorialResponseViewModel> GetByIdAsync(int id)
    {
        var tema = await _repository.GetByIdAsync(id);
        if (tema == null) return null;

        return new TemaEditorialResponseViewModel
        {
            Id = tema.Id,
            Descricao = tema.Descricao,
            CorPrimaria = tema.CorPrimaria,
            CorSecundaria = tema.CorSecundaria,
            CorFonte = tema.CorFonte,
            Logo = tema.Logo
        };
    }

    public async Task CreateAsync(TemaEditorialViewModel model)
    {
        var tema = new Core.Entities.TemaEditorial
        {
            Descricao = model.Descricao,
            CorPrimaria = model.CorPrimaria,
            CorSecundaria = model.CorSecundaria,
            CorFonte = model.CorFonte,
            Logo = model.Logo
        };

        await _repository.AddAsync(tema);
    }
    
    public async Task UpdateAsync(int id, TemaEditorialViewModel model)
    {
        var tema = await _repository.GetByIdAsync(id);
        if (tema == null)
            throw new KeyNotFoundException("Tema editorial não encontrado.");

        tema.Descricao = model.Descricao;
        tema.CorPrimaria = model.CorPrimaria;
        tema.CorSecundaria = model.CorSecundaria;
        tema.CorFonte = model.CorFonte;
        tema.Logo = model.Logo;

        await _repository.UpdateAsync(tema);
    }

    public async Task DeleteAsync(int id)
    {
        var tema = await _repository.GetByIdAsync(id);
        if (tema == null)
            throw new KeyNotFoundException("Tema editorial não encontrado.");

        await _repository.DeleteAsync(tema);
    }
}