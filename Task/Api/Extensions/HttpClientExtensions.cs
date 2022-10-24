using System.Net;
using System.Text.Json;

namespace Api.Extensions;

public static class HttpClientExtensions
{
    public static async Task<T?> GetAsync<T>(this string url, ILogger logger)
    {
        logger.LogInformation(url);
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(url);
        if (response.StatusCode is HttpStatusCode.OK)
        {
            var stream = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<T>(stream, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return result;
        }
        return default;
    }
}