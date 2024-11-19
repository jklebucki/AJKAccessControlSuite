using AJKAccessControl.Domain.Entities;
using AJKAccessControl.Domain.Responses;
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

        public async Task<OperationResult<PersonDTO>> GetByIdAsync(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null)
            {
                return new OperationResult<PersonDTO> { Succeeded = false, Errors = new List<string> { "Person not found" } };
            }
            return new OperationResult<PersonDTO> { Succeeded = true, Data = MapToDTO(person) };
        }

        public async Task<OperationResult<IEnumerable<PersonDTO>>> GetAllAsync()
        {
            var persons = await _personRepository.GetAllAsync();
            return new OperationResult<IEnumerable<PersonDTO>> { Succeeded = true, Data = persons.Select(MapToDTO) };
        }

        public async Task<OperationResult<string>> AddAsync(PersonDTO personDTO)
        {
            try
            {
                var person = MapToEntity(personDTO);
                await _personRepository.AddAsync(person);
            }
            catch (Exception ex)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new List<string> { ex.Message } };
            }

            return new OperationResult<string> { Succeeded = true, Data = "Person added successfully" };
        }

        public async Task<OperationResult<string>> UpdateAsync(PersonDTO personDTO)
        {
            var person = await _personRepository.GetByIdAsync(personDTO.Id);
            if (person == null)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new List<string> { "Person not found" } };
            }

            try
            {
                person.FirstName = personDTO.FirstName;
                person.LastName = personDTO.LastName;
                person.IsEmployee = personDTO.IsEmployee;
                person.Company = personDTO.Company;
                person.CreatedBy = personDTO.CreatedBy;
                person.CreatedAt = personDTO.CreatedAt;
                person.UpdatedAt = personDTO.UpdatedAt;
                await _personRepository.UpdateAsync(person);
            }
            catch (Exception ex)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new List<string> { ex.Message } };
            }

            return new OperationResult<string> { Succeeded = true, Data = "Person updated successfully" };
        }

        public async Task<OperationResult<string>> DeleteAsync(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new List<string> { "Person not found" } };
            }

            await _personRepository.DeleteAsync(id);
            return new OperationResult<string> { Succeeded = true, Data = "Person deleted successfully" };
        }

        private PersonDTO MapToDTO(Person person)
        {
            return new PersonDTO
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                IsEmployee = person.IsEmployee,
                Company = person.Company,
                CreatedBy = person.CreatedBy,
                CreatedAt = person.CreatedAt,
                UpdatedAt = person.UpdatedAt
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
                Company = personDTO.Company,
                CreatedBy = personDTO.CreatedBy,
                CreatedAt = personDTO.CreatedAt,
                UpdatedAt = personDTO.UpdatedAt
            };
        }
    }
}
