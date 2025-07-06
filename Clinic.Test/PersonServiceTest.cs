using System;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;
using Moq;

namespace Clinic.Test
{
    public class PersonServiceTest
    {


        //Add Person Func Tests
        [Fact]
        public void AddPerson_WithValidPerson_ReturnsNewId()
        {
            //Arrange
            var person = new Person
            {
                PersonID = 1,
                Name = "John Doe",
       
            };
            var MockRep = new Mock<IPersonRepository>();

            MockRep.Setup(x => x.Add(It.IsAny<Person>())).Returns(1);
            PersonService personService = new PersonService(MockRep.Object);

            //Act
            var result = personService.AddPerson(person);

            //Assert
            Assert.Equal(1, result);
        }
        [Fact]
        public void AddPerson_WithNonValidPerson_ThrowsException()
        {
            //Arrange
            var person = new Person
            {
                PersonID = 1,
                Name = "John Doe",
            };
            var MockRep = new Mock<IPersonRepository>();
            MockRep.Setup(x => x.Add(It.IsAny<Person>())).Returns(0);
            PersonService personService = new PersonService(MockRep.Object);

            //Act
            var ex = Assert.Throws<Exception>(() => personService.AddPerson(person));

            //Assert
            Assert.Equal("Person failed to save to the database.", ex.Message);

        }
        [Fact]
        public void AddPerson_WithNullPerson_ThrowsArgumentNullException()
        {
            var mockRepo = new Mock<IPersonRepository>();
            var service = new PersonService(mockRepo.Object);

            var ex= Assert.Throws<ArgumentNullException>(() => service.AddPerson(null));

            Assert.Contains("Person cannot be null", ex.Message);
        }


        // Delete Person Func Tests
        [Fact]
        public void DeletePerson_WithValidId_ReturnsTrue()
        {
            //Arrange
            var person = new Person
            {
                PersonID = 1,
                Name = "John Doe",
            };

            // Arrange: Create a mock repository
            var MockRep = new Mock<IPersonRepository>();

            // Setup: When GetByID is called with any ID, return the mock person
            MockRep.Setup(x => x.GetByID(It.IsAny<int>())).Returns(person);

            // Setup: When Delete is called with that person, return true (success)
            MockRep.Setup(x => x.Delete(It.IsAny<Person>())).Returns(true);

            // Inject the mock repository into the service
            PersonService personService = new PersonService(MockRep.Object);

            // Act: Call DeletePerson with a valid ID
            var result = personService.DeletePerson(1);

            // Assert: Expect the result to be true
            Assert.True(result);
        }

        [Fact]
        public void DeletePerson_WithInvalidId_ThrowsKeyNotFoundException()
        {
            // Arrange
            var MockRep = new Mock<IPersonRepository>();
            MockRep.Setup(x => x.GetByID(It.IsAny<int>())).Returns((Person)null);
            PersonService personService = new PersonService(MockRep.Object);
            // Act & Assert
            var ex = Assert.Throws<KeyNotFoundException>(() => personService.DeletePerson(1));
            Assert.Contains("Person with ID 1 not found", ex.Message);
        }

        [Fact]
        public void DeletePerson_WithNegativeId_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var MockRep = new Mock<IPersonRepository>();
            PersonService personService = new PersonService(MockRep.Object);
            // Act & Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => personService.DeletePerson(-1));
            Assert.Contains("ID must be greater than zero", ex.Message);
        }

        //Get Person By Id Func Tests
        [Fact]
        public void GetPersonById_WithValidId_ReturnsPerson()
        {
            // Arrange
            var person = new Person
            {
                PersonID = 1,
                Name = "John Doe",
            };
            var MockRep = new Mock<IPersonRepository>();
            MockRep.Setup(x => x.GetByID(It.IsAny<int>())).Returns(person);
            PersonService personService = new PersonService(MockRep.Object);
            // Act
            var result = personService.GetPersonById(1);
            // Assert
            Assert.Equal(person, result);
        }
        [Fact]
        public void GetPersonById_WithInvalidId_ThrowsKeyNotFoundException()
        {
            // Arrange
            var MockRep = new Mock<IPersonRepository>();
            MockRep.Setup(x => x.GetByID(It.IsAny<int>())).Returns((Person)null);
            PersonService personService = new PersonService(MockRep.Object);
            // Act & Assert
            var ex = Assert.Throws<KeyNotFoundException>(() => personService.GetPersonById(1));
            Assert.Contains("Person with ID 1 not found", ex.Message);
        }
        [Fact]
        public void GetPersonById_WithNegativeId_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var MockRep = new Mock<IPersonRepository>();
            PersonService personService = new PersonService(MockRep.Object);
            // Act & Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => personService.GetPersonById(-1));
            Assert.Contains("ID must be greater than zero", ex.Message);
        }


        //Update Person Func Tests
        [Fact]
        public void UpdatePerson_WithValidPerson_ReturnsTrue()
        {
            // Arrange
            var person = new Person
            {
                PersonID = 1,
                Name = "John Doe",
            };
            var MockRep = new Mock<IPersonRepository>();
            MockRep.Setup(x => x.GetByID(It.IsAny<int>())).Returns(person);
            MockRep.Setup(x => x.Update(It.IsAny<Person>())).Returns(true);
            PersonService personService = new PersonService(MockRep.Object);
            // Act
            var result = personService.UpdatePerson(person);
            // Assert
            Assert.True(result);
        }
        [Fact]
        public void UpdatePerson_WithNonExistingPerson_ThrowsKeyNotFoundException()
        {
            // Arrange
            var person = new Person
            {
                PersonID = 1,
                Name = "John Doe",
            };
            var MockRep = new Mock<IPersonRepository>();
            MockRep.Setup(x => x.GetByID(It.IsAny<int>())).Returns((Person)null);
            PersonService personService = new PersonService(MockRep.Object);
            // Act & Assert
            var ex = Assert.Throws<KeyNotFoundException>(() => personService.UpdatePerson(person));
            Assert.Contains("Person with ID 1 not found", ex.Message);
        }
        [Fact]
        public void UpdatePerson_WithDifferentId_ThrowsInvalidOperationException()
        {
            // Arrange
            var person = new Person
            {
                PersonID = 1,
                Name = "John Doe",
            };
            var existingPerson = new Person
            {
                PersonID = 2,
                Name = "Jane Doe",
            };
            var MockRep = new Mock<IPersonRepository>();
            MockRep.Setup(x => x.GetByID(It.IsAny<int>())).Returns(existingPerson);
            PersonService personService = new PersonService(MockRep.Object);
            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => personService.UpdatePerson(person));
            Assert.Contains("Cannot update a person with a different ID", ex.Message);
        }

    }
}
