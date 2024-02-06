namespace Tashtibaat.Models
{
    public class DesignOrder
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public int Total { get; set; }
        public bool Cash { get; set; }
        public bool Bank { get; set; }
        public Users User { get; set; }
        public ICollection<DesignToMeters> DesignToMeters { get; set; }
    }

    public class DesignOrderDto
    {
        public string Address { get; set; }
        public string Notes { get; set; }
        public int Total { get; set; }
        public bool Cash { get; set; }
        public bool Bank { get; set; }
        public ICollection<DesignToMetersDto> DesignToMeters { get; set; }
    }
}
