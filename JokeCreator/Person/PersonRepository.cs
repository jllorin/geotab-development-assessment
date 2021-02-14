using JokeCreator.Common;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JokeCreator.Person
{
    public interface IPersonRepository<T>
    {
        Task<T> GetPerson();        
    }

    public class PersonRepository : HttpClientBaseRepository<Person, PersonRepository>, IPersonRepository<Person>
    {
        public PersonRepository(ILogger<PersonRepository> logger, IHttpClientFactory clientFactory) : base(logger, clientFactory)
        {
        }

        public async Task<Person> GetPerson()
        {
            string url = "https://www.names.privserv.com/api/";
            var person = await GetObject(url);
            return person;
        }
    }
}
