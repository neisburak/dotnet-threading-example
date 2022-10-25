using Common.Services.Abstract;
using Console.Extensions;

var app = AppBuilder.Build();
var postService = app.Get<IPostService>();

await postService.GetAsync();
