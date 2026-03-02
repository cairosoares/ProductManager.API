using FluentValidation.TestHelper;
using ProductManager.Application.DTOs;
using ProductManager.Application.Validators;

namespace ProductManager.Tests.Tests
{
    public class ProdutoValidatorTests
    {
        private readonly ProdutoValidator _validator = new ProdutoValidator();

        [Fact]
        public void ProdutoDto_Valido_NaoDeveGerarErro()
        {
            var dto = new ProdutoDto
            {
                CodigoProduto = 1,
                Nome = "Produto Teste",
                Preco = 100m,
                Categoria = "Teste",
                Estoque = 5
            };

            var result = _validator.TestValidate(dto);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void ProdutoDto_Invalido_DeveGerarErros()
        {
            var dto = new ProdutoDto
            {
                CodigoProduto = 0,
                Nome = "",
                Preco = 0,
                Categoria = "Teste",
                Estoque = -1
            };

            var result = _validator.TestValidate(dto);
            result.ShouldHaveValidationErrorFor(x => x.CodigoProduto);
            result.ShouldHaveValidationErrorFor(x => x.Nome);
            result.ShouldHaveValidationErrorFor(x => x.Preco);
            result.ShouldHaveValidationErrorFor(x => x.Estoque);
        }
    }
}
