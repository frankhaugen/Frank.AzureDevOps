using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace Frank.AzureDevOps;

public interface IDevOpsPullRequestClient
{
    Task<IEnumerable<GitPullRequest>> GetPullRequestsAsync(GitRepository repository);
    
    Task<IEnumerable<GitPullRequest>> GetPullRequestsAsync(TeamProjectReference project, params int[] pullRequestIds);
    
    Task<GitPullRequest> GetPullRequestAsync(GitRepository repository, int pullRequestId);
    
    Task ApprovePullRequestAsync(GitRepository repository, int pullRequestId);
}