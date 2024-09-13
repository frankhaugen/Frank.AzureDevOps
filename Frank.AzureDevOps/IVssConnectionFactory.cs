using Microsoft.VisualStudio.Services.WebApi;

namespace Frank.AzureDevOps;

/// <summary>
/// Factory interface for creating VssConnection instances.
/// </summary>
public interface IVssConnectionFactory
{
    /// <summary>
    /// Gets a VssConnection instance.
    /// </summary>
    /// <returns>A VssConnection instance or null if unable to create one.</returns>
    VssConnection? GetConnection();
}