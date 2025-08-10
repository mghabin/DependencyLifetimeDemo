# Architecture Documentation

## Overview

This repository demonstrates Dependency Injection (DI) lifetimes in ASP.NET Core using a simplified, Microsoft-recommended approach. The architecture focuses on clarity and educational value.

## Design Principles

1. **Simplicity First**: Following Microsoft's documentation patterns for clear understanding
2. **Single Responsibility**: Each component has one clear purpose
3. **Observable Behavior**: Using GUIDs to make instance creation visible
4. **Minimal Complexity**: No unnecessary abstractions or patterns

## Core Components

### 1. Operation Interfaces (`IOperation.cs`)
- **IOperation**: Base interface with `OperationId` property
- **IOperationTransient**: Marker interface for transient lifetime
- **IOperationScoped**: Marker interface for scoped lifetime  
- **IOperationSingleton**: Marker interface for singleton lifetime

### 2. Operation Implementation (`Operation.cs`)
- Single class implementing all three lifetime interfaces
- Generates unique GUID on instantiation
- Makes instance creation observable

### 3. Service Layer (`OperationService.cs`)
- Depends on all three lifetime variants
- Demonstrates how different lifetimes interact
- Provides snapshot functionality for easy comparison

### 4. Controller (`OperationsController.cs`)
- Three endpoints demonstrating different aspects:
  - `/api/Operations`: Complete comparison
  - `/api/Operations/simple`: Basic lifetime tracking
  - `/api/Operations/multiple`: Multiple injections per request

## Lifetime Behaviors

### Transient
- **Creation**: New instance every time it's requested
- **Use Case**: Lightweight, stateless services
- **Example**: Utility services, calculators

### Scoped
- **Creation**: One instance per HTTP request
- **Use Case**: Request-specific state
- **Example**: Database contexts, unit of work

### Singleton
- **Creation**: One instance for entire application lifetime
- **Use Case**: Shared state, expensive resources
- **Example**: Configuration, caches

## Registration Pattern

```csharp
// Clear, explicit registration showing lifetime differences
builder.Services.AddTransient<IOperationTransient, Operation>();
builder.Services.AddScoped<IOperationScoped, Operation>();
builder.Services.AddSingleton<IOperationSingleton, Operation>();
```

## Key Learning Points

1. **Same Implementation, Different Lifetimes**: Shows how lifetime is about registration, not implementation
2. **Observable Differences**: GUIDs make instance creation visible
3. **Real-World Patterns**: Demonstrates practical usage scenarios
4. **No Magic**: Everything is explicit and traceable

## Best Practices Demonstrated

1. **Interface Segregation**: Separate interfaces for different lifetimes
2. **Dependency Injection**: Constructor injection throughout
3. **Testability**: All dependencies are injected
4. **Clear Naming**: Intent is obvious from names

## Common Pitfalls Avoided

1. **No Captive Dependencies**: No scoped services in singletons
2. **No Service Locator**: Pure dependency injection
3. **No Hidden State**: All state is explicit
4. **Thread Safety**: Singleton is immutable

## Educational Value

This architecture is optimized for:
- **Learning**: Clear demonstration of concepts
- **Teaching**: Easy to explain and understand
- **Experimentation**: Simple to modify and test
- **Reference**: Clean example of DI patterns

## References

- [Microsoft DI Documentation](https://docs.microsoft.com/aspnet/core/fundamentals/dependency-injection)
- [Service Lifetime Guidelines](https://docs.microsoft.com/aspnet/core/fundamentals/dependency-injection#service-lifetimes)
