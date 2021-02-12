using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JokeCreator.Joke
{
    public interface IJokeRepository
    {
        Task<Joke> GetRandomJoke();
        Task<Joke> GetRandomJoke(string category);
    }

    public class JokeRepository : IJokeRepository
    {
        private readonly ILogger<JokeRepository> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public JokeRepository(ILogger<JokeRepository> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }


        public async Task<Joke> GetRandomJoke()
        {
            string url = "https://api.chucknorris.io/jokes/random";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            HttpClient client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return new Joke();
            }
            return new Joke();
        }

        public async Task<Joke> GetRandomJoke(string category)
        {            
            // need to html encode
            string url = $"https://api.chucknorris.io/jokes/random?category={category}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            HttpClient client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return new Joke();
            }
            return new Joke();
        }
    }
}
