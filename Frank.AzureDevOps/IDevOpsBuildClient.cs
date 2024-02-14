using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;

namespace Frank.AzureDevOps;

public interface IDevOpsBuildClient
{
    Task<IEnumerable<BuildDefinitionReference>> GetBuildDefinitionReferencesAsync(TeamProjectReference project);
    Task<BuildDefinition> GetBuildDefinitionAsync(BuildDefinitionReference buildDefinitionReference);
    Task<IEnumerable<BuildDefinition?>> GetBuildDefinitionsAsync(IEnumerable<BuildDefinitionReference> buildDefinitionReferences);
    Task<IEnumerable<Build?>> GetBuildsAsync(BuildDefinitionReference buildDefinition);
    Task<IEnumerable<Build?>> QueueBuildsAsync(IEnumerable<BuildDefinitionReference> buildDefinitionReferences);
}