using HospitalManagementSystem.Data.Models.Emergency;
using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.Inpatient;

namespace HospitalManagementSystem.Data.Models.Nursing_Staff
{
    public class NurseAssignment
    {
        public int Id { get; set; }
        public int NurseId { get; set; }
        public int? AdmissionId { get; set; } 
        public int? ErVisitId { get; set; } 
        public DateTime AssignedAt { get; set; }
        public ShiftType Shift { get; set; }
        public bool IsDeleted { get; set; } = false;

        public Nurse Nurse { get; set; }
        public Admission Admission { get; set; }
        public ErVisit ErVisit { get; set; }
    }
}