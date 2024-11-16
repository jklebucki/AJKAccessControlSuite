using AJKAccessControl.Domain.Entities;
using AJKAccessControl.Domain.Responses;
using AJKAccessControl.Infrastructure.Repositories;
using AJKAccessControl.Shared.Configurations;
using AJKAccessControl.Shared.DTOs;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AJKAccessControl.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtSettings _jwtSettings;

        public UserService(IUserRepository userRepository, JwtSettings jwtSettings)
        {
            _userRepository = userRepository;
            _jwtSettings = jwtSettings;
        }

        public async Task<OperationResult<string>> RegisterUserAsync(RegisterUserDto registerDto)
        {
            var user = new User
            {
                UserName = registerDto.UserName,
                PhoneNumber = registerDto.PhoneNumber,
                Email = registerDto.Email!,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName
            };

            return await _userRepository.CreateUserAsync(user, registerDto.Password, registerDto.Role);
        }

        public async Task<OperationResult<string>> LoginUserAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByUserNamelAsync(loginDto.UserName);
            if (user == null || !(await _userRepository.CheckPasswordAsync(user, loginDto.Password)).Succeeded)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new List<string> { "Invalid login attempt" } };
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty)
            };

            foreach (var role in user.Roles)
            {
                if (!string.IsNullOrEmpty(role))
                    claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new OperationResult<string>
            {
                Succeeded = true,
                Data = tokenHandler.WriteToken(token)
            };
        }

        public async Task<OperationResult<string>> DeleteUserAsync(DeleteUserDto deleteUserDto)
        {
            var user = await _userRepository.GetUserByUserNamelAsync(deleteUserDto.UserName);
            if (user == null)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new List<string> { "User not found" } };
            }

            var result = await _userRepository.DeleteUserAsync(user);
            return new OperationResult<string> { Succeeded = result.Succeeded, Errors = result.Errors };
        }

        public async Task<OperationResult<string>> ChangePasswordAsync(ChangePasswordDto changePasswordDto)
        {
            var user = await _userRepository.GetUserByUserNamelAsync(changePasswordDto.UserName);
            if (user == null)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new List<string> { "User not found" } };
            }

            return await _userRepository.ChangePasswordAsync(user, changePasswordDto.Password);
        }

        public async Task<OperationResult<string>> UpdateUserAsync(string userName, UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.GetUserByUserNamelAsync(userName);
            if (user == null)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new List<string> { "User not found" } };
            }

            user.PhoneNumber = updateUserDto.PhoneNumber;
            user.Email = updateUserDto.Email;
            user.FirstName = updateUserDto.FirstName;
            user.LastName = updateUserDto.LastName;
            user.Roles = updateUserDto.Roles;

            if (!string.IsNullOrEmpty(updateUserDto.Password))
            {
                var passwordResult = await _userRepository.ChangePasswordAsync(user, updateUserDto.Password);
                if (!passwordResult.Succeeded)
                {
                    return passwordResult;
                }
            }
            return await _userRepository.UpdateUserAsync(userName, user);
        }

        public async Task<UserDto> GetUserAsync(string userName)
        {
            var user = await _userRepository.GetUserByUserNamelAsync(userName);
            if (user == null)
            {
                return new UserDto();
            }

            return new UserDto
            {
                UserName = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                Roles = user.Roles
            };
        }

        public async Task<OperationResult<string>> AddUserToRoleAsync(AddUserToRoleDto addRoleDto)
        {
            return await _userRepository.AddUserToRoleAsync(addRoleDto.UserName, addRoleDto.Role);

        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            var users = await _userRepository.GetUsersAsync();
            return users.Select(user => new UserDto
            {
                UserName = user.UserName ?? string.Empty,
                Email = user.Email ?? string.Empty,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            });

        }
    }
}