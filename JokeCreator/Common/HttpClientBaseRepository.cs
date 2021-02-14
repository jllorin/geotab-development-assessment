using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace JokeCreator.Common
{
    public abstract class HttpClientBaseRepository<T, U>
    {
        private readonly ILogger<U> _logger;
        private readonly IHttpClientFactory _clientFactory;

        public HttpClientBaseRepository(ILogger<U> logger, IHttpClientFactory clientFactory)
        {
            _logger = logger;
            _clientFactory = clientFactory;
        }

        public async Task<T> GetList(string url)
        {            
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                HttpClient client = _clientFactory.CreateClient();
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var responseArray = JArray.Parse(responseBody).ToObject<T>();
                return responseArray;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Http client call on url: {url} in person repository failed. ");
                throw ex;
            }
        }

        public async Task<T> GetObject(string url)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                HttpClient client = _clientFactory.CreateClient();
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                var responseObject = JObject.Parse(responseBody).ToObject<T>();
                return responseObject;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Http client call on url: {url} in joke repository failed. ");
                throw ex;
            }
        }

    }
}
