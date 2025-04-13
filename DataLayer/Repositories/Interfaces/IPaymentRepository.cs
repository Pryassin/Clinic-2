namespace DataLayer.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        
        Payment GetById(int ID);
        public bool Update(Payment entity);
        public int Add(Payment entity);
    }
}
