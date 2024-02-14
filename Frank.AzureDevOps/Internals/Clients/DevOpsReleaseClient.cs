using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;

namespace Frank.AzureDevOps;

public class DevOpsReleaseClient(ReleaseHttpClient releaseClient) : IDevOpsReleaseClient
{
    public async Task<IEnumerable<Release>> GetReleasesAsync(ReleaseDefinition releaseDefinition) => await releaseClient.GetReleasesAsync(releaseDefinition.ProjectReference.Id, releaseDefinition.Id);
    public async Task<IEnumerable<Release>> GetReleasesAsync(TeamProjectReference project) => await releaseClient.GetReleasesAsync(project.Id);
    public async Task<IEnumerable<ReleaseDefinition>> GetReleaseDefinitionsAsync(TeamProjectReference project) => await releaseClient.GetReleaseDefinitionsAsync(project.Id);
}