using AutoMapper;
using ProductManager.Application.DTOs;
using ProductManager.Domain.Entities;

namespace ProductManager.Application.Mapping
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<Produto, ProdutoDto>().ReverseMap();
            CreateMap<Produto, CriarProdutoDto>().ReverseMap();
        }
    }
}
