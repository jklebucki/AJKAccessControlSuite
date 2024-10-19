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

        [HttpGet("get-user/{email}")]
        [Authorize]
        public async Task<IActionResult> GetUser(string email)
        {
            var user = await _userService.GetUserAsync(email);
            if (user == null || user.Email != email)
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }

        [HttpGet("get-users")]
        [Authorize]
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

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(DeleteUserDto deleteUserDto)
        {
            var result = await _userService.DeleteUserAsync(deleteUserDto);
            if (!result.Succeeded)
            {
                return BadRequest(string.Join("|", result.Errors));
            }

            return Ok("User deleted successfully.");
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var result = await _userService.ForgotPasswordAsync(forgotPasswordDto);
            if (!result.Succeeded)
            {
                return BadRequest(string.Join("|", result.Errors));
            }

            return Ok("Password reset email sent.");
        }

        [HttpPut("update/{email}")]
        [Authorize]
        public async Task<IActionResult> Update(string email, UpdateUserDto updateUserDto)
        {
            var result = await _userService.UpdateUserAsync(updateUserDto);
            if (!result.Succeeded)
            {
                return BadRequest(string.Join("|", result.Errors));
            }

            return Ok("User updated successfully.");
        }
    }
}