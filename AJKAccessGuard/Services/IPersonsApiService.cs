using AJKAccessControl.Domain.Responses;
using AJKAccessControl.Shared.DTOs;

namespace AJKAccessGuard.Services
{
    public interface IPersonsApiService
    {
        Task<OperationResult<PersonDTO>> GetPersonAsync(int id, string token);
        Task<OperationResult<IEnumerable<PersonDTO>>> GetAllPersonsAsync(string token);
        Task<OperationResult<string>> AddPersonAsync(PersonDTO personDTO, string token);
        Task<OperationResult<string>> UpdatePersonAsync(int id, PersonDTO personDTO, string token);
        Task<OperationResult<string>> DeletePersonAsync(int id, string token);
    }
}
