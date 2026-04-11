using StockyApi.Models;
using StockyApi.DataContext;
using Microsoft.EntityFrameworkCore;

namespace StockyApi.Services.Category
{
    public class CategoryService : ICategoryInterface
    {
        private readonly ApplicationDbContext _db;

        public CategoryService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<CategoryModel> BuscarCategoriaPorId(Guid id)
        {
            return await _db.Category.FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        public async Task<AppResponse<CategoryModel>> CreateCategory(CategoryModel category)
        {
            AppResponse<CategoryModel> response = new AppResponse<CategoryModel>();

            try
            {
                category.CategoryId = Guid.NewGuid();
                category.DataCriacao = DateTime.UtcNow;
                category.DataAtualizacao = DateTime.UtcNow;
                _db.Category.Add(category);
                await _db.SaveChangesAsync();

                response.Dados = category;

            } catch (Exception ex)
            {
                response.Sucesso = false;
                response.Mensagem = $"Ocorreu um erro ao cria a categoria: {ex.Message}";
            }

            response.Mensagem = "Categoria criada com sucesso!";
            return response;
        }

        public async Task<AppResponse<CategoryModel>> DeleteCategory(Guid id)
        {
            AppResponse<CategoryModel> response = new AppResponse<CategoryModel>();

            try{

                var category = await BuscarCategoriaPorId(id);

                _db.Category.Remove(category);
                await _db.SaveChangesAsync();

            } catch (Exception ex)
            {
                response.Sucesso = false;
                response.Mensagem = $"Ocorreu um erro ao deletar a categoria: {ex.Message}";
            }

            response.Mensagem = "Categoria deletada com sucesso!";
            return response;
        }

        public async Task<AppResponse<Pagination<CategoryModel>>> GetCategories(int skip, int take)
        {
            AppResponse<Pagination<CategoryModel>> response = new ();

            try
            {
                var _total = await _db.Category.CountAsync();

                if (_total == 0)
                {
                    response.Sucesso = false;
                    response.Mensagem = "Nenhuma categoria encontrada.";
                }

                var categories = await _db.Category
                    .Skip(skip)
                    .Take(take)
                    .ToListAsync();

                Pagination<CategoryModel> pagination = new ()
                 {
                    Skip = skip,
                    Take = take,
                    Total = _total,
                    Items = categories
                };

                response.Dados = pagination;
                return response;

            } catch (Exception ex)
            {
                response.Sucesso = false;
                response.Mensagem = $"Ocorreu um erro ao obter as categorias: {ex.Message}";
            }

            return response;
        }

        public async Task<AppResponse<CategoryModel>> GetCategoryById(Guid id)
        {
            AppResponse<CategoryModel> response = new AppResponse<CategoryModel>();

            try
            {

                var category = await BuscarCategoriaPorId(id);

                if (category == null)
                {
                    response.Sucesso = false;
                    response.Mensagem = "Categoria não encontrada.";
                }
                
                response.Dados = category;

            } catch (Exception ex)
            {
                response.Sucesso = false;
                response.Mensagem = $"Ocorreu um erro ao obter a categoria: {ex.Message}";
            }

            return response;
        }

        public async Task<AppResponse<CategoryModel>> UpdateCategory(CategoryModel categoryUpdate)
        {
            AppResponse<CategoryModel> response = new AppResponse<CategoryModel>();

            try
            {
                _db.Category.Update(categoryUpdate);
                await _db.SaveChangesAsync();
                
                response.Dados = categoryUpdate;

            } catch (Exception ex)
            {
                response.Sucesso = false;
                response.Mensagem = $"Ocorreu um erro ao atualizar a categoria: {ex.Message}";
            }
            response.Mensagem = "Categoria atualizada com sucesso!";
            return response;
        }
    }
}
