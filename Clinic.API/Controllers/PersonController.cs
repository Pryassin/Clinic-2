using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Clinic.API.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PersonController : ControllerBase
    {
        private readonly PersonService _personService;
        public PersonController(PersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("{id}")]
        public ActionResult<Person> GetPersonByID(int id)
        {

            try
            {
                var person = _personService.GetPersonById(id);
                return Ok(person);
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
    }
}

