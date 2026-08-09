using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.PharmacysRepository.PharmacyInventoryRepository
{
    public interface IPharmacyInventoryRepository : IGenericRepository<PharmacyInventory>
    {
        Task<PharmacyInventory?> CheckMedicineStockAsync(int pharmacyId, int medicineId);
        Task<IEnumerable<(string MedicineName, string PharmacyName, DateTime ExpiryDate, int Quantity)>> GetExpiringSoonMedicinesAsync();
        Task<IEnumerable<(string MedicineName, string PharmacyName, int Quantity)>> GetLowStockMedicinesAsync();
    }
}