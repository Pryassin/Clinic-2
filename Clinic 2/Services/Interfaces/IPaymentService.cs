public interface IPaymentService
{

    Payment GetById(int ID);
    int Add(Payment entity);
    bool Update(Payment entity);

}
