using AJKAccessControl.Domain.Entities;

namespace AJKAccessControl.Application.Services
{
    public interface IPersonService
    {
        Task<Person> GetByIdAsync(int id);
        Task<IEnumerable<Person>> GetAllAsync();
        Task AddAsync(Person person);
        Task UpdateAsync(Person person);
        Task DeleteAsync(int id);
    }
}
