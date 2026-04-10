using PortalGtf.Application.ViewModels.EditorialVM;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Application.Services.EditorialServices;

public class EditorialService : IEditorialService
{
    private  readonly IEditorialRepository _editorialRepository;

    public EditorialService(IEditorialRepository editorialRepository)
    {
        _editorialRepository = editorialRepository;
    }
    public async Task<List<EditorialViewModel>> GetAllAsync()
    {
        var editoriais = await _editorialRepository.GetAllAsync();
        
        return editoriais.Select(e => new EditorialViewModel
        {
            Id = e.Id,
            TipoPostagem = e.TipoPostagem,
            TemaEditorialId = e.TemaEditorialId,
            EmissoraId = e.EmissoraId,
            EmissoraNome = e.Emissora?.NomeSocial,
        }).ToList();
    }

    public async Task<List<EditorialViewModel>> GetByEmissoraAsync(int emissoraId)
    {
        var editoriais = await _editorialRepository.GetByEmissoraAsync(emissoraId);

        return editoriais.Select(e => new EditorialViewModel
        {
            Id = e.Id,
            TipoPostagem = e.TipoPostagem,
            TemaEditorialId = e.TemaEditorialId,
            EmissoraId = e.EmissoraId,
            EmissoraNome = e.Emissora?.NomeSocial,
        }).ToList();
    }

    public async Task<EditorialViewModel?> GetByIdAsync(int id)
    {
        var editoriais = await _editorialRepository.GetByIdAsync(id);

        if (editoriais == null)
            return null;

        return new EditorialViewModel
        {
            Id = editoriais.Id,
            TipoPostagem = editoriais.TipoPostagem,
            TemaEditorialId = editoriais.TemaEditorialId,
            EmissoraId = editoriais.EmissoraId,
            EmissoraNome = editoriais.Emissora?.NomeSocial,
        };
    }
    public async Task<int> CreateAsync(EditorialCreateViewModel model)
    {
        var editorial = new Editorial
        {
            TipoPostagem = model.TipoPostagem,
            TemaEditorialId =  model.TemaEditorialId,
            EmissoraId = model.EmissoraId,
        };
        await _editorialRepository.AddAsync(editorial);
        return editorial.Id;
    }

    public async Task<bool> UpdateAsync(int id, EditorialCreateViewModel model)
    {
        var  editoriais = await _editorialRepository.GetByIdAsync(id);
        if (editoriais == null)
            return false;

        editoriais.TipoPostagem = model.TipoPostagem;
        editoriais.TemaEditorialId = model.TemaEditorialId;
        editoriais.EmissoraId = model.EmissoraId;
        
        await _editorialRepository.UpdateAsync(editoriais);
        return true;
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var editorial = await _editorialRepository.GetByIdAsync(id);

        if (editorial ==null)
            return false;
        
        await _editorialRepository.DeleteAsync(editorial);
        return true;
    }
}
