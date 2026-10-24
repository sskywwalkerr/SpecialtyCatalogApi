using SpecialtyCatalog.Client.Models;

namespace SpecialtyCatalog.Client.Services;

// Отправляет HTTP-запросы к /api/directions
public class DirectionApiClient
{
    private readonly HttpClient _http;

    public DirectionApiClient(HttpClient http)
    {
        _http = http;
    }

    // GET /api/directions?search=...
    public async Task<List<DirectionDto>> GetAllAsync(string? search = null)
    {
        var url = $"api/directions?search={Uri.EscapeDataString(search ?? "")}";
        return await _http.GetFromJsonAsync<List<DirectionDto>>(url) ?? new();
    }

    // GET /api/directions/{id}, если записи нет, вернёт null
    public async Task<DirectionDto?> GetByIdAsync(int id)
    {
        var response = await _http.GetAsync($"api/directions/{id}");
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<DirectionDto>()
            : null;
    }

    // POST /api/directions
    public async Task<bool> CreateAsync(DirectionDto direction)
    {
        var response = await _http.PostAsJsonAsync("api/directions", direction);
        return response.IsSuccessStatusCode;
    }

    // PUT /api/directions/{id}
    public async Task<bool> UpdateAsync(DirectionDto direction)
    {
        var response = await _http.PutAsJsonAsync($"api/directions/{direction.Id}", direction);
        return response.IsSuccessStatusCode;
    }

    // DELETE /api/directions/{id}
    public async Task<bool> DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/directions/{id}");
        return response.IsSuccessStatusCode;
    }
}
