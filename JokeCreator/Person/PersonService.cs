using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokeCreator.Person
{
    public interface IPersonService
    {
        Task<SubstitutePerson> GetRandomPerson();
    }

    public class PersonService : IPersonService
    {
        private readonly IPersonRepository<Person> _personRepository;

        public PersonService(IPersonRepository<Person> personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<SubstitutePerson> GetRandomPerson()
        {
            Person person = await _personRepository.GetPerson();
            var substitutePerson = new SubstitutePerson()
            {
                FirstName = person.Name,
                LastName = person.Surname
            };
            return substitutePerson;
        }
    }
}
