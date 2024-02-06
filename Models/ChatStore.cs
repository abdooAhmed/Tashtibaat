namespace Tashtibaat.Models
{
    public class ChatStore
    {
        public List<ChatItem> ChatList { get; set; }
        public List<String> UserList { get; set; }

        public ChatStore()
        {
            ChatList = new List<ChatItem>();
            UserList = new List<String>();
        }
    }
}
