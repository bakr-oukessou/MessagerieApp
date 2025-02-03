using MessagerieApp.Models.TransactionData;

namespace MessagerieApp.Repository.Interfaces.TransactionData
{
	public interface IMessageRepository
	{

		Task<Message> GetByIdAsync(int id);
		Task<IEnumerable<Message>> GetAllAsync();
		Task AddAsync(Message message);
		Task UpdateAsync(Message message);
		Task DeleteAsync(int id);
		Task<IEnumerable<Message>> GetBySenderIdAsync(int senderId);
		Task<IEnumerable<Message>> GetByReceiverIdAsync(int receiverId);
	}
}
