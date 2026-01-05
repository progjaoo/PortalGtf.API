using PortalGtf.Core.Entities;

namespace PortalGtf.Core.Interfaces;

public interface IUsuarioRepository
{
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task<Usuario?> GetByIdAsync(int id);
    Task AddAsync(Usuario usuario);
    Task<Usuario?> GetByEmailAndPasswordAsync(string email, string senhaHash);
    Task UpdateAsync(Usuario usuario);
}