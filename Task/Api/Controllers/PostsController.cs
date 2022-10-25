using Microsoft.AspNetCore.Mvc;
using Common.Services.Abstract;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostService _postService;

    public PostsController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var task = _postService.GetAsync(); // Invoked without waiting.

        var postResult = await _postService.GetAsync(1); // Waiting for response here.

        var result = await task; // Waiting for response too.

        return result is not null ? Ok(result) : BadRequest();
    }
}