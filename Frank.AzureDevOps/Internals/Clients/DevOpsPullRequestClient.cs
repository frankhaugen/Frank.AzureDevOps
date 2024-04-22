using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.VisualStudio.Services.Identity.Client;

namespace Frank.AzureDevOps;

public class DevOpsPullRequestClient(GitHttpClient gitHttpClient, ProjectHttpClient projectHttpClient, IdentityHttpClient identityHttpClient) : IDevOpsPullRequestClient
{
    public async Task<IEnumerable<GitPullRequest>> GetPullRequestsAsync(GitRepository repository)
    {
        return await gitHttpClient.GetPullRequestsAsync(repository.ProjectReference.Id, repository.Id, new GitPullRequestSearchCriteria()
        {
            Status = PullRequestStatus.Active
        });
    }

    public async Task<IEnumerable<GitPullRequest>> GetPullRequestsAsync(TeamProjectReference project, params int[] pullRequestIds)
    {
        var repositoryIds = (await gitHttpClient.GetRepositoriesAsync(project.Id)).Select(x => x.Id);
        var pullRequests = new List<GitPullRequest>();
        foreach (var repositoryId in repositoryIds)
        {
            foreach (var pullRequestId in pullRequestIds)
            {
                var pullRequest = await gitHttpClient.GetPullRequestAsync(project.Id, repositoryId, pullRequestId);
                if (pullRequest != null)
                    pullRequests.Add(pullRequest);
            }
        }
        return pullRequests;
    }

    public async Task<GitPullRequest> GetPullRequestAsync(GitRepository repository, int pullRequestId)
    {
        return await gitHttpClient.GetPullRequestAsync(repository.ProjectReference.Id, repository.Id, pullRequestId);
    }

    public async Task ApprovePullRequestAsync(GitRepository repository, int pullRequestId)
    {
        var pullRequest = await GetPullRequestAsync(repository, pullRequestId);
        var identityRef = await identityHttpClient.GetIdentitySelfAsync();
        
        await gitHttpClient.UpdatePullRequestReviewerAsync(new IdentityRefWithVote()
        {
            Id = identityRef.Id.ToString(),
            Vote = 10
        }, repository.ProjectReference.Id, repository.Id, pullRequest.PullRequestId, identityRef.Id.ToString());
    }
}