namespace DataLayer.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
       public IQueryable<Payment> GetPaymentsByPatient(int patientId);
        public IQueryable<Payment> GetPaymentsByDateRange(DateTime from, DateTime to);
        
   
      

    }
}
