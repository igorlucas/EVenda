using Domain;
using EstoqueService.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EstoqueService.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly EstoqueServiceContext _db;

        public ProdutoService(EstoqueServiceContext db) => _db = db;
        public async Task<IEnumerable<Produto>> ListarProdutos() => await _db.Produtos.ToArrayAsync();

        public void AdicionarProduto(Produto produto)
        {
            ValidarProduto(produto);
            _db.Produtos.Add(produto);
        }

        public void AtualizarProduto(Produto produto)
        {
            ValidarProduto(produto);
            _db.Entry(produto).State = EntityState.Modified;
        }

        public async Task<bool> SalvarAsync() => (await _db.SaveChangesAsync() > 0);

        private static void ValidarProduto(Produto produto)
        {
            if (produto.Quantidade < 0)
            {
                throw new ArgumentOutOfRangeException("Quantidade", "O campo Quantidade não deve ser negativo");
            }
            if (produto.Preco < 0M)
            {
                throw new ArgumentOutOfRangeException("Preco", "O campo Preco não deve ser negativo");
            }
        }
    }
}

