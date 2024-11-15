using AJKAccessControl.Application.Services;
using AJKAccessControl.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AJKAccessControlAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("get-user/{userName}")]
        [Authorize (Roles = "Admin, Supervisor, User")]
        public async Task<IActionResult> GetUser(string userName)
        {
            var user = await _userService.GetUserAsync(userName);
            if (user == null || user.UserName != userName)
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }

        [HttpGet("get-users")]
        [Authorize(Roles = "Admin, Supervisor")]
        public async Task<IActionResult> GetUsers()
        {
            var user = await _userService.GetUsersAsync();
            if (user == null)
            {
                return NotFound("No users found.");
            }
            return Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerDto)
        {
            var result = await _userService.RegisterUserAsync(registerDto);
            if (!result.Succeeded)
            {
                return BadRequest(string.Join("|", result.Errors));
            }

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _userService.LoginUserAsync(loginDto);
            if (result == null || result.Data == string.Empty)
            {
                return Unauthorized(result != null ? string.Join("|", result.Errors) : "Unauthorized access.");
            }

            return Ok(new { Token = result.Data });
        }

        [HttpDelete("delete/{userName}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string userName)
        {
            var userDto = await _userService.GetUserAsync(userName);
            if (userDto == null || userDto.UserName != userName)
            {
                return NotFound("User not found.");
            }
            var result = await _userService.DeleteUserAsync(new DeleteUserDto { UserName = userName });
            if (!result.Succeeded)
            {
                return BadRequest(string.Join("|", result.Errors));
            }

            return Ok("User deleted successfully.");
        }

        [HttpPut("change-password")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto forgotPasswordDto)
        {
            var result = await _userService.ChangePasswordAsync(forgotPasswordDto);
            if (!result.Succeeded)
            {
                return BadRequest(string.Join("|", result.Errors));
            }

            return Ok("Password changed successfully.");
        }

        [HttpPut("update/{userName}")]
        [Authorize(Roles = "Admin, Supervisor")]
        public async Task<IActionResult> Update(string userName, UpdateUserDto updateUserDto)
        {
            var result = await _userService.UpdateUserAsync(userName, updateUserDto);
            if (!result.Succeeded)
            {
                return BadRequest(string.Join("|", result.Errors));
            }

            return Ok("User updated successfully.");
        }
    }
}