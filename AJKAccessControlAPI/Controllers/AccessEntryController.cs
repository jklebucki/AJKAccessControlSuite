
using AJKAccessControl.Application.Services;
using AJKAccessControl.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AJKAccessControlAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessEntryController : ControllerBase
    {
        private readonly IAccessEntryService _accessEntryService;

        public AccessEntryController(IAccessEntryService accessEntryService)
        {
            _accessEntryService = accessEntryService;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Supervisor, User")]
        public async Task<IActionResult> GetAccessEntry(int id)
        {
            var result = await _accessEntryService.GetByIdAsync(id);
            if (!result.Succeeded)
            {
                return NotFound(string.Join("|", result.Errors));
            }
            return Ok(result.Data);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Supervisor, User")]
        public async Task<IActionResult> GetAccessEntries()
        {
            var result = await _accessEntryService.GetAllAsync();
            if (!result.Succeeded)
            {
                return NotFound(string.Join("|", result.Errors));
            }
            return Ok(result.Data);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Supervisor, User")]
        public async Task<IActionResult> AddAccessEntry([FromBody] AccessEntryDto accessEntryDTO)
        {
            var result = await _accessEntryService.AddAsync(accessEntryDTO);
            if (!result.Succeeded)
            {
                return BadRequest(string.Join("|", result.Errors));
            }
            return CreatedAtAction(nameof(GetAccessEntry), new { id = accessEntryDTO.Id }, accessEntryDTO);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Supervisor, User")]
        public async Task<IActionResult> UpdateAccessEntry(int id, [FromBody] AccessEntryDto accessEntryDTO)
        {
            if (id != accessEntryDTO.Id)
            {
                return BadRequest("ID mismatch");
            }

            var result = await _accessEntryService.UpdateAsync(accessEntryDTO);
            if (!result.Succeeded)
            {
                return BadRequest(string.Join("|", result.Errors));
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Supervisor, User")]
        public async Task<IActionResult> DeleteAccessEntry(int id)
        {
            var result = await _accessEntryService.DeleteAsync(id);
            if (!result.Succeeded)
            {
                return BadRequest(string.Join("|", result.Errors));
            }
            return NoContent();
        }
    }
}