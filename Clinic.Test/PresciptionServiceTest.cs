using DataLayer.Repositories.Interfaces;
using Moq;

public class PresciptionServiceTest
{
    private readonly Mock<IPrescriptionsRepository> _mockRepo;
    private readonly PrescriptionsService _service;
    public PresciptionServiceTest()
    {
        _mockRepo = new Mock<IPrescriptionsRepository>();
        _service = new PrescriptionsService(_mockRepo.Object);
    }
    // -------------------- AddPrescription Tests --------------------

    [Fact]
    public void AddPrescription_Null_ThrowsArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => _service.AddPrescription(null));
        Assert.Contains("prescription", ex.ParamName);
    }

    [Fact]
    public void AddPrescription_EmptyMedicationName_ThrowsArgumentException()
    {
        var prescription = new Prescription { MedicationName = "  " };
        var ex = Assert.Throws<ArgumentException>(() => _service.AddPrescription(prescription));
        Assert.Contains("Medication name is required", ex.Message);
    }

    [Fact]
    public void AddPrescription_StartDateAfterEndDate_ThrowsArgumentException()
    {
        var prescription = new Prescription
        {
            MedicationName = "TestMed",
            StartDate = DateTime.Now.AddDays(5),
            EndDate = DateTime.Now
        };
        var ex = Assert.Throws<ArgumentException>(() => _service.AddPrescription(prescription));
        Assert.Contains("Start date must be earlier", ex.Message);
    }

    [Fact]
    public void AddPrescription_Valid_ReturnsId()
    {
        var prescription = new Prescription
        {
            MedicationName = "TestMed",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(5)
        };
        _mockRepo.Setup(r => r.Add(prescription)).Returns(1);

        var result = _service.AddPrescription(prescription);
        Assert.Equal(1, result);
    }


    // -------------------- DeletePrescription Tests --------------------

    [Fact]
    public void DeletePrescription_InvalidId_ThrowsArgumentException()
    {
        var ex = Assert.Throws<ArgumentException>(() => _service.DeletePrescription(0));
        Assert.Contains("Invalid prescription ID", ex.Message);
    }

    [Fact]
    public void DeletePrescription_NotFound_ReturnsFalse()
    {
        _mockRepo.Setup(r => r.GetByID(1)).Returns((Prescription)null);
        var result = _service.DeletePrescription(1);
        Assert.False(result);
    }

    [Fact]
    public void DeletePrescription_Valid_ReturnsTrue()
    {
        var prescription = new Prescription { PrescriptionID = 1 };
        _mockRepo.Setup(r => r.GetByID(1)).Returns(prescription);
        _mockRepo.Setup(r => r.Delete(prescription)).Returns(true);

        var result = _service.DeletePrescription(1);
        Assert.True(result);
    }


    // -------------------- GetPrescriptionById Tests --------------------

    [Fact]
    public void GetPrescriptionById_InvalidId_ThrowsArgumentException()
    {
        var ex = Assert.Throws<ArgumentException>(() => _service.GetPrescriptionById(0));
        Assert.Contains("Invalid prescription ID", ex.Message);
    }

    [Fact]
    public void GetPrescriptionById_NotFound_ThrowsKeyNotFoundException()
    {
        _mockRepo.Setup(r => r.GetByID(1)).Returns((Prescription)null);
        var ex = Assert.Throws<KeyNotFoundException>(() => _service.GetPrescriptionById(1));
        Assert.Contains("Prescription not found", ex.Message);
    }

    [Fact]
    public void GetPrescriptionById_Valid_ReturnsPrescription()
    {
        var prescription = new Prescription { PrescriptionID = 1 };
        _mockRepo.Setup(r => r.GetByID(1)).Returns(prescription);

        var result = _service.GetPrescriptionById(1);
        Assert.Equal(1, result.PrescriptionID);
    }


    // -------------------- GetPrescriptionByMedicalRecordId Tests --------------------

    [Fact]
    public void GetPrescriptionByMedicalRecordId_InvalidId_ThrowsArgumentException()
    {
        var ex = Assert.Throws<ArgumentException>(() => _service.GetPrescriptionByMedicalRecordId(0));
        Assert.Contains("Invalid l record ID", ex.Message);
    }

    [Fact]
    public void GetPrescriptionByMedicalRecordId_Valid_ReturnsPrescription()
    {
        var prescription = new Prescription { PrescriptionID = 2 };
        _mockRepo.Setup(r => r.GetByMedicalRecordId(1)).Returns(prescription);

        var result = _service.GetPrescriptionByMedicalRecordId(1);
        Assert.NotNull(result);
    }


    // -------------------- GetPrescriptionByPatientId Tests --------------------

    [Fact]
    public void GetPrescriptionByPatientId_InvalidId_ThrowsArgumentException()
    {
        var ex = Assert.Throws<ArgumentException>(() => _service.GetPrescriptionByPatientId(0));
        Assert.Contains("Invalid patient ID", ex.Message);
    }

    [Fact]
    public void GetPrescriptionByPatientId_Valid_ReturnsPrescription()
    {
        var prescription = new Prescription { PrescriptionID = 3 };
        _mockRepo.Setup(r => r.GetByPatientId(1)).Returns(prescription);

        var result = _service.GetPrescriptionByPatientId(1);
        Assert.NotNull(result);
    }


    // -------------------- SearchPrescriptionByMedicationName Tests --------------------

    [Fact]
    public void SearchPrescriptionByMedicationName_EmptyName_ThrowsArgumentException()
    {
        var ex = Assert.Throws<ArgumentException>(() => _service.SearchPrescriptionByMedicationName(" "));
        Assert.Contains("Medication name is required", ex.Message);
    }

    [Fact]
    public void SearchPrescriptionByMedicationName_Valid_ReturnsPrescription()
    {
        var prescription = new Prescription { MedicationName = "TestMed" };
        _mockRepo.Setup(r => r.SearchByMedicationName("TestMed")).Returns(prescription);

        var result = _service.SearchPrescriptionByMedicationName("TestMed");
        Assert.NotNull(result);
    }


    // -------------------- UpdatePrescription Tests --------------------

    [Fact]
    public void UpdatePrescription_Null_ThrowsArgumentNullException()
    {
        var ex = Assert.Throws<ArgumentNullException>(() => _service.UpdatePrescription(null));
        Assert.Contains("prescription", ex.ParamName);
    }

    [Fact]
    public void UpdatePrescription_NotExist_ThrowsKeyNotFoundException()
    {
        var prescription = new Prescription { PrescriptionID = 5 };
        _mockRepo.Setup(r => r.DoesExist(5)).Returns(false);

        var ex = Assert.Throws<KeyNotFoundException>(() => _service.UpdatePrescription(prescription));
        Assert.Contains("Prescription not found", ex.Message);
    }

    [Fact]
    public void UpdatePrescription_EmptyMedicationName_ThrowsArgumentException()
    {
        var prescription = new Prescription { PrescriptionID = 5, MedicationName = " " };
        _mockRepo.Setup(r => r.DoesExist(5)).Returns(true);

        var ex = Assert.Throws<ArgumentException>(() => _service.UpdatePrescription(prescription));
        Assert.Contains("Medication name is required", ex.Message);
    }

    [Fact]
    public void UpdatePrescription_StartAfterEnd_ThrowsArgumentException()
    {
        var prescription = new Prescription
        {
            PrescriptionID = 5,
            MedicationName = "Test",
            StartDate = DateTime.Now.AddDays(5),
            EndDate = DateTime.Now
        };
        _mockRepo.Setup(r => r.DoesExist(5)).Returns(true);

        var ex = Assert.Throws<ArgumentException>(() => _service.UpdatePrescription(prescription));
        Assert.Contains("Start date must be earlier", ex.Message);
    }

    [Fact]
    public void UpdatePrescription_Valid_ReturnsTrue()
    {
        var prescription = new Prescription
        {
            PrescriptionID = 5,
            MedicationName = "Test",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(3)
        };
        _mockRepo.Setup(r => r.DoesExist(5)).Returns(true);
        _mockRepo.Setup(r => r.Update(prescription)).Returns(true);

        var result = _service.UpdatePrescription(prescription);
        Assert.True(result);
    }

}
