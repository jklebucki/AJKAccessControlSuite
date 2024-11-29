
using AJKAccessControl.Domain.Entities;

namespace AJKAccessControl.Infrastructure.Repositories
{
    public interface IChangeLogRepository
    {
        Task<ChangeLog> GetByIdAsync(int id);
        Task<IEnumerable<ChangeLog>> GetAllAsync();
        Task<int> AddAsync(ChangeLog changeLog);
        Task<int> UpdateAsync(ChangeLog changeLog);
        Task DeleteAsync(int id);
    }
}