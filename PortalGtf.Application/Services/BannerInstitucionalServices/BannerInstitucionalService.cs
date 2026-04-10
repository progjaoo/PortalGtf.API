using PortalGtf.Application.ViewModels.BannerInstitucionalVM;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Application.Services.BannerInstitucionalServices;

public class BannerInstitucionalService : IBannerInstitucionalService
{
    private readonly IBannerInstitucionalRepository _repository;

    public BannerInstitucionalService(IBannerInstitucionalRepository repository)
    {
        _repository = repository;
    }

    private static BannerInstitucionalViewModel Map(BannerInstitucional banner)
    {
        return new BannerInstitucionalViewModel
        {
            Id = banner.Id,
            Titulo = banner.Titulo,
            EmissoraId = banner.EmissoraId,
            EmissoraNome = banner.Emissora?.NomeSocial ?? "",
            MidiaId = banner.MidiaId,
            MidiaUrl = banner.Midia?.Url ?? "",
            LinkUrl = banner.LinkUrl,
            NovaAba = banner.NovaAba,
            Posicao = banner.Posicao,
            Ordem = banner.Ordem,
            Ativo = banner.Ativo,
            DataCriacao = banner.DataCriacao,
        };
    }

    public async Task<List<BannerInstitucionalViewModel>> GetAllAsync()
    {
        var banners = await _repository.GetAllAsync();
        return banners.Select(Map).ToList();
    }

    public async Task<List<BannerInstitucionalViewModel>> GetAtivosPorEmissoraAsync(int emissoraId, string? posicao)
    {
        var banners = await _repository.GetAtivosPorEmissoraAsync(emissoraId, posicao);
        return banners.Select(Map).ToList();
    }

    public async Task<BannerInstitucionalViewModel?> GetByIdAsync(int id)
    {
        var banner = await _repository.GetByIdAsync(id);
        return banner == null ? null : Map(banner);
    }

    public async Task<int> CreateAsync(BannerInstitucionalCreateViewModel model)
    {
        var banner = new BannerInstitucional
        {
            Titulo = model.Titulo,
            EmissoraId = model.EmissoraId,
            MidiaId = model.MidiaId,
            LinkUrl = model.LinkUrl,
            NovaAba = model.NovaAba,
            Posicao = model.Posicao,
            Ordem = model.Ordem,
            Ativo = model.Ativo,
            DataCriacao = DateTime.UtcNow,
        };

        await _repository.AddAsync(banner);
        return banner.Id;
    }

    public async Task<bool> UpdateAsync(int id, BannerInstitucionalCreateViewModel model)
    {
        var banner = await _repository.GetByIdAsync(id);
        if (banner == null) return false;

        banner.Titulo = model.Titulo;
        banner.EmissoraId = model.EmissoraId;
        banner.MidiaId = model.MidiaId;
        banner.LinkUrl = model.LinkUrl;
        banner.NovaAba = model.NovaAba;
        banner.Posicao = model.Posicao;
        banner.Ordem = model.Ordem;
        banner.Ativo = model.Ativo;

        await _repository.UpdateAsync(banner);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var banner = await _repository.GetByIdAsync(id);
        if (banner == null) return false;

        await _repository.DeleteAsync(banner);
        return true;
    }
}
