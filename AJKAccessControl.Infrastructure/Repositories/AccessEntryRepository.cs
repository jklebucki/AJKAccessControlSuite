
using AJKAccessControl.Domain.Entities;
using AJKAccessControl.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AJKAccessControl.Infrastructure.Repositories
{
    public class AccessEntryRepository : IAccessEntryRepository
    {
        private readonly AccessControlDbContext _context;

        public AccessEntryRepository(AccessControlDbContext context)
        {
            _context = context;
        }

        public async Task<AccessEntry> GetByIdAsync(int id)
        {
            return await _context.AccessEntries.FindAsync(id) ?? throw new KeyNotFoundException($"AccessEntry with id {id} not found.");
        }

        public async Task<IEnumerable<AccessEntry>> GetAllAsync()
        {
            return await _context.AccessEntries.ToListAsync();
        }

        public async Task<int> AddAsync(AccessEntry accessEntry)
        {
            accessEntry.CreatedAt = DateTime.UtcNow;
            await _context.AccessEntries.AddAsync(accessEntry);
            await _context.SaveChangesAsync();
            return accessEntry.Id;
        }

        public async Task<int> UpdateAsync(AccessEntry accessEntry)
        {
            accessEntry.UpdatedAt = DateTime.UtcNow;
            _context.AccessEntries.Update(accessEntry);
            await _context.SaveChangesAsync();
            return accessEntry.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var accessEntry = await _context.AccessEntries.FindAsync(id);
            if (accessEntry != null)
            {
                _context.AccessEntries.Remove(accessEntry);
                await _context.SaveChangesAsync();
            }
        }
    }
}