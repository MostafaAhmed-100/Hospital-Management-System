namespace HospitalManagementSystem.DTOs.PatientDTOs
{
    public class PatientWithMedicalHistoryResponseDto : PatientResponseDto
    {
        public IEnumerable<MedicalRecordDto> MedicalRecords { get; set; }
    }
}
