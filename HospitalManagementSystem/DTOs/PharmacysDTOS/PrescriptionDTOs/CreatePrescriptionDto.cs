namespace HospitalManagementSystem.DTOs.PharmacysDTOS.PrescriptionDTOs
{
    public class CreatePrescriptionDto
    {
        public int RecordId { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
    }
}
