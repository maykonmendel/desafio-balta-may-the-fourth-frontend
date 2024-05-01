using System.Net.Http.Json;
using System.Text.Json;
using MayTheFourth.Models;

namespace MayTheFourth.Services;

public class StarshipService(IHttpClientFactory httpClientFactory)
{
    public async Task<PagedResult<Starship>?> GetAllAsync(int currentPage, int pageSize)
    {
        try
        {
            var client = httpClientFactory.CreateClient(Configuration.HttpClientName);
            var response = await client.GetAsync($"api/starships?page={currentPage}&take={pageSize}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var starships = JsonSerializer.Deserialize<PagedResult<Starship>>(json, options);
                if (starships is null) return starships ?? new PagedResult<Starship>();
                starships.CurrentPage = currentPage;
                starships.PageSize = pageSize;
                
                return starships;
            }
            else
            {
                return new PagedResult<Starship> { Result = new List<Starship>(), Total = 0, CurrentPage = 0, PageSize = pageSize };
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Erro ao recuperar personagens: {ex.Message}");
            return new PagedResult<Starship> { Result = new List<Starship>(), Total = 0, CurrentPage = 0, PageSize = pageSize };
        }
    }

    public async Task<Starship> GetByIdAsync(int id)
    {
        var client = httpClientFactory.CreateClient(Configuration.HttpClientName);
        return await client.GetFromJsonAsync<Starship>($"api/starships/{id}") ?? new();
    }
}