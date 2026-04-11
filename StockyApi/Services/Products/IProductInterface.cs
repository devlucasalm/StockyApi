using StockyApi.Models;

namespace StockyApi.Services.Products
{
    public interface IProductInterface
    {
        public Task<AppResponse<Pagination<ProductsModel>>> GetProducts(int skip, int take);
        public Task<AppResponse<ProductsModel>> GetProductById(Guid id);
        public Task<AppResponse<ProductsModel>> CreateProduct(ProductsModel product);
        public Task<AppResponse<ProductsModel>> UpdateProduct(ProductsModel productUpdate);
        public Task<AppResponse<ProductsModel>> DeleteProduct(Guid id);
    }
}
