using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Domain.Entities
{
    public class Produto
    {
        public int Id { get; private set; }
        public int CodigoProduto { get; private set; }
        public string Nome { get; private set; }
        public string? Descricao { get; private set; }
        public string Categoria { get; private set; }
        public decimal Preco { get; private set; }
        public int Estoque { get; private set; }

        protected Produto() { }

        public Produto(int codigoProduto, string nome, decimal preco, int estoque, string categoria, string? descricao = null)
        {
            CodigoProduto = codigoProduto > 0 ? codigoProduto : throw new ArgumentException("Codigo do produto deve ser maior que zero.");
            Nome = !string.IsNullOrWhiteSpace(nome) ? nome : throw new ArgumentException("Nome é obrigatório.");
            Preco = preco > 0 ? preco : throw new ArgumentException("Preço deve ser maior que zeroo.");
            Estoque = estoque >= 0 ? estoque : throw new ArgumentException("Estoque não pode ser negativo.");
            Categoria = !string.IsNullOrWhiteSpace(categoria) ? categoria : throw new ArgumentException("Categoria é obrigatório.");
            Descricao = descricao;
        }

        public void Atualizar(string nome, decimal preco, int estoque,string categoria, string? descricao)
        {
            Nome = !string.IsNullOrWhiteSpace(nome) ? nome : throw new ArgumentException("Nome é obrigatório.");
            Preco = preco > 0 ? preco : throw new ArgumentException("Preço deve ser maior que zero.");
            Estoque = estoque >= 0 ? estoque : throw new ArgumentException("Estoque não pode ser negativo.");
            Categoria = !string.IsNullOrWhiteSpace(categoria) ? categoria : throw new ArgumentException("Categoria é obrigatório.");
            Descricao = descricao;
        }
    }
}
