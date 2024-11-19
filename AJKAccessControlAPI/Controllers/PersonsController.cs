using AJKAccessControl.Application.Services;
using AJKAccessControl.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AJKAccessControlAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonsController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Supervisor, User")]
        public async Task<ActionResult<PersonDTO>> GetPerson(int id)
        {
            var person = await _personService.GetByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Supervisor, User")]
        public async Task<ActionResult<IEnumerable<PersonDTO>>> GetPersons()
        {
            var persons = await _personService.GetAllAsync();
            return Ok(persons);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Supervisor, User")]
        public async Task<ActionResult> AddPerson([FromBody] PersonDTO personDTO)
        {
            await _personService.AddAsync(personDTO);
            return CreatedAtAction(nameof(GetPerson), new { id = personDTO.Id }, personDTO);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Supervisor, User")]
        public async Task<ActionResult> UpdatePerson(int id, [FromBody] PersonDTO personDTO)
        {
            if (id != personDTO.Id)
            {
                return BadRequest();
            }

            await _personService.UpdateAsync(personDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Supervisor, User")]
        public async Task<ActionResult> DeletePerson(int id)
        {
            await _personService.DeleteAsync(id);
            return NoContent();
        }
    }
}
