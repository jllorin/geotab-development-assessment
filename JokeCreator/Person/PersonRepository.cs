using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JokeCreator.Person
{
    public interface IPersonRepository
    {
        Task<Person> GetPerson();        
    }

    public class PersonRepository : IPersonRepository
    {
        private readonly ILogger<PersonRepository> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public PersonRepository(ILogger<PersonRepository> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        public async Task<Person> GetPerson()
        {
            string url = "https://www.names.privserv.com/api/";
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                HttpClient client = _clientFactory.CreateClient();
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var person = JObject.Parse(responseBody).ToObject<Person>();
                return person;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Http client call on url: {url} in person repository failed. ");
                throw ex;
            }
        }
    }
}
