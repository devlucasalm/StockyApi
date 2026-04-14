public class ProductCreateDto
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public int Quantidade { get; set; }
    public Guid? CategoryId { get; set; } = Guid.Empty;

    public IFormFile Imagem { get; set; }
}