using PortalGtf.Application.ViewModels.SubcategoryVM;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Application.Services.SubcategoryServices;

public class SubcategoriaService : ISubcategoriaService
{
    private readonly ISubcategoriaRepository _repository;

    public SubcategoriaService(ISubcategoriaRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<SubcategoriaViewModel>> GetAllAsync()
    {
        var subcategorias = await _repository.GetAllAsync();
        
        if (subcategorias == null)
            return null;

        return subcategorias.Select(s => new SubcategoriaViewModel
        {
            Nome = s.Nome,
            Slug = s.Slug,
            EditorialId = s.EditorialId
        }).ToList();
    }
    public async Task<SubcategoriaDetailViewModel> GetByIdAsync(int id)
    {
        var sub = await _repository.GetByIdAsync(id);

        return new SubcategoriaDetailViewModel
        {
            Id = sub.Id,
            Nome = sub.Nome,
            Slug = sub.Slug,
            EditorialId = sub.EditorialId
        };
    }

    

    public async Task CreateAsync(string nome, int editorialId)
    {
        var slug = nome.ToLower().Replace(" ", "-");

        var sub = new Subcategoria
        {
            Nome = nome,
            Slug = slug,
            EditorialId = editorialId
        };

        await _repository.AddAsync(sub);
        await _repository.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var sub = await _repository.GetByIdAsync(id);
        if (sub == null)
            throw new Exception("Subcategoria não encontrada");

        await _repository.DeleteAsync(sub);
        await _repository.SaveChangesAsync();
    }
}