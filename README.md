# 🔧 .NET Dependency Injection Lifetimes Demo

A polished, interactive demonstration of dependency injection lifetimes in ASP.NET Core. This repository provides a clear, educational example of how Transient, Scoped, and Singleton services behave differently in real applications.

## 🌟 Features

- **Interactive Web Interface**: Beautiful landing page explaining DI concepts
- **Real-time Console Logging**: See exactly when instances are created
- **Multiple API Endpoints**: Different views to understand lifetime behaviors
- **Clean Architecture**: Following Microsoft's recommended patterns
- **Educational Focus**: Built for learning and teaching DI concepts

## 🚀 Quick Start

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   cd DependencyLifetime
   ```

2. **Run the application**
   ```bash
   cd DependencyLifetimeDemo
   dotnet run
   ```

3. **Open your browser**
   - Navigate to http://localhost:5000 for the interactive guide
   - Or visit http://localhost:5000/swagger for API documentation

## 📚 Understanding Service Lifetimes

### 🔄 Transient
- **Behavior**: New instance created every time it's requested
- **Use Cases**: Lightweight, stateless services
- **Example**: Utility services, calculators
- **In Demo**: Notice different IDs everywhere

### 📌 Scoped
- **Behavior**: One instance per HTTP request
- **Use Cases**: Database contexts, unit of work
- **Example**: Entity Framework DbContext
- **In Demo**: Same ID within request, different between requests

### 🌍 Singleton
- **Behavior**: One instance for entire application lifetime
- **Use Cases**: Configuration, caches
- **Example**: Application settings, memory cache
- **In Demo**: Same ID always
- **⚠️ Warning**: Must be thread-safe!

## 🔍 API Endpoints

### 1. Complete Comparison
```http
GET /api/Operations
```
Shows comprehensive comparison with analysis and validation.

### 2. Simple View
```http
GET /api/Operations/simple
```
Simplified view with short IDs for easy comparison. Call multiple times to observe behavior.

### 3. Multiple Injections
```http
GET /api/Operations/multiple
```
Demonstrates multiple injections within the same request.

## 🏗️ Architecture

```
DependencyLifetimeDemo/
├── Controllers/
│   └── OperationsController.cs    # API endpoints with clear demonstrations
├── Services/
│   ├── IOperation.cs              # Lifetime marker interfaces
│   ├── Operation.cs               # Implementation with observable behavior
│   └── OperationService.cs        # Service demonstrating dependencies
├── Pages/
│   └── Index.html                 # Interactive web interface
├── Program.cs                     # DI registration and configuration
└── README.md                      # Project documentation
```

## 💡 Key Learning Points

1. **Lifetime is about registration, not implementation**
   - Same `Operation` class behaves differently based on registration
   - The container manages instance creation and disposal

2. **Observable behavior through GUIDs**
   - Each instance gets a unique ID on creation
   - Compare IDs to understand instance reuse

3. **Console logging for transparency**
   - Watch the console to see exactly when instances are created
   - Instance numbers show creation order

4. **Real-world patterns**
   - Clean separation of interfaces
   - Constructor injection throughout
   - No service locator anti-pattern

## 🛠️ Technical Details

- **Framework**: .NET 8.0
- **Type**: ASP.NET Core Web API
- **Features**: Swagger UI, Static Files, Minimal APIs
- **Patterns**: Dependency Injection, SOLID Principles

## 📖 Educational Use

This demo is perfect for:
- **Learning**: Understanding DI lifetimes hands-on
- **Teaching**: Clear examples for instructors
- **Reference**: Clean implementation to refer back to
- **Interviews**: Demonstrating DI knowledge

## 🎯 Best Practices Demonstrated

- ✅ Interface segregation with marker interfaces
- ✅ Constructor injection only
- ✅ Clear naming conventions
- ✅ Comprehensive XML documentation
- ✅ No captive dependencies
- ✅ Thread-safe singleton implementation

## 🔗 References

- [Microsoft: Service Lifetimes](https://docs.microsoft.com/aspnet/core/fundamentals/dependency-injection#service-lifetimes)
- [Microsoft: DI in ASP.NET Core](https://docs.microsoft.com/aspnet/core/fundamentals/dependency-injection)
- [.NET DI Guidelines](https://docs.microsoft.com/dotnet/core/extensions/dependency-injection-guidelines)

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

Built with ❤️ for the .NET community. Perfect for learning, teaching, and mastering dependency injection!
