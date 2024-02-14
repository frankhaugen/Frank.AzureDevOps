using Microsoft.TeamFoundation.Core.WebApi;

namespace Frank.AzureDevOps;

public interface IDevOpsProjectClient
{
    Task<IEnumerable<DevOpsProjectInfo>> GetProjectsHeadersAsync();
    Task<TeamProjectReference> GetProjectAsync(string name);
    Task<IEnumerable<TeamProjectReference>> GetProjectsAsync();
}