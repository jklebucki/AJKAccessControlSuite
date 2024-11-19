using AJKAccessControl.Domain.Responses;
using AJKAccessControl.Shared.DTOs;

public interface IPersonService
{
    Task<OperationResult<PersonDTO>> GetByIdAsync(int id);
    Task<OperationResult<IEnumerable<PersonDTO>>> GetAllAsync();
    Task<OperationResult<string>> AddAsync(PersonDTO personDTO);
    Task<OperationResult<string>> UpdateAsync(PersonDTO personDTO);
    Task<OperationResult<string>> DeleteAsync(int id);
}
