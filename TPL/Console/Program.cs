using Console;
using Console.Configuration;

var app = AppBuilder.Build();

app.ForEach(perform: true);