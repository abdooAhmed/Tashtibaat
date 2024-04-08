namespace Tashtibaat.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ProductsCategory Category { get; set; }
        public string Picture { get; set; }
        public int MinimumPrice { get; set; }
        public int MaximumPrice { get; set; }
        public ICollection<Order> Orders { get; set; }
    }

    public class ProductDto
    {
        public string Name { set; get; }
        public int CategoryId {  get; set; }
        public IFormFile? Picture { get; set; }
        public int MinimumPrice { get; set; }
        public int MaximumPrice { get; set; }
    }
}
