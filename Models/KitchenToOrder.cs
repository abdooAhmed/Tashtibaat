namespace Tashtibaat.Models
{
    public class KitchenToOrder
    {
        public int Id { get; set; }
        public float Space { get; set; }
        public Kitchen Kitchen { get; set; }
        public ServicesOrder ServicesOrder { get; set; }
    }
}
