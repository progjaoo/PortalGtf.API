using System.Security.Claims;
using PortalGtf.Application.Services.PostServices;
using PortalGtf.Application.ViewModels.PostsVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalGtf.Core.Enums;

namespace PortalGtf.API.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _service;

        public PostController(IPostService service)
        {
            _service = service;
        }
        // TODO: realizar todos os GET`s do POST
        [HttpGet("slug/{slug}")]
        public async Task<IActionResult> GetBySlug(string slug)
        {
            var post = await _service.GetBySlugAsync(slug);

            if (post == null)
                return NotFound();

            return Ok(post);
        }
        [HttpGet("/sitemap.xml")]
        public async Task<IActionResult> Sitemap()
        {
            var xml = await _service.GenerateSitemapAsync();

            return Content(xml, "application/xml");
        }
        [HttpGet("/robots.txt")]
        public IActionResult Robots()
        {
            var robots = @"User-agent: *
                Allow: /
                Sitemap: https://portalgtf.com.br/sitemap.xml";

            return Content(robots, "text/plain");
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("buscarTodosRascunhos")]
        public async Task<IActionResult> GetAllByRascunho()
        {
            var result = await _service.GetAllPostsByStatusRascunho();
            return Ok(result);
        }
        [HttpGet("buscarTodasRevisoes")]
        public async Task<IActionResult> GetAllByRevisao()
        {
            var result = await _service.GetAllPostsByStatusRevisao();
            return Ok(result);
        }
        [HttpGet("regiao/{regiaoId}")]
        public async Task<IActionResult> GetByRegiao(
            int regiaoId,
            int page = 1,
            int pageSize = 10)
        {
            var result = await _service
                .GetPostsByRegiaoAsync(regiaoId, page, pageSize);

            return Ok(result);
        }
        [HttpGet("mais-recentes")]
        public async Task<IActionResult> GetAllByRecents()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }
        [HttpGet("mais-lidas")]
        public async Task<IActionResult> GetMostRead([FromQuery] int? emissoraId = null, [FromQuery] int limit = 4, [FromQuery] int days = 7)
        {
            var result = await _service.GetMostReadAsync(emissoraId, limit, days);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
        [HttpPost("{id}/visualizacao")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterView(int id)
        {
            var forwardedFor = Request.Headers["X-Forwarded-For"].FirstOrDefault();
            var ip = !string.IsNullOrWhiteSpace(forwardedFor)
                ? forwardedFor.Split(',').FirstOrDefault()?.Trim()
                : HttpContext.Connection.RemoteIpAddress?.ToString();

            await _service.RegisterViewAsync(id, ip);
            return NoContent();
        }
        [HttpGet("editorial/{editorialId}")]
        public async Task<IActionResult> GetByEditorial(int editorialId)
        {
            var result = await _service.GetByEditorialAsync(editorialId);
            return Ok(result);
        }
        [HttpGet("user/{userName}")]
        public async Task<IActionResult> GetByUser(string userName)
        {
            var result = await _service.GetByUserAsync(userName);
            return Ok(result);
        }
        [HttpGet("public")]
        public async Task<IActionResult> GetPublished()
        {
            var result = await _service.GetAllPublishedAsync();
            return Ok(result);
        }
        [HttpGet("destaques")]
        public async Task<IActionResult> GetDestaques()
        {
            var posts = await _service.GetDestaquesAsync();

            return Ok(posts);
        }
        [HttpGet("destaques88fm")]
        public async Task<IActionResult> GetDestaquesOitentaOitofm()
        {
            var posts = await _service.GetDestaquesbBy88FmAsync();

            return Ok(posts);
        }
        [HttpGet("destaquesFatoPopular")]
        public async Task<IActionResult> GetDestaquesFatoPopular()
        {
            var posts = await _service.GetDestaquesbByFatoPopularAsync();

            return Ok(posts);
        }
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Query inválida");

            var result = await _service.SearchAsync(query);
            return Ok(result);
        }
        [HttpGet("status")]
        public async Task<IActionResult> GetByStatus([FromQuery] StatusPost statusPost)
        {
            var result = await _service.GetByStatusAsync(statusPost);
            return Ok(result);
        }
        [HttpGet("buscarPorEmissora/{emissoraId}")]
        public async Task<IActionResult> GetAllByEmissora(int emissoraId)
        {
            var result = await _service.GetAllPostsByEmissora(emissoraId);
            return Ok(result);
        }
        [HttpGet("buscarPorSubcategory/{subcategoryId}")]
        public async Task<IActionResult> GetBySubcategory(int subcategoryId)
        {
            var result = await _service.GetBySubcategoryAsync(subcategoryId);
            return Ok(result);
        }
        [HttpGet("filtro")]
        public async Task<IActionResult> GetFiltered(
            [FromQuery] FiltroPostEnum dateFilter = FiltroPostEnum.QualquerData,
            [FromQuery] OrdenaPostEnum orderBy = OrdenaPostEnum.MaisRecente)
        {
            var filter = new PostEnumViewModel()
            {
                FiltroData = dateFilter,
                OrdenarPor = orderBy
            };

            var result = await _service.GetFilteredAsync(filter);

            return Ok(result);
        }
        [HttpPut("{id}/destaque")]
        public async Task<IActionResult> SetDestaque(int id, [FromQuery] bool destaque)
        {
            await _service.SetDestaqueAsync(id, destaque);

            return NoContent();
        }
        /// <summary>
        /// Criar novo post
        /// </summary>
        [HttpPost("criarPost")]
        public async Task<IActionResult> Criar([FromBody] PostCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                await _service.CreateAsync(model);

                return Ok(new { message = "Post criado com sucesso como rascunho." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        /// <summary>
        /// Atualizar post
        /// </summary>
        [HttpPut("{id}/updatePost")]
        public async Task<IActionResult> Update(int id, [FromBody] PostUpdateViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _service.UpdateAsync(id, model);
                return Ok(new { message = "Post atualizado com sucesso" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        /// <summary>
        /// Deletar post
        /// </summary>
        [HttpDelete("{id}/deletePost")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok(new { message = "Post removido com sucesso" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
        [HttpPut("{id}/enviar-revisao")] // todo: vai  aparecer para o revisador
        public async Task<IActionResult> EnviarParaRevisao(int id, [FromBody] PostEnviarRevisaoViewModel model)
        {
            await _service.EnviarParaRevisaoAsync(id, model);
            return Ok(new { message = "Post enviado para revisão" });
        }
        [HttpPut("{id}/aprovar")]
        public async Task<IActionResult> Aprovar(int id) // ToDo: vai aparecer para quem aprovará a postagem
        {
            await _service.AprovarPostAsync(id);

            return Ok(new { message = "Post aprovado e publicado" });
        }
        [HttpPut("{id}/enviarParaAprovacao")]
        public async Task<IActionResult> EnviarParaAprovacao(int id) 
        {
            await _service.EnviarParaAprovacao(id);

            return Ok(new { message = "Post enviado para aprovação" });
        }
        [HttpPut("{id}/rejeitar")]
        public async Task<IActionResult> Rejeitar(int id)
        {
            await _service.RejeitarPostAsync(id);

            return Ok(new { message = "Post rejeitado" });
        }
    }
}
