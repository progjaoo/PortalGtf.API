namespace PortalGtf.Application.Services.ProgramacaoRadioServices;

using PortalGtf.Application.ViewModels.ProgramacaoVM;

public interface IProgramacaoRadioService
{
    Task<List<ProgramacaoRadioViewModel>> GetAllAsync();
    Task<List<ProgramacaoRadioViewModel>> GetByEmissoraAsync(int emissoraId);

    Task<ProgramacaoRadioViewModel?> GetByIdAsync(int id);

    Task CreateAsync(ProgramacaoRadioCreateViewModel model);

    Task UpdateAsync(int id, ProgramacaoRadioUpdateViewModel model);

    Task DeleteAsync(int id);
}
