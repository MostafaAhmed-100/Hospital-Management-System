using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.Clinics_Doctors;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.DepartmentRepository
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AppDbContext appDbcontext) : base(appDbcontext)
        {
        }

        public async Task<Department?> GetDepartmentWithClinicsAndDoctorsAsync(int departmentId)
        {
            var Department = await _AppDbcontext.Departments
                .Include(x => x.Clinics)
                .Include(x => x.Doctors)
                .AsSplitQuery()
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(x => x.Id == departmentId);

            return Department;
        }
    }
}
