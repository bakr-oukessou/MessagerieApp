using System.Collections.Generic;
using System.Threading.Tasks;
using MessagerieApp.Models;

namespace MessagerieApp.Repositories
{
    public interface IMaintenanceDiagnosisRepository
    {
        Task<IEnumerable<MaintenanceDiagnosis>> GetAllMaintenanceDiagnosesAsync();
        Task<MaintenanceDiagnosis> GetMaintenanceDiagnosisByIdAsync(string id);
        Task AddMaintenanceDiagnosisAsync(MaintenanceDiagnosis diagnosis);
        Task UpdateMaintenanceDiagnosisAsync(MaintenanceDiagnosis diagnosis);
        Task DeleteMaintenanceDiagnosisAsync(string id);
    }
}