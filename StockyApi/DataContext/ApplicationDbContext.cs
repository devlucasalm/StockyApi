using Microsoft.EntityFrameworkCore;

namespace StockyApi.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Models.ProductsModel> Products { get; set; }
        public DbSet<Models.CategoryModel> Category { get; set; }
    }
}
