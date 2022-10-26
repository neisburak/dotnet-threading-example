using Console;
using Console.Configuration;

var app = AppBuilder.Build();

await app.ContinueWith(perform: false);

await app.WhenAll(perform: false);

await app.WhenAny(perform: false);

app.WaitAll(perform: false);

app.WaitAny(perform: false);

await app.Delay(perform: false);

await app.Run(perform: false);

await app.StartNew(perform: false);

await app.FromResult(perform: false);

try
{
    var tokenSource = new CancellationTokenSource();
    var task = app.CancelationToken(tokenSource.Token, perform: true);
    tokenSource.Cancel();
    await task;
}
catch (TaskCanceledException ex)
{
    app.WriteLine(ex.Message);
}

app.Result(perform: false);

var post = await app.ValueTask(perform: true);
app.WriteLine(post);