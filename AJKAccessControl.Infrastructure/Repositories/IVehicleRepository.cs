
using AJKAccessControl.Domain.Entities;

namespace AJKAccessControl.Infrastructure.Repositories
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetByIdAsync(int id);
        Task<IEnumerable<Vehicle>> GetAllAsync();
        Task<int> AddAsync(Vehicle vehicle);
        Task<int> UpdateAsync(Vehicle vehicle);
        Task DeleteAsync(int id);
    }
}