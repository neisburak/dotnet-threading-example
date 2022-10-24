using System.Net;
using System.Text.Json;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var result = await GetAsync<IEnumerable<Post>>("https://jsonplaceholder.typicode.com/posts");
        if (result is not null) return Ok(result);
        return BadRequest();
    }

    private async Task<T?> GetAsync<T>(string url)
    {
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