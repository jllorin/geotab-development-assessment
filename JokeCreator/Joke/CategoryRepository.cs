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
    public interface ICategoryRepository<T>
    {
        Task<T> GetCategories();
    }

    public class CategoryRepository : HttpClientBaseRepository<List<string>, CategoryRepository>, ICategoryRepository<List<string>>
    {
        public CategoryRepository(ILogger<CategoryRepository> logger, IHttpClientFactory clientFactory) : base(logger, clientFactory)
        {
        }


        public async Task<List<string>> GetCategories()
        {
            string url = "https://api.chucknorris.io/jokes/categories";
            var categories = await GetList(url);
            return categories;
        }
    }
}
