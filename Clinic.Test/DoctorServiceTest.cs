using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clinic_2.Services;
using DataLayer.Repositories.Interfaces;
using Moq;

namespace TClinic.Test
{
    public class DoctorServiceTest
    {
       
        [Fact]
        public void AddDoctor_WithValidID_ReturnNewId()
        {
            Doctor doctor=new Doctor() { DoctorID=1,PersonID=1,Specialization="Heart"};

            //Arrange 
            var Mock = new Mock<IDoctorRepository>();
            Mock.Setup(s=>s.Add(It.IsAny<Doctor>())).Returns(1);   
            var DoctorService = new DoctorService(Mock.Object);
            //Act
            var resut = DoctorService.AddDoctor(doctor);
            //Assert
            Assert.Equal(resut, 1);
        }
        [Fact]
        public void AddDoctor_DoctorExist_ThrowException()
        {
            // Arrange
            var doctor = new Doctor() { DoctorID = 1, PersonID = 1, Specialization = "Heart" };
            var mockRepo = new Mock<IDoctorRepository>();
            mockRepo.Setup(r => r.DoesExist(doctor.DoctorID)).Returns(true);
            var doctorService = new DoctorService(mockRepo.Object);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => doctorService.AddDoctor(doctor));
        }

        [Fact]
        public void AddDoctor_NullDoctor_ThrowArgumentNullException()
        {
            // Arrange
            var mockRepo = new Mock<IDoctorRepository>();
            var doctorService = new DoctorService(mockRepo.Object);
            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() => doctorService.AddDoctor(null));
            Assert.Contains("Doctor cannot be null", ex.Message);
        }

        //Find DoctorByID Test Func
        
        [Fact]
        public void FindDoctor_InvalidId_ThrowException()
        {
           
            var mockRepo = new Mock<IDoctorRepository>();
            var doctorService = new DoctorService(mockRepo.Object);
            // Act & Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => doctorService.FindDoctorById(-1));

            Assert.Contains("ID must be greater than zero", ex.Message);

        }

        [Fact]
        public void FindDoctorById_NotFound_ThrowException()
        {
            var mockRepo = new Mock<IDoctorRepository>();
            mockRepo.Setup(r => r.GetByID(It.IsAny<int>())).Returns((Doctor)null);
            var doctorService = new DoctorService(mockRepo.Object);
            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => doctorService.FindDoctorById(1));
            Assert.Contains("Doctor not found", ex.Message);

        }

        [Fact]
        public void FindDoctorById_Found_Return_DoctorObject()
        {
            Doctor doctor = new Doctor();
            var mockRepo = new Mock<IDoctorRepository>();
            mockRepo.Setup(r => r.GetByID(It.IsAny<int>())).Returns(doctor);
            var doctorService = new DoctorService(mockRepo.Object);

            var result = doctorService.FindDoctorById(1);

            Assert.NotNull(result);

        }

        //Find DoctorByName Test Func

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void FindDoctorByName_InvalidName_ThrowsArgumentNullException(string input)
        {
            var mockRepo = new Mock<IDoctorRepository>();
            var service = new DoctorService(mockRepo.Object);

            var ex = Assert.Throws<ArgumentNullException>(() => service.FindDoctorByName(input));
            Assert.Equal("name", ex.ParamName);
        }

        [Fact]
        public void FindDoctorByName_DoctorNotFound_ThrowsInvalidOperationException()
        {
            var mockRepo = new Mock<IDoctorRepository>();
            mockRepo.Setup(r => r.GetDoctorByName(It.IsAny<string>())).Returns((Doctor)null);

            var service = new DoctorService(mockRepo.Object);

            var ex = Assert.Throws<InvalidOperationException>(() => service.FindDoctorByName("Ali"));
            Assert.Equal("Doctor not found.", ex.Message);
        }

        [Fact]
        public void FindDoctorByName_ValidName_ReturnsDoctor()
        {
            var expectedDoctor = new Doctor { DoctorID = 1,Person= new Person() { Name = "Ali" } };
            var mockRepo = new Mock<IDoctorRepository>();
            mockRepo.Setup(r => r.GetDoctorByName("Ali")).Returns(expectedDoctor);

            var service = new DoctorService(mockRepo.Object);

            var result = service.FindDoctorByName("Ali");

            Assert.NotNull(result);
            Assert.Equal("Ali", result.Person.Name);
        }


        //Update Doctor Test Func
        [Fact]
        public void UpdateDoctor_WithNullDoctor_ThrowsArgumentNullException()
        {
            var mockRepo = new Mock<IDoctorRepository>();
            var service = new DoctorService(mockRepo.Object);

            var ex = Assert.Throws<ArgumentNullException>(() => service.UpdateDoctor(null));
            Assert.Equal("doctor", ex.ParamName);
        }
        [Fact]
        public void UpdateDoctor_WhenDoctorDoesNotExist_ThrowsInvalidOperationException()
        {
            var doctor = new Doctor { DoctorID = 1 };
            var mockRepo = new Mock<IDoctorRepository>();
            mockRepo.Setup(r => r.DoesExist(1)).Returns(false);

            var service = new DoctorService(mockRepo.Object);

            var ex = Assert.Throws<InvalidOperationException>(() => service.UpdateDoctor(doctor));
            Assert.Equal("Doctor does not exist.", ex.Message);
        }
        [Fact]
        public void UpdateDoctor_WithValidDoctor_ReturnsTrue()
        {
            var doctor = new Doctor { DoctorID = 1 };
            var mockRepo = new Mock<IDoctorRepository>();
            mockRepo.Setup(r => r.DoesExist(1)).Returns(true);
            mockRepo.Setup(r => r.Update(doctor)).Returns(true);

            var service = new DoctorService(mockRepo.Object);

            var result = service.UpdateDoctor(doctor);

            Assert.True(result);
        }

        //Get Doctor By Specialization Test Func
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void GetDoctorsBySpecialization_WithInvalidInput_ThrowsArgumentNullException(string spec)
        {
            var mockRepo = new Mock<IDoctorRepository>();
            var service = new DoctorService(mockRepo.Object);

            var ex = Assert.Throws<ArgumentNullException>(() => service.GetDoctorsBySpecialization(spec));
            Assert.Equal("spec", ex.ParamName);
        }

        [Fact]
        public void GetDoctorsBySpecialization_NoDoctorsFound_ThrowsInvalidOperationException()
        {
            var mockRepo = new Mock<IDoctorRepository>();
            mockRepo.Setup(r => r.GetBySpecialization("Cardiology")).Returns(new List<Doctor>().AsQueryable());

            var service = new DoctorService(mockRepo.Object);

            var ex = Assert.Throws<InvalidOperationException>(() => service.GetDoctorsBySpecialization("Cardiology"));
            Assert.Equal("No doctors found with the specified specialization.", ex.Message);
        }
        [Fact]
        public void GetDoctorsBySpecialization_ValidSpec_ReturnsDoctors()
        {
            var doctors = new List<Doctor> { new Doctor { DoctorID = 1 } }.AsQueryable();

            var mockRepo = new Mock<IDoctorRepository>();
            mockRepo.Setup(r => r.GetBySpecialization("Neuro")).Returns(doctors);

            var service = new DoctorService(mockRepo.Object);

            var result = service.GetDoctorsBySpecialization("Neuro");

            Assert.NotNull(result);
            Assert.Single(result);
        }
        // GetDoctorsWithAppointmentsToday
        [Fact]
        public void GetDoctorsWithAppointmentsToday_NoDoctorsFound_ThrowsInvalidOperationException()
        {
            var mockRepo = new Mock<IDoctorRepository>();
            mockRepo.Setup(r => r.GetDoctorsWithAppointmentsToday()).Returns(new List<Doctor>().AsQueryable());

            var service = new DoctorService(mockRepo.Object);

            var ex = Assert.Throws<InvalidOperationException>(() => service.GetDoctorsWithAppointmentsToday());
            Assert.Equal("No doctors found with appointments today.", ex.Message);
        }

        [Fact]
        public void GetDoctorsWithAppointmentsToday_DoctorsFound_ReturnsList()
        {
            var doctors = new List<Doctor> { new Doctor { DoctorID = 1 } }.AsQueryable();

            var mockRepo = new Mock<IDoctorRepository>();
            mockRepo.Setup(r => r.GetDoctorsWithAppointmentsToday()).Returns(doctors);

            var service = new DoctorService(mockRepo.Object);

            var result = service.GetDoctorsWithAppointmentsToday();

            Assert.NotNull(result);
            Assert.Single(result);
        }
       


    }
}
