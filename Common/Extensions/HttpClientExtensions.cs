using System.Net;
using System.Text.Json;

namespace Common.Extensions;

public static class HttpClientExtensions
{
    private static readonly HttpClient _httpClient = new();
    private static readonly JsonSerializerOptions _serializerOptions = new() { PropertyNameCaseInsensitive = true };

    public static async Task<T?> GetAsync<T>(this string url)
    {
        var response = await _httpClient.GetAsync(url);
        if (response.StatusCode is HttpStatusCode.OK)
        {
            var stream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<T>(stream, _serializerOptions);
            return result;
        }
        return default;
    }
}