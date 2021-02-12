using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokeCreator.Joke
{
    [ApiController]
    [Route("[controller]")]

    public class JokeController : ControllerBase
    {
        private readonly ILogger<JokeController> _logger;
        private readonly IJokeService _jokeService;
        
        public JokeController(ILogger<JokeController> logger, IJokeService jokeService)
        {
            _logger = logger;
            _jokeService = jokeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<string>>> Get(Option option)
        {
            if (ModelState.IsValid)
            {
                return BadRequest();
            }

            List<string> jokeList = await _jokeService.GetRandomJokes(option);
            return Ok(jokeList);
        }
    }
}
