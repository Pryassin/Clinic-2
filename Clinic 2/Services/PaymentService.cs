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
       var result= _paymentRepository.Add(payment);
        if (result <= 0)
        {
            throw new Exception("Payment failed to save to the database.");
        }
        return result;
    }

    public bool DeletePayment(int paymentId)
    {
        if (paymentId < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(paymentId), "Payment ID must be greater than zero");
        }
        var payment = _paymentRepository.GetByID(paymentId);
        if (payment == null)
        {
            throw new KeyNotFoundException($"Payment with ID {paymentId} not found");
        }
        return _paymentRepository.Delete(payment);
    }

    public Payment GetPaymentById(int paymentId)
    {
        if(paymentId < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(paymentId), "Payment ID must be greater than zero");
        }
        var payment = _paymentRepository.GetByID(paymentId);
        if (payment == null)
        {
            throw new KeyNotFoundException($"Payment with ID {paymentId} not found");
        }
        return payment;
    }

    public bool UpdatePayment(Payment payment)
    {
        CheckPaymentIsNotNull(payment);

        if (_paymentRepository.Update(payment) == true)
        {
            return true;
        }
        else
        {
            throw new Exception("Failed to update payment");
        }
    }
    void ValidateDates(DateTime from, DateTime to)
    {
        if (from > to)
        {
            throw new ArgumentException("From date must be less than or equal to To date");
        }
        if (from == DateTime.MinValue || to == DateTime.MinValue)
        {
            throw new ArgumentException("From and To dates must be valid dates");
        }
        if (from == DateTime.MaxValue || to == DateTime.MaxValue)
        {
            throw new ArgumentException("From and To dates must be valid dates");
        }
        if (from == DateTime.Now || to == DateTime.Now)
        {
            throw new ArgumentException("From and To dates must be valid dates");
        }
    }
    public IQueryable<Payment> GetPaymentsByDateRange(DateTime from, DateTime to)
    {
        ValidateDates(from, to);
       return _paymentRepository.GetPaymentsByDateRange(from, to);
    }
    public IQueryable<Payment> GetPaymentsByPatient(int patientId)
    {
        if(patientId < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(patientId), "Patient ID must be greater than zero");
        }
        var result= _paymentRepository.GetPaymentsByPatient(patientId);
        if(result == null)
        {
            throw new KeyNotFoundException($"No payments found for patient with ID {patientId}");
        }
        return result;
    }

   
}


   


