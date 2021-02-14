using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JokeCreator.Joke;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;

namespace JokeCreatorTest.Joke
{
    public class JokeControllerTest
    {
        private ILogger<JokeController> _logger;
        private IJokeService _jokeService;
        private JokeController _jokeController;

        [SetUp]
        public void Setup()
        {
            _jokeService = Substitute.For<IJokeService>();
            _logger = Substitute.For<ILogger<JokeController>>();
            _jokeController = new JokeController(_logger, _jokeService);
        }

        [Test]
        public async Task GetRandomJokes_Should_Return_BadRequest_When_ModelIsInvalid()
        {
            var response = await _jokeController.GetRandomJokes(null);            
            Assert.IsInstanceOf<BadRequestObjectResult>(response.Result);
        }

        [Test]
        public async Task GetRandomJokes_Should_Return_Ok()
        {
            Option option = new Option()
            {
                Category = "",
                NoOfJokes = 12,
                ReplaceFirstNameWith = "",
                ReplaceLastNameWith = ""
            };

            _jokeService.GetRandomJokes(Arg.Any<Option>()).Returns(Task.FromResult(new List<string>()));
            var response = await _jokeController.GetRandomJokes(option);

            await _jokeService.Received().GetRandomJokes(Arg.Any<Option>());
            Assert.IsInstanceOf<OkObjectResult>(response.Result);
        }
    }
}
