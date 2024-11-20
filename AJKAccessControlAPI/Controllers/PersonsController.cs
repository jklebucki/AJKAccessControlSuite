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
        public async Task<IActionResult> GetPerson(int id)
        {
            var result = await _personService.GetByIdAsync(id);
            if (!result.Succeeded)
            {
                return NotFound(string.Join("|", result.Errors));
            }
            return Ok(result.Data);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Supervisor, User")]
        public async Task<IActionResult> GetPersons()
        {
            var result = await _personService.GetAllAsync();
            if (!result.Succeeded)
            {
                return NotFound(string.Join("|", result.Errors));
            }
            return Ok(result.Data);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Supervisor, User")]
        public async Task<IActionResult> AddPerson([FromBody] PersonDTO personDTO)
        {
            var result = await _personService.AddAsync(personDTO);
            if (!result.Succeeded)
            {
                return BadRequest(string.Join("|", result.Errors));
            }
            return CreatedAtAction(nameof(GetPerson), new { id = personDTO.Id }, personDTO);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Supervisor, User")]
        public async Task<IActionResult> UpdatePerson(int id, [FromBody] PersonDTO personDTO)
        {
            if (id != personDTO.Id)
            {
                return BadRequest("ID mismatch");
            }

            var result = await _personService.UpdateAsync(personDTO);
            if (!result.Succeeded)
            {
                return BadRequest(string.Join("|", result.Errors));
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Supervisor, User")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var result = await _personService.DeleteAsync(id);
            if (!result.Succeeded)
            {
                return BadRequest(string.Join("|", result.Errors));
            }
            return NoContent();
        }
    }
}
