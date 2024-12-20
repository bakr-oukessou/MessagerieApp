namespace MessagerieApp.Models
{
    public class Message
    {
        private int id;
        private int senderId;
        private User sender;
        private List<int> receiverId;
        private List<User> receivers;
        private string content;
        private string status;

        public Message(int id, int senderId, User sender, List<int> receiverIds, List<User> receivers, string content)
        {
            this.id = id;
            this.senderId = senderId;
            this.sender = sender;
            this.receiverId = receiverIds;
            this.receivers = receivers;
            this.content = content;
        }

        public int Id { get => id; set => id = value; }
        public int SenderId { get => senderId; set => senderId = value; }
        public User Sender { get => sender; set => sender = value; }
        public List<int> ReceiverId { get => receiverId; set => receiverId = value; }
        public List<User> Receivers { get => receivers; set => receivers = value; }
        public string Content { get => content; set => content = value; }
        public string Status { get => status; set => status = value; }
    }
}
