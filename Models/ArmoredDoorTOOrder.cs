namespace Tashtibaat.Models
{
    public class ArmoredDoorTOOrder
    {
        public int Id { get; set; }
        public ArmoredDoor ArmoredDoor { get; set; }
        public ServicesOrder ServicesOrder { get; set; }
    }
}
