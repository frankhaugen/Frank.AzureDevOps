using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace Frank.AzureDevOps;

public class DevOpsBuildPipelineClient(BuildHttpClient buildClient, GitHttpClient gitClient) : IDevOpsBuildPipelineClient
{
    public async Task<IEnumerable<BuildPipeline>> GetPipelinesAsync(TeamProjectReference project)
    {
        var pipelines = new List<BuildPipeline>();
        var repositories = await gitClient.GetRepositoriesAsync(project.Id);
        var buildDefinitions = await buildClient.GetFullDefinitionsAsync(project.Id);
        foreach (var buildDefinition in buildDefinitions)
        {
            var repository = repositories.FirstOrDefault(x => x.Id == buildDefinition?.Repository.Id.ToGuid());
            if (repository != null && buildDefinition != null)
            {
                pipelines.Add(new(project.Id, project.Name, buildDefinition.Id, buildDefinition.Name, repository.Id, repository.Name));
            }
        }

        return pipelines;
    }

    public async Task<IEnumerable<Build?>> QueuePipelinesAsync(IEnumerable<BuildPipeline> pipelines)
    {
        var buildResults = new List<Build?>();

        foreach (var pipeline in pipelines)
        {
            var result = await QueuePipelineAsync(pipeline);
            buildResults.Add(result);
        }

        return buildResults;
    }

    public async Task<Build?> QueuePipelineAsync(BuildPipeline pipeline) => await buildClient.QueueBuildAsync(new Build()
    {
        Reason = BuildReason.Manual,
        Definition = new DefinitionReference()
        {
            Id = pipeline.BuildDefinitionId
        },
        Project = new TeamProjectReference()
        {
            Id = pipeline.ProjectId
        }
    });
}