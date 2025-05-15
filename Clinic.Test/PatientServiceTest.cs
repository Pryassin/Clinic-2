using DataLayer.Repositories.Interfaces;
using Moq;

public class PatientServiceTest
{
    private readonly Mock<IPatientRepository> _mockRepo;
    private readonly PatientService _service;
    public PatientServiceTest()
    {
        _mockRepo = new Mock<IPatientRepository>();
        _service = new PatientService(_mockRepo.Object);
    }
    // -------------------- AddPatient Tests --------------------

    [Fact]
    public void AddPatient_Null_ThrowsArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => _service.AddPaytient(null));
        Assert.Contains("Patient cannot be null", ex.Message);
    }

    [Fact]
    public void AddPatient_AlreadyExists_ThrowsArgumentException()
    {
        var patient = new Patient { PatientID = 1 };
        _mockRepo.Setup(r => r.DoesExist(1)).Returns(true);

        var ex = Assert.Throws<ArgumentException>(() => _service.AddPaytient(patient));
        Assert.Contains("Patient already exists", ex.Message);
    }

    [Fact]
    public void AddPatient_Valid_ReturnsPatientId()
    {
        var patient = new Patient { PatientID = 1 };
        _mockRepo.Setup(r => r.DoesExist(1)).Returns(false);
        _mockRepo.Setup(r => r.Add(patient)).Returns(1);

        var result = _service.AddPaytient(patient);
        Assert.Equal(1, result);
    }


    // -------------------- DeletePatient Tests --------------------

    [Fact]
    public void DeletePatient_NegativeId_ThrowsArgumentException()
    {
        var ex = Assert.Throws<ArgumentException>(() => _service.DeletePatient(-1));
        Assert.Contains("Invalid Patient ID", ex.Message);
    }

    [Fact]
    public void DeletePatient_NotFound_ThrowsArgumentNullException()
    {
        _mockRepo.Setup(r => r.GetByID(1)).Returns((Patient)null);

        var ex = Assert.Throws<ArgumentNullException>(() => _service.DeletePatient(1));
        Assert.Contains("Patient cannot be null", ex.Message);
    }

    [Fact]
    public void DeletePatient_Valid_ReturnsTrue()
    {
        var patient = new Patient { PatientID = 1 };
        _mockRepo.Setup(r => r.GetByID(1)).Returns(patient);
        _mockRepo.Setup(r => r.Delete(patient)).Returns(true);

        var result = _service.DeletePatient(1);
        Assert.True(result);
    }


    // -------------------- GetPatientById Tests --------------------

    [Fact]
    public void GetPatientById_NegativeId_ThrowsArgumentException()
    {
        var ex = Assert.Throws<ArgumentException>(() => _service.GetPatientById(-5));
        Assert.Contains("Invalid Patient ID", ex.Message);
    }

    [Fact]
    public void GetPatientById_NotFound_ThrowsArgumentNullException()
    {
        _mockRepo.Setup(r => r.GetByID(1)).Returns((Patient)null);

        var ex = Assert.Throws<ArgumentNullException>(() => _service.GetPatientById(1));
        Assert.Contains("Patient cannot be null", ex.Message);
    }

    [Fact]
    public void GetPatientById_Valid_ReturnsPatient()
    {
        var patient = new Patient { PatientID = 1 };
        _mockRepo.Setup(r => r.GetByID(1)).Returns(patient);

        var result = _service.GetPatientById(1);
        Assert.Equal(1, result.PatientID);
    }


    // -------------------- UpdatePatient Tests --------------------

    [Fact]
    public void UpdatePatient_Null_ThrowsArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => _service.UpdatePatient(null));
        Assert.Contains("Patient cannot be null", ex.Message);
    }

    [Fact]
    public void UpdatePatient_Valid_ReturnsTrue()
    {
        var patient = new Patient { PatientID = 1 };
        _mockRepo.Setup(r => r.Update(patient)).Returns(true);

        var result = _service.UpdatePatient(patient);
        Assert.True(result);
    }

}