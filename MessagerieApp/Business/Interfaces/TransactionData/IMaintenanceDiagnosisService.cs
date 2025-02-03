using System.Collections.Generic;
using System.Threading.Tasks;
using MessagerieApp.Models.TransactionData;

namespace MessagerieApp.Business.Interfaces.TransactionData
{
	public interface IMaintenanceDiagnosisService
	{
		Task<IEnumerable<MaintenanceDiagnosis>> GetAllMaintenanceDiagnosesAsync();
		Task<MaintenanceDiagnosis> GetMaintenanceDiagnosisByIdAsync(string id);
		Task AddMaintenanceDiagnosisAsync(MaintenanceDiagnosis diagnosis);
		Task UpdateMaintenanceDiagnosisAsync(MaintenanceDiagnosis diagnosis);
		Task DeleteMaintenanceDiagnosisAsync(string id);
	}
}