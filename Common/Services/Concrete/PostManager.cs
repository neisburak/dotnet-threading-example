using Common.Entities;
using Common.Extensions;
using Common.Services.Abstract;
using Microsoft.Extensions.Logging;

namespace Common.Services.Concrete;

public class PostManager : IPostService
{
    private const string POSTS_URL = $"{Constants.BASE_URL}/posts";

    private readonly ILogger<PostManager> _logger;

    public PostManager(ILogger<PostManager> logger)
    {
        _logger = logger;
    }

    public async Task<Post?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation($"{nameof(GetAsync)}({id}) triggered at {DateTime.Now.GetTime()} with thread id {Thread.CurrentThread.ManagedThreadId}.");

            return await $"{POSTS_URL}/{id}".GetAsync<Post>(cancellationToken);
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }

    public async Task<IEnumerable<Post>?> GetAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.LogInformation($"{nameof(GetAsync)}() triggered at {DateTime.Now.GetTime()} with thread id {Thread.CurrentThread.ManagedThreadId}.");

            return await POSTS_URL.GetAsync<IEnumerable<Post>>(cancellationToken);
        }
        catch (TaskCanceledException ex)
        {
            _logger.LogError(ex.Message);
            return null;
        }
    }
}