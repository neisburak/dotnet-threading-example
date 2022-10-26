using Microsoft.AspNetCore.Mvc;
using Common.Services.Abstract;
using Common.Entities;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IPostService _postService;
    private readonly ILogger<PostsController> _logger;


    public PostsController(IPostService postService, ILogger<PostsController> logger)
    {
        _logger = logger;
        _postService = postService;
    }

    [HttpGet("{id}")]
    public async Task<Post?> GetAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.Register(() => _logger.LogInformation("Task cancelling registered."));
            await Task.Delay(10000, cancellationToken);
            return await _postService.GetAsync(id, cancellationToken);
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
    {
        var task = _postService.GetAsync(cancellationToken); // Invoked immediately without waiting.

        var postResult = await _postService.GetAsync(1, cancellationToken); // Waiting for response here.

        var result = await task; // Waiting for response too.

        return result is not null ? Ok(result) : BadRequest();
    }
}