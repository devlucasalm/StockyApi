using Microsoft.AspNetCore.Mvc;
using StockyApi.Models;
using StockyApi.Services.Category;

namespace StockyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryInterface _categoryService;

        public CategoryController(ICategoryInterface categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<AppResponse<Pagination<CategoryModel>>>> GetCategories(int skip, int take)
        {
            return Ok(await _categoryService.GetCategories(skip, take));
        }

        [HttpPost]
        public async Task<ActionResult<AppResponse<CategoryModel>>> CreateCategory(CategoryModel categoria)
        {
            return Ok(await _categoryService.CreateCategory(categoria));
        }

        [HttpDelete]
        public async Task<ActionResult<AppResponse<CategoryModel>>> DeleteCategory(Guid id)
        {
            return Ok(await _categoryService.DeleteCategory(id));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppResponse<CategoryModel>>> GetCategoryById(Guid id)
        {
            return Ok(await _categoryService.GetCategoryById(id));
        }

        [HttpPut]
        public async Task<ActionResult<AppResponse<CategoryModel>>> UpdateCategory(CategoryModel categoria)
        {
            return Ok(await _categoryService.UpdateCategory(categoria));
        }
    }
}
