using Api.Models;
using Api.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly ILogger<PostsController> _logger;
    private readonly string _postsUrl = "https://jsonplaceholder.typicode.com/posts";

    public PostsController(ILogger<PostsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var task = _postsUrl.GetAsync<IEnumerable<Post>>(_logger); // Invoked without waiting.

        var postResult = await $"{_postsUrl}/1".GetAsync<Post>(_logger); // Waiting for response here.

        var result = await task; // Waiting for response too.

        if (result is not null) return Ok(result);
        return BadRequest();
    }
}