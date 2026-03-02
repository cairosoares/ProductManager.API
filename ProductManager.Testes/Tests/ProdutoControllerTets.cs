using Microsoft.AspNetCore.Mvc;
using Moq;
using ProductManager.API.Controllers;
using ProductManager.Application.DTOs;
using ProductManager.Application.Services;
using ProductManager.Domain.Entities;

namespace ProductManager.Tests.Tests
{
    public class ProdutosControllerTests
    {
        private readonly Mock<IProdutoService> _serviceMock;
        private readonly ProdutosController _controller;

        public ProdutosControllerTests()
        {
            _serviceMock = new Mock<IProdutoService>();
            _controller = new ProdutosController(_serviceMock.Object);
        }

        [Fact]
        public async Task ObterTodos_DeveRetornarOkComProdutos()
        {
            // Arrange
            var produtos = new List<ProdutoDto>
        {
            new ProdutoDto { Id = 1, Nome = "Produto 1", Categoria = "Teste"},
            new ProdutoDto { Id = 2, Nome = "Produto 2", Categoria = "Teste"}
        };
            _serviceMock.Setup(s => s.ObterTodosAsync()).ReturnsAsync(produtos);

            // Act
            var result = await _controller.ObterTodos();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsAssignableFrom<IEnumerable<ProdutoDto>>(okResult.Value);
            Assert.Equal(2, ((List<ProdutoDto>)returnValue).Count);
        }

        [Fact]
        public async Task ObterPorId_ProdutoExistente_DeveRetornarOk()
        {
            // Arrange
            var produto = new ProdutoDto { Id = 1, Nome = "Produto 1", Categoria = "Teste" };
            _serviceMock.Setup(s => s.ObterPorIdAsync(1)).ReturnsAsync(produto);

            // Act
            var result = await _controller.ObterPorId(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<ProdutoDto>(okResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task ObterPorId_ProdutoNaoExistente_DeveRetornarNotFound()
        {
            // Arrange
            _serviceMock.Setup(s => s.ObterPorIdAsync(1)).ThrowsAsync(new KeyNotFoundException());

            // Act
            var result = await _controller.ObterPorId(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Adicionar_DeveRetornarCreatedAtAction()
        {
            // Arrange
            var dto = new CriarProdutoDto { Nome = "Produto Novo", Categoria = "Teste"};
            var produto = new ProdutoDto { Id = 1, Nome = "Produto Novo", Categoria = "Teste" };
            _serviceMock.Setup(s => s.AdicionarAsync(dto)).ReturnsAsync(produto);

            // Act
            var result = await _controller.Adicionar(dto);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.ObterPorId), createdResult.ActionName);
            var returnValue = Assert.IsType<ProdutoDto>(createdResult.Value);
            Assert.Equal(1, returnValue.Id);
        }

        [Fact]
        public async Task Atualizar_ProdutoExistente_DeveRetornarNoContent()
        {
            // Arrange
            var dto = new ProdutoDto { Nome = "Atualizado" , Categoria = "Teste" };
            _serviceMock.Setup(s => s.AtualizarAsync(1, dto)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Atualizar(1, dto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Atualizar_ProdutoNaoExistente_DeveRetornarNotFound()
        {
            // Arrange
            var dto = new ProdutoDto { Nome = "Atualizado" , Categoria = "Teste" };
            _serviceMock.Setup(s => s.AtualizarAsync(1, dto)).ThrowsAsync(new KeyNotFoundException());

            // Act
            var result = await _controller.Atualizar(1, dto);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task AtualizarParcial_ProdutoExistente_DeveRetornarNoContent()
        {
            // Arrange
            var dto = new ProdutoDto { Nome = "Atualizado" , Categoria = "Teste" };
            _serviceMock.Setup(s => s.AtualizarParcialAsync(1, dto)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AtualizarParcial(1, dto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task AtualizarParcial_ProdutoNaoExistente_DeveRetornarNotFound()
        {
            // Arrange
            var dto = new ProdutoDto { Nome = "Atualizado", Categoria = "Teste" };
            _serviceMock.Setup(s => s.AtualizarParcialAsync(1, dto)).ThrowsAsync(new KeyNotFoundException());

            // Act
            var result = await _controller.AtualizarParcial(1, dto);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Remover_ProdutoExistente_DeveRetornarOk()
        {
            // Arrange
            _serviceMock.Setup(s => s.RemoverAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Remover(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("produto foi removido com sucesso", okResult.Value);
        }

        [Fact]
        public async Task Remover_ProdutoNaoExistente_DeveRetornarNotFound()
        {
            // Arrange
            _serviceMock.Setup(s => s.RemoverAsync(1)).ThrowsAsync(new KeyNotFoundException());

            // Act
            var result = await _controller.Remover(1);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}
