using CRUDAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDAPI.Data
{
    public class ProdutoDbContext : DbContext, IProdutoDbContext
    {
        public ProdutoDbContext
          (DbContextOptions<ProdutoDbContext> opts) : base(opts) { }

        public ProdutoDbContext() : base() { }

        public virtual DbSet<Produto> Produtos { get; set; }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
