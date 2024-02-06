namespace Tashtibaat.Models
{
    public enum Type
    {
        Apartment,
        Villa,
        Office,
        Restaurant
    }

    public enum Status
    {
        Brick,
        UnPlastered,
        Finishing,
    }

    public class Preview
    {
        public int Id { get; set; }

        public string Address { get; set; }
        public string Notes { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public DateTime Date { get; set; }
        public Users User { get; set; }
        public bool Cash { get; set; }
        public bool Bank { get; set; }

    }

    public class PreviewDto
    {
        public string Address { get; set; }
        public string Notes { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public bool Cash { get; set; }
        public DateTime Date { get; set; }
        public bool Bank { get; set; }
    }
}
