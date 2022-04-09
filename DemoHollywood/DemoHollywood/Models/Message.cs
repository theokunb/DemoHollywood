namespace DemoHollywood.Models
{
    public class Message
    {
        public Message() { }

        public string Author { get; set; }
        public string Text { get; set; }

        public string DisplayMessage => Author + ": " + Text;
    }
}
