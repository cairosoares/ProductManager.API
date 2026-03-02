using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProductManager.Application.DTOs
{
    public class CriarProdutoDto : AtualizarProdutoDto
    {
        [JsonPropertyName("productCode")]
        public int CodigoProduto { get; set; }
    }
}
