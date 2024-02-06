namespace Tashtibaat.Models
{
    public class ServicesOrder
    {
        public int Id { get; set; }
        public string PhoneNubmer {  get; set; }
        public string Address {  get; set; }
        public string? Notes {  get; set; }
        public Users User { get; set; }
    }
    public class ServicesOrderDto
    {
        public int Id { get; set; }
        public string PhoneNubmer { get; set; }
        public string Address { get; set; }
        public string? Notes { get; set; }
        public int ServiceId { get; set; }
    }
}
