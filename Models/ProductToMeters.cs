namespace Tashtibaat.Models
{
    
    public class ProductToMeters
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public Products Products { get; set; }
    }

    public class ProductToMetersDto
    {
        public int Metters { get; set; }
        public int ProductId { get; set; }
    }
}
