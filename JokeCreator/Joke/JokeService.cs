using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokeCreator.Joke
{
    public interface IJokeService
    {
        Task<List<string>> GetRandomJokes(Option option);
    }

    public class JokeService : IJokeService
    {
        private readonly ILogger<JokeService> _logger;
        private readonly IJokeRepository _jokeRepository;

        public JokeService(ILogger<JokeService> logger, IJokeRepository jokeRepository)
        {
            _logger = logger;
            _jokeRepository = jokeRepository;
        }

        public async Task<List<string>> GetRandomJokes(Option option)
        {
            List<string> jokes = new List<string>();
            for (int i = 0; i < option.NoOfJokes; i++)
            {
                var randomJoke = new Joke();
                if (string.IsNullOrEmpty(option.Category))
                {
                    randomJoke = await _jokeRepository.GetRandomJoke();                    
                }
                else
                {
                    randomJoke = await _jokeRepository.GetRandomJoke(option.Category);                    
                }
                
                if (!string.IsNullOrEmpty(option.ReplaceFirstNameWith) && !string.IsNullOrEmpty(option.ReplaceLastNameWith))
                {
                    // This will all replace chuck with new firstname, better than old code
                    randomJoke.Value = randomJoke.Value.Replace("Chuck", option.ReplaceFirstNameWith, StringComparison.InvariantCultureIgnoreCase);
                    randomJoke.Value = randomJoke.Value.Replace("Norris", option.ReplaceLastNameWith, StringComparison.InvariantCultureIgnoreCase);
                }
                jokes.Add(randomJoke.Value);
            }
            return jokes;
        }
    }
}
