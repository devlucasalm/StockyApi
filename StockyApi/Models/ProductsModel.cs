using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StockyApi.Models
{
    [Table("products")]
    public class ProductsModel
    {
        [Key]
        public Guid ProductId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public string ImagemUrl { get; set; } = string.Empty;
        public bool Ativo { get; set; } = true;
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; } = DateTime.Now.ToLocalTime();

        public CategoryModel? Category { get; set; } 
        public Guid? CategoryId  { get; set; }
    }
}

