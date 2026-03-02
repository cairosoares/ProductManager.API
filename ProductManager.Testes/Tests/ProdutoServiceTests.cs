using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using ProductManager.Application.DTOs;
using ProductManager.Application.Services;
using ProductManager.Domain.Entities;
using ProductManager.Domain.Interfaces;

namespace ProductManager.Tests.Tests
{
    public class ProdutoServiceTests
    {
        private readonly Mock<IProdutoRepository> _repositoryMock;
        private readonly IMapper _mapper;
        private readonly ProdutoService _service;

        public ProdutoServiceTests()
        {
            _repositoryMock = new Mock<IProdutoRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Produto, ProdutoDto>();
                cfg.CreateMap<CriarProdutoDto, Produto>();
            }, new LoggerFactory());
            _mapper = config.CreateMapper();

            _service = new ProdutoService(_repositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task ObterTodosAsync_DeveRetornarTodosProdutos()
        {
            // Arrange
            var produtos = new List<Produto>
        {
            new Produto(1, "Produto 1", 10, 5, "Categoria A", "Descrição A"),
            new Produto(2, "Produto 2", 20, 10, "Categoria B", "Descrição B")
        };
            _repositoryMock.Setup(r => r.ObterTodosAsync()).ReturnsAsync(produtos);

            // Act
            var result = await _service.ObterTodosAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, p => p.Nome == "Produto 1");
            Assert.Contains(result, p => p.Nome == "Produto 2");
        }

        [Fact]
        public async Task ObterPorIdAsync_ProdutoExistente_DeveRetornarProduto()
        {
            // Arrange
            var produto = new Produto(1, "Produto 1", 10, 5, "Categoria A", "Descrição A");
            _repositoryMock.Setup(r => r.ObterPorIdAsync(1)).ReturnsAsync(produto);

            // Act
            var result = await _service.ObterPorIdAsync(1);

            // Assert
            Assert.Equal("Produto 1", result.Nome);
        }

        [Fact]
        public async Task ObterPorIdAsync_ProdutoNaoExistente_DeveLancarExcecao()
        {
            // Arrange
            _repositoryMock.Setup(r => r.ObterPorIdAsync(1)).ReturnsAsync((Produto)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.ObterPorIdAsync(1));
        }

        [Fact]
        public async Task AdicionarAsync_DeveAdicionarProduto()
        {
            // Arrange
            var dto = new CriarProdutoDto
            {
                Nome = "Produto Novo",
                Preco = 15,
                Estoque = 5,
                CodigoProduto = 4,
                Categoria = "Categoria X",
                Descricao = "Descrição X"
            };

            _repositoryMock.Setup(r => r.AdicionarAsync(It.IsAny<Produto>())).Returns(Task.CompletedTask);

            // Act
            var result = await _service.AdicionarAsync(dto);

            // Assert
            Assert.Equal("Produto Novo", result.Nome);
            _repositoryMock.Verify(r => r.AdicionarAsync(It.IsAny<Produto>()), Times.Once);
        }

        [Fact]
        public async Task AtualizarAsync_ProdutoExistente_DeveAtualizarCampos()
        {
            // Arrange
            var produto = new Produto(1, "Produto 1", 10, 5, "Categoria A", "Descrição A");
            var dto = new ProdutoDto
            {
                Nome = "Atualizado",
                Preco = 20,
                Estoque = 10,
                Categoria = "Categoria B",
                Descricao = "Descrição B"
            };
            _repositoryMock.Setup(r => r.ObterPorIdAsync(1)).ReturnsAsync(produto);
            _repositoryMock.Setup(r => r.AtualizarAsync(produto)).Returns(Task.CompletedTask);

            // Act
            await _service.AtualizarAsync(1, dto);

            // Assert
            Assert.Equal("Atualizado", produto.Nome);
            Assert.Equal(20, produto.Preco);
            _repositoryMock.Verify(r => r.AtualizarAsync(produto), Times.Once);
        }

        [Fact]
        public async Task AtualizarParcialAsync_ProdutoExistente_DeveAtualizarCamposNaoNulos()
        {
            // Arrange
            var produto = new Produto(1, "Produto 1", 10, 5, "Categoria A", "Descrição A");
            var dto = new ProdutoDto
            {
                Nome = null,
                Preco = 0,
                Estoque = -1,
                Categoria = "Categoria B",
                Descricao = null
            };
            _repositoryMock.Setup(r => r.ObterPorIdAsync(1)).ReturnsAsync(produto);
            _repositoryMock.Setup(r => r.AtualizarAsync(produto)).Returns(Task.CompletedTask);

            // Act
            await _service.AtualizarParcialAsync(1, dto);

            // Assert
            Assert.Equal("Produto 1", produto.Nome); // não alterado
            Assert.Equal(10, produto.Preco); // não alterado
            Assert.Equal(5, produto.Estoque); // não alterado
            Assert.Equal("Categoria B", produto.Categoria); // alterado
            _repositoryMock.Verify(r => r.AtualizarAsync(produto), Times.Once);
        }

        [Fact]
        public async Task RemoverAsync_DeveChamarRepositorio()
        {
            // Arrange
            _repositoryMock.Setup(r => r.RemoverAsync(1)).Returns(Task.CompletedTask);

            // Act
            await _service.RemoverAsync(1);

            // Assert
            _repositoryMock.Verify(r => r.RemoverAsync(1), Times.Once);
        }
    }
}
