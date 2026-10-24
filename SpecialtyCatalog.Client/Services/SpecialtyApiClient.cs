using SpecialtyCatalog.Client.Models;

namespace SpecialtyCatalog.Client.Services;

// Отправляет HTTP-запросы к /api/specialties
public class SpecialtyApiClient
{
    private readonly HttpClient _http;

    public SpecialtyApiClient(HttpClient http)
    {
        _http = http;
    }

    // GET /api/specialties?directionId=...&search=...
    public async Task<List<SpecialtyDto>> GetAllAsync(int? directionId = null, string? search = null)
    {
        var url = $"api/specialties?directionId={directionId}&search={Uri.EscapeDataString(search ?? "")}";
        return await _http.GetFromJsonAsync<List<SpecialtyDto>>(url) ?? new();
    }

    // GET /api/specialties/{id}
    public async Task<SpecialtyDto?> GetByIdAsync(int id)
    {
        var response = await _http.GetAsync($"api/specialties/{id}");
        return response.IsSuccessStatusCode
            ? await response.Content.ReadFromJsonAsync<SpecialtyDto>()
            : null;
    }

    // POST /api/specialties
    public async Task<bool> CreateAsync(SpecialtyDto specialty)
    {
        var response = await _http.PostAsJsonAsync("api/specialties", specialty);
        return response.IsSuccessStatusCode;
    }

    // PUT /api/specialties/{id}
    public async Task<bool> UpdateAsync(SpecialtyDto specialty)
    {
        var response = await _http.PutAsJsonAsync($"api/specialties/{specialty.Id}", specialty);
        return response.IsSuccessStatusCode;
    }

    // DELETE /api/specialties/{id}
    public async Task<bool> DeleteAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/specialties/{id}");
        return response.IsSuccessStatusCode;
    }
}
