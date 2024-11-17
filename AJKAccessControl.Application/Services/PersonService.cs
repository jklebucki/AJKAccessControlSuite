using AJKAccessControl.Domain.Entities;
using AJKAccessControl.Infrastructure.Repositories;
using AJKAccessControl.Shared.DTOs;

namespace AJKAccessControl.Application.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<PersonDTO> GetByIdAsync(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            return person == null ? new PersonDTO() : MapToDTO(person);
        }

        public async Task<IEnumerable<PersonDTO>> GetAllAsync()
        {
            var persons = await _personRepository.GetAllAsync();
            return persons.Select(MapToDTO);
        }

        public async Task AddAsync(PersonDTO personDTO)
        {
            var person = MapToEntity(personDTO);
            await _personRepository.AddAsync(person);
        }

        public async Task UpdateAsync(PersonDTO personDTO)
        {
            var person = MapToEntity(personDTO);
            await _personRepository.UpdateAsync(person);
        }

        public async Task DeleteAsync(int id)
        {
            await _personRepository.DeleteAsync(id);
        }

        private PersonDTO MapToDTO(Person person)
        {
            return new PersonDTO
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                IsEmployee = person.IsEmployee,
                Company = person.Company
            };
        }

        private Person MapToEntity(PersonDTO personDTO)
        {
            return new Person
            {
                Id = personDTO.Id,
                FirstName = personDTO.FirstName,
                LastName = personDTO.LastName,
                IsEmployee = personDTO.IsEmployee,
                Company = personDTO.Company
            };
        }
    }
}
