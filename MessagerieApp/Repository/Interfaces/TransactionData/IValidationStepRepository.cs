using MessagerieApp.Models.TransactionData;

namespace MessagerieApp.Repository.Interfaces.TransactionData
{
	public interface IValidationStepRepository
	{
		Task<List<ValidationStep>> GetByRequestIdAsync(int requestId);
		Task AddValidationStepAsync(ValidationStep step);
	}
}
