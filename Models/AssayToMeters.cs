namespace Tashtibaat.Models
{
    public class AssayToMeters
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public Assay Assay { get; set; }
    }

    public class AssayToMetersDto
    {
        
        public int Metters { get; set; }
        public int AssayId { get; set; }
    }
}
