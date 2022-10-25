using Common.Services.Abstract;
using Console.Configuration;

var app = AppBuilder.Build();
var postService = app.Get<IPostService>();

postService.GetAsync().ContinueWith(async (posts) =>
{
    foreach (var post in await posts)
    {
        app.WriteLine(post);
    }
});
