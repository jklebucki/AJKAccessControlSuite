using AJKAccessControl.Application.Services;
using AJKAccessControl.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AJKAccessControlAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Supervisor, User")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var result = await _vehicleService.GetByIdAsync(id);
            if (!result.Succeeded)
            {
                return NotFound(string.Join("|", result.Errors));
            }
            return Ok(result.Data);
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Supervisor, User")]
        public async Task<IActionResult> GetVehicles()
        {
            var result = await _vehicleService.GetAllAsync();
            if (!result.Succeeded)
            {
                return NotFound(string.Join("|", result.Errors));
            }
            return Ok(result.Data);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Supervisor, User")]
        public async Task<IActionResult> AddVehicle([FromBody] VehicleDTO vehicleDTO)
        {
            var result = await _vehicleService.AddAsync(vehicleDTO);
            if (!result.Succeeded)
            {
                return BadRequest(string.Join("|", result.Errors));
            }
            return CreatedAtAction(nameof(GetVehicle), new { id = vehicleDTO.Id }, vehicleDTO);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Supervisor, User")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] VehicleDTO vehicleDTO)
        {
            if (id != vehicleDTO.Id)
            {
                return BadRequest("ID mismatch");
            }

            var result = await _vehicleService.UpdateAsync(vehicleDTO);
            if (!result.Succeeded)
            {
                return BadRequest(string.Join("|", result.Errors));
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Supervisor, User")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var result = await _vehicleService.DeleteAsync(id);
            if (!result.Succeeded)
            {
                return BadRequest(string.Join("|", result.Errors));
            }
            return NoContent();
        }
    }
}