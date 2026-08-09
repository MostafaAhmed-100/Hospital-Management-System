using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.Inpatient;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.InpatientRepositorys.BedRepository
{
    public interface IBedRepository : IGenericRepository<Bed>
    {
        Task<IEnumerable<Bed>> GetAvailableBedsAsync();
        Task<int> GetAvailableBedsCountAsync();
        Task<Dictionary<BedStatus, int>> GetBedsDistributionByStatusAsync();
    }
}

