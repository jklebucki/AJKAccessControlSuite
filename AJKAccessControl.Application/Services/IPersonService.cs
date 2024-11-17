using AJKAccessControl.Shared.DTOs;

namespace AJKAccessControl.Application.Services
{
    public interface IPersonService
    {
        Task<PersonDTO> GetByIdAsync(int id);
        Task<IEnumerable<PersonDTO>> GetAllAsync();
        Task AddAsync(PersonDTO personDTO);
        Task UpdateAsync(PersonDTO personDTO);
        Task DeleteAsync(int id);
    }
}
