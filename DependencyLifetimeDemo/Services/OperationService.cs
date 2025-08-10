namespace DependencyLifetimeDemo.Services;

/// <summary>
/// Service that depends on all operation types to demonstrate lifetime differences.
/// This pattern is recommended by Microsoft for understanding DI lifetimes.
/// </summary>
public class OperationService
{
    public IOperationTransient TransientOperation { get; }
    public IOperationScoped ScopedOperation { get; }
    public IOperationSingleton SingletonOperation { get; }
    
    public OperationService(
        IOperationTransient transientOperation,
        IOperationScoped scopedOperation,
        IOperationSingleton singletonOperation)
    {
        TransientOperation = transientOperation;
        ScopedOperation = scopedOperation;
        SingletonOperation = singletonOperation;
    }
    
    /// <summary>
    /// Gets a snapshot of all operation IDs for easy comparison.
    /// </summary>
    public OperationSnapshot GetSnapshot()
    {
        return new OperationSnapshot
        {
            TransientId = TransientOperation.OperationId,
            ScopedId = ScopedOperation.OperationId,
            SingletonId = SingletonOperation.OperationId,
            Timestamp = DateTime.UtcNow
        };
    }
}

/// <summary>
/// Simple DTO to show operation IDs.
/// </summary>
public class OperationSnapshot
{
    public Guid TransientId { get; set; }
    public Guid ScopedId { get; set; }
    public Guid SingletonId { get; set; }
    public DateTime Timestamp { get; set; }
}
