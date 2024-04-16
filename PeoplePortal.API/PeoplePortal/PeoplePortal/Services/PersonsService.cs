using PeoplePrtal.Data.Repository.Persons;
using PeoplePrtal.Models;

namespace PeoplePrtal.Services
{
    public interface IPersonsService
    {
        Task<IEnumerable<Person>> GetPersons();
        Task<Person> FindByIdNumber(string IdNumber);
        Task CreatePerson(Person person);
        Task UpdatePersonDetails(Person person);
        void DeletePerson(Person person);
    }
    public class PersonsService: IPersonsService
    {
        private readonly IPersonsRepository _personsRepository;
        public PersonsService(IPersonsRepository personsRepository) 
        {
            _personsRepository = personsRepository;
        }

        public async Task CreatePerson(Person person)
        {
            await _personsRepository.Create(person);
            await _personsRepository.Save();
        }

        public async void DeletePerson(Person person)
        {
            _personsRepository.Delete(person);
            await _personsRepository.Save();
        }

        public async Task<Person> FindByIdNumber(string IdNumber)
        => await _personsRepository.FindByIdNumberAsync(IdNumber);

        public async Task<IEnumerable<Person>> GetPersons()
        => await _personsRepository.GetAll();

        public async Task UpdatePersonDetails(Person person)
        {
            _personsRepository.Update(person);
            await _personsRepository.Save();
        }
    }
}
