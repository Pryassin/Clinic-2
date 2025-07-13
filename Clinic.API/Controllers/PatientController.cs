using AutoMapper;
using Clinic.Models.DTOs.Person;
using Microsoft.AspNetCore.Mvc;
using ModelsLayer.DTOs;
using ModelsLayer.DTOs.Patient;
namespace APILayer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IPersonService _personService;
        private readonly IMapper _mapper;

        public PatientController(IPatientService patientService,IPersonService personservice,IMapper mapper)
        {
            _patientService = patientService;
            _personService = personservice;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("{Id}")]
        public ActionResult<Patient> GetPatientByID(int Id)
        {
            try
            {
                var Pation = _patientService.GetPatientById(Id);
                return Ok(Pation);

            }
            catch(KeyNotFoundException exception)
            {
                return  NotFound(exception.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Somthing went wrong ");
            }
        }

        [HttpPost]
        [Route("api/")]
        public ActionResult<int> AddPatient([FromBody] CreateUpdatePatientDTO patient)
        {
            try
            {
                var Person = new Person
                {
                    Name = patient.Name,
                    Address = patient.Address,
                    Email = patient.Email,
                    DateOfBirth = patient.DateOfBirth,
                    Gender = patient.Gender,
                    PhoneNumber = patient.PhoneNumber,

                };
                var personId=_personService.AddPerson(Person);

                var Patient=_mapper.Map<Patient>(patient);

                Patient.PersonID = personId;
                Patient.EmergencyContactName = patient.EmergencyContactName;
                Patient.EmergencyContactPhone = patient.EmergencyContactPhone;

                var PatientID=_patientService.AddPaytient(Patient);

                return Ok($"Patient was added successfuly with ID: ,{Patient.PatientID}");

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("{Id}")]
        public ActionResult UpdatePatient(int Id, [FromBody] CreateUpdatePatientDTO CUpatientDTO)
        {
            try
            {
                var existingPatient = _patientService.GetPatientById(Id);
                var personId = existingPatient.PersonID;

                // Update person
                var person = _mapper.Map<Person>(CUpatientDTO);
                person.PersonId = personId;
                _personService.UpdatePerson(person);

                // Update patient directly
                existingPatient.EmergencyContactName = CUpatientDTO.EmergencyContactName;
                existingPatient.EmergencyContactPhone = CUpatientDTO.EmergencyContactPhone;

                _patientService.UpdatePatient(existingPatient);

                return Ok($"Patient with id {existingPatient.PatientID} was updated successfully");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error!");
            }
        }


        [HttpDelete]
        [Route("{Id}")]

        public ActionResult DeletePatient(int Id)
        {
            try
            {
                var patient = _patientService.GetPatientById(Id);

                // deleting the person  will delete the patient since we have onDelete: ReferentialAction.Cascade from Person to Patient
                _personService.DeletePerson(patient.PersonID);
               

                return StatusCode(200, $"Patient with id {patient.PatientID} was deleted successfuly");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        


    }
}
