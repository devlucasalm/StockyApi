namespace StockyApi.Models
{
    public class Pagination<T>
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public int Total { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
