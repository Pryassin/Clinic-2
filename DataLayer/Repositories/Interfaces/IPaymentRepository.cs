namespace DataLayer.Repositories.Interfaces
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        IEnumerable<Payment> GetAll();
    }
}
