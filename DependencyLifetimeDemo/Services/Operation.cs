namespace DependencyLifetimeDemo.Services;

/// <summary>
/// Simple implementation of all operation interfaces.
/// Each instance gets a unique ID to demonstrate when new instances are created.
/// </summary>
/// <remarks>
/// This class implements all three lifetime interfaces to demonstrate that
/// the lifetime behavior is determined by service registration, not by the implementation.
/// The same class behaves differently based on how it's registered in the DI container.
/// </remarks>
public class Operation : IOperationTransient, IOperationScoped, IOperationSingleton
{
    private static int _instanceCount = 0;
    
    /// <summary>
    /// Unique ID assigned when this instance is created.
    /// Compare these IDs to understand when new instances are created vs reused.
    /// </summary>
    public Guid OperationId { get; }
    
    /// <summary>
    /// Instance number for debugging (shows creation order).
    /// </summary>
    public int InstanceNumber { get; }
    
    /// <summary>
    /// Timestamp when this instance was created.
    /// </summary>
    public DateTime CreatedAt { get; }
    
    public Operation()
    {
        OperationId = Guid.NewGuid();
        InstanceNumber = Interlocked.Increment(ref _instanceCount);
        CreatedAt = DateTime.UtcNow;
        
        // Optional: Log creation for learning purposes
        Console.WriteLine($"[{CreatedAt:HH:mm:ss.fff}] Operation instance #{InstanceNumber} created with ID: {OperationId:N}");
    }
}
