using JokeCreator.Joke;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JokeCreatorTest.Joke
{
    public class JokeServiceTest
    {
        private ILogger<JokeService> _logger;
        private IJokeRepository<JokeCreator.Joke.Joke> _jokeRepository;
        private IJokeService _jokeService;

        [SetUp]
        public void Setup()
        {
            _jokeRepository = Substitute.For<IJokeRepository<JokeCreator.Joke.Joke>>();
            _logger = Substitute.For<ILogger<JokeService>>();
            _jokeService = new JokeService(_logger, _jokeRepository);
        }

        [Test]
        public async Task GetRandomJokes_Should_Have_8Jokes()
        {
            Option option = new Option()
            {
                Category = "",
                NoOfJokes = 8,
                ReplaceFirstNameWith = "",
                ReplaceLastNameWith = ""
            };
            _jokeRepository.GetRandomJoke().Returns(Task.FromResult(new JokeCreator.Joke.Joke()));

            var values = await _jokeService.GetRandomJokes(option);
            Assert.AreEqual(8, values.Count);
        }

        [Test]
        public async Task GetRandomJokes_Should_Call_GetRandomJoke_WithoutCategory_When_CategoryNotProvided()
        {
            Option option = new Option()
            {
                Category = "",
                NoOfJokes = 8,
                ReplaceFirstNameWith = "",
                ReplaceLastNameWith = ""
            };
            _jokeRepository.GetRandomJoke().Returns(Task.FromResult(new JokeCreator.Joke.Joke()));

            var values = await _jokeService.GetRandomJokes(option);
            await _jokeRepository.Received().GetRandomJoke();
        }

        [Test]
        public async Task GetRandomJokes_Should_Call_GetRandomJoke_WithCategory_When_CategoryProvided()
        {
            Option option = new Option()
            {
                Category = "food",
                NoOfJokes = 8,
                ReplaceFirstNameWith = "",
                ReplaceLastNameWith = ""
            };
            _jokeRepository.GetRandomJoke(Arg.Any<string>()).Returns(Task.FromResult(new JokeCreator.Joke.Joke()));

            var values = await _jokeService.GetRandomJokes(option);
            await _jokeRepository.Received().GetRandomJoke("food");
        }

        [Test]
        public async Task GetRandomJokes_Should_ReplaceChuck_With_Jason()
        {
            Option option = new Option()
            {
                Category = "",
                NoOfJokes = 1,
                ReplaceFirstNameWith = "Jason",
                ReplaceLastNameWith = "Llorin"
            };
            _jokeRepository.GetRandomJoke().Returns(Task.FromResult(new JokeCreator.Joke.Joke()
            {
                Value = "Chuck Norris is the best."
            }));

            var values = await _jokeService.GetRandomJokes(option);
            Assert.IsTrue(values[0].IndexOf("Jason") > -1);
        }
    }
}
