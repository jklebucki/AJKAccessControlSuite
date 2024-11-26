using AJKAccessControl.Domain.Responses;
using AJKAccessControl.Shared.DTOs;

namespace AJKAccessGuard.Services
{
    public interface IPersonsApiService
    {
        Task<OperationResult<PersonDto>> GetPersonAsync(int id, string token);
        Task<OperationResult<IEnumerable<PersonDto>>> GetAllPersonsAsync(string token);
        Task<OperationResult<string>> AddPersonAsync(PersonDto personDTO, string token);
        Task<OperationResult<string>> UpdatePersonAsync(int id, PersonDto personDTO, string token);
        Task<OperationResult<string>> DeletePersonAsync(int id, string token);
    }
}
