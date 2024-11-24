
using AJKAccessControl.Domain.Entities;
using AJKAccessControl.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AJKAccessControl.Infrastructure.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly AccessControlDbContext _context;

        public VehicleRepository(AccessControlDbContext context)
        {
            _context = context;
        }

        public async Task<Vehicle> GetByIdAsync(int id)
        {
            return await _context.Vehicles.FindAsync(id) ?? throw new KeyNotFoundException($"Vehicle with id {id} not found.");
        }

        public async Task<IEnumerable<Vehicle>> GetAllAsync()
        {
            return await _context.Vehicles.ToListAsync();
        }

        public async Task<int> AddAsync(Vehicle vehicle)
        {
            vehicle.CreatedAt = DateTime.UtcNow;
            await _context.Vehicles.AddAsync(vehicle);
            await _context.SaveChangesAsync();
            return vehicle.Id;
        }

        public async Task<int> UpdateAsync(Vehicle vehicle)
        {
            vehicle.UpdatedAt = DateTime.UtcNow;
            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();
            return vehicle.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var vehicle = await _context.Vehicles.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
                await _context.SaveChangesAsync();
            }
        }
    }
}