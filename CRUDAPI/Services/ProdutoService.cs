using CRUDAPI.Data;
using CRUDAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPI.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly ProdutoDbContext _context;

        public ProdutoService(ProdutoDbContext context)
        {
            _context = context;
        }

        public async Task<Produto> CreateProdutoAsync(Produto produtoDto)
        {
            ValidateProduto(produtoDto);

            _context.Produtos.Add(produtoDto);
            await _context.SaveChangesAsync();
            return produtoDto;
        }

        public async Task<Produto> DeleteLogByIdAsync(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return null;
            }

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        public async Task<List<Produto>> GetAllProdutosAsync()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<Produto> GetProdutoByIdAsync(int id)
        {
            return await _context.Produtos.FindAsync(id);
        }

        public async Task<Produto> UpdateProdutoAsync(int id, Produto produtoDto)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return null;
            }

            ValidateProduto(produtoDto);

            produto.Name = produtoDto.Name;
            produto.Price = produtoDto.Price;
            produto.StockQuantity = produtoDto.StockQuantity;

            _context.Produtos.Update(produto);
            await _context.SaveChangesAsync();
            return produto;
        }

        private void ValidateProduto(Produto produto)
        {
            if (produto.Price <= 0)
            {
                throw new ArgumentException("Preço deve ser maior que 0.");
            }

            if (produto.StockQuantity < 0)
            {
                throw new ArgumentException("Quantidade em estoque deve ser igual ou maior que 0.");
            }
        }
    }
}
