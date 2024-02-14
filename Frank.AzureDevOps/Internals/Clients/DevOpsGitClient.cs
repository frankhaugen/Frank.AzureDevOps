using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace Frank.AzureDevOps;

public class DevOpsGitClient(GitHttpClient client) : IDevOpsGitClient
{
    public async Task<IEnumerable<GitRepository>> GetRepositoriesFromProjectAsync(TeamProjectReference project) => await client.GetRepositoriesAsync(project.Id);

    public async Task<IEnumerable<GitRepository>> GetRepositoriesAsync(IEnumerable<TeamProjectReference> projects)
    {
        var repositories = new List<GitRepository>();
        foreach (var project in projects) 
            repositories.AddRange(await GetRepositoriesFromProjectAsync(project));
        return repositories;
    }

    /// <inheritdoc />
    public async Task<ReadOnlyMemory<byte>> DownloadRepositoryArchiveAsync(GitRepository repository)
    {
        return default;
    }
}