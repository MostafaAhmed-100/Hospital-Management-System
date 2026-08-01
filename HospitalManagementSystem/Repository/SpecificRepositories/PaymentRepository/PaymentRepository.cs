using HospitalManagementSystem.Data;
using HospitalManagementSystem.Data.Models.Billing_Insurance;
using HospitalManagementSystem.Repository.GenericRepository;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem.Repository.SpecificRepositories.PaymentRepository
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(AppDbContext appDbcontext) : base(appDbcontext)
        {
        }

        public async Task<IEnumerable<Payment?>> GetPaymentsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var Payments = await _AppDbcontext.Payments
                .Where(x => x.PaymentDate >= startDate && x.PaymentDate <= endDate)
                .Include(x => x.Invoice)
                .AsNoTracking()
                .ToListAsync();
            return Payments;
        }
    }
}
