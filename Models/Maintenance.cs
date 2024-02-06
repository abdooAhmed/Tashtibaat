namespace Tashtibaat.Models
{
    public enum MaintenanceType
    {
        Electrician,
        Plumber,
        CarPenter,
        AirConditioning,
        Security
    }
    public class Maintenance
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public int Type { get; set; }
        public Users User { get; set; }
        public bool Cash { get; set; }
        public bool Bank { get; set; }
    }
}
