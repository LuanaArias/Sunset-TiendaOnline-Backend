using Microsoft.EntityFrameworkCore;
using Sunset_TiendaOnline.Data;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();

// para poder usar la bd
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        $"Host={Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost"};" +
        $"Port={Environment.GetEnvironmentVariable("DB_PORT") ?? "5432"};" +
        $"Database={Environment.GetEnvironmentVariable("DB_NAME") ?? "sunset"};" +
        $"Username={Environment.GetEnvironmentVariable("DB_USER") ?? "postgres"};" +
        $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "postgres"}"
    )
);


builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter()
        );
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
