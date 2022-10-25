using Console;
using Console.Configuration;

var app = AppBuilder.Build();

await app.ContinueWith(perform: false);

await app.WhenAll();