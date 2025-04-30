using DataLayer.Data;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;

public class PaymentService :IPaymentService
{
    IPaymentRepository _paymentRepository;  
    public PaymentService(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }
    void CheckPaymentIsNotNull(Payment payment)
    {
        if (payment == null)
        {
            throw new ArgumentNullException(nameof(payment), "Payment cannot be null");
        }
    }
    public int AddPayment(Payment payment)
    {
        CheckPaymentIsNotNull(payment);
       return _paymentRepository.Add(payment);
    }

    public bool DeletePayment(int paymentId)
    {
        throw new NotImplementedException();
    }

    public Payment GetPaymentById(int paymentId)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Payment> GetPaymentsByDateRange(DateTime from, DateTime to)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Payment> GetPaymentsByPatient(int patientId)
    {
        throw new NotImplementedException();
    }

    public bool UpdatePayment(Payment payment)
    {
        CheckPaymentIsNotNull(payment);
    }
}


   


