using Microsoft.EntityFrameworkCore;
using StockyApi.DataContext;
using StockyApi.Models;

namespace StockyApi.Services.Dashboard
{
    public class DashboardService : IDashboardInterface
    {
        private readonly ApplicationDbContext _db;

        public DashboardService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<AppResponse<DashboardDto>> GetDashboard()
        {
            var response = new AppResponse<DashboardDto>();

            try
            {
                var totalProdutos = await _db.Products.CountAsync();
                var totalCategorias = await _db.Category.CountAsync();

                var produtosAtivos = await _db.Products.CountAsync(p => p.Ativo);
                var produtosInativos = await _db.Products.CountAsync(p => !p.Ativo);

                var totalItensEstoque = await _db.Products.SumAsync(p => p.Quantidade);

                var valorTotalEstoque = await _db.Products
                    .SumAsync(p => p.Preco * p.Quantidade);

                var produtosPorCategoria = await _db.Products
                    .Include(p => p.Category)
                    .GroupBy(p => p.Category.Nome)
                    .Select(g => new GraficoCategoriaDto
                    {
                        Categoria = g.Key,
                        Quantidade = g.Count()
                    })
                    .ToListAsync();

                var estoquePorProduto = await _db.Products
                    .Select(p => new GraficoProdutoDto
                    {
                        Nome = p.Nome,
                        Quantidade = p.Quantidade
                    })
                    .ToListAsync();

                var produtosBaixoEstoque = await _db.Products
                    .Where(p => p.Quantidade <= 5)
                    .Select(p => new GraficoProdutoDto
                    {
                        Nome = p.Nome,
                        Quantidade = p.Quantidade
                    })
                    .ToListAsync();

                var dashboard = new DashboardDto
                {
                    TotalProdutos = totalProdutos,
                    TotalCategorias = totalCategorias,
                    ProdutosAtivos = produtosAtivos,
                    ProdutosInativos = produtosInativos,
                    TotalItensEstoque = totalItensEstoque,
                    ValorTotalEstoque = valorTotalEstoque,

                    ProdutosPorCategoria = produtosPorCategoria,
                    EstoquePorProduto = estoquePorProduto,
                    ProdutosBaixoEstoque = produtosBaixoEstoque
                };

                response.Dados = dashboard;
                response.Mensagem = "Dashboard carregado com sucesso!";
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                response.Mensagem = $"Erro ao carregar dashboard: {ex.Message}";
            }

            return response;
        }
    }
}