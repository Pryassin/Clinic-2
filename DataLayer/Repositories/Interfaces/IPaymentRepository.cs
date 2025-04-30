namespace DataLayer.Repositories.Interfaces
{
    public interface IPaymentRepository:IBaseRepository<Payment>
    {
       public IQueryable<Payment> GetPaymentsByPatient(int patientId);
        public IQueryable<Payment> GetPaymentsByDateRange(DateTime from, DateTime to);
        
   
      

    }
}
