using MessagerieApp.Models.TransversalData;

namespace MessagerieApp.Models.TransactionData
{
	public class Message
	{
		private int id;
		private int senderId;
		private User sender;
		private List<int> receiverIds;
		private List<User> receivers;
		private string content;
		private MessageStatus status;
		public DateTime Timestamp { get; set; }
		public bool IsArchived { get; set; }

		public Message(int id, int senderId, User sender, List<int> receiverIds, List<User> receivers, string content, MessageStatus status, DateTime timestamp, bool isArchived)
		{
			Id = id;
			SenderId = senderId;
			Sender = sender;
			ReceiverIds = receiverIds;
			Receivers = receivers;
			Content = content;
			Status = status;
			Timestamp = timestamp;
			IsArchived = isArchived;
		}
		public Message() { }

		public int Id { get => id; set => id = value; }
		public int SenderId { get => senderId; set => senderId = value; }
		public User Sender { get => sender; set => sender = value; }
		public List<int> ReceiverIds { get => receiverIds; set => receiverIds = value; }
		public List<User> Receivers { get => receivers; set => receivers = value; }
		public string Content { get => content; set => content = value; }
		public MessageStatus Status { get => status; set => status = value; }
	}
	public enum MessageStatus
	{
		Unread,
		Read,
		Deleted
	}
}
