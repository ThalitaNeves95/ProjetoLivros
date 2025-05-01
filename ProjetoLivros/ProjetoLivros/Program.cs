using ProjetoLivros.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProjetoLivrosContext>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
