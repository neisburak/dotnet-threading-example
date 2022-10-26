using Common.Entities;

namespace Common.Services.Abstract;

public interface IPostService
{
    Task<Post?> GetAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Post>?> GetAsync(CancellationToken cancellationToken = default);
}