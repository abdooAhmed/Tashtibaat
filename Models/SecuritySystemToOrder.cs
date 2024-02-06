namespace Tashtibaat.Models
{
    public class SecuritySystemToOrder
    {
        public int Id { get; set; }
        public SecuritySurveillance SecuritySurveillance { get; set; }
        public ServicesOrder ServicesOrder { get; set; }
    }
}
