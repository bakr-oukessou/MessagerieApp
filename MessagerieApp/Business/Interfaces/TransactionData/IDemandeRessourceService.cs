using System.Collections.Generic;
using System.Threading.Tasks;
using MessagerieApp.Models.MasterData;
using MessagerieApp.Models.TransactionData;

namespace MessagerieApp.Business.Interfaces.TransactionData
{
	public interface IDemandeRessourceService
	{
		Task<IEnumerable<DemandeRessource>> GetAllDemandesAsync();
		Task<DemandeRessource> GetDemandeByIdAsync(int id);
		Task AddDemandeAsync(DemandeRessource demande);
		Task UpdateDemandeAsync(DemandeRessource demande);
		Task DeleteDemandeAsync(int id);
		Task UpdateDemandeStatusAsync(int demandeId, string status);
		Task<IEnumerable<DemandeRessource>> GetDemandesByDepartmentAsync(int departmentId);
		Task AddItemToDemandeAsync(int demandeRessourceId, RessourceItem item);

		Task<List<DemandeRessource>> GetDemandesParUtilisateurAsync(int userId);
		Task<List<DemandeRessource>> GetDemandesEnAttenteAsync();
		Task CreerDemandeAsync(DemandeRessource demande);
		Task MettreAJourDemandeAsync(DemandeRessource demande);
		Task SupprimerDemandeAsync(int id);
		Task ValiderDemandeAsync(int demandeId, int validatorId, string commentaire);
		Task RejeterDemandeAsync(int demandeId, int validatorId, string commentaire);


		// Workflow principal
		Task InitierDemandeAuxEnseignants(int chefDepartementId, int departementId);
		Task<DemandeRessource> SoumettreDemandeEnseignant(DemandeRessource demande);
		Task ValiderDemandeChef(int demandeId, int chefDepartementId);
		Task ValiderDemandeResponsable(int demandeId, int responsableId);
		Task RejeterDemande(int demandeId, int validateurId, string raison);

		// Gestion des demandes
		Task<DemandeRessource> ObtenirDemandeParId(int id);
		Task<IEnumerable<DemandeRessource>> ObtenirHistoriqueDemandes(int departementId);
		Task<IEnumerable<DemandeRessource>> ObtenirDemandesEnAttente(int? departementId = null);
	}
}