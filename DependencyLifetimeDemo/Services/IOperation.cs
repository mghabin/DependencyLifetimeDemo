namespace DependencyLifetimeDemo.Services;

/// <summary>
/// Base operation interface following Microsoft's DI documentation pattern.
/// Each implementation will have a unique ID to track instance creation.
/// </summary>
public interface IOperation
{
    /// <summary>
    /// Gets the unique operation ID assigned at construction.
    /// This ID helps track when new instances are created.
    /// </summary>
    Guid OperationId { get; }
}

/// <summary>
/// Marker interface for transient operations.
/// </summary>
/// <remarks>
/// Transient lifetime services are created each time they're requested from the service container.
/// This lifetime works best for lightweight, stateless services.
/// </remarks>
public interface IOperationTransient : IOperation
{
}

/// <summary>
/// Marker interface for scoped operations.
/// </summary>
/// <remarks>
/// Scoped lifetime services are created once per client request (connection).
/// Use scoped services for operations that should maintain state within a request but not across requests.
/// </remarks>
public interface IOperationScoped : IOperation
{
}

/// <summary>
/// Marker interface for singleton operations.
/// </summary>
/// <remarks>
/// Singleton lifetime services are created once and reused for the entire application lifetime.
/// Use singleton services for expensive resources or application-wide state.
/// WARNING: Singleton services must be thread-safe.
/// </remarks>
public interface IOperationSingleton : IOperation
{
}
