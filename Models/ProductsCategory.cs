namespace Tashtibaat.Models
{
    public class ProductsCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Products> Products { get; set; }
        public string Picture { get; set; }
        
    }

    public class CategoryDto
    {
        public string Name { set; get; }
        public IFormFile? Picture { get; set; }
    }
}
