namespace Frank.AzureDevOps;

public record GitInfo(Guid RepositoryId, string RepositoryName, Uri path);