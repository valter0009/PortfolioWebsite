using PortfolioWebsite.Client.Services.Contracts;
using PortfolioWebsite.Models.DTOs;
using System.Net.Http.Json;

namespace PortfolioWebsite.Client.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClient;

        public ProductService(IHttpClientFactory httpClient)
        {
            this._httpClient = httpClient;
        }
        private HttpClient GetClient(bool requiresAuth = false)
        {
            return _httpClient.CreateClient(requiresAuth ? "AuthorizedClient" : "AnonymousClient");
        }
        public async Task<ProductDto> GetItem(int id)
        {
            var httpClient = GetClient();
            try
            {
                var response = await httpClient.GetAsync($"api/Product/{id}");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default;
                    }

                    return await response.Content.ReadFromJsonAsync<ProductDto>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ProductDto>> GetItems()
        {
            var httpClient = GetClient();
            try
            {
                var response = await httpClient.GetAsync("api/Product");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<ProductDto>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<ProductDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ProductDto> DeleteItem(int id)
        {
            var httpClient = GetClient(true);
            try
            {
                var response = await httpClient.DeleteAsync($"api/Product/{id}");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return default;
                    }

                    return await response.Content.ReadFromJsonAsync<ProductDto>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
