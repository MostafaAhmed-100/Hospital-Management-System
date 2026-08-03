using HospitalManagementSystem.Data.Models.Surgery;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.SurgeryTeamRepository
{
    public class SurgeryTeamRepository : GenericRepository<SurgeryTeam>, ISurgeryTeamRepository
    {
        public SurgeryTeamRepository(Data.AppDbContext appDbcontext) : base(appDbcontext) { }
    }
}
