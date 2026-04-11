using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockyApi.Models
{
    [Table("category")]
    public class CategoryModel
    {
        [Key]
        public Guid? CategoryId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; } = DateTime.Now.ToLocalTime();


    }
}
