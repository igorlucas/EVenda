using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VendaService.Models;

namespace VendaService.Data
{
    public class ProdutoConfigurationModel : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");
            builder.HasKey(produto => produto.Id);
            builder.Property(produto => produto.Codigo).IsRequired();
            builder.Property(produto => produto.Nome).IsRequired().HasMaxLength(80);
            builder.Property(produto => produto.Quantidade).IsRequired();
            builder.Property(produto => produto.Preco).IsRequired();
            builder.HasIndex(produto => new
            {
                produto.Codigo,
                produto.Nome
            }).IsUnique(true);
            builder.HasCheckConstraint("CK_Produtos_Quantidade", "[Quantidade] >=0");
            builder.HasCheckConstraint("CK_Produtos_Preco", "[Preco] >=0");
        }
    }
}
