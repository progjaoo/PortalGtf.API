using PortalGtf.Core.Entities;

namespace PortalGtf.Core.Interfaces;

public interface IUsuarioEmissoraRepository
{
    Task<List<UsuarioEmissora>> GetAllAsync();
    Task<UsuarioEmissora?> GetByIdAsync(int id);
    Task AddAsync(UsuarioEmissora usuarioEmissora);
    Task UpdateAsync(UsuarioEmissora usuarioEmissora);
    Task DeleteAsync(UsuarioEmissora usuarioEmissora);
}