using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;
using Microsoft.VisualStudio.Services.WebApi;

namespace Frank.AzureDevOps;

/// <summary>
/// Implementation of <see cref="IDevOpsClient"/>.
/// </summary>
public class DevOpsClient : IDevOpsClient
{
    /// <inheritdoc/>
    public GitHttpClient GitClient { get; }

    /// <inheritdoc/>
    public WorkItemTrackingHttpClient WorkItemClient { get; }

    /// <inheritdoc/>
    public BuildHttpClient BuildClient { get; }

    /// <inheritdoc/>
    public ProjectHttpClient ProjectClient { get; }

    /// <inheritdoc/>
    public ReleaseHttpClient2 ReleaseClient { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DevOpsClient"/> class.
    /// </summary>
    /// <param name="connection"></param>
    public DevOpsClient(VssConnection connection)
    {
        GitClient = connection.GetClient<GitHttpClient>();
        WorkItemClient = connection.GetClient<WorkItemTrackingHttpClient>();
        BuildClient = connection.GetClient<BuildHttpClient>();
        ProjectClient = connection.GetClient<ProjectHttpClient>();
        ReleaseClient = connection.GetClient<ReleaseHttpClient2>();
    }
}