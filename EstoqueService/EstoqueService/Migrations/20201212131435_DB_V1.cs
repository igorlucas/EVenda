using Microsoft.EntityFrameworkCore.Migrations;

namespace EstoqueService.Migrations
{
    public partial class DB_V1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Codigo = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(maxLength: 80, nullable: false),
                    Quantidade = table.Column<int>(nullable: false),
                    Preco = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.CheckConstraint("CK_Produtos_Quantidade", "[Quantidade] >=0");
                    table.CheckConstraint("CK_Produtos_Preco", "[Preco] >=0");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_Codigo_Nome",
                table: "Produtos",
                columns: new[] { "Codigo", "Nome" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produtos");
        }
    }
}
