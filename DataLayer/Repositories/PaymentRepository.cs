using DataLayer.Data;
using DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ClinicDbContext _context;
        public PaymentRepository(ClinicDbContext context) 
        {
            _context = context;
        }
        public Payment GetPaymentById(int ID)
        {
            return _context.Set<Payment>().Find(ID);
        }
        public int AddPayment(Payment entity)
        {
           return _context.Set<Payment>().Add(entity).Entity.PaymentID;
        }
        public bool UpdatePayment(Payment entity)
        {
            return _context.Set<Payment>().Update(entity).Entity.PaymentID > 0;
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
