namespace Frank.AzureDevOps;

public record BuildPipeline(Guid ProjectId, string ProjectName, int BuildDefinitionId, string BuildDefinitionName, Guid RepositoryId, string RepositoryName);