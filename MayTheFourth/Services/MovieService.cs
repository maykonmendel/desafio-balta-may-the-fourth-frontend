using System.Collections;
using System.Net.Http.Json;
using MayTheFourth.Models;

namespace MayTheFourth.Services;

public class MovieService(IHttpClientFactory httpClientFactory)
{
    public async Task<IEnumerable<Movie>> GetMoviesAsync(int page, int take)
    {
        var client = httpClientFactory.CreateClient(Configuration.HttpClientName);
        return await client.GetFromJsonAsync<IEnumerable<Movie>>("api/films?page=" + page + "&take=" + take) ?? [];
    }
}