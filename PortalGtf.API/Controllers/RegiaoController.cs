using Microsoft.AspNetCore.Mvc;
using PortalGtf.Infrastructure.Repositories;

namespace PortalGtf.API.Controllers;

[ApiController] 
[Route("api/regiao")]
public class RegiaoController : ControllerBase
{
    private readonly RegiaoRepository _regiaoRepository;

    public RegiaoController(RegiaoRepository regiaoRepository)
    {
        _regiaoRepository = regiaoRepository;
    }
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var result = await _regiaoRepository.GetAllAsync();
        return Ok(result);
    }
}