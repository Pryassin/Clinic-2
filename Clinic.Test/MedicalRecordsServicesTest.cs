using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using DataLayer.Repositories.Interfaces;

namespace Clinic.Tests.Services
{
    public class MedicalRecordsServiceTests
    {
        private readonly Mock<IMedicalRecordRepository> _mockRepo;
        private readonly MedicalRecordsService _service;

        public MedicalRecordsServiceTests()
        {
            _mockRepo = new Mock<IMedicalRecordRepository>();
            _service = new MedicalRecordsService(_mockRepo.Object);
        }

        [Fact]
        public void AddMedicalRecord_WithNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _service.AddMedicalRecord(null));
        }

        [Fact]
        public void AddMedicalRecord_AlreadyExists_ThrowsArgumentException()
        {
            var record = new MedicalRecord { MedicalRecordID = 1 };
            _mockRepo.Setup(r => r.DoesExist(1)).Returns(true);

            var ex = Assert.Throws<ArgumentException>(() => _service.AddMedicalRecord(record));
            Assert.Equal("Medical record already exists", ex.Message);
        }

        [Fact]
        public void AddMedicalRecord_Valid_ReturnsId()
        {
            var record = new MedicalRecord { MedicalRecordID = 1 };
            _mockRepo.Setup(r => r.DoesExist(1)).Returns(false);
            _mockRepo.Setup(r => r.Add(record)).Returns(1);

            var result = _service.AddMedicalRecord(record);

            Assert.Equal(1, result);
        }

        [Fact]
        public void DeleteMedicalRecord_WithNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _service.DeleteMedicalRecord(null));
        }

        [Fact]
        public void DeleteMedicalRecord_NotExists_ThrowsArgumentException()
        {
            var record = new MedicalRecord { MedicalRecordID = 1 };
            _mockRepo.Setup(r => r.DoesExist(1)).Returns(false);

            var ex = Assert.Throws<ArgumentException>(() => _service.DeleteMedicalRecord(record));
            Assert.Equal("Medical record does not exist", ex.Message);
        }

        [Fact]
        public void DeleteMedicalRecord_Valid_ReturnsTrue()
        {
            var record = new MedicalRecord { MedicalRecordID = 1 };
            _mockRepo.Setup(r => r.DoesExist(1)).Returns(true);
            _mockRepo.Setup(r => r.Delete(record)).Returns(true);

            var result = _service.DeleteMedicalRecord(record);

            Assert.True(result);
        }

        [Fact]
        public void GetMedicalRecordByID_NegativeId_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _service.GetMedicalRecordByID(-1));
        }

        [Fact]
        public void GetMedicalRecordByID_NotFound_ThrowsKeyNotFoundException()
        {
            _mockRepo.Setup(r => r.GetByID(1)).Returns((MedicalRecord)null);

            var ex = Assert.Throws<KeyNotFoundException>(() => _service.GetMedicalRecordByID(1));
            Assert.Equal("Medical record with ID 1 not found", ex.Message);
        }

        [Fact]
        public void GetMedicalRecordByID_Valid_ReturnsRecord()
        {
            var record = new MedicalRecord { MedicalRecordID = 1 };
            _mockRepo.Setup(r => r.GetByID(1)).Returns(record);

            var result = _service.GetMedicalRecordByID(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.MedicalRecordID);
        }

        [Fact]
        public void UpdateMedicalRecord_WithNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _service.UpdateMedicalRecord(null));
        }

        [Fact]
        public void UpdateMedicalRecord_NotExists_ThrowsArgumentException()
        {
            var record = new MedicalRecord { MedicalRecordID = 1 };
            _mockRepo.Setup(r => r.DoesExist(1)).Returns(false);

            var ex = Assert.Throws<ArgumentException>(() => _service.UpdateMedicalRecord(record));
            Assert.Equal("Medical record does not exist", ex.Message);
        }

        [Fact]
        public void UpdateMedicalRecord_Valid_ReturnsTrue()
        {
            var record = new MedicalRecord { MedicalRecordID = 1 };
            _mockRepo.Setup(r => r.DoesExist(1)).Returns(true);
            _mockRepo.Setup(r => r.Update(record)).Returns(true);

            var result = _service.UpdateMedicalRecord(record);

            Assert.True(result);
        }
    }
}
