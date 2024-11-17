using AJKAccessControl.Domain.Entities;
using AJKAccessControl.Infrastructure.Data;
using AJKAccessControl.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AJKAccessControl.Domain.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly AccessControlDbContext _context;

        public PersonRepository(AccessControlDbContext context)
        {
            _context = context;
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            return await _context.Persons.FindAsync(id) ?? throw new KeyNotFoundException($"Person with id {id} not found.");
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _context.Persons.ToListAsync();
        }

        public async Task AddAsync(Person person)
        {
            await _context.Persons.AddAsync(person);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Person person)
        {
            _context.Persons.Update(person);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person != null)
            {
                _context.Persons.Remove(person);
                await _context.SaveChangesAsync();
            }
        }
    }
}
