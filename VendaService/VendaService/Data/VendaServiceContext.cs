using Microsoft.EntityFrameworkCore;
using VendaService.Models;

namespace VendaService.Data
{
    public class VendaServiceContext : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        
        public VendaServiceContext(DbContextOptions<VendaServiceContext> options) 
            : base(options) => Database.Migrate();

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=VendaService.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
            => modelBuilder.ApplyConfiguration(new ProdutoConfigurationModel());
    }

}
