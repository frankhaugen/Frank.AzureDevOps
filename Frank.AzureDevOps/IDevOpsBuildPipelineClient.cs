using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.Core.WebApi;

namespace Frank.AzureDevOps;

public interface IDevOpsBuildPipelineClient
{
    Task<IEnumerable<BuildPipeline>> GetPipelinesAsync(TeamProjectReference project);
    Task<IEnumerable<Build?>> QueuePipelinesAsync(IEnumerable<BuildPipeline> pipelines);
    Task<Build?> QueuePipelineAsync(BuildPipeline pipeline);
}