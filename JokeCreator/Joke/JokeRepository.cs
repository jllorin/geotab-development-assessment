using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
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

        private async Task<Joke> CallClient(string url)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                HttpClient client = _clientFactory.CreateClient();
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var joke = JObject.Parse(responseBody).ToObject<Joke>();
                return joke;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Http client call on url: {url} in joke repository failed. ");
                throw ex;
            }
        }

        public async Task<Joke> GetRandomJoke()
        {
            string url = "https://api.chucknorris.io/jokes/random";
            return await CallClient(url);
        }

        public async Task<Joke> GetRandomJoke(string category)
        {            
            // need to html encode
            string url = $"https://api.chucknorris.io/jokes/random?category={category}";
            return await CallClient(url);
        }
    }
}
