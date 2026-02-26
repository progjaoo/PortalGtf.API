using PortalGtf.Application.ViewModels.StreamingVM;
using PortalGtf.Core.Entities;
using PortalGtf.Core.Interfaces;

namespace PortalGtf.Application.Services.StreamingServices;

public class StreamingService : IStreamingService
{
    private readonly IStreamingRepository _streamingRepository;
    public StreamingService(IStreamingRepository streamingRepository)
    {
        _streamingRepository = streamingRepository;
    }
    
    public async Task<List<StreamingViewModel>> GetAllAsync()
    {
        var streamings = await _streamingRepository.GetAllAsync();

        return streamings.Select(s => new StreamingViewModel
        {
            Id =  s.Id,
            EmissoraId =  s.EmissoraId,
            Url =  s.Url,
            Porta =  s.Porta,
            TipoStream =  s.TipoStream,
            LinkApi = s.LinkApi
        }).ToList();
    }

    public async Task<StreamingViewModel?> GetByIdAsync(int id)
    {
        var streaming = await _streamingRepository.GetByIdAsync(id);
        
        if (streaming == null)
            return null;

        return new StreamingViewModel
        {
            Id = streaming.Id,
            EmissoraId = streaming.EmissoraId,
            Url = streaming.Url,
            Porta = streaming.Porta,
            TipoStream = streaming.TipoStream,
            LinkApi = streaming.LinkApi
        };
    }

    public async Task<int> CreateAsync(StreamingCreateViewModel model)
    {
        var streaming = new Streaming
        {
            EmissoraId = model.EmissoraId,
            Url = model.Url,
            Porta = model.Porta,
            TipoStream = model.TipoStream,
            LinkApi = model.LinkApi
        };

        await _streamingRepository.AddAsync(streaming);
        return streaming.Id;
    }

    public async Task<bool> UpdateAsync(int id, StreamingCreateViewModel model)
    {
        var streaming = await _streamingRepository.GetByIdAsync(id);
        
        if (streaming == null)
            return false;
        
        streaming.EmissoraId = model.EmissoraId;
        streaming.Url = model.Url;
        streaming.Porta = model.Porta;
        streaming.TipoStream = model.TipoStream;
        streaming.LinkApi = model.LinkApi;
        
        await _streamingRepository.UpdateAsync(streaming);
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var streaming = await _streamingRepository.GetByIdAsync(id);
        
        if(streaming == null)
            return false;
        
        await _streamingRepository.DeleteAsync(streaming);
        return true;    
    }
}