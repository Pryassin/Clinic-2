using AutoMapper;
using Clinic.Models.DTOs.Person;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ModelsLayer.DTOs.Person;

namespace Clinic.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PersonController : ControllerBase
    {
        private readonly PersonService _personService;
        private readonly IMapper _mapper;
        public PersonController(PersonService personService, IMapper mapper)
        {
            _personService = personService;
            _mapper = mapper;
        }

        [HttpGet("/{id}")]
        public ActionResult<Person> GetPersonByID(int id)
        {

            try
            {
                var person = _personService.GetPersonById(id);
                var PersonToShow = _mapper.Map<GetPersonDto>(person);
                return Ok(PersonToShow);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                {
                    return StatusCode(500, "Something went wrong");
                }
            }
        }

        [HttpPost]
        [Route("")]
        public ActionResult<int> AddPerson([FromBody] CreatePersonDto person)
        {
            try
            {
                var mappedPerson = _mapper.Map<Person>(person);  // Different variable name
                var personId = _personService.AddPerson(mappedPerson);
                return Created("Person was created successfuly with id: ", personId);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception es)
            {
                return StatusCode(500, "Somthing went wrong !");
            }

        }

        [HttpDelete]
        [Route("/{id}")]
        public ActionResult DeletePerson(int id)
        {
            try
            {
                var result=_personService.DeletePerson(id);
                return StatusCode(200, $"Person with id: {id} has been deleted successfully.");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(500, ex.Message);  
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An unexpected error occurred");
                    
            }
            
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult UpdatePerson(int id,[FromBody] UpdatePersonDto person)
        {
            try
            {
                Person per= _mapper.Map<Person>(person);
                
                per.PersonId = id;
                var Person = _personService.UpdatePerson(per);
                return StatusCode(200, $"Person with Id {id} has been updated successfully");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch(InvalidOperationException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Internal Server Error!");
            }
        }
    }
}

