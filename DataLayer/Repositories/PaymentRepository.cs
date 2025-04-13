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
        public Payment GetById(int ID)
        {
            return _context.Set<Payment>().Find(ID);
        }

        public int Add(Payment entity)
        {
           return _context.Set<Payment>().Add(entity).Entity.PaymentID;
        }
        public bool Update(Payment entity)
        {
            return _context.Set<Payment>().Update(entity).Entity.PaymentID > 0;
        }
    }
}
