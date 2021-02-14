using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokeCreator.Joke
{
    [Produces("application/json")]
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

        [HttpPost("generate")]
        public async Task<ActionResult<List<string>>> GetRandomJokes([FromBody] Option option)
        {
            try
            {
                if (option == null || !ModelState.IsValid)
                {
                    return BadRequest(option);
                }

                List<string> jokeList = await _jokeService.GetRandomJokes(option);
                return Ok(jokeList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.StackTrace);
                return StatusCode(500);
            }
        }
    }
}
