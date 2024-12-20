using MessagerieApp.Models;

namespace MessagerieApp.Business
{
    public class MessageService
    {
        private readonly ApplicationDbContext _context;
        private readonly NotificationService _notificationService;

        public async Task<Message> SendMessage(int senderId, List<int> receiverIds, string content)
        {
            var message = new Message
            {
                SenderId = senderId,
                ReceiverIds = receiverIds,
                Content = content,
                Timestamp = DateTime.Now,
                Status = MessageStatus.Unread
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            // Créer des notifications pour chaque destinataire
            foreach (var receiverId in receiverIds)
            {
                await _notificationService.CreateNotification(receiverId, message.Id);
            }

            return message;
        }

        public async Task<List<Message>> SearchMessages(int userId,
            string searchTerm = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            MessageStatus? status = null)
        {
            var query = _context.Messages
                .Where(m => m.ReceiverIds.Contains(userId) || m.SenderId == userId);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(m => m.Content.Contains(searchTerm));
            }

            if (startDate.HasValue)
            {
                query = query.Where(m => m.Timestamp >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(m => m.Timestamp <= endDate.Value);
            }

            if (status.HasValue)
            {
                query = query.Where(m => m.Status == status.Value);
            }

            return await query
                .Include(m => m.Sender)
                .Include(m => m.Receivers)
                .OrderByDescending(m => m.Timestamp)
                .ToListAsync();
        }

        public async Task<Message> MarkMessageAsRead(int messageId, int userId)
        {
            var message = await _context.Messages.FindAsync(messageId);
            if (message != null && message.ReceiverIds.Contains(userId))
            {
                message.Status = MessageStatus.Read;
                await _context.SaveChangesAsync();
            }
            return message;
        }

        public async Task ArchiveMessage(int messageId, int userId)
        {
            var message = await _context.Messages.FindAsync(messageId);
            if (message != null && (message.SenderId == userId || message.ReceiverIds.Contains(userId)))
            {
                message.IsArchived = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
