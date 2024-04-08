namespace Tashtibaat.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public float Total { get; set; }
        public ICollection<ProductToMeters> ProductToMeters { get; set; }
        public Users Users { get; set; }
        public bool Cash {  get; set; }
        public bool Bank {  get; set; }
    }

    public class OrderDto
    {
        public string Address { get; set; }
        public string Notes { get; set; }
        public float Total { get; set; }
        public List<ProductToMetersDto> ProductToMeters { get; set; }
        public bool Cash { get; set; }
        public bool Bank { get; set; }
    }
}
