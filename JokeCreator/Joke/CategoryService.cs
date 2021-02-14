using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JokeCreator.Joke
{
    public interface ICategoryService
    {
        Task<List<string>> GetCategories();
    }

    public class CategoryService : ICategoryService
    {
        private readonly ILogger<CategoryService> _logger;
        private readonly ICategoryRepository<List<string>> _categoryRepository;

        public CategoryService(ILogger<CategoryService> logger, ICategoryRepository<List<string>> categoryRepository)
        {
            _logger = logger;
            _categoryRepository = categoryRepository;
        }

        public async Task<List<string>> GetCategories()
        {
            var categories = await _categoryRepository.GetCategories();
            return categories;
        }
    }
}
