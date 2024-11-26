using AJKAccessControl.Domain.Responses;
using AJKAccessControl.Shared.DTOs;

namespace AJKAccessControl.Application.Services
{
    public interface IPersonService
    {
        Task<OperationResult<PersonDto>> GetByIdAsync(int id);
        Task<OperationResult<IEnumerable<PersonDto>>> GetAllAsync();
        Task<OperationResult<string>> AddAsync(PersonDto personDTO);
        Task<OperationResult<string>> UpdateAsync(PersonDto personDTO);
        Task<OperationResult<string>> DeleteAsync(int id);
    }
}
