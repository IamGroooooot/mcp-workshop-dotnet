using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using McpTodoServer.ContainerApp.Data;
using McpTodoServer.ContainerApp.Repositories;
using McpTodoServer.ContainerApp.Services;

// Build the application
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Create a shared SQLite connection for in-memory database
// This connection must be kept open throughout the application lifecycle
var connectionString = "Data Source=:memory:";
var connection = new SqliteConnection(connectionString);
connection.Open();

// Configure Entity Framework with SQLite in-memory database
builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseSqlite(connection, sqliteOptions =>
    {
        sqliteOptions.CommandTimeout(30);
    })
    .EnableDetailedErrors(builder.Environment.IsDevelopment())
    .EnableSensitiveDataLogging(builder.Environment.IsDevelopment()));

// Register repository and service classes
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<ITodoService, TodoService>();

// Add logging
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddConsole();
    if (builder.Environment.IsDevelopment())
    {
        loggingBuilder.AddDebug();
    }
});

// Build the app
var app = builder.Build();

// Initialize the database
await InitializeDatabaseAsync(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Sample weather forecast endpoint (keeping as requested)
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

// Demo endpoint to test todo functionality (not a full API implementation)
app.MapGet("/todo-demo", async (ITodoService todoService) =>
{
    try
    {
        // Demo the 5 operations: Create, List, Update, Complete, Delete
        var todo1 = await todoService.CreateTodoAsync("Learn Entity Framework Core");
        var todo2 = await todoService.CreateTodoAsync("Build ASP.NET Core API");
        var todo3 = await todoService.CreateTodoAsync("Deploy to production");
        
        var allTodos = await todoService.GetAllTodosAsync();
        
        await todoService.UpdateTodoAsync(todo2.Id, "Build amazing ASP.NET Core API");
        await todoService.CompleteTodoAsync(todo1.Id);
        
        var stats = await todoService.GetTodoStatisticsAsync();
        
        await todoService.DeleteTodoAsync(todo3.Id);
        
        var finalTodos = await todoService.GetAllTodosAsync();
        
        return Results.Ok(new 
        { 
            Message = "Todo operations completed successfully",
            InitialTodos = allTodos,
            Statistics = stats,
            FinalTodos = finalTodos
        });
    }
    catch (Exception ex)
    {
        return Results.Problem($"Error during todo demo: {ex.Message}");
    }
})
.WithName("TodoDemo");

// Run the application
app.Run();

/// <summary>
/// Initializes the database by ensuring it's created and applying any pending migrations.
/// </summary>
/// <param name="app">The web application.</param>
static async Task InitializeDatabaseAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        var context = services.GetRequiredService<TodoDbContext>();
        
        // Ensure the database is created
        await context.Database.EnsureCreatedAsync();
        
        logger.LogInformation("In-memory SQLite database initialized successfully");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while initializing the database");
        throw;
    }
}

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
