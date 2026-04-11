using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockyApi.Models;
using StockyApi.Services.Products;

namespace StockyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase

    {
        private readonly IProductInterface _productService;
        public ProductController(IProductInterface productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<AppResponse<Pagination<ProductsModel>>>> GetProducts(int skip, int take)
        {
            return Ok(await _productService.GetProducts(skip, take));
        }

        [HttpPost]
        public async Task<ActionResult<AppResponse<ProductsModel>>> CreateProduct([FromForm] ProductCreateDto dto)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            return Ok(await _productService.CreateProduct(dto, baseUrl));
        }

        [HttpDelete]
        public async Task<ActionResult<AppResponse<ProductsModel>>> DeleteProduct(Guid id)
        {
            return Ok(await _productService.DeleteProduct(id));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppResponse<ProductsModel>>> GetProductById(Guid id)
        {
            return Ok(await _productService.GetProductById(id));
        }

        [HttpPut]
        public async Task<ActionResult<AppResponse<ProductsModel>>> UpdateProduct([FromForm] ProductUpdateDto dto)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            return Ok(await _productService.UpdateProduct(dto, baseUrl));
        }
    }
}
