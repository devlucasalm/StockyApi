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
        public async Task<AppResponse<ProductsModel>> CreateProduct(ProductCreateDto dto, string baseUrl)
        {
            AppResponse<ProductsModel> response = new AppResponse<ProductsModel>();

            try
            {
                var product = new ProductsModel
                {
                    ProductId = Guid.NewGuid(),
                    Nome = dto.Nome,
                    Descricao = dto.Descricao,
                    Preco = dto.Preco,
                    Quantidade = dto.Quantidade,
                    CategoryId = dto.CategoryId,
                    DataCriacao = DateTime.UtcNow,
                    DataAtualizacao = DateTime.UtcNow
                };

                if (dto.Imagem != null && dto.Imagem.Length > 0)
                {
                    var pasta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");

                    Directory.CreateDirectory(pasta);

                    var nomeArquivo = Guid.NewGuid() + Path.GetExtension(dto.Imagem.FileName);
                    var caminho = Path.Combine(pasta, nomeArquivo);

                    using (var stream = new FileStream(caminho, FileMode.Create))
                    {
                        await dto.Imagem.CopyToAsync(stream);
                    }

                    var url = $"{baseUrl}/images/{nomeArquivo}";
                    product.ImagemUrl = url;
                }

                _db.Products.Add(product);
                await _db.SaveChangesAsync();

                response.Dados = product;
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                response.Mensagem = $"Erro ao criar produto: {ex.Message}";
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

        public async Task<AppResponse<ProductsModel>> UpdateProduct(ProductUpdateDto dto, string baseUrl)
        {
            AppResponse<ProductsModel> response = new AppResponse<ProductsModel>();

            try
            {
                var product = await _db.Products.FirstOrDefaultAsync(p => p.ProductId == dto.ProductId);

                if (product == null)
                {
                    response.Sucesso = false;
                    response.Mensagem = "Produto não encontrado.";
                    return response;
                }

                product.Nome = dto.Nome;
                product.Descricao = dto.Descricao;
                product.Preco = dto.Preco;
                product.Ativo = dto.Ativo;
                product.Quantidade = dto.Quantidade;
                product.CategoryId = dto.CategoryId;
                product.DataAtualizacao = DateTime.UtcNow;

                if (dto.Imagem != null && dto.Imagem.Length > 0)
                {
                    var pasta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                    Directory.CreateDirectory(pasta);

                    if (!string.IsNullOrEmpty(product.ImagemUrl))
                    {
                        var nomeAntigo = Path.GetFileName(product.ImagemUrl);
                        var caminhoAntigo = Path.Combine(pasta, nomeAntigo);

                        if (File.Exists(caminhoAntigo))
                        {
                            File.Delete(caminhoAntigo);
                        }
                    }

                    var nomeArquivo = Guid.NewGuid() + Path.GetExtension(dto.Imagem.FileName);
                    var caminho = Path.Combine(pasta, nomeArquivo);

                    using (var stream = new FileStream(caminho, FileMode.Create))
                    {
                        await dto.Imagem.CopyToAsync(stream);
                    }

                    product.ImagemUrl = $"{baseUrl}/images/{nomeArquivo}";
                }

                _db.Products.Update(product);
                await _db.SaveChangesAsync();

                response.Dados = product;
            }
            catch (Exception ex)
            {
                response.Sucesso = false;
                response.Mensagem = $"Erro ao atualizar produto: {ex.Message}";
            }

            return response;
        }
    }
}
