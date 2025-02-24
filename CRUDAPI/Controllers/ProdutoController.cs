using CRUDAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUDAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {

        private readonly ILogger<ProdutoController> _logger;

        public ProdutoController(ILogger<ProdutoController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetProdutos")]
        public IEnumerable<Produto> Get()
        {
            var produtos = new List<Produto>();
            return produtos.ToArray();
        }
    }
}
