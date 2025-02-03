using MessagerieApp.Models.TransactionData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MessagerieApp.Pages
{
	public class MessagesModel : PageModel
    {
        public List<Message> MessagesList { get; set; } = new List<Message>();

        public void OnGet()
        {
            // Fetch messages from the database or service
            // Example:
            // MessagesList = _messageService.GetAllMessages();
        }

        public void OnPost(int senderId, int receiverId, string messageContent)
        {
            // Handle creation of a new message
            // Example:
            // var newMessage = new Message { SenderId = senderId, ReceiverId = receiverId, MessageContent = messageContent };
            // _messageService.AddMessage(newMessage);
        }
    }
}
