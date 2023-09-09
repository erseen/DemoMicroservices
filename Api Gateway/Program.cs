using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("ocelot.json", optional: false , reloadOnChange: true)
    .AddEnvironmentVariables();
builder.Services.AddOcelot(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();
await app.UseOcelot();

// Configure the HTTP request pipeline.
    
app.UseAuthorization();

app.MapControllers();

app.Run();
