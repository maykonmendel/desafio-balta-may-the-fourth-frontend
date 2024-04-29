using System.Collections;
using System.Net.Http.Json;
using MayTheFourth.Models;

namespace MayTheFourth.Services;

public class MovieService(IHttpClientFactory httpClientFactory)
{
    public async Task<IEnumerable<Movie>> GetMoviesAsync()
    {
        var client = httpClientFactory.CreateClient(Configuration.HttpClientName);
        return await client.GetFromJsonAsync<IEnumerable<Movie>>("api/filmes") ?? [];
    }
}