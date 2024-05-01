using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.Work.WebApi;
using Microsoft.VisualStudio.Services.Account.Client;
using Microsoft.VisualStudio.Services.Identity.Client;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;

namespace Frank.AzureDevOps;

public interface IDevOpsInnerClients
{
    FilterContainer Filters { get; }
    
    ReleaseHttpClient ReleaseClient { get; }
    
    GitHttpClient GitClient { get; }
    
    BuildHttpClient BuildClient { get; }
    
    ProjectHttpClient ProjectClient { get; }
    
    WorkHttpClient WorkItemsClient { get; }
    
    IIdentityHttpClient IdentityClient { get; }
    
    AccountHttpClient AccountClient { get; }
    
    
    Task<IEnumerable<TeamProjectReference>> GetProjectsAsync(bool invertFiltering = false);
    Task<IEnumerable<GitRepository>> GetRepositoriesAsync(bool invertFiltering = false);
    Task<IEnumerable<BuildDefinition>> GetBuildDefinitionsAsync(bool invertFiltering = false);
    Task<IEnumerable<BuildDefinitionReference>> GetBuildReferencesAsync(bool invertFiltering = false);
    Task<IEnumerable<ReleaseDefinition>> GetReleaseDefinitionsAsync(bool invertFiltering = false);
    Task<IEnumerable<GitPullRequest>> GetPullRequestsAsync(bool invertFiltering = false);
}