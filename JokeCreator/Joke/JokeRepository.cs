using JokeCreator.Common;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JokeCreator.Joke
{
    public interface IJokeRepository<T>
    {
        Task<T> GetRandomJoke();
        Task<T> GetRandomJoke(string category);
    }

    public class JokeRepository : HttpClientBaseRepository<Joke, JokeRepository>, IJokeRepository<Joke>
    {

        public JokeRepository(ILogger<JokeRepository> logger, IHttpClientFactory clientFactory) : base(logger, clientFactory)
        {
        }

        public async Task<Joke> GetRandomJoke()
        {
            string url = "https://api.chucknorris.io/jokes/random";
            var joke = await GetObject(url);
            return joke;
        }

        public async Task<Joke> GetRandomJoke(string category)
        {            
            // need to html encode
            string url = $"https://api.chucknorris.io/jokes/random?category={category}";
            var joke = await GetObject(url);
            return joke;
        }
    }
}
