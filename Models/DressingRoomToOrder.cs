namespace Tashtibaat.Models
{
    public class DressingRoomToOrder
    {
        public int Id { get; set; }
        public DressingRoom DressingRoom { get; set; }
        public float Space {  get; set; }
        public ServicesOrder ServicesOrder { get; set; }
    }
}
