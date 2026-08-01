using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.Billing_Insurance;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;
using HospitalManagementSystem.Data.Models.Enums;

namespace HospitalManagementSystem.Repository.SpecificRepositories.InvoiceRepository
{
    public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
    {
        public InvoiceRepository(AppDbContext appDbcontext) : base(appDbcontext)
        {
        }

        public async Task<Invoice?> GetInvoiceWithPaymentsAsync(int invoiceId)
        {
            var Invoice = await _AppDbcontext.Invoices
                .Include(x => x.Payments)
                .AsNoTrackingWithIdentityResolution()
                .FirstOrDefaultAsync(x => x.Id == invoiceId);

            return Invoice;
        }

        public async Task<IEnumerable<Invoice?>> GetUnpaidInvoicesByPatientAsync(int patientId)
        {
            var Invoices = await _AppDbcontext.Invoices
                .Where(x => x.PatientId == patientId && x.Status != InvoiceStatus.Paid)
                .AsNoTracking()
                .ToListAsync();

            return Invoices;
        }
    }
}