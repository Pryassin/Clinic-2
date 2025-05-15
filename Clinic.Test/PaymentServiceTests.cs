using Moq;
using DataLayer.Repositories.Interfaces;

namespace Clinic.Tests.Services
{
    public class PaymentServiceTests
    {
        private readonly Mock<IPaymentRepository> _mockRepo;
        private readonly PaymentService _service;

        public PaymentServiceTests()
        {
            _mockRepo = new Mock<IPaymentRepository>();
            _service = new PaymentService(_mockRepo.Object);
        }

        [Fact]
        public void AddPayment_ShouldReturnId_WhenSuccessful()
        {
            var payment = new Payment { PaymentID = 1, AmountPaid  = 100, PaymentDate = DateTime.Today };

            _mockRepo.Setup(r => r.Add(payment)).Returns(1);

            var result = _service.AddPayment(payment);

            Assert.Equal(1, result);
        }

        [Fact]
        public void AddPayment_ShouldThrow_WhenPaymentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _service.AddPayment(null));
        }

        [Fact]
        public void DeletePayment_ShouldReturnTrue_WhenSuccessful()
        {
            var payment = new Payment { PaymentID = 1 };
            _mockRepo.Setup(r => r.GetByID(1)).Returns(payment);
            _mockRepo.Setup(r => r.Delete(payment)).Returns(true);

            var result = _service.DeletePayment(1);

            Assert.True(result);
        }

        [Fact]
        public void DeletePayment_ShouldThrow_WhenPaymentNotFound()
        {
            _mockRepo.Setup(r => r.GetByID(1)).Returns((Payment)null);

            Assert.Throws<KeyNotFoundException>(() => _service.DeletePayment(1));
        }

        [Fact]
        public void GetPaymentById_ShouldReturnPayment_WhenFound()
        {
            var payment = new Payment { PaymentID = 1 };
            _mockRepo.Setup(r => r.GetByID(1)).Returns(payment);

            var result = _service.GetPaymentById(1);

            Assert.Equal(1, result.PaymentID);
        }

        [Fact]
        public void UpdatePayment_ShouldReturnTrue_WhenSuccessful()
        {
            var payment = new Payment { PaymentID = 1 };
            _mockRepo.Setup(r => r.Update(payment)).Returns(true);

            var result = _service.UpdatePayment(payment);

            Assert.True(result);
        }

        [Fact]
        public void GetPaymentsByPatient_ShouldReturnResults()
        {
            var payments = new List<Payment> { new Payment { Appointments = new Appointments { PatientID = 1 } } }.AsQueryable();
            _mockRepo.Setup(r => r.GetPaymentsByPatient(1)).Returns(payments);

            var result = _service.GetPaymentsByPatient(1);

            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetPaymentsByDateRange_ShouldThrow_WhenInvalidRange()
        {
            var from = DateTime.Today;
            var to = DateTime.Today.AddDays(-1); // Invalid

            Assert.Throws<ArgumentException>(() => _service.GetPaymentsByDateRange(from, to));
        }
    }
}
