namespace DataLayer.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        
        Payment GetPaymentById(int ID);
        public bool UpdatePayment(Payment entity);
        public int AddPayment(Payment entity);
        public IQueryable<Payment> GetPaymentsByPatient(int patientId);
        public IQueryable<Payment> GetPaymentsByDateRange(DateTime from, DateTime to);
   
      

    }
}
