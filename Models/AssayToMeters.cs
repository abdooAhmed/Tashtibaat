namespace Tashtibaat.Models
{
    public class AssayToMeters
    {
        public int Id { get; set; }
        public float Quantity { get; set; }
        public Assay Assay { get; set; }
    }

    public class AssayToMetersDto
    {
        
        public int Quantity { get; set; }
        public int AssayId { get; set; }
    }
}
