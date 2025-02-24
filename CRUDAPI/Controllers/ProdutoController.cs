using CRUDAPI.Models;
using CRUDAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CRUDAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ILogger<ProdutoController> _logger;
        private readonly IProdutoService _produtoService;

        public ProdutoController(ILogger<ProdutoController> logger, IProdutoService produtoService)
        {
            _logger = logger;
            _produtoService = produtoService;
        }

        [HttpGet(Name = "GetProdutos")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            var produtos = await _produtoService.GetAllProdutosAsync();
            return Ok(produtos);
        }

        [HttpPost(Name = "AddProduto")]
        public async Task<ActionResult> AddProduto(Produto produto)
        {
            try
            {
                var createdProduto = await _produtoService.CreateProdutoAsync(produto);
                return CreatedAtAction(nameof(GetProdutoById), new { id = createdProduto.ProductID }, createdProduto);
            }
            catch (ArgumentException ex)
            {
               _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}", Name = "UpdateProduto")]
        public async Task<ActionResult> UpdateProduto(int id, Produto produto)
        {
            try
            {
                var updatedProduto = await _produtoService.UpdateProdutoAsync(id, produto);
                if (updatedProduto == null)
                {
                    return NotFound();
                }
                return Ok(updatedProduto);
            }
            catch (ArgumentException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}", Name = "DeleteProduto")]
        public async Task<ActionResult> DeleteProduto(int id)
        {
            var deletedProduto = await _produtoService.DeleteLogByIdAsync(id);
            if (deletedProduto == null)
            {
                _logger.LogInformation($"Produto id {id} não encontrado");
                return NotFound();
            }
            return Ok(deletedProduto);
        }

        [HttpGet("{id}", Name = "GetProdutoById")]
        public async Task<ActionResult<Produto>> GetProdutoById(int id)
        {
            var produto = await _produtoService.GetProdutoByIdAsync(id);
            if (produto == null)
            {
                _logger.LogInformation($"Produto id {id} não encontrado");
                return NotFound();
            }
            return Ok(produto);
        }
    }
}
