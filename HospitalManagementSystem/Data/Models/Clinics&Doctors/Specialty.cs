namespace HospitalManagementSystem.Data.Models.Clinics_Doctors
{
    public class Specialty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<Doctor> Doctors { get; set; }

    }
}
