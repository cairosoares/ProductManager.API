using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProductManager.Application.DTOs
{
    public class AtualizarProdutoDto
    {
        [JsonPropertyName("title")]
        public required string Nome { get; set; }

        [JsonPropertyName("price")]
        public decimal Preco { get; set; }

        [JsonPropertyName("description")]
        public string Descricao { get; set; } = string.Empty;

        [JsonPropertyName("category")]
        public required string Categoria { get; set; }

        [JsonPropertyName("stock")]
        public int Estoque {  get; set; }

    }
}
