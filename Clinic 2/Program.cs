using Clinic_2.Services;
using DataLayer.Data;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Test
{
    internal class Program
    {  
        static void Main(string[] args)
        {


            var context = new ClinicDbContext();
            Doctor doctor = new Doctor { PersonID = 4 ,Specialization="Heart"};
            DoctorService doctorService = new DoctorService(context);
            //Add a new patient
            int ID = doctorService.Add(doctor);
            Console.WriteLine($"Added Doctor with ID: {ID}");

            //Add a new Person
            //Person person = new Person { Name = "saber", Gender = 'M', Address = "Kaous", DateOfBirth = new DateTime(2004, 2, 2),
            //    PhoneNumber="0797964533",Email="saber@gmail.com" };
            //IPersonRepository personRepository = new PersonRepository(context);
            //PersonService personService = new PersonService(personRepository);
            //personService.Add(person);
            //Console.WriteLine($"Added Person with ID: {person.PersonID}");


        }
    }
    
}

