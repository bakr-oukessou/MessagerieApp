using System.Collections.Generic;
using System.Threading.Tasks;
using MessagerieApp.Business.Interfaces.TransactionData;
using MessagerieApp.Models.TransactionData;
using MessagerieApp.Repository.Interfaces.TransactionData;

namespace MessagerieApp.Services
{
	public class MaintenanceDiagnosisService : IMaintenanceDiagnosisService
    {
        private readonly IMaintenanceDiagnosisRepository _maintenanceDiagnosisRepository;

        public MaintenanceDiagnosisService(IMaintenanceDiagnosisRepository maintenanceDiagnosisRepository)
        {
            _maintenanceDiagnosisRepository = maintenanceDiagnosisRepository;
        }

        public async Task<IEnumerable<MaintenanceDiagnosis>> GetAllMaintenanceDiagnosesAsync()
        {
            return await _maintenanceDiagnosisRepository.GetAllMaintenanceDiagnosesAsync();
        }

        public async Task<MaintenanceDiagnosis> GetMaintenanceDiagnosisByIdAsync(string id)
        {
            return await _maintenanceDiagnosisRepository.GetMaintenanceDiagnosisByIdAsync(id);
        }

        public async Task AddMaintenanceDiagnosisAsync(MaintenanceDiagnosis diagnosis)
        {
            await _maintenanceDiagnosisRepository.AddMaintenanceDiagnosisAsync(diagnosis);
        }

        public async Task UpdateMaintenanceDiagnosisAsync(MaintenanceDiagnosis diagnosis)
        {
            await _maintenanceDiagnosisRepository.UpdateMaintenanceDiagnosisAsync(diagnosis);
        }

        public async Task DeleteMaintenanceDiagnosisAsync(string id)
        {
            await _maintenanceDiagnosisRepository.DeleteMaintenanceDiagnosisAsync(id);
        }
    }
}