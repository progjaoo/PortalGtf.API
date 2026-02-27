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
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
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
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Query inválida");

            var result = await _service.SearchAsync(query);
            return Ok(result);
        }
        [HttpGet("admin/status")]
        public async Task<IActionResult> GetByStatus([FromQuery] StatusPost statusPost)
        {
            var result = await _service.GetByStatusAsync(statusPost);
            return Ok(result);
        }
        [HttpGet("buscarPorEmissora/{emissoraId}")]
        public async Task<IActionResult> GetAllByEmissora([FromQuery] int emissoraId)
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
        /// <summary>
        /// Criar novo post
        /// </summary>
        [HttpPost("createPost")]
        public async Task<IActionResult> Create([FromBody] PostCreateViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.CreateAsync(model);

            return Created("", new { message = "Post criado com sucesso" });
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
        public async Task<IActionResult> EnviarParaRevisao(int id)
        {
            await _service.EnviarParaRevisaoAsync(id);
            return Ok(new { message = "Post enviado para revisão" });
        }
        [HttpPut("{id}/aprovar")]
        public async Task<IActionResult> Aprovar(int id) // ToDo: vai aparecer para quem aprovará a postagem
        {
            await _service.AprovarPostAsync(id);

            return Ok(new { message = "Post aprovado e publicado" });
        }
        [HttpPut("{id}/rejeitar")]
        public async Task<IActionResult> Rejeitar(int id)
        {
            await _service.RejeitarPostAsync(id);

            return Ok(new { message = "Post rejeitado" });
        }
        [HttpPost("{id}/upload-imagem")]
        public async Task<IActionResult> UploadImagem(int id, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Arquivo inválido");

            using var stream = file.OpenReadStream();

            var result = await _service.UploadImagemAsync(
                id,
                stream,
                file.FileName,
                file.ContentType
            );

            return Ok(new { url = result });
        }
    }
}