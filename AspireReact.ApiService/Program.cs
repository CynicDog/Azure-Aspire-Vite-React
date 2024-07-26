using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Add data source 
builder.AddNpgsqlDataSource("aspiringreact");

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapGet("/weatherforecast", async (NpgsqlDataSource dataSource) =>
{
    using var conn = dataSource.CreateConnection();
    await conn.OpenAsync();

    var summaries = new List<string>();
    
    var cmd = new NpgsqlCommand("select summary from public.weathersummaries", conn);

    await using var reader = await cmd.ExecuteReaderAsync();
    while (await reader.ReadAsync())
    {
        summaries.Add(reader.GetString(0));
    }

    // Ensure there are summaries to use
    if (!summaries.Any())
    {
        summaries = new List<string> { "No data available" };
    }

    var forecast = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Count)]
            ))
        .ToArray();

    return forecast;
});

app.MapDefaultEndpoints();
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
