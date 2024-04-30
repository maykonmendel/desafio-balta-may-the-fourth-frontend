using System.Collections;
using System.Net.Http.Json;
using MayTheFourth.Models;

namespace MayTheFourth.Services;

public class CharacterService(IHttpClientFactory httpClientFactory)
{
    public async Task<IEnumerable<Character>> GetCharactersAsync(int page, int take)
    {
        var client = httpClientFactory.CreateClient(Configuration.HttpClientName);
        return await client.GetFromJsonAsync<IEnumerable<Character>>("api/people?page=" + page + "&take=" + take) ?? [];
    }
}