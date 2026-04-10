using PortalGtf.Core.Entities;

namespace PortalGtf.Core.Interfaces;

public interface IProgramacaoRadiorRepository
{
    Task<List<ProgramacaoRadio>> GetAllAsync();
    Task<List<ProgramacaoRadio>> GetByEmissoraAsync(int emissoraId);
    Task<ProgramacaoRadio> GetByIdAsync(int id);
    Task AddAsync(ProgramacaoRadio programacaoRadio);
    Task UpdateAsync(ProgramacaoRadio programacaoRadio);
    Task DeleteAsync(int id);
}
