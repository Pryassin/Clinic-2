public interface IPaymentService
{
    public int AddPayment(Payment payment);
    public bool UpdatePayment(Payment payment);
    public bool DeletePayment(int paymentId);
    public Payment GetPaymentById(int paymentId);
    public IQueryable<Payment> GetPaymentsByPatient(int patientId);
    public IQueryable<Payment> GetPaymentsByDateRange(DateTime from, DateTime to);
}
