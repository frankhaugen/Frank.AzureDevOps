using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;

namespace Frank.AzureDevOps;

public class DevOpsBuildClient(BuildHttpClient client) : IDevOpsBuildClient
{
    public async Task<IEnumerable<BuildDefinitionReference>> GetBuildDefinitionReferencesAsync(TeamProjectReference project) => await client.GetDefinitionsAsync(project.Id);

    public async Task<BuildDefinition> GetBuildDefinitionAsync(BuildDefinitionReference buildDefinitionReference) => await client.GetDefinitionAsync(buildDefinitionReference.Project.Id, buildDefinitionReference.Id);

    public async Task<IEnumerable<BuildDefinition?>> GetBuildDefinitionsAsync(IEnumerable<BuildDefinitionReference> buildDefinitionReferences)
    {
        var buildDefinitions = new List<BuildDefinition>();
        foreach (var buildDefinitionReference in buildDefinitionReferences)
        {
            var buildDefinition = await client.GetDefinitionAsync(buildDefinitionReference.Project.Id, buildDefinitionReference.Id);
            buildDefinitions.Add(buildDefinition);
        }
        return buildDefinitions;
    }

    public async Task<IEnumerable<Build?>> GetBuildsAsync(BuildDefinitionReference buildDefinition) => await client.GetBuildsAsync(buildDefinition.Project.Id, new List<int>() { buildDefinition.Id });

    public async Task<IEnumerable<Build?>> QueueBuildsAsync(IEnumerable<BuildDefinitionReference> buildDefinitionReferences)
    {
        var buildResults = new List<Build?>();

        foreach (var buildDefinitionReference in buildDefinitionReferences)
        {
            var result = await QueueBuildAsync(buildDefinitionReference.Project.Id, buildDefinitionReference.Id);
            buildResults.Add(result);
        }

        return buildResults;
    }

    public async Task<Build?> QueueBuildAsync(Guid projectId, int buildId) => await client.QueueBuildAsync(new Build()
    {
        Definition = new DefinitionReference()
        {
            Id = buildId
        },
        Project = new TeamProjectReference()
        {
            Id = projectId
        }
    });
}