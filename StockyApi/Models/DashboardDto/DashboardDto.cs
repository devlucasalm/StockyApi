namespace StockyApi.Models
{
    public class DashboardDto
    {
        public int TotalProdutos { get; set; }
        public int TotalCategorias { get; set; }
        public int ProdutosAtivos { get; set; }
        public int ProdutosInativos { get; set; }
        public int TotalItensEstoque { get; set; }
        public decimal ValorTotalEstoque { get; set; }

        public List<GraficoCategoriaDto> ProdutosPorCategoria { get; set; }
        public List<GraficoProdutoDto> EstoquePorProduto { get; set; }
        public List<GraficoProdutoDto> ProdutosBaixoEstoque { get; set; }
    }
}