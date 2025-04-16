namespace DataLayer.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        
        Payment GetPaymentById(int ID);
        public bool UpdatePayment(Payment entity);
        public int AddPayment(Payment entity);
     
    }
}
