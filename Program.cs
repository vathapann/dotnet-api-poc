var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// HTTPS redirect (keep it as you like)
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Static files (optional if you do Option B)
app.UseDefaultFiles();  // looks for index.html
app.UseStaticFiles();

app.UseAuthorization();

// API routes
app.MapControllers();

// Simple landing page
app.MapGet("/", () =>
    Results.Content(
        "<h1>.NET API</h1><p>It works! Try <code>/weatherforecast</code> or <code>/api/hello</code>.</p>",
        "text/html"
    ));

// Extra sample API route
app.MapGet("/api/hello", () => new { message = "Hello from API" });
app.MapGet("/weatherforecast", () => new[]
{
    new WeatherForecast(DateTime.Now, 25, "Sunny"),
    new WeatherForecast(DateTime.Now.AddDays(1), 28, "Partly cloudy"),
    new WeatherForecast(DateTime.Now.AddDays(2), 22, "Rainy"),
});
app.Run();

// File: WeatherForecast.cs
public class WeatherForecast
{
    public DateTime Date { get; }
    public int TemperatureC { get; }
    public string Summary { get; }

    public WeatherForecast(DateTime date, int temperatureC, string summary)
    {
        Date = date;
        TemperatureC = temperatureC;
        Summary = summary;
    }
}