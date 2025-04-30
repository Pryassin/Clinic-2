using DataLayer.Data;
using DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        private readonly ClinicDbContext _context;
        public PaymentRepository(ClinicDbContext context) :base(context)
        {
            _context = context;
        }
     
        public IQueryable<Payment>GetPaymentsByPatient(int patientId)
        {
            return _context.Payments.FromSqlInterpolated(@$"SELECT pay.* FROM dbo.Payments AS pay
            INNER JOIN  dbo.Appointments AS app
            ON app.PaymentID=pay.PaymentID
            WHERE app.PatientID={patientId}");
        }
        public IQueryable<Payment> GetPaymentsByDateRange(DateTime from, DateTime to)
        {
           return _context.Payments.FromSqlInterpolated(@$"SELECT * FROM dbo.Payments
            WHERE PaymentDate BETWEEN {from} AND {to}");
        }      

    }
}
