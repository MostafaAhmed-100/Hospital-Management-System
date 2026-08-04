using HospitalManagementSystem.Data.Models.Billing_Insurance;
using HospitalManagementSystem.Repository.GenericRepository;

namespace HospitalManagementSystem.Repository.SpecificRepositories.InvoiceRepository
{
    public interface IInvoiceRepository : IGenericRepository<Invoice>
    {
        Task<Invoice?> GetInvoiceWithPaymentsAsync(int invoiceId);
        Task<IEnumerable<Invoice?>> GetUnpaidInvoicesByPatientAsync(int patientId);
    }
}