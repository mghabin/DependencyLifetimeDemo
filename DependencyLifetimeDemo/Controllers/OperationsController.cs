using DependencyLifetimeDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace DependencyLifetimeDemo.Controllers;

/// <summary>
/// Simple controller following Microsoft's recommended pattern for demonstrating DI lifetimes.
/// Shows how Transient, Scoped, and Singleton services behave differently.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OperationsController : ControllerBase
{
    private readonly IOperationTransient _transientOperation;
    private readonly IOperationScoped _scopedOperation;
    private readonly IOperationSingleton _singletonOperation;
    private readonly OperationService _operationService;
    private readonly ILogger<OperationsController> _logger;
    
    public OperationsController(
        IOperationTransient transientOperation,
        IOperationScoped scopedOperation,
        IOperationSingleton singletonOperation,
        OperationService operationService,
        ILogger<OperationsController> logger)
    {
        _transientOperation = transientOperation;
        _scopedOperation = scopedOperation;
        _singletonOperation = singletonOperation;
        _operationService = operationService;
        _logger = logger;
    }
    
    /// <summary>
    /// Shows the operation IDs from both controller and service.
    /// This clearly demonstrates:
    /// - Transient: Different IDs (new instance each time)
    /// - Scoped: Same ID within request (same instance per request)
    /// - Singleton: Same ID always (same instance for app lifetime)
    /// </summary>
    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation("Getting operation IDs");
        
        return Ok(new
        {
            Title = "Dependency Injection Lifetime Comparison",
            Description = "Compare the IDs below. Notice how they differ based on lifetime:",
            
            FromController = new
            {
                Transient = _transientOperation.OperationId,
                Scoped = _scopedOperation.OperationId,
                Singleton = _singletonOperation.OperationId
            },
            
            FromService = new
            {
                Transient = _operationService.TransientOperation.OperationId,
                Scoped = _operationService.ScopedOperation.OperationId,
                Singleton = _operationService.SingletonOperation.OperationId
            },
            
            Analysis = new
            {
                TransientComparison = _transientOperation.OperationId != _operationService.TransientOperation.OperationId
                    ? "✅ Different IDs - Each injection gets a new instance"
                    : "❌ Same IDs - This should not happen!",
                    
                ScopedComparison = _scopedOperation.OperationId == _operationService.ScopedOperation.OperationId
                    ? "✅ Same IDs - Same instance within this request"
                    : "❌ Different IDs - This should not happen!",
                    
                SingletonComparison = _singletonOperation.OperationId == _operationService.SingletonOperation.OperationId
                    ? "✅ Same IDs - Same instance for entire app lifetime"
                    : "❌ Different IDs - This should not happen!"
            },
            
            Summary = new
            {
                Transient = "New instance every time it's requested",
                Scoped = "Same instance within a single HTTP request",
                Singleton = "Same instance for entire application lifetime"
            },
            
            Timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff UTC")
        });
    }
    
    /// <summary>
    /// Simplified endpoint that just shows the current operation IDs.
    /// Call this multiple times to see how IDs change (or don't change).
    /// </summary>
    [HttpGet("simple")]
    public IActionResult GetSimple()
    {
        // Get short versions of IDs for easier comparison
        var transientId = _transientOperation.OperationId.ToString().Substring(0, 8);
        var scopedId = _scopedOperation.OperationId.ToString().Substring(0, 8);
        var singletonId = _singletonOperation.OperationId.ToString().Substring(0, 8);
        
        return Ok(new
        {
            Instructions = "🔄 Call this endpoint multiple times to observe lifetime behavior",
            Operations = new
            {
                Transient = new { 
                    ShortId = transientId,
                    FullId = _transientOperation.OperationId,
                    Behavior = "Changes every call"
                },
                Scoped = new { 
                    ShortId = scopedId,
                    FullId = _scopedOperation.OperationId,
                    Behavior = "Changes per request"
                },
                Singleton = new { 
                    ShortId = singletonId,
                    FullId = _singletonOperation.OperationId,
                    Behavior = "Never changes"
                }
            },
            RequestTime = DateTime.UtcNow.ToString("HH:mm:ss.fff")
        });
    }
    
    /// <summary>
    /// Demonstrates multiple resolutions within the same request.
    /// Shows that even within one request, transient creates new instances.
    /// </summary>
    [HttpGet("multiple")]
    public IActionResult GetMultiple(
        [FromServices] IOperationTransient anotherTransient,
        [FromServices] IOperationScoped anotherScoped,
        [FromServices] IOperationSingleton anotherSingleton)
    {
        return Ok(new
        {
            Description = "Multiple resolutions within the same request",
            
            FirstResolution = new
            {
                Transient = _transientOperation.OperationId,
                Scoped = _scopedOperation.OperationId,
                Singleton = _singletonOperation.OperationId
            },
            
            SecondResolution = new
            {
                Transient = anotherTransient.OperationId,
                Scoped = anotherScoped.OperationId,
                Singleton = anotherSingleton.OperationId
            },
            
            Analysis = new
            {
                TransientSame = _transientOperation.OperationId == anotherTransient.OperationId 
                    ? "❌ ERROR: Should be different!" 
                    : "✅ Different (as expected for Transient)",
                ScopedSame = _scopedOperation.OperationId == anotherScoped.OperationId 
                    ? "✅ Same (as expected for Scoped)" 
                    : "❌ ERROR: Should be same!",
                SingletonSame = _singletonOperation.OperationId == anotherSingleton.OperationId 
                    ? "✅ Same (as expected for Singleton)" 
                    : "❌ ERROR: Should be same!"
            },
            
            Summary = "Within a single request: Transient creates new instances, Scoped reuses the same instance, Singleton always uses the global instance"
        });
    }
}
