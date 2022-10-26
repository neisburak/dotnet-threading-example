using Console;
using Console.Configuration;

var app = AppBuilder.Build();

app.ResizeForEach(perform: false);

app.TotalSizeForEach(perform: false, raceCondition: true);

app.TotalSizeFor(perform: false);

app.TotalSizeForEachWithSharedData(perform: false);

var tokenSource = new CancellationTokenSource();
tokenSource.Cancel();
app.WithCancellationToken(tokenSource.Token, perform: true);