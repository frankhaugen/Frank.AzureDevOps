using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace Frank.AzureDevOps;

public interface IDevOpsGitClient
{
    Task<IEnumerable<GitRepository>> GetRepositoriesFromProjectAsync(TeamProjectReference project);
    
    Task<IEnumerable<GitRepository>> GetRepositoriesAsync(IEnumerable<TeamProjectReference> projects);
    
    Task<ReadOnlyMemory<byte>> DownloadRepositoryArchiveAsync(GitRepository repository);
}