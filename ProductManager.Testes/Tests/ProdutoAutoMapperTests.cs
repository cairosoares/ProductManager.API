using AutoMapper;
using Microsoft.Extensions.Logging;
using ProductManager.Application.DTOs;
using ProductManager.Application.Mapping;
using ProductManager.Domain.Entities;
using FluentAssertions;

namespace ProductManager.Tests.Tests
{
    public class ProdutoMapperTests
    {
        private readonly IMapper _mapper;

        public ProdutoMapperTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new ProdutoProfile()), new LoggerFactory());
            _mapper = config.CreateMapper();
        }

        [Fact]
        public void ProdutoParaDto_DeveMapearCorretamente()
        {
            var produto = new Produto(1, "Notebook", 3500, 10, "Dell");
            var dto = _mapper.Map<ProdutoDto>(produto);

            dto.CodigoProduto.Should().Be(produto.CodigoProduto);
            dto.Nome.Should().Be(produto.Nome);
            dto.Preco.Should().Be(produto.Preco);
            dto.Estoque.Should().Be(produto.Estoque);
            dto.Descricao.Should().Be(produto.Descricao);
        }

        [Fact]
        public void DtoParaProduto_DeveMapearCorretamente()
        {
            var dto = new ProdutoDto
            {
                CodigoProduto = 1,
                Nome = "Notebook",
                Preco = 3500,
                Estoque = 10,
                Categoria = "teste",
                Descricao = "Dell"
            };

            var produto = _mapper.Map<Produto>(dto);

            produto.CodigoProduto.Should().Be(dto.CodigoProduto);
            produto.Nome.Should().Be(dto.Nome);
            produto.Preco.Should().Be(dto.Preco);
            produto.Estoque.Should().Be(dto.Estoque);
            produto.Descricao.Should().Be(dto.Descricao);
        }
    }
}
