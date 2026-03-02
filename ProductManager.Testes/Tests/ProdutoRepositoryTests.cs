using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProductManager.Domain.Entities;
using ProductManager.Infrastructure.Data;
using ProductManager.Infrastructure.Repositories;
using Xunit;

namespace ProductManager.Tests.Tests
{
    public class ProdutoRepositoryTests
    {
        private async Task<AppDbContext> ObterContextoInMemoryAsync()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: $"Db_{Guid.NewGuid()}")
                .Options;

            var context = new AppDbContext(options);

            // Popular com alguns dados iniciais
            context.Produtos.AddRange(new List<Produto>
        {
            new Produto(1, "Produto 1", 10, 5, "A", "Desc 1" ),
            new Produto(2, "Produto 2", 20, 10, "B", "Desc 2")
        });

            await context.SaveChangesAsync();
            return context;
        }

        [Fact]
        public async Task ObterTodosAsync_DeveRetornarTodosProdutos()
        {
            var context = await ObterContextoInMemoryAsync();
            var repo = new ProdutoRepository(context);

            var produtos = await repo.ObterTodosAsync();

            Assert.Equal(2, produtos.Count());
        }

        [Fact]
        public async Task ObterPorIdAsync_ProdutoExistente_DeveRetornarProduto()
        {
            var context = await ObterContextoInMemoryAsync();
            var repo = new ProdutoRepository(context);

            var produto = await repo.ObterPorIdAsync(1);

            Assert.NotNull(produto);
            Assert.Equal("Produto 1", produto.Nome);
        }

        [Fact]
        public async Task ObterPorIdAsync_ProdutoNaoExistente_DeveRetornarNull()
        {
            var context = await ObterContextoInMemoryAsync();
            var repo = new ProdutoRepository(context);

            var produto = await repo.ObterPorIdAsync(999);

            Assert.Null(produto);
        }

        [Fact]
        public async Task AdicionarAsync_DeveAdicionarProduto()
        {
            var context = await ObterContextoInMemoryAsync();
            var repo = new ProdutoRepository(context);

            var novoProduto = new Produto(2, "Produto 2", 20, 10, "B", "Desc 2");

            await repo.AdicionarAsync(novoProduto);

            var produtos = await repo.ObterTodosAsync();
            Assert.Equal(3, produtos.Count());
            Assert.Contains(produtos, p => p.Nome == "Produto 2");
        }

        [Fact]
        public async Task AtualizarAsync_DeveAlterarProdutoExistente()
        {
            var context = await ObterContextoInMemoryAsync();
            var repo = new ProdutoRepository(context);

            var produto = await repo.ObterPorIdAsync(1);
            produto.Atualizar("Atualizado", 10, 10, "teste", "teste");

            await repo.AtualizarAsync(produto);

            var atualizado = await repo.ObterPorIdAsync(1);
            Assert.Equal("Atualizado", atualizado.Nome);
        }

        [Fact]
        public async Task RemoverAsync_ProdutoExistente_DeveRemover()
        {
            var context = await ObterContextoInMemoryAsync();
            var repo = new ProdutoRepository(context);

            await repo.RemoverAsync(1);

            var produto = await repo.ObterPorIdAsync(1);
            Assert.Null(produto);

            var produtos = await repo.ObterTodosAsync();
            Assert.Single(produtos);
        }

        [Fact]
        public async Task RemoverAsync_ProdutoNaoExistente_NaoLancarErro()
        {
            var context = await ObterContextoInMemoryAsync();
            var repo = new ProdutoRepository(context);

            var exception = await Record.ExceptionAsync(() => repo.RemoverAsync(999));

            Assert.Null(exception);
        }
    }
}
