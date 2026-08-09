using HospitalManagementSystem.Data.Models.Pharmacys;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.PharmacysRepository.MedicineRepository
{
    public interface IMedicineRepository : IGenericRepository<Medicine>
    {
        Task<IEnumerable<Medicine?>> SearchMedicinesByNameAsync(string name);
        Task<IEnumerable<(string MedicineName, int SalesCount)>> GetTopSellingMedicinesAsync();
        Task<IEnumerable<(string Category, int Count)>> GetMedicinePrescriptionDistributionAsync();
    }
}
