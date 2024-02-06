namespace Tashtibaat.Models
{
    public class ChatItem
    {
        public Guid Id { get; set; }
        public Users User { get; set; }
        public String UserName { get; set; }
        public String Message { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}
