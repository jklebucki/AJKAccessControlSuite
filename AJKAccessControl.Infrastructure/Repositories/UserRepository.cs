using AJKAccessControl.Domain.Entities;
using AJKAccessControl.Domain.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AJKAccessControl.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> GetUserByUserNamelAsync(string userName)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userName);
            if (user == null)
            {
                return new User();
            }
            var roles = await _userManager.GetRolesAsync(user);
            if (roles != null)
            {
                user.Roles = [.. roles];
            }
            return user;
        }

        public async Task<OperationResult<string>> CreateUserAsync(User user, string password, string role)
        {
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
            return new OperationResult<string>
            {
                Succeeded = result.Succeeded,
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }

        public async Task<OperationResult<string>> DeleteUserAsync(User user)
        {
            var result = await _userManager.DeleteAsync(user);
            return new OperationResult<string>
            {
                Succeeded = result.Succeeded,
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }

        public async Task<OperationResult<string>> CheckPasswordAsync(User user, string password)
        {
            var result = await _userManager.CheckPasswordAsync(user, password);
            return new OperationResult<string>
            {
                Succeeded = result,
                Errors = result ? new List<string>() : new List<string> { "Password check failed" }
            };
        }

        public async Task<OperationResult<string>> UpdateUserAsync(User user, string password)
        {
            if (string.IsNullOrEmpty(user.UserName))
            {
                throw new ArgumentException("Username cannot be null or empty.", nameof(user.UserName));
            }

            var existingUser = await _userManager.FindByNameAsync(user.UserName);
            if (existingUser == null)
            {
                return new OperationResult<string>
                {
                    Succeeded = false,
                    Errors = new List<string> { "User not found" }
                };
            }

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;

            var updateResult = await _userManager.UpdateAsync(existingUser);
            if (!updateResult.Succeeded)
            {
                return new OperationResult<string>
                {
                    Succeeded = false,
                    Errors = updateResult.Errors.Select(e => e.Description).ToList()
                };
            }

            if (!string.IsNullOrEmpty(password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(existingUser);
                var passwordResult = await _userManager.ResetPasswordAsync(existingUser, token, password);
                return new OperationResult<string>
                {
                    Succeeded = passwordResult.Succeeded,
                    Errors = passwordResult.Succeeded ? new List<string>() : passwordResult.Errors.Select(e => e.Description).ToList()
                };
            }

            return new OperationResult<string>
            {
                Succeeded = true,
                Errors = new List<string>()
            };
        }

        public async Task<OperationResult<string>> AddUserToRoleAsync(string userName, string role)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
                return new OperationResult<string>
                {
                    Succeeded = false,
                    Errors = new List<string> { "User not found" }
                };
            var result = await _userManager.AddToRoleAsync(user, role);
            return new OperationResult<string>
            {
                Succeeded = result.Succeeded,
                Errors = result.Succeeded ? new List<string>() : new List<string> { "Failed to add user to role" }
            };
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<OperationResult<string>> ChangePasswordAsync(User user, string password)
        {
            await  _userManager.RemovePasswordAsync(user);
            var result = await _userManager.AddPasswordAsync(user, password);
            return new OperationResult<string>
            {
                Succeeded = result.Succeeded,
                Errors = result.Succeeded ? new List<string>() : result.Errors.Select(e => e.Description).ToList()
            };
        }
    }
}