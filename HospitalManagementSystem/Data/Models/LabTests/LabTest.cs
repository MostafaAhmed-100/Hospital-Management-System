using HospitalManagementSystem.Data.Models.Enums;
using HospitalManagementSystem.Data.Models.OutpatientVisits;

namespace HospitalManagementSystem.Data.Models.LabTests
{
    public class LabTest
    {
        public int Id { get; set; }
        public int RecordId { get; set; }
        public string TestName { get; set; }
        public DateTime TestDate { get; set; }
        public string? Result { get; set; }
        public LabTestStatus Status { get; set; } = LabTestStatus.Pending;
        public bool IsDeleted { get; set; } = false;

        public MedicalRecord MedicalRecord { get; set; }
    }
}
