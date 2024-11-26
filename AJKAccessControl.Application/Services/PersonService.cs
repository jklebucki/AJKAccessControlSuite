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

        public async Task<OperationResult<PersonDto>> GetByIdAsync(int id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null)
            {
                return new OperationResult<PersonDto> { Succeeded = false, Errors = new List<string> { "Person not found" } };
            }
            return new OperationResult<PersonDto> { Succeeded = true, Data = MapToDTO(person) };
        }

        public async Task<OperationResult<IEnumerable<PersonDto>>> GetAllAsync()
        {
            var persons = await _personRepository.GetAllAsync();
            return new OperationResult<IEnumerable<PersonDto>> { Succeeded = true, Data = persons.Select(MapToDTO) };
        }

        public async Task<OperationResult<string>> AddAsync(PersonDto personDto)
        {
            try
            {
                var person = MapToEntity(personDto);
                var personId = await _personRepository.AddAsync(person);
                return new OperationResult<string> { Succeeded = true, Data = $"{personId}" };
            }
            catch (Exception ex)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new List<string> { ex.Message } };
            }
        }

        public async Task<OperationResult<string>> UpdateAsync(PersonDto personDto)
        {
            var person = await _personRepository.GetByIdAsync(personDto.Id);
            if (person == null)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new List<string> { "Person not found" } };
            }

            try
            {
                person.FirstName = personDto.FirstName;
                person.LastName = personDto.LastName;
                person.IsEmployee = personDto.IsEmployee;
                person.Company = personDto.Company;
                person.CreatedBy = personDto.CreatedBy;
                person.CreatedAt = personDto.CreatedAt;
                person.UpdatedAt = personDto.UpdatedAt;
                var personId = await _personRepository.UpdateAsync(person);
                return new OperationResult<string> { Succeeded = true, Data = $"{personId}" };
            }
            catch (Exception ex)
            {
                return new OperationResult<string> { Succeeded = false, Errors = new List<string> { ex.Message } };
            }

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

        private PersonDto MapToDTO(Person person)
        {
            return new PersonDto
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

        private Person MapToEntity(PersonDto personDto)
        {
            return new Person
            {
                Id = personDto.Id,
                FirstName = personDto.FirstName,
                LastName = personDto.LastName,
                IsEmployee = personDto.IsEmployee,
                Company = personDto.Company,
                CreatedBy = personDto.CreatedBy,
                CreatedAt = personDto.CreatedAt,
                UpdatedAt = personDto.UpdatedAt
            };
        }
    }
}
