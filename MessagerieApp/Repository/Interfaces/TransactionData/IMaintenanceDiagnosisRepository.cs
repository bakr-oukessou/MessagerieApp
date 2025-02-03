using System.Collections.Generic;
using System.Threading.Tasks;
using MessagerieApp.Models.TransactionData;

namespace MessagerieApp.Repository.Interfaces.TransactionData
{
	public interface IMaintenanceDiagnosisRepository
	{
		Task<IEnumerable<MaintenanceDiagnosis>> GetAllMaintenanceDiagnosesAsync();
		Task<MaintenanceDiagnosis> GetMaintenanceDiagnosisByIdAsync(string id);
		Task AddMaintenanceDiagnosisAsync(MaintenanceDiagnosis diagnosis);
		Task UpdateMaintenanceDiagnosisAsync(MaintenanceDiagnosis diagnosis);
		Task DeleteMaintenanceDiagnosisAsync(string id);

		Task<ConstatMaintenance> GetByIdAsync(int id);
		Task<IEnumerable<ConstatMaintenance>> GetAllAsync();
		Task AddAsync(ConstatMaintenance constat);
		Task UpdateAsync(ConstatMaintenance constat);
		Task DeleteAsync(int id);
		Task<IEnumerable<ConstatMaintenance>> GetByStatusAsync(string status);
		Task<IEnumerable<ConstatMaintenance>> GetByResourceIdAsync(int resourceId);
	}
}