using StockyApi.Models;

namespace StockyApi.Services.Products
{
    public interface IProductInterface
    {
        public Task<AppResponse<Pagination<ProductsModel>>> GetProducts(int skip, int take);
        public Task<AppResponse<ProductsModel>> GetProductById(Guid id);
        public Task<AppResponse<ProductsModel>> CreateProduct(ProductCreateDto product, string baseUrl);
        Task<AppResponse<ProductsModel>> UpdateProduct(ProductUpdateDto dto, string baseUrl);
        public Task<AppResponse<ProductsModel>> DeleteProduct(Guid id);
    }
}
