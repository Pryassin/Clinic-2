using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using Clinic_2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ModelsLayer.DTOs.Doctor;

namespace APILayer.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class DoctorController:ControllerBase
    {
        private readonly IPersonService _personservice;
        private readonly IDoctorService _doctorservice;
        private readonly IMapper _mapper;
        public DoctorController(IDoctorService doctorService,IPersonService personService,IMapper mapper)
        {
            _personservice = personService;
            _doctorservice = doctorService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("")]
        public ActionResult<int> AddDoctor([FromBody] CreateUpdateDoctorDTO doctor)
        {
            try
            {
                var docCreate = _mapper.Map<Doctor>(doctor);
                var Person = new Person
                {
                    Name = doctor.Name,
                    Address = doctor.Address,
                    Email = doctor.Email,
                    DateOfBirth = doctor.DateOfBirth,
                    Gender = doctor.Gender,
                    PhoneNumber = doctor.PhoneNumber,

                };
               
                docCreate.PersonID = _personservice.AddPerson(Person);
                _doctorservice.AddDoctor(docCreate);
                return StatusCode(200, $"Doctor was created with id {docCreate.DoctorID}");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

       
        }


        [HttpPut]
        [Route("{Id}")]
        public ActionResult UpdateDoctor(int Id, [FromBody] CreateUpdateDoctorDTO doctor)
        {
            if(Id<=0)
            {
                return BadRequest("Invalid Id");
            }
            try
            {

              //Update the Person First
                var ExistingDoc = _doctorservice.FindDoctorById(Id);
                var Person = _mapper.Map<Person>(doctor);
                Person.PersonId = ExistingDoc.PersonID;

                _personservice.UpdatePerson(Person);

                ExistingDoc.Specialization = doctor.Specialization;

                _doctorservice.UpdateDoctor(ExistingDoc);

                return Ok($"Doctor with Id {ExistingDoc.DoctorID} was updated successfuly");

            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"Doctor with ID {Id} not found");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error!");
            }

        }

        [HttpGet]
        [Route("")]
        public ActionResult<GetDoctorDTO> GetDoctorById(int Id)
        {
            try
            {
                var doctor = _doctorservice.FindDoctorById(Id);
                var docDTO = _mapper.Map<GetDoctorDTO>(doctor);

                return Ok(docDTO);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound($"Doctor With Id {Id} Not found");
            }
            catch(ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public ActionResult DeleteDoctor(int Id)
        {
            try
            {
                var doc = _doctorservice.FindDoctorById(Id);
                // delete the person will delete the doctor (cascade on delete)
       
                _personservice.DeletePerson(doc.PersonID);

                return Ok($"Doctor with id {doc.DoctorID} was deleted succesfuly");
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(ex.Message);  
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
