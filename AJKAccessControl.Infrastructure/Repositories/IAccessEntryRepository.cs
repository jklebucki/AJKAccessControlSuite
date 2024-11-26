
using AJKAccessControl.Domain.Entities;

namespace AJKAccessControl.Infrastructure.Repositories
{
    public interface IAccessEntryRepository
    {
        Task<AccessEntry> GetByIdAsync(int id);
        Task<IEnumerable<AccessEntry>> GetAllAsync();
        Task<int> AddAsync(AccessEntry accessEntry);
        Task<int> UpdateAsync(AccessEntry accessEntry);
        Task DeleteAsync(int id);
    }
}