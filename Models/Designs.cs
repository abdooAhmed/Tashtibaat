namespace Tashtibaat.Models
{
    
    public class Designs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MinimumPrice { get; set; }
        public int MaximumPrice { get; set; }
        public string Unit { get; set; }

    }
    public class DesignsDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int MinimumPrice { get; set; }
        public int MaximumPrice { get; set; }
        public string Unit { get; set; }
    }
}
