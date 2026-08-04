namespace HospitalManagementSystem.DTOs.OutpatientVisitsDTOS.PatientDTOs
{
    public class PatientWithMedicalHistoryResponseDto : PatientResponseDto
    {
        public IEnumerable<MedicalRecordDto> MedicalRecords { get; set; }
    }
}
