using DataLayer.Data;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;

/// <summary>
/// Service class for handling payment-related operations.
/// </summary>
public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="PaymentService"/> class.
    /// </summary>
    /// <param name="paymentRepository">The payment repository dependency.</param>
    public PaymentService(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    /// <summary>
    /// Checks if the provided payment is not null.
    /// </summary>
    /// <param name="payment">The payment to check.</param>
    /// <exception cref="ArgumentNullException">Thrown if payment is null.</exception>
    void CheckPaymentIsNotNull(Payment payment)
    {
        if (payment == null)
        {
            throw new ArgumentNullException(nameof(payment), "Payment cannot be null");
        }
    }

    /// <summary>
    /// Adds a new payment to the repository.
    /// </summary>
    /// <param name="payment">The payment to add.</param>
    /// <returns>The ID of the added payment.</returns>
    /// <exception cref="ArgumentNullException">Thrown if payment is null.</exception>
    /// <exception cref="Exception">Thrown if the payment fails to save.</exception>
    public int AddPayment(Payment payment)
    {
        CheckPaymentIsNotNull(payment);
        var result = _paymentRepository.Add(payment);
        if (result <= 0)
        {
            throw new Exception("Payment failed to save to the database.");
        }
        return result;
    }

    /// <summary>
    /// Deletes a payment by its ID.
    /// </summary>
    /// <param name="paymentId">The ID of the payment to delete.</param>
    /// <returns>True if the payment was deleted; otherwise, false.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if paymentId is less than zero.</exception>
    /// <exception cref="KeyNotFoundException">Thrown if the payment is not found.</exception>
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

    /// <summary>
    /// Retrieves a payment by its ID.
    /// </summary>
    /// <param name="paymentId">The ID of the payment to retrieve.</param>
    /// <returns>The payment with the specified ID.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if paymentId is less than zero.</exception>
    /// <exception cref="KeyNotFoundException">Thrown if the payment is not found.</exception>
    public Payment GetPaymentById(int paymentId)
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
        return payment;
    }

    /// <summary>
    /// Updates an existing payment.
    /// </summary>
    /// <param name="payment">The payment to update.</param>
    /// <returns>True if the update was successful.</returns>
    /// <exception cref="ArgumentNullException">Thrown if payment is null.</exception>
    /// <exception cref="Exception">Thrown if the update fails.</exception>
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

    /// <summary>
    /// Validates the provided date range.
    /// </summary>
    /// <param name="from">The start date.</param>
    /// <param name="to">The end date.</param>
    /// <exception cref="ArgumentException">Thrown if the dates are invalid.</exception>
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

    /// <summary>
    /// Retrieves payments within a specified date range.
    /// </summary>
    /// <param name="from">The start date.</param>
    /// <param name="to">The end date.</param>
    /// <returns>An <see cref="IQueryable{Payment}"/> of payments in the date range.</returns>
    /// <exception cref="ArgumentException">Thrown if the dates are invalid.</exception>
    public IQueryable<Payment> GetPaymentsByDateRange(DateTime from, DateTime to)
    {
        ValidateDates(from, to);
        return _paymentRepository.GetPaymentsByDateRange(from, to);
    }

    /// <summary>
    /// Retrieves payments for a specific patient.
    /// </summary>
    /// <param name="patientId">The ID of the patient.</param>
    /// <returns>An <see cref="IQueryable{Payment}"/> of payments for the patient.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if patientId is less than zero.</exception>
    /// <exception cref="KeyNotFoundException">Thrown if no payments are found for the patient.</exception>
    public IQueryable<Payment> GetPaymentsByPatient(int patientId)
    {
        if (patientId < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(patientId), "Patient ID must be greater than zero");
        }
        var result = _paymentRepository.GetPaymentsByPatient(patientId);
        if (result == null)
        {
            throw new KeyNotFoundException($"No payments found for patient with ID {patientId}");
        }
        return result;
    }
}


   


