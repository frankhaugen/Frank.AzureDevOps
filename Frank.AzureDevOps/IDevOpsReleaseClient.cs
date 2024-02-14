using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;

namespace Frank.AzureDevOps;

public interface IDevOpsReleaseClient
{
    Task<IEnumerable<Release>> GetReleasesAsync(ReleaseDefinition releaseDefinition);
    Task<IEnumerable<Release>> GetReleasesAsync(TeamProjectReference project);
    Task<IEnumerable<ReleaseDefinition>> GetReleaseDefinitionsAsync(TeamProjectReference project);
}