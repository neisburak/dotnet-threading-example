using Console;
using Console.Configuration;
using Console.Data;

var app = AppBuilder.Build();

app.AsParallel(perform: false);

app.ForAll(perform: false);

var context = app.GetService<NorthwindContext>();

var list = context.Products.ToList();
list.ForEach(f => app.WriteLine(f.ProductName));