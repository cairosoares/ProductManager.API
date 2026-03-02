using ProductManager.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManager.Application.Services
{
    public interface IProdutoService
    {
        Task<IEnumerable<ProdutoDto>> ObterTodosAsync();
        Task<ProdutoDto> ObterPorIdAsync(int id);
        Task<ProdutoDto> AdicionarAsync(CriarProdutoDto dto);
        Task AtualizarAsync(int id, ProdutoDto dto);
        Task AtualizarParcialAsync(int id, ProdutoDto dto);
        Task RemoverAsync(int id);
    }
}
