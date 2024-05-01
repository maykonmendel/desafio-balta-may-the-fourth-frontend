using System.Net.Http.Json;
using System.Text.Json;
using MayTheFourth.Models;

namespace MayTheFourth.Services;

public class MovieService(IHttpClientFactory httpClientFactory)
{
    public async Task<PagedResult<Movie>?> GetAllAsync(int currentPage, int pageSize)
    {
        try
        {
            var client = httpClientFactory.CreateClient(Configuration.HttpClientName);
            var response = await client.GetAsync($"api/films?page={currentPage}&take={pageSize}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var movies = JsonSerializer.Deserialize<PagedResult<Movie>>(json, options);
                if (movies is null) return movies ?? new PagedResult<Movie>();
                movies.CurrentPage = currentPage;
                movies.PageSize = pageSize;

                return movies;
            }
            else
            {
                return new PagedResult<Movie> { Result = new List<Movie>(), Total = 0, CurrentPage = 0, PageSize = pageSize };
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Erro ao recuperar filmes: {ex.Message}");
            return new PagedResult<Movie> { Result = new List<Movie>(), Total = 0, CurrentPage = 0, PageSize = pageSize };
        }
    }

    public async Task<Movie> GetByIdAsync(int id)
    {
        var client = httpClientFactory.CreateClient(Configuration.HttpClientName);
        return await client.GetFromJsonAsync<Movie>($"api/films/{id}") ?? new();
    }
}