using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.Work.WebApi;
using Microsoft.VisualStudio.Services.Account.Client;
using Microsoft.VisualStudio.Services.Identity.Client;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi;
using Microsoft.VisualStudio.Services.ReleaseManagement.WebApi.Clients;

namespace Frank.AzureDevOps;

public class DevOpsInnerClients(
    ReleaseHttpClient releaseClient,
    GitHttpClient gitClient,
    BuildHttpClient buildClient,
    ProjectHttpClient projectClient,
    WorkHttpClient workItemsClient,
    IIdentityHttpClient identityClient,
    AccountHttpClient accountClient,
    FilterContainer filters)
    : IDevOpsInnerClients
{
    public FilterContainer Filters { get; } = filters;
    public ReleaseHttpClient ReleaseClient { get; } = releaseClient;
    public GitHttpClient GitClient { get; } = gitClient;
    public BuildHttpClient BuildClient { get; } = buildClient;
    public ProjectHttpClient ProjectClient { get; } = projectClient;
    public WorkHttpClient WorkItemsClient { get; } = workItemsClient;
    public IIdentityHttpClient IdentityClient { get; } = identityClient;
    public AccountHttpClient AccountClient { get; } = accountClient;
    
    public async Task<IEnumerable<TeamProjectReference>> GetProjectsAsync(bool invertFiltering = false) => 
        (await ProjectClient.GetProjects())
        .Where(project => invertFiltering == Filters.ProjectFilterExclusions.Any(x => x.Id == project.Id || x.Name == project.Name))
        .ToList();
    
    public async Task<IEnumerable<GitRepository>> GetRepositoriesAsync(bool invertFiltering = false) => 
        (await GitClient.GetRepositoriesAsync())
        .Where(repository => invertFiltering == Filters.ProjectFilterExclusions.Any(x => x.Id == repository.ProjectReference.Id || x.Name == repository.ProjectReference.Name))
        .Where(repository => invertFiltering == Filters.RepositoryFilterExclusions.Any(x => x.Id == repository.Id || x.Name == repository.Name))
        .OrderBy(x => x.ProjectReference.Name)
        .ThenBy(x => x.Name)
        .ToList();
    
    public async Task<IEnumerable<BuildDefinition>> GetBuildDefinitionsAsync(bool invertFiltering = false)
    {
        var projects = await GetProjectsAsync(invertFiltering);
        var definitions = new List<BuildDefinition>();
        foreach (var project in projects)
        {
            definitions.AddRange(await BuildClient.GetFullDefinitionsAsync(project.Id));
        }
        return definitions
            .Where(definition => invertFiltering == Filters.ProjectFilterExclusions.Any(x => x.Id == definition.Project.Id || x.Name == definition.Project.Name))
            .Where(definition => invertFiltering == Filters.BuildDefinitionFilterExclusions.Any(x => x.Id == definition.Id || x.Name == definition.Name))
            .Where(definition => invertFiltering == Filters.RepositoryFilterExclusions.Any(x =>  x.Id == definition.Repository.Id.ToGuid() || x.Name == definition.Repository.Name))
            .OrderBy(x => x.Project.Name)
            .ThenBy(x => x.Name)
            .ToList();
    }
    
    public async Task<IEnumerable<BuildDefinitionReference>> GetBuildReferencesAsync(bool invertFiltering = false)
    {
        var projects = await GetProjectsAsync(invertFiltering);
        var definitions = new List<BuildDefinitionReference>();
        foreach (var project in projects)
        {
            definitions.AddRange(await BuildClient.GetDefinitionsAsync(project.Id));
        }
        return definitions
            .Where(definition => invertFiltering == Filters.ProjectFilterExclusions.Any(x => x.Id == definition.Project.Id || x.Name == definition.Project.Name))
            .Where(definition => invertFiltering == Filters.BuildDefinitionFilterExclusions.Any(x => x.Id == definition.Id || x.Name == definition.Name))
            .OrderBy(x => x.Project.Name)
            .ThenBy(x => x.Name)
            .ToList();
    }
    
    public async Task<IEnumerable<BuildTrigger>> GetBuildTriggersAsync(bool invertFiltering = false)
    {
        var builds = await GetBuildDefinitionsAsync(invertFiltering);
        return builds
            .SelectMany(build => build.Triggers)
            .OrderBy(x => x.TriggerType)
            .ToList();
    }
    
    public async Task<IEnumerable<ReleaseDefinition>> GetReleaseDefinitionsAsync(bool invertFiltering = false)
    {
        var projects = await GetProjectsAsync(invertFiltering);
        var definitions = new List<ReleaseDefinition>();
        foreach (var project in projects)
        {
            definitions.AddRange(await ReleaseClient.GetReleaseDefinitionsAsync(project.Id));
        }
        return definitions
            .Where(definition => invertFiltering == Filters.ProjectFilterExclusions.Any(x => x.Id == definition.ProjectReference.Id || x.Name == definition.ProjectReference.Name))
            .Where(definition => invertFiltering == Filters.ReleaseDefinitionFilterExclusions.Any(x => x.Id == definition.Id || x.Name == definition.Name))
            .OrderBy(x => x.ProjectReference.Name)
            .ThenBy(x => x.Name)
            .ToList();
    }

    public async Task<IEnumerable<GitPullRequest>> GetPullRequestsAsync(bool invertFiltering = false)
    {
        var repositories = await GetRepositoriesAsync(invertFiltering);
        var pullRequests = new List<GitPullRequest>();
        
        foreach (var repo in repositories)
            pullRequests.AddRange(await GitClient.GetPullRequestsAsync(
                repo.ProjectReference.Id,
                repo.Id,
                new GitPullRequestSearchCriteria() { Status = PullRequestStatus.Active }
            ));

        return pullRequests;
    }
}