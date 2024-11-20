using AJKAccessControl.Domain.Entities;

namespace AJKAccessControl.Infrastructure.Repositories
{
    public interface IPersonRepository
    {
        Task<Person> GetByIdAsync(int id);
        Task<IEnumerable<Person>> GetAllAsync();
        Task<int> AddAsync(Person person);
        Task<int> UpdateAsync(Person person);
        Task DeleteAsync(int id);
    }
}
