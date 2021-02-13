using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JokeCreator.Joke
{
    public interface ICategoryRepository
    {
        Task<List<string>> GetCategories();
    }

    public class CategoryRepository : ICategoryRepository
    {
        private readonly ILogger<CategoryRepository> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public CategoryRepository(ILogger<CategoryRepository> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }


        public async Task<List<string>> GetCategories()
        {
            string url = "https://api.chucknorris.io/jokes/categories";
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                HttpClient client = _clientFactory.CreateClient();
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var categories = JArray.Parse(responseBody).ToObject<List<string>>();
                return categories;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Http client call on url: {url} in person repository failed. ");
                throw ex;
            }
        }
    }
}
