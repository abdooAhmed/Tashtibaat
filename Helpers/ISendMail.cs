using Tashtibaat.Models;

namespace Tashtibaat.Helpers
{
    public interface ISendMail
    {
        public bool Send(string ReceiverUserName,string ReceiverEmail, string Link,string Subject);
        public bool Send(Users user, string Subject,string Text);
    }
}
