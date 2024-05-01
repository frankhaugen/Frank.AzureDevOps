using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;

namespace Frank.AzureDevOps;

public class DevOpsClient(IDevOpsInnerClients clients) : IDevOpsClient
{

    public async Task<IEnumerable<GitRepository>> GetRepositoriesAsync(bool invertFiltering = false) => await clients.GetRepositoriesAsync(invertFiltering);
    public async Task<IEnumerable<BuildDefinition>> GetBuildDefinitionsAsync(bool invertFiltering = false) => await clients.GetBuildDefinitionsAsync(invertFiltering);

    public async Task<IEnumerable<BuildDefinition>> GetNonMaintainedBuildPipelinesAsync(bool invertFiltering = false)
    {
        var allPipelines = await clients.GetBuildDefinitionsAsync(invertFiltering);
        return allPipelines;
        // return allPipelines.Where(pipeline => pipeline.LatestCompletedBuild?.FinishTime > DateTime.UtcNow.AddDays(-12));
    }
    
    public async Task RemoveTriggersFromPipelinesAsync(IEnumerable<BuildDefinition> pipelines)
    {
        foreach (var pipeline in pipelines)
            if (pipeline.Triggers.Count != 0)
            {
                pipeline.Triggers.Clear(); // This removes all triggers
                await clients.BuildClient.UpdateDefinitionAsync(pipeline, pipeline.Project.Id);
            }
    }

    /// <inheritdoc />
    public async Task DeleteBuildPipelinesAsync(IEnumerable<BuildDefinition> filtered)
    {
        foreach (var pipeline in filtered)
        {
            await clients.BuildClient.DeleteDefinitionAsync(pipeline.Project.Id, pipeline.Id);
        }
    }
}

public interface IDevOpsClient
{
    Task<IEnumerable<BuildDefinition>> GetNonMaintainedBuildPipelinesAsync(bool invertFiltering = false);
    Task<IEnumerable<GitRepository>> GetRepositoriesAsync(bool invertFiltering = false);
    Task<IEnumerable<BuildDefinition>> GetBuildDefinitionsAsync(bool invertFiltering = false);
    
    Task RemoveTriggersFromPipelinesAsync(IEnumerable<BuildDefinition> pipelines);

    Task DeleteBuildPipelinesAsync(IEnumerable<BuildDefinition> filtered);
}