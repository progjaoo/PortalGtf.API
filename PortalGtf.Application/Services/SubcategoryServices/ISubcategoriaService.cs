using PortalGtf.Application.ViewModels.SubcategoryVM;
using PortalGtf.Core.Entities;

namespace PortalGtf.Application.Services.SubcategoryServices;

public interface ISubcategoriaService
{
    Task<List<SubcategoriaViewModel>> GetAllAsync();
    Task<SubcategoriaDetailViewModel> GetByIdAsync(int id);
    Task CreateAsync(string nome, int editorialId);
    Task DeleteAsync(int id);
}