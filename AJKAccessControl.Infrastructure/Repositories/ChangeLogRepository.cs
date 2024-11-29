
using AJKAccessControl.Domain.Entities;
using AJKAccessControl.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AJKAccessControl.Infrastructure.Repositories
{
    public class ChangeLogRepository : IChangeLogRepository
    {
        private readonly AccessControlDbContext _context;

        public ChangeLogRepository(AccessControlDbContext context)
        {
            _context = context;
        }

        public async Task<ChangeLog> GetByIdAsync(int id)
        {
            return await _context.ChangeLogs.FindAsync(id) ?? throw new KeyNotFoundException($"ChangeLog with id {id} not found.");
        }

        public async Task<IEnumerable<ChangeLog>> GetAllAsync()
        {
            return await _context.ChangeLogs.ToListAsync();
        }

        public async Task<int> AddAsync(ChangeLog changeLog)
        {
            changeLog.ChangedAt = DateTime.UtcNow;
            await _context.ChangeLogs.AddAsync(changeLog);
            await _context.SaveChangesAsync();
            return changeLog.Id;
        }

        public async Task<int> UpdateAsync(ChangeLog changeLog)
        {
            changeLog.ChangedAt = DateTime.UtcNow;
            _context.ChangeLogs.Update(changeLog);
            await _context.SaveChangesAsync();
            return changeLog.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var changeLog = await _context.ChangeLogs.FindAsync(id);
            if (changeLog != null)
            {
                _context.ChangeLogs.Remove(changeLog);
                await _context.SaveChangesAsync();
            }
        }
    }
}