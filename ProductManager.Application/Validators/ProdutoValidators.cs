using FluentValidation;
using ProductManager.Application.DTOs;

namespace ProductManager.Application.Validators
{
    public class ProdutoValidator : AbstractValidator<CriarProdutoDto>
    {
        public ProdutoValidator()
        {
            RuleFor(x => x.CodigoProduto)
                .GreaterThan(0)
                .WithMessage("Código do produto deve ser maior que zero.");

            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage("Nome é obrigatório.")
                .MaximumLength(150)
                .WithMessage("Nome não pode ter mais que 150 caracteres.");

            RuleFor(x => x.Preco)
                .GreaterThan(0)
                .WithMessage("Preço deve ser maior que zero.");

            RuleFor(x => x.Estoque)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Estoque não pode ser negativo.");
        }
    }
}
