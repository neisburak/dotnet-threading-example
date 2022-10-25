using Common.Entities;

namespace Common.Services.Abstract;

public interface IPostService
{
    Task<Post?> GetAsync(int id);
    Task<IEnumerable<Post>?> GetAsync();
}