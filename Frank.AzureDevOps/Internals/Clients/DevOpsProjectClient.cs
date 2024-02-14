using Microsoft.TeamFoundation.Core.WebApi;

namespace Frank.AzureDevOps;

public class DevOpsProjectClient(ProjectHttpClient client) : IDevOpsProjectClient
{
    public async Task<IEnumerable<DevOpsProjectInfo>> GetProjectsHeadersAsync() => (await GetProjectsAsync()).Select(x => new DevOpsProjectInfo(x.Id, x.Name));

    public async Task<TeamProjectReference> GetProjectAsync(string name) => (await client.GetProjects()).First(x => x.Name == name);

    public async Task<IEnumerable<TeamProjectReference>> GetProjectsAsync() => await client.GetProjects();
}