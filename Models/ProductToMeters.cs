namespace Tashtibaat.Models
{
    
    public class ProductToMeters
    {
        public int Id { get; set; }
        public float Quantity { get; set; }
        public float Total { get; set; }
        public Products Products { get; set; }
    }

    public class ProductToMetersDto
    {
        public float Quantity { get; set; }
        public int ProductId { get; set; }
        public float Total { get; set; }
    }
}
