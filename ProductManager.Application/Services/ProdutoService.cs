using AutoMapper;
using ProductManager.Application.DTOs;
using ProductManager.Domain.Entities;
using ProductManager.Domain.Interfaces;

namespace ProductManager.Application.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _repository;
        private readonly IMapper _mapper;

        public ProdutoService(IProdutoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProdutoDto>> ObterTodosAsync()
        {
            var produtos = await _repository.ObterTodosAsync();
            return _mapper.Map<IEnumerable<ProdutoDto>>(produtos);
        }

        public async Task<ProdutoDto> ObterPorIdAsync(int id)
        {
            var produto = await _repository.ObterPorIdAsync(id)
                ?? throw new KeyNotFoundException("Produto não encontrado");
            return _mapper.Map<ProdutoDto>(produto);
        }

        public async Task<ProdutoDto> AdicionarAsync(CriarProdutoDto dto)
        {
            var produto = _mapper.Map<Produto>(dto);
            await _repository.AdicionarAsync(produto);
            return _mapper.Map<ProdutoDto>(produto);
        }

        public async Task AtualizarAsync(int id, ProdutoDto dto)
        {
            var produto = await _repository.ObterPorIdAsync(id)
                ?? throw new KeyNotFoundException("Produto não encontrado");

            produto.Atualizar(dto.Nome, dto.Preco, dto.Estoque, dto.Categoria, dto.Descricao);
            await _repository.AtualizarAsync(produto);
        }

        public async Task AtualizarParcialAsync(int id, ProdutoDto dto)
        {
            var produto = await _repository.ObterPorIdAsync(id)
                ?? throw new KeyNotFoundException("Produto não encontrado");

            // Apenas atualiza os campos não nulos
            var nome = string.IsNullOrWhiteSpace(dto.Nome) ? produto.Nome : dto.Nome;
            var preco = dto.Preco <= 0 ? produto.Preco : dto.Preco;
            var estoque = dto.Estoque < 0 ? produto.Estoque : dto.Estoque;
            var categoria = string.IsNullOrWhiteSpace(dto.Categoria) ? produto.Categoria : dto.Categoria;
            var descricao = dto.Descricao ?? produto.Descricao;

            produto.Atualizar(nome, preco, estoque, categoria, descricao);
            await _repository.AtualizarAsync(produto);
        }

        public async Task RemoverAsync(int id)
        {
            await _repository.RemoverAsync(id);
        }
    }
}
