using StockyApi.Models;

namespace StockyApi.Services.Category
{
    public interface ICategoryInterface
    {
        public Task<AppResponse<Pagination<CategoryModel>>> GetCategories(int skip, int take);
        public Task<AppResponse<CategoryModel>> GetCategoryById(Guid id);
        public Task<AppResponse<CategoryModel>> CreateCategory(CategoryModel category);
        public Task<AppResponse<CategoryModel>> UpdateCategory(CategoryModel categoryUpdate);
        public Task<AppResponse<CategoryModel>> DeleteCategory(Guid id);

    }
}
