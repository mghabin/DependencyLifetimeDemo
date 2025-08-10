# DependencyLifetime Demo API

A simple ASP.NET Core Web API demonstrating the three dependency injection lifetimes following Microsoft's recommended patterns.

## Quick Start

```bash
dotnet run
```

Navigate to http://localhost:5000 (or the port shown in console) to access Swagger UI.

## API Endpoints

### 1. `/api/Operations` - Main Comparison
Shows how the same service behaves with different lifetimes:
- **Transient**: Different instance IDs everywhere
- **Scoped**: Same instance ID within a request
- **Singleton**: Same instance ID always

### 2. `/api/Operations/simple` - Track Changes
Call multiple times to see:
- **Transient**: New ID every request
- **Scoped**: New ID per request (but same within request)
- **Singleton**: Never changes

### 3. `/api/Operations/multiple` - Multiple Injections
Shows multiple injections within the same request:
- **Transient**: Different even in same request
- **Scoped**: Same within request
- **Singleton**: Always the same

## Project Structure

```
├── Controllers/
│   └── OperationsController.cs    # API endpoints
├── Services/
│   ├── Iinterfaces
│   ├── Operation.cs               # Simple imOperation.cs              # Lifetime plementation
│   └── OperationService.cs        # Service using all lifetimes
├── Program.cs                     # DI registration
└── appsettings.json              # Configuration
```

## How It Works

Each `Operation` instance gets a unique GUID when created. By comparing these GUIDs, you can see when new instances are created vs. when existing instances are reused.

```csharp
// All three use the same implementation but different lifetimes:
builder.Services.AddTransient<IOperationTransient, Operation>();
builder.Services.AddScoped<IOperationScoped, Operation>();
builder.Services.AddSingleton<IOperationSingleton, Operation>();
```

## Example Output

```json
{
  "fromController": {
    "transient": "a1b2c3d4-...",  // Different
    "scoped": "e5f6g7h8-...",     // Same as service
    "singleton": "i9j0k1l2-..."   // Same as service
  },
  "fromService": {
    "transient": "m3n4o5p6-...",  // Different
    "scoped": "e5f6g7h8-...",     // Same as controller
    "singleton": "i9j0k1l2-..."   // Same as controller
  }
}
```

## Learn More

- [Microsoft Docs: Service Lifetimes](https://learn.microsoft.com/aspnet/core/fundamentals/dependency-injection#service-lifetimes)
- [Dependency Injection in .NET](https://learn.microsoft.com/dotnet/core/extensions/dependency-injection)
