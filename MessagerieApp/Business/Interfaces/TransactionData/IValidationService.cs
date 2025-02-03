using MessagerieApp.Models.TransactionData;

namespace MessagerieApp.Business.Interfaces.TransactionData
{
	public interface IValidationService
	{
		Task<List<ValidationStep>> ObtenirHistoriqueValidationsAsync(int demandeId);
		Task AjouterValidationAsync(int demandeId, int validatorId, string commentaire);
	}
}
