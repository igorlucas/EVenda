using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EstoqueService.Services
{
    public interface IProdutoService
    {
        void AdicionarProduto(Produto produto);
        void AtualizarProduto(Produto produto);
        Task<IEnumerable<Produto>> ListarProdutos();
        Task<bool> SalvarAsync();
    }
}
