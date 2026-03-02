using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ProductManager.Application.Mapping;
using ProductManager.Application.Services;
using ProductManager.Application.Validators;
using ProductManager.Domain.Entities;
using ProductManager.Domain.Interfaces;
using ProductManager.Infrastructure.Data;
using ProductManager.Infrastructure.Repositories;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("BancoDeDadosTemp"));

        // AutoMapper
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ProdutoProfile());
        }, new LoggerFactory());
        var mapper = mapperConfig.CreateMapper();
        builder.Services.AddSingleton(mapper);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AngularLocal",
                policy =>
                {
                    policy
                        .WithOrigins("http://localhost:4200") // Angular
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials(); // se usar cookies/token
                });
        });

        // Repositorio e Service
        builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
        builder.Services.AddScoped<IProdutoService, ProdutoService>();

        // Controllers + FluentValidation
        builder.Services.AddControllers();
        builder.Services.AddRouting(o => o.LowercaseUrls = true);
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddFluentValidationClientsideAdapters();
        builder.Services.AddValidatorsFromAssemblyContaining<ProdutoValidator>();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            if (!context.Produtos.Any())
            {
                context.Produtos.AddRange(
                    new Produto(1001, "Notebook Gamer", 4500.00m, 10, "Eletrônicos", "Notebook com processador i7 e 16GB RAM"),
                    new Produto(1002, "Camiseta Polo", 79.90m, 50, "Roupas", "Camiseta de algodão confortável"),
                    new Produto(1003, "Chocolate Amargo", 15.50m, 200, "Alimentos", "Tablete de chocolate 70%"),
                    new Produto(1004, "Livro C# Avançado", 120.00m, 30, "Livros", "Livro sobre programação em C#"),
                    new Produto(1005, "Carrinho de Controle Remoto", 250.00m, 25, "Brinquedos", "Brinquedo para crianças acima de 6 anos"),
                    new Produto(1006, "Smartphone Android", 2000.00m, 40, "Eletrônicos", "Celular com 128GB de armazenamento"),
                    new Produto(1007, "Calça Jeans", 149.90m, 60, "Roupas", "Calça jeans masculina"),
                    new Produto(1008, "Biscoitos Sortidos", 9.90m, 150, "Alimentos", "Pacote de biscoitos variados"),
                    new Produto(1009, "Livro de Receitas", 45.00m, 35, "Livros", "Livro com receitas rápidas e fáceis"),
                    new Produto(1010, "Boneca Fashion", 180.00m, 20, "Brinquedos", "Boneca com acessórios de moda"),
                    new Produto(1011, "Fone de Ouvido Bluetooth", 350.00m, 30, "Eletrônicos", "Fone sem fio com ótima qualidade"),
                    new Produto(1012, "Jaqueta Masculina", 299.90m, 15, "Roupas", "Jaqueta de couro sintético"),
                    new Produto(1013, "Arroz Integral", 28.00m, 100, "Alimentos", "Pacote de 5kg de arroz integral"),
                    new Produto(1014, "Livro de História", 85.00m, 25, "Livros", "História mundial resumida"),
                    new Produto(1015, "Quebra-cabeça 500 peças", 60.00m, 40, "Brinquedos", "Puzzle colorido para todas as idades"),
                    new Produto(1016, "Monitor LED 24\"", 850.00m, 12, "Eletrônicos", "Monitor Full HD"),
                    new Produto(1017, "Blusa de Frio", 129.90m, 35, "Roupas", "Blusa feminina de lã"),
                    new Produto(1018, "Macarrão Integral", 7.50m, 180, "Alimentos", "Pacote de 500g"),
                    new Produto(1019, "Livro de Matemática", 95.00m, 20, "Livros", "Conteúdos para ensino médio"),
                    new Produto(1020, "Jogos de Tabuleiro", 120.00m, 22, "Brinquedos", "Diversos jogos clássicos"),
                    new Produto(1021, "Tablet 10\"", 1200.00m, 18, "Eletrônicos", "Tablet para estudos e entretenimento"),
                    new Produto(1022, "Saia Feminina", 89.90m, 40, "Roupas", "Saia leve e confortável"),
                    new Produto(1023, "Azeite Extra Virgem", 35.00m, 60, "Alimentos", "Garrafa de 500ml"),
                    new Produto(1024, "Livro de Ficção", 65.00m, 28, "Livros", "Romance de fantasia"),
                    new Produto(1025, "Pelúcia Urso", 75.00m, 50, "Brinquedos", "Ursinho macio para crianças"),
                    new Produto(1026, "Câmera Digital", 950.00m, 8, "Eletrônicos", "Câmera compacta com zoom 20x"),
                    new Produto(1027, "Chapéu Masculino", 59.90m, 25, "Roupas", "Chapéu de aba larga"),
                    new Produto(1028, "Leite UHT", 4.50m, 100, "Alimentos", "Pacote de 1L"),
                    new Produto(1029, "Livro Infantil", 40.00m, 35, "Livros", "Histórias curtas para crianças"),
                    new Produto(1030, "Carrinho de Montar", 150.00m, 20, "Brinquedos", "Brinquedo educativo"),
                    new Produto(1031, "Smartwatch", 600.00m, 15, "Eletrônicos", "Relógio inteligente com monitoramento"),
                    new Produto(1032, "Tênis Esportivo", 199.90m, 40, "Roupas", "Tênis para corrida"),
                    new Produto(1033, "Cereal Matinal", 12.00m, 80, "Alimentos", "Pacote de 500g"),
                    new Produto(1034, "Livro de Ciências", 110.00m, 22, "Livros", "Conteúdos de física e química"),
                    new Produto(1035, "Brinquedo Musical", 90.00m, 18, "Brinquedos", "Instrumento de brinquedo"),
                    new Produto(1036, "Teclado Mecânico", 450.00m, 10, "Eletrônicos", "Teclado RGB para jogos"),
                    new Produto(1037, "Camisa Social", 119.90m, 30, "Roupas", "Camisa masculina formal"),
                    new Produto(1038, "Farinha de Trigo", 6.50m, 120, "Alimentos", "Pacote de 1kg"),
                    new Produto(1039, "Livro de Romance", 55.00m, 25, "Livros", "Romance contemporâneo"),
                    new Produto(1040, "Boneco de Ação", 80.00m, 35, "Brinquedos", "Personagem de super-herói"),
                    new Produto(1041, "Headset Gamer", 320.00m, 12, "Eletrônicos", "Fone com microfone e LED"),
                    new Produto(1042, "Casaco Feminino", 249.90m, 20, "Roupas", "Casaco de inverno"),
                    new Produto(1043, "Molho de Tomate", 8.50m, 90, "Alimentos", "Garrafa 340g"),
                    new Produto(1044, "Livro de Aventura", 70.00m, 18, "Livros", "História de viagem e exploração"),
                    new Produto(1045, "Quebra-cabeça 1000 peças", 120.00m, 15, "Brinquedos", "Desafio para adultos"),
                    new Produto(1046, "Monitor Gamer 27\"", 1800.00m, 7, "Eletrônicos", "Alta taxa de atualização"),
                    new Produto(1047, "Blusa Casual", 99.90m, 45, "Roupas", "Blusa feminina leve"),
                    new Produto(1048, "Óleo de Coco", 22.00m, 70, "Alimentos", "Garrafa 500ml"),
                    new Produto(1049, "Livro de Mistério", 60.00m, 20, "Livros", "Suspense policial"),
                    new Produto(1050, "Trenzinho de Madeira", 140.00m, 18, "Brinquedos", "Brinquedo educativo")
                );

                context.SaveChanges();
            }
        }

        app.UseCors("AngularLocal");
        app.UseSwagger();
        app.UseSwaggerUI();
        app.MapControllers();
        app.Run();
    }
}