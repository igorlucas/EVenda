using Domain;
using Microsoft.EntityFrameworkCore;

namespace EstoqueService.Data
{
    public class EstoqueServiceContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }

        public EstoqueServiceContext(DbContextOptions<EstoqueServiceContext> options) 
            : base(options) => Database.Migrate();

        protected override void OnConfiguring(DbContextOptionsBuilder options) 
            => options.UseSqlite("Data Source=EstoqueService.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
            => modelBuilder.ApplyConfiguration(new ProdutoConfigurationModel());
    }
}