using System.Net.Http.Json;
using System.Text.Json;
using MayTheFourth.Models;

namespace MayTheFourth.Services;

public class CharacterService(IHttpClientFactory httpClientFactory)
{
    public async Task<PagedResult<Character>?> GetAllAsync(int currentPage, int pageSize)
    {
        try
        {
            var client = httpClientFactory.CreateClient(Configuration.HttpClientName);
            var response = await client.GetAsync($"api/people?page={currentPage}&take={pageSize}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var characters = JsonSerializer.Deserialize<PagedResult<Character>>(json, options);
                characters.CurrentPage = currentPage;
                characters.PageSize = pageSize;
                
                return characters;
            }
            else
            {
                return new PagedResult<Character> { Result = new List<Character>(), Total = 0, CurrentPage = 0, PageSize = pageSize };
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Erro ao recuperar personagens: {ex.Message}");
            return new PagedResult<Character> { Result = new List<Character>(), Total = 0, CurrentPage = 0, PageSize = pageSize };
        }
    }

    public async Task<Character> GetByIdAsync(int id)
    {
        var client = httpClientFactory.CreateClient(Configuration.HttpClientName);
        return await client.GetFromJsonAsync<Character>($"api/people/{id}") ?? new();
    }
}