using System.Collections.Generic;
using System.Threading.Tasks;
using MessagerieApp.Business.Interfaces.TransactionData;
using MessagerieApp.Models.MasterData;
using MessagerieApp.Models.TransactionData;
using MessagerieApp.Repository.Interfaces.TransactionData;

namespace MessagerieApp.Services
{
	public class DemandeRessourceService : IDemandeRessourceService
    {
        private readonly IDemandeRessourceRepository _demandeRessourceRepository;

        public DemandeRessourceService(IDemandeRessourceRepository demandeRessourceRepository)
        {
            _demandeRessourceRepository = demandeRessourceRepository;
        }

        public async Task<IEnumerable<DemandeRessource>> GetAllDemandesAsync()
        {
            return await _demandeRessourceRepository.GetAllDemandesAsync();
        }

        public async Task<DemandeRessource> GetDemandeByIdAsync(int id)
        {
            return await _demandeRessourceRepository.GetDemandeByIdAsync(id);
        }

        public async Task AddDemandeAsync(DemandeRessource demande)
        {
            await _demandeRessourceRepository.AddDemandeAsync(demande);
        }

        public async Task UpdateDemandeAsync(DemandeRessource demande)
        {
            await _demandeRessourceRepository.UpdateDemandeAsync(demande);
        }

        public async Task DeleteDemandeAsync(int id)
        {
            await _demandeRessourceRepository.DeleteDemandeAsync(id);
        }

        public async Task UpdateDemandeStatusAsync(int demandeId, string status)
        {
            await _demandeRessourceRepository.UpdateDemandeStatusAsync(demandeId, status);
        }

        public async Task<IEnumerable<DemandeRessource>> GetDemandesByDepartmentAsync(int departmentId)
        {
            return await _demandeRessourceRepository.GetDemandesByDepartmentAsync(departmentId);
        }

        public Task AddItemToDemandeAsync(int demandeRessourceId, RessourceItem item)
        {
            throw new NotImplementedException();
        }



		public async Task<DemandeRessource> GetDemandeByIdAsync(int id)
		{
			return await _demandeRessourceRepository.GetByIdAsync(id);
		}

		public async Task<List<DemandeRessource>> GetDemandesParUtilisateurAsync(int userId)
		{
			return await _demandeRessourceRepository.GetBySenderIdAsync(userId);
		}

		public async Task CreerDemandeAsync(DemandeRessource demande)
		{
			demande.CreatedAt = DateTime.UtcNow;
			demande.Status = RequestStatus.EnAttente;
			await _demandeRessourceRepository.AddAsync(demande);
		}

		public async Task MettreAJourDemandeAsync(DemandeRessource demande)
		{
			await _demandeRessourceRepository.UpdateAsync(demande);
		}

		public async Task SupprimerDemandeAsync(int id)
		{
			await _demandeRessourceRepository.DeleteAsync(id);
		}

		public async Task ValiderDemandeAsync(int demandeId, int validatorId, string commentaire)
		{
			var demande = await _demandeRessourceRepository.GetByIdAsync(demandeId);
			if (demande != null)
			{
				demande.CurrentStatus = RequestStatus.Validee;
				demande.ValidationSteps.Add(new ValidationStep
				{
					ValidatorId = validatorId,
					ValidationDate = DateTime.UtcNow,
					Comments = commentaire
				});
				await _demandeRessourceRepository.UpdateAsync(demande);
			}
		}

		public async Task RejeterDemandeAsync(int demandeId, int validatorId, string commentaire)
		{
			var demande = await _demandeRessourceRepository.GetByIdAsync(demandeId);
			if (demande != null)
			{
				demande.CurrentStatus = RequestStatus.Rejetee;
				demande.ValidationSteps.Add(new ValidationStep
				{
					ValidatorId = validatorId,
					ValidationDate = DateTime.UtcNow,
					Comments = commentaire
				});
				await _demandeRessourceRepository.UpdateAsync(demande);
			}
		}
	}
}