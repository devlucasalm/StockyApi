namespace StockyApi.Models
{
    public class AppResponse<T>
    {
        public T? Dados { get; set; }
        public string? Mensagem { get; set; }
        public bool Sucesso { get; set; } = true;
    }
}
