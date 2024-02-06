namespace Tashtibaat.Models
{
    public class AssayOrder
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public int Total { get; set; }
        public bool Cash { get; set; }
        public bool Bank { get; set; }
        public Users User { get; set; }
        public ICollection<AssayToMeters> AssayToMeters { get; set; }

    }
    public class AssayOrderDto
    {
        public string Address { get; set; }
        public string Notes { get; set; }
        public int Total { get; set; }
        public bool Cash { get; set; }
        public bool Bank { get; set; }
        public ICollection<AssayToMetersDto> AssayToMeters { get; set; }
    }
}
