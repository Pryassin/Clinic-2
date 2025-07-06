using Moq;
using DataLayer.Repositories.Interfaces;

public class AppointmentServiceTests
{
    private readonly Mock<IAppointmentRepository> _mockRepo;
    private readonly AppointmentService _service;

    public AppointmentServiceTests()
    {
        _mockRepo = new Mock<IAppointmentRepository>();
        _service = new AppointmentService(_mockRepo.Object);
    }

    //Test GetByAppointmentID

    [Fact]
    public void GetAppointmentById_ValidID_ReturnAppointment()
    { 
        var appointment = new Appointments
        {
            AppointmentId = 1,
        };
        _mockRepo.Setup(s => s.GetByID(It.IsAny<int>())).Returns(appointment);

       var result = _service.GetAppointmentById(1);
        
        Assert.Equal(appointment, result);
    }

    [Fact]
    public void GetAppointmentById_ValidID_ReturnNotFoundException()
    {
        var appointment = new Appointments
        {
            AppointmentId = 1,
        };
        _mockRepo.Setup(s => s.GetByID(It.IsAny<int>())).Returns((Appointments) null);
      
        var ex = Assert.Throws<KeyNotFoundException>(() => _service.GetAppointmentById(1));

        Assert.Contains("Appointment does not exist", ex.Message);
    }

    [Fact]
    public void GetAppointmentById_NegativeID_ThrowsArgumentOutOfRangeException()
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _service.GetAppointmentById(-1));
        Assert.Contains("cannot be negative", ex.Message);
    }

    // RescheduleAppointment Test
    [Fact]
    public void RescheduleAppointment_PastDate_ThrowsArgumentException()
    {
        var appointment = new Appointments { AppointmentId = 1, DoctorID = 1, PatientID = 1 };
        var pastDate = DateTime.Now.AddHours(-1);

        var ex = Assert.Throws<ArgumentException>(() => _service.RescheduleAppointment(appointment, pastDate));
        Assert.Contains("future", ex.Message);
    }

    [Fact]
    public void RescheduleAppointment_DoctorOrPatientNotAvailable_ReturnsFalse()
    {
        var appointment = new Appointments { AppointmentId = 1, DoctorID = 1, PatientID = 1 };
        var newDate = DateTime.Now.AddDays(1);

        _mockRepo.Setup(r => r.IsDoctorAvailableForRescheduel(1, newDate, 1)).Returns(false);
        _mockRepo.Setup(r => r.IsPatientAvailableForRescheduel(1, newDate, 1)).Returns(true);

        var result = _service.RescheduleAppointment(appointment, newDate);
        Assert.False(result);
    }


    //Schedule Appointment Test

    [Fact]
    public void ScheduleAppointment_DoctorAndPatientAvailable_ReturnsAppointmentId()
    {
        var doctor = new Doctor { DoctorID = 1 };
        var patient = new Patient { PatientID = 1 };
        var date = DateTime.Now.AddDays(1);

        _mockRepo.Setup(r => r.IsDoctorAvailable(1, date)).Returns(true);
        _mockRepo.Setup(r => r.IsPatientAvailable(1, date)).Returns(true);
        _mockRepo.Setup(r => r.Add(It.IsAny<Appointments>())).Callback<Appointments>(a => a.AppointmentId = 10);

        var result = _service.ScheduleAppointment(doctor, patient, date);
        Assert.Equal(10, result);
    }

    [Fact]
    public void ScheduleAppointment_NotAvailable_ReturnsMinusOne()
    {
        var doctor = new Doctor { DoctorID = 1 };
        var patient = new Patient { PatientID = 1 };
        var date = DateTime.Now.AddDays(1);

        _mockRepo.Setup(r => r.IsDoctorAvailable(1, date)).Returns(false);
        _mockRepo.Setup(r => r.IsPatientAvailable(1, date)).Returns(false);

        var result = _service.ScheduleAppointment(doctor, patient, date);
        Assert.Equal(-1, result);
    }

    //Cancel Appointment Test

    [Fact]
    public void CancelAppointment_NullAppointment_ThrowsArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => _service.CancelAppointment(null));
        Assert.Contains("Appointment cannot be null", ex.Message);
    }

    [Fact]
    public void CancelAppointment_PastDate_ThrowsInvalidOperationException()
    {
        var appointment = new Appointments
        {
            AppointmentId = 1,
            AppointmentDateTime = DateTime.Now.AddDays(-1),
            AppointmentStatus = enAppointmentStatus.Scheduled
        };

        var ex = Assert.Throws<InvalidOperationException>(() => _service.CancelAppointment(appointment));
        Assert.Contains("in the past", ex.Message);
    }

    [Fact]
    public void CancelAppointment_NotExists_ThrowsException()
    {
        var appointment = new Appointments
        {
            AppointmentId = 1,
            AppointmentDateTime = DateTime.Now.AddDays(1),
            AppointmentStatus = enAppointmentStatus.Scheduled
        };

        _mockRepo.Setup(r => r.DoesExist(1)).Returns(false);

        var ex = Assert.Throws<Exception>(() => _service.CancelAppointment(appointment));
        Assert.Contains("Appointment does not exist", ex.Message);
    }

    [Fact]
    public void CancelAppointment_Valid_ReturnsTrue()
    {
        var appointment = new Appointments
        {
            AppointmentId = 1,
            AppointmentDateTime = DateTime.Now.AddDays(1),
            AppointmentStatus = enAppointmentStatus.Rescheduled
        };

        _mockRepo.Setup(r => r.DoesExist(1)).Returns(true);
        _mockRepo.Setup(r => r.CancelAppointment(appointment)).Returns(true);

        var result = _service.CancelAppointment(appointment);
        Assert.True(result);
    }

    //GetAppointmentsByDoctorId Test
    [Fact]
    public void GetAppointmentsByDoctorId_NegativeId_ThrowsArgumentOutOfRangeException()
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _service.GetAppointmentsByDoctorId(-1));
        Assert.Contains("cannot be negative", ex.Message);
    }

    [Fact]
    public void GetAppointmentsByDoctorId_NoAppointments_ThrowsException()
    {
        _mockRepo.Setup(r => r.GetAppointmentsByDoctorId(1)).Returns((IQueryable<Appointments>)null);

        var ex = Assert.Throws<Exception>(() => _service.GetAppointmentsByDoctorId(1));
        Assert.Contains("No appointments found for this doctor", ex.Message);
    }

    [Fact]
    public void GetAppointmentsByDoctorId_ValidId_ReturnsAppointments()
    {
        var appointments = new List<Appointments>
    {
        new Appointments { AppointmentId = 1, DoctorID = 1 }
    }.AsQueryable();

        _mockRepo.Setup(r => r.GetAppointmentsByDoctorId(1)).Returns(appointments);

        var result = _service.GetAppointmentsByDoctorId(1);
        Assert.NotNull(result);
        Assert.Single(result);
    }

    //Test GetAppointmentsByPatientId

    [Fact]
    public void GetAppointmentsByPatientId_NegativeId_ThrowsArgumentOutOfRangeException()
    {
        var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _service.GetAppointmentsByPatientId(-5));
        Assert.Contains("cannot be negative", ex.Message);
    }

    [Fact]
    public void GetAppointmentsByPatientId_NoAppointments_ThrowsException()
    {
        _mockRepo.Setup(r => r.GetAppointmentsByPatientId(1)).Returns((IQueryable<Appointments>)null);

        var ex = Assert.Throws<Exception>(() => _service.GetAppointmentsByPatientId(1));
        Assert.Contains("No appointments found for this patient", ex.Message);
    }

    [Fact]
    public void GetAppointmentsByPatientId_ValidId_ReturnsAppointments()
    {
        var appointments = new List<Appointments>
    {
        new Appointments { AppointmentId = 1, PatientID = 1 }
    }.AsQueryable();

        _mockRepo.Setup(r => r.GetAppointmentsByPatientId(1)).Returns(appointments);

        var result = _service.GetAppointmentsByPatientId(1);
        Assert.NotNull(result);
        Assert.Single(result);
    }

}