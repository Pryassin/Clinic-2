using DataLayer.Data;
using DataLayer.Repositories.Interfaces;

namespace DataLayer.Repositories
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        private readonly ClinicDbContext _context;
        public PaymentRepository(ClinicDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Payment> GetAll()
        {
            return _context.Set<Payment>().ToList(); // Corrected the syntax and added ToList() to fetch all records
        }


    }
}
