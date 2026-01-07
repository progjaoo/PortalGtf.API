using PortalGtf.Core.Entities;

namespace PortalGtf.Core.Interfaces;

public interface IFuncaoPermissaoRepository
{
    Task<List<FuncaoPermissao>> GetAllAsync();
    Task<FuncaoPermissao?> GetByIdAsync(int id);
    Task AddAsync(FuncaoPermissao entity);
    Task DeleteAsync(FuncaoPermissao entity);
}