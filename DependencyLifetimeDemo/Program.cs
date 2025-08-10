using DependencyLifetimeDemo.Services;

var builder = WebApplication.CreateBuilder(args);

// ===== DEPENDENCY INJECTION LIFETIME DEMONSTRATION =====
// This follows Microsoft's recommended pattern for demonstrating DI lifetimes
// Reference: https://learn.microsoft.com/aspnet/core/fundamentals/dependency-injection

// Register Operations with different lifetimes
// Each uses the same implementation (Operation) but with different lifetime behavior:

// TRANSIENT: New instance created every time it's requested
builder.Services.AddTransient<IOperationTransient, Operation>();

// SCOPED: New instance created once per HTTP request/scope
builder.Services.AddScoped<IOperationScoped, Operation>();

// SINGLETON: Single instance created and reused for entire application lifetime
builder.Services.AddSingleton<IOperationSingleton, Operation>();

// Register the service that depends on all operations
builder.Services.AddTransient<OperationService>();

// Standard ASP.NET Core services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() 
    { 
        Title = "DI Lifetime Demo API", 
        Version = "v1",
        Description = "Demonstrates Transient, Scoped, and Singleton lifetimes in .NET DI"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DI Lifetime Demo API v1");
        c.RoutePrefix = "swagger"; // Move swagger to /swagger
    });
}

// Enable static files (for our HTML page)
app.UseDefaultFiles(); // Looks for index.html in wwwroot
app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Log when the application starts
app.Lifetime.ApplicationStarted.Register(() =>
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("🚀 DI Lifetime Demo started! Navigate to http://localhost:5000");
});

app.Run();
