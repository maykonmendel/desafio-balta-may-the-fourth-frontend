using System.Net.Http.Json;
using System.Text.Json;
using MayTheFourth.Models;

namespace MayTheFourth.Services;

public class VehicleService(IHttpClientFactory httpClientFactory)
{
    public async Task<PagedResult<Vehicle>?> GetAllAsync(int currentPage, int pageSize)
    {
        try
        {
            var client = httpClientFactory.CreateClient(Configuration.HttpClientName);
            var response = await client.GetAsync($"api/Vehicles?page={currentPage}&take={pageSize}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                var vehicles = JsonSerializer.Deserialize<PagedResult<Vehicle>>(json, options);
                if (vehicles is null) return vehicles ?? new PagedResult<Vehicle>();
                vehicles.CurrentPage = currentPage;
                vehicles.PageSize = pageSize;
                
                return vehicles;
            }
            else
            {
                return new PagedResult<Vehicle> { Result = new List<Vehicle>(), Total = 0, CurrentPage = 0, PageSize = pageSize };
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Erro ao recuperar personagens: {ex.Message}");
            return new PagedResult<Vehicle> { Result = new List<Vehicle>(), Total = 0, CurrentPage = 0, PageSize = pageSize };
        }
    }

    public async Task<Vehicle> GetByIdAsync(int id)
    {
        var client = httpClientFactory.CreateClient(Configuration.HttpClientName);
        return await client.GetFromJsonAsync<Vehicle>($"api/Vehicles/{id}") ?? new();
    }
}