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
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            var result = await _userService.RegisterUserAsync(registerDto);
            if (!result)
            {
                return BadRequest("User registration failed.");
            }

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var token = await _userService.LoginUserAsync(loginDto);
            if (token == null || token == string.Empty)
            {
                return Unauthorized("Invalid credentials.");
            }

            return Ok(new { Token = token });
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete(DeleteUserDto deleteUserDto)
        {
            var result = await _userService.DeleteUserAsync(deleteUserDto);
            if (!result)
            {
                return BadRequest("User deletion failed.");
            }

            return Ok("User deleted successfully.");
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var result = await _userService.ForgotPasswordAsync(forgotPasswordDto);
            if (!result)
            {
                return BadRequest("Password reset failed.");
            }

            return Ok("Password reset email sent.");
        }

        [HttpPut("update/{email}")]
        [Authorize]
        public async Task<IActionResult> Update(string email, UpdateUserDto updateUserDto)
        {
            var result = await _userService.UpdateUserAsync(updateUserDto);
            if (!result)
            {
                return BadRequest("User update failed.");
            }

            return Ok("User updated successfully.");
        }
    }
}