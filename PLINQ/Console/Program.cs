using Console;
using Console.Configuration;
using Console.Data;

var app = AppBuilder.Build();

app.AsParallel(perform: false);

app.ForAll(perform: false);


var context = app.GetService<NorthwindContext>();

app.ForAll(context, perform: false);

app.WithDegreeOfParallelism(context, perform: false);

app.WithExecuteMode(context, perform: false);

app.AsOrdered(context, perform: false);

app.ExceptionHandling(context, perform: true);

var tokenSource = new CancellationTokenSource();
tokenSource.Cancel();
app.CanellationToken(context, tokenSource.Token, perform: true);