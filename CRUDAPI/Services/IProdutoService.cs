using CRUDAPI.Models;

namespace CRUDAPI.Services
{
    public interface IProdutoService
    {
        Task<Produto> CreateProdutoAsync(Produto produtoDto);
        Task<Produto> UpdateProdutoAsync(int id, Produto produtoDto);

        Task<Produto> DeleteLogByIdAsync(int id);

        Task<Produto> GetProdutoByIdAsync(int id);

        Task<List<Produto>> GetAllProdutosAsync();
    }
}
