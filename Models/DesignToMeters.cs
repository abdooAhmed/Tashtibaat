namespace Tashtibaat.Models
{
    public class DesignToMeters
    {
        public int Id {  get; set; }
        public int Number {  get; set; }
        public Designs Designs { get; set; }
    }

    public class DesignToMetersDto
    {
        public int Metters { get; set; }
        public int DesignId { get; set; }
    }
}
