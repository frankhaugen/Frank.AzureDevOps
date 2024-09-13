using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;

namespace Frank.AzureDevOps;

/// <summary>
/// Provides access to Azure DevOps resource clients.
/// </summary>
public interface IDevOpsClient
{
    /// <summary>
    /// Gets the Git client for source control operations.
    /// </summary>
    GitHttpClient GitClient { get; }

    /// <summary>
    /// Gets the Work Item Tracking client for managing work items.
    /// </summary>
    WorkItemTrackingHttpClient WorkItemClient { get; }

    /// <summary>
    /// Gets the Build client for build operations.
    /// </summary>
    BuildHttpClient BuildClient { get; }

    /// <summary>
    /// Gets the Project client for project operations.
    /// </summary>
    ProjectHttpClient ProjectClient { get; }

    /// <summary>
    /// Gets the Release client for release management operations.
    /// </summary>
    ReleaseHttpClient2 ReleaseClient { get; }
}