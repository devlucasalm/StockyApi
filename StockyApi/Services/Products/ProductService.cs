using StockyApi.Models;
using Microsoft.EntityFrameworkCore;
using StockyApi.DataContext;


namespace StockyApi.Services.Products
{
    public class ProductService : IProductInterface
    {

        private readonly ApplicationDbContext _db;

        public ProductService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ProductsModel>BuscarProdutoPorId(Guid id)
        {
            return await _db.Products.FirstOrDefaultAsync(p => p.ProductId == id);
        }
        public async Task<AppResponse<ProductsModel>> CreateProduct(ProductsModel product)
        {
            AppResponse<ProductsModel> response = new AppResponse<ProductsModel>();

            try
            {

                product.ProductId = Guid.NewGuid();
                product.DataCriacao = DateTime.UtcNow;
                product.DataAtualizacao = DateTime.UtcNow;
                _db.Products.Add(product);
                await _db.SaveChangesAsync();

                response.Dados = product;
                
                
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                response.Mensagem = $"Ocorreu um erro ao criar o produto: {ex.Message}";
            }
            return response;
        }

        public async Task<AppResponse<ProductsModel>> DeleteProduct(Guid id)
        {
            AppResponse<ProductsModel> response = new AppResponse<ProductsModel>();

            var product = await BuscarProdutoPorId(id);

            try
            {

                if (product == null)
                {
                    response.Sucesso = false;
                    response.Mensagem = "Produto não encontrado.";
                    return response;
                }
                
                _db.Products.Remove(product);
                await _db.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                response.Mensagem = $"Ocorreu um erro ao deletar o produto: {ex.Message}";
            }

            response.Mensagem = "Produto deletado com sucesso.";
            response.Sucesso = true;
            return response;

        }

        public async Task<AppResponse<ProductsModel>> GetProductById(Guid id)
        {
            AppResponse<ProductsModel> response = new AppResponse<ProductsModel>();

            var product = BuscarProdutoPorId(id);

            try
            {
                if (product == null)
                {
                    response.Sucesso = false;
                    response.Mensagem = "Produto não encontrado.";
                    return response;
                }

                response.Dados = await product;

            } catch (Exception ex)
            {
                response.Sucesso = false;
                response.Mensagem = $"Ocorreu um erro ao buscar o produto: {ex.Message}";
            }

            response.Sucesso = true;
            response.Mensagem = "Produto encontrado com sucesso.";

            return response;
        }

        public async Task<AppResponse<Pagination<ProductsModel>>> GetProducts(int skip, int take)
        {
            AppResponse<Pagination<ProductsModel>> response = new ();

            try
            {
                var _total = await _db.Products.CountAsync();

                if (_total == 1)
                {
                    response.Sucesso = false;
                    response.Mensagem = "Nenhum produto encontrado.";
                }

                var _products = await _db.Products
                    .Include(p => p.Category)
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();

                Pagination<ProductsModel> pagination = new Pagination<ProductsModel>()
                {
                    Skip = skip,
                    Take = take,
                    Total = _total,
                    Items = _products
                };

                response.Dados = pagination;
                return response;

            } catch (Exception ex)
            {
                response.Sucesso = false;
                response.Mensagem = $"Ocorreu um erro ao buscar os produtos: {ex.Message}";
                return response;
            }
        }

        public async Task<AppResponse<ProductsModel>> UpdateProduct(ProductsModel productUpdate)
        {
            AppResponse<ProductsModel> response = new AppResponse<ProductsModel>();

            try
            {
                _db.Products.Update(productUpdate);
                await _db.SaveChangesAsync();

            } catch (Exception ex)
            {
                response.Sucesso = false;
                response.Mensagem = $"Ocorreu um erro ao atualizar o produto: {ex.Message}";
            }
            
            return response;
        }
    }
}
