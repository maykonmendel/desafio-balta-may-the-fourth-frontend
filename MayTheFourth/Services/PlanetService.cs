using System.Net.Http.Json;
using System.Text.Json;
using MayTheFourth.Models;

namespace MayTheFourth.Services;

public class PlanetService(IHttpClientFactory httpClientFactory)
{
    public async Task<PagedResult<Planet>?> GetAllAsync(int currentPage, int pageSize)
    {
        try
        {
            var client = httpClientFactory.CreateClient(Configuration.HttpClientName);
            var response = await client.GetAsync($"api/planets?page={currentPage}&take={pageSize}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var planets = JsonSerializer.Deserialize<PagedResult<Planet>>(json, options);
                if (planets is null) return planets ?? new PagedResult<Planet>();
                planets.CurrentPage = currentPage;
                planets.PageSize = pageSize;
                
                return planets;
            }
            else
            {
                return new PagedResult<Planet> { Result = new List<Planet>(), Total = 0, CurrentPage = 0, PageSize = pageSize };
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Erro ao recuperar personagens: {ex.Message}");
            return new PagedResult<Planet> { Result = new List<Planet>(), Total = 0, CurrentPage = 0, PageSize = pageSize };
        }
    }

    public async Task<Planet> GetByIdAsync(int id)
    {
        var client = httpClientFactory.CreateClient(Configuration.HttpClientName);
        return await client.GetFromJsonAsync<Planet>($"api/planets/{id}") ?? new();
    }
}