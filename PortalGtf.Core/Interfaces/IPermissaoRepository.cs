using PortalGtf.Core.Entities;

namespace PortalGtf.Core.Interfaces;

public interface IPermissaoRepository
{
    Task<List<Permissao>> GetAllPermissoesByUsuarioAsync(int usuarioId);
    Task<List<Permissao>> GetAll();
    Task<Permissao> GetById(int id);
    Task AddAsync(Permissao permissao);
    Task UpdateAsync(Permissao permissao);
    Task DeleteAsync(Permissao permissao);
}