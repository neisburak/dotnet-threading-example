using Common.Entities;
using Common.Extensions;
using Common.Services.Abstract;
using Microsoft.Extensions.Logging;

namespace Common.Services.Concrete;

public class PostManager : IPostService
{
    private const string POSTS_URL = "https://jsonplaceholder.typicode.com/posts";

    private readonly ILogger<PostManager> _logger;

    public PostManager(ILogger<PostManager> logger)
    {
        _logger = logger;
    }

    public async Task<Post?> GetAsync(int id)
    {
        _logger.LogInformation($"{nameof(GetAsync)}(id) triggered.");

        return await $"{POSTS_URL}/{id}".GetAsync<Post>();
    }

    public async Task<IEnumerable<Post>?> GetAsync()
    {
        _logger.LogInformation($"{nameof(GetAsync)}() triggered.");

        return await POSTS_URL.GetAsync<IEnumerable<Post>>();
    }
}