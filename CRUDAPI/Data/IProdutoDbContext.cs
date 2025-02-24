using CRUDAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPI.Data
{
    public interface IProdutoDbContext
    {
        DbSet<Produto> Produtos { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
