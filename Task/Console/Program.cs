using Console;
using Console.Configuration;

var app = AppBuilder.Build();

await app.ContinueWith(perform: false);

await app.WhenAll(perform: false);

await app.WhenAny(perform: false);

app.WaitAll(perform: false);

app.WaitAny(perform: false);

await app.Delay(perform: false);

await app.Run(perform: true);