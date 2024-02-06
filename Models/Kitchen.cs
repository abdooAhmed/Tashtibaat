namespace Tashtibaat.Models
{
    public class Kitchen
    {
        public int Id { get; set; }
        public int MinimumPrice { get; set; }
        public int MaximumPrice { get; set; }
    }

    public class KitchenAndDressingRoomOrderDto
    {
        public string PhoneNubmer { get; set; }
        public string Address { get; set; }
        public float Space {  get; set; }
        public string? Notes { get; set; }
        public int ServiceId {get; set; }
    }
}
